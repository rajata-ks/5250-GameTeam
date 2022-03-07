using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Game.Models;
using Game.Helpers;
using Game.ViewModels;
using Game.GameRules;
using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;
using System;

namespace Game.Engine.EngineGame
{
    /// <summary>
    /// Engine controls the turns
    /// 
    /// A turn is when a Character takes an action or a Monster takes an action
    /// 
    /// </summary>
    public class TurnEngine : EngineBase.TurnEngineBase, ITurnEngineInterface
    {
        #region Algrorithm
        /* 
            Need to decide who takes the next turn
            Target to Attack
            Should Move, or Stay put (can hit with weapon range?)
            Death
            Manage Round...
          
            Attack or Move
            Roll To Hit
            Decide Hit or Miss
            Decide Damage
            Death
            Drop Items
            Turn Over
        */
        #endregion Algrorithm

        // Hold the BaseEngine
        public new EngineSettingsModel EngineSettings = EngineSettingsModel.Instance;

        /// <summary>
        /// CharacterModel Attacks...
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override bool TakeTurn(PlayerInfoModel Attacker)
        {
            // Choose Action.  Such as Move, Attack etc.

            // INFO: Teams, if you have other actions they would go here.

            var result = false;

            // If the action is not set, then try to set it or use Attact
            if (EngineSettings.CurrentAction == ActionEnum.Unknown)
            {
                // Set the action if one is not set
                EngineSettings.CurrentAction = DetermineActionChoice(Attacker);
            }

            switch (EngineSettings.CurrentAction)
            {
                case ActionEnum.Attack:
                    result = Attack(Attacker);
                    break;

                case ActionEnum.Ability:
                    result = UseAbility(Attacker);
                    break;

                case ActionEnum.Move:
                    result = MoveAsTurn(Attacker);
                    break;

                case ActionEnum.Rest:
                    result = Rest(Attacker);
                    break;
            }

            if (Attacker.PlayerType == PlayerTypeEnum.Character)
            {
                EngineSettings.BattleScore.TurnCount++;
            }

            // Save the Previous Action off
            EngineSettings.PreviousAction = EngineSettings.CurrentAction;

            // Reset the Action to unknown for next time
            EngineSettings.CurrentAction = ActionEnum.Unknown;

            return result;
        }
         
        /// <summary>
        /// Restore Attacker health by a bonus amount.
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool Rest(PlayerInfoModel Attacker)
        {
            var restBonus = 2;
            if (Attacker.CurrentHealth + restBonus > Attacker.MaxHealth)
            {
                Attacker.CurrentHealth = Attacker.MaxHealth;
            } 
            else 
            { 
                Attacker.CurrentHealth += restBonus;
            }

            return true;
        }
       
        /// <summary>
        /// Move Based on Speed 
        /// Limit the distance a player can move to their speed (max limit : speed )
        /// Find a Desired Target
        /// Get to move the number of Speed
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override bool MoveAsTurn(PlayerInfoModel Attacker)
        {

            // For Attack, Choose Who
            EngineSettings.CurrentDefender = AttackChoice(Attacker);

            if (EngineSettings.CurrentDefender == null)
            {
                return false;
            }

            // Get X, Y for Defender
            var locationDefender = EngineSettings.MapModel.GetLocationForPlayer(EngineSettings.CurrentDefender);
            if (locationDefender == null)
            {
                return false;
            }

            var locationAttacker = EngineSettings.MapModel.GetLocationForPlayer(Attacker);
            if (locationAttacker == null)
            {
                return false;
            }

            // Find Location Nearest to Defender that is Open.

            // store all the empty locations to list (distance map)

            SortedDictionary<int, List<MapModelLocation>> distanceMap = EngineSettings.MapModel.calculateDistances(locationDefender);
            var speed = Attacker.Speed;

            //Traverse the distanceMap list to find the best spot according to speed 
            foreach (KeyValuePair<int, List<MapModelLocation>> entry in distanceMap)
            {
                var result = EngineSettings.MapModel.ReturnClosest(locationAttacker, speed, entry.Value);
                if (result != null)
                {
                    MapModelLocation targetLocation = result;
                    Debug.WriteLine(string.Format("{0} moves from {1},{2} to {3},{4} with speed {5}", locationAttacker.Player.Name, locationAttacker.Column, locationAttacker.Row, targetLocation.Column, targetLocation.Row, speed));
                    EngineSettings.BattleMessagesModel.TurnMessage = Attacker.Name + " approaches " + EngineSettings.CurrentDefender.Name;
                    return EngineSettings.MapModel.MovePlayerOnMap(locationAttacker, targetLocation);
                }

            }
            return false;

        }

        /// <summary>
        /// Decide to use an Ability or not
        /// 
        /// Set the Ability
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override bool ChooseToUseAbility(PlayerInfoModel Attacker)
        {
            // See if healing is needed.
            EngineSettings.CurrentActionAbility = Attacker.SelectHealingAbility();
            if (EngineSettings.CurrentActionAbility != AbilityEnum.Unknown)
            {
                EngineSettings.CurrentAction = ActionEnum.Ability;
                return true;
            }

            // If not needed, then role dice to see if other ability should be used
            // <30% chance
            if (DiceHelper.RollDice(1, 10) < 3)
            {
                EngineSettings.CurrentActionAbility = Attacker.SelectAbilityToUse();

                if (EngineSettings.CurrentActionAbility != AbilityEnum.Unknown)
                {
                    // Ability can , switch to unknown to exit
                    EngineSettings.CurrentAction = ActionEnum.Ability;
                    return true;
                }

                // No ability available
                return false;
            }

            // Don't try
            return false;
        }

        /// <summary>
        /// Pick the Character to Attack
        /// </summary>
        public override PlayerInfoModel SelectCharacterToAttack()
        {
            if (EngineSettings.PlayerList == null)
            {
                return null;
            }

            if (EngineSettings.PlayerList.Count < 1)
            {
                return null;
            }

            // Select min level Chr in the list
            var Defender = EngineSettings.PlayerList
                .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Character)
                .OrderBy(m => m.Level).FirstOrDefault();

            return Defender;
        }

        /// <summary>
        /// Pick the Monster to Attack
        /// </summary>
        public override PlayerInfoModel SelectMonsterToAttack()
        {
            if (EngineSettings.PlayerList == null)
            {
                return null;
            }

            if (EngineSettings.PlayerList.Count < 1)
            {
                return null;
            }

            // Select first one to hit in the list for now...
            // Attack the Weakness (lowest Max Health) MonsterModel first 
            var Defender = EngineSettings.PlayerList
                .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Monster)
                .OrderBy(m => m.MaxHealth).FirstOrDefault();

            return Defender;
        }

        /// <summary>
        /// // MonsterModel Attacks CharacterModel
        /// </summary>
        public override bool TurnAsAttack(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            // Set Messages to empty
            _ = EngineSettings.BattleMessagesModel.ClearMessages();

            // Do the Attack
            _ = CalculateAttackStatus(Attacker, Target);

            // See if the Battle Settings Overrides the Roll
            EngineSettings.BattleMessagesModel.HitStatus = BattleSettingsOverride(Attacker);

            if (Attacker.Name == "Doug")
            {
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
            }

            switch (EngineSettings.BattleMessagesModel.HitStatus)
            {
                case HitStatusEnum.Miss:
                    // It's a Miss

                    break;

                case HitStatusEnum.CriticalMiss:
                    // It's a Critical Miss, so Bad things may happen
                    _ = DetermineCriticalMissProblem(Attacker);

                    break;

                case HitStatusEnum.CriticalHit:
                case HitStatusEnum.Hit:
                    // It's a Hit

                    //Calculate Damage
                    EngineSettings.BattleMessagesModel.DamageAmount = Attacker.GetDamageRollValue();

                    // If critical Hit, double the damage
                    if (EngineSettings.BattleMessagesModel.HitStatus == HitStatusEnum.CriticalHit)
                    {
                        EngineSettings.BattleMessagesModel.DamageAmount *= 2;
                    }

                    if (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.IFeelGood == true && Attacker.PlayerType == PlayerTypeEnum.Character)
                    {
                        var bonusAttack = DiceHelper.RollDice(1, 20);
                        var chanceForBonus = DiceHelper.RollDice(1, 20);
                        if (chanceForBonus > 10)
                        {
                            EngineSettings.BattleMessagesModel.AttackStatus += " I Feel Good...";
                            EngineSettings.BattleMessagesModel.DamageAmount += bonusAttack;
                            Target.Defense -= bonusAttack;
                        }
                    }

                    // Apply the Damage
                    _ = ApplyDamage(Target);

                    EngineSettings.BattleMessagesModel.TurnMessageSpecial = EngineSettings.BattleMessagesModel.GetCurrentHealthMessage();

                    // Check if Dead and Remove
                    _ = RemoveIfDead(Target);

                    // If it is a character apply the experience earned
                    _ = CalculateExperience(Attacker, Target);

                    break;
            }

            EngineSettings.BattleMessagesModel.TurnMessage = Attacker.Name + EngineSettings.BattleMessagesModel.AttackStatus + Target.Name + EngineSettings.BattleMessagesModel.TurnMessageSpecial + EngineSettings.BattleMessagesModel.ExperienceEarned;
            Debug.WriteLine(EngineSettings.BattleMessagesModel.TurnMessage);

            return true;
        }

        /// <summary>
        /// Target Died
        /// 
        /// Process for death...
        /// 
        /// Returns the count of items dropped at death
        /// </summary>
        public override bool TargetDied(PlayerInfoModel Target)
        {
            bool found;

            // Mark Status in output
            EngineSettings.BattleMessagesModel.TurnMessageSpecial = " and causes death on the spot. ";

            // Removing the 
            _ = EngineSettings.MapModel.RemovePlayerFromMap(Target);

            // INFO: Teams, Hookup your Boss if you have one...

            // Using a switch so in the future additional PlayerTypes can be added (Boss...)
            switch (Target.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    // Add the Character to the killed list
                    EngineSettings.BattleScore.CharacterAtDeathList += Target.FormatOutput() + "\n";

                    EngineSettings.BattleScore.CharacterModelDeathList.Add(Target);

                    _ = DropItems(Target);

                    found = EngineSettings.CharacterList.Remove(EngineSettings.CharacterList.Find(m => m.Guid.Equals(Target.Guid)));
                    found = EngineSettings.PlayerList.Remove(EngineSettings.PlayerList.Find(m => m.Guid.Equals(Target.Guid)));

                    return true;

                case PlayerTypeEnum.Monster:
                default:
                    // Add one to the monsters killed count...
                    EngineSettings.BattleScore.MonsterSlainNumber++;

                    // Add the MonsterModel to the killed list
                    EngineSettings.BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                    EngineSettings.BattleScore.MonsterModelDeathList.Add(Target);

                    _ = DropItems(Target);

                    found = EngineSettings.MonsterList.Remove(EngineSettings.MonsterList.Find(m => m.Guid.Equals(Target.Guid)));
                    found = EngineSettings.PlayerList.Remove(EngineSettings.PlayerList.Find(m => m.Guid.Equals(Target.Guid)));

                    return true;
            }
        }

        /// <summary>
        /// Drop Items
        /// </summary>
        public override int DropItems(PlayerInfoModel Target)
        {
            var DroppedMessage = "\nItems Dropped : \n";

            // Drop Items to ItemModel Pool
            var myItemList = Target.DropAllItems();
            var itemCount = ((int)BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.FirstOrDefault().Difficulty) / 10;
            // I feel generous, even when characters die, random drops happen :-)
            // If Random drops are enabled, then add some....
            myItemList.AddRange(GetRandomMonsterItemDrops(itemCount));

            // Add to ScoreModel
            foreach (var ItemModel in myItemList)
            {
                EngineSettings.BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                DroppedMessage += ItemModel.Name + "\n";
            }

            EngineSettings.ItemPool.AddRange(myItemList);

            if (myItemList.Count == 0)
            {
                DroppedMessage = " Nothing dropped. ";
            }

            EngineSettings.BattleMessagesModel.DroppedMessage = DroppedMessage;

            EngineSettings.BattleScore.ItemModelDropList.AddRange(myItemList);

            return myItemList.Count();
        }

        /// <summary>
        /// Roll To Hit
        /// </summary>
        public override HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {
            var d20 = DiceHelper.RollDice(1, 20);

            if (d20 == 1)
            {
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                EngineSettings.BattleMessagesModel.AttackStatus = " rolls a 1 to fail miserably and miss ";

                if (EngineSettings.BattleSettingsModel.AllowCriticalMiss)
                {
                    EngineSettings.BattleMessagesModel.AttackStatus = " rolls 1 to completly miss ";
                    EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.CriticalMiss;
                }

                return EngineSettings.BattleMessagesModel.HitStatus;
            }

            if (d20 == 20)
            {
                EngineSettings.BattleMessagesModel.AttackStatus = " rolls 20 for guaranteed hit ";
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Hit;

                if (EngineSettings.BattleSettingsModel.AllowCriticalHit)
                {
                    EngineSettings.BattleMessagesModel.AttackStatus = " rolls 20 for critical damage ";
                    EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.CriticalHit;
                }
                return EngineSettings.BattleMessagesModel.HitStatus;
            }

            var ToHitScore = d20 + AttackScore;
            if (ToHitScore < DefenseScore)
            {
                EngineSettings.BattleMessagesModel.AttackStatus = " rolls " + d20 + " and fails the attack on ";

                // Miss
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                EngineSettings.BattleMessagesModel.DamageAmount = 0;
                return EngineSettings.BattleMessagesModel.HitStatus;
            }

            EngineSettings.BattleMessagesModel.AttackStatus = " rolls " + d20 + " and strikes ";

            // Hit
            EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
            return EngineSettings.BattleMessagesModel.HitStatus;
        }

        /// <summary>
        /// Each round, the monsters will be able to drop from 0 - 12 items in the item pool.
        /// </summary>
        public override List<ItemModel> GetRandomMonsterItemDrops(int itemCount)
        {
            // You decide how to drop monster items, level, etc.

            //Every monster will have a chance to drop items.
            var NumberToDrop = ((int)BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.FirstOrDefault().Difficulty) / 10;

            var result = new List<ItemModel>();

            for (var i = 0; i < NumberToDrop; i++)
            {
                //Determine how manyy items the monster drops.
                var itemDrop = DiceHelper.RollDice(1, 3);
                
                // Get a random Unique Item
                switch (itemDrop) {
                    case 1:
                        //Monster dropped two items.
                        var dropOne = ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetMonsterUniqueItem());
                        result.Add(dropOne);
                        break;
                    case 2:
                        //Monster dropped two items.
                        var dropTwo = ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetMonsterUniqueItem());
                        result.Add(dropTwo);
                        dropTwo = ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetMonsterUniqueItem());
                        result.Add(dropTwo);
                        break;
                    default:
                        //Monster will drop no items.
                        break;
                }
                
            }

            return result;
        }

        /// <summary>
        /// Determine what Actions to do based on even or odd
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override ActionEnum DetermineActionChoice(PlayerInfoModel Attacker)
        {
            var d20 = DiceHelper.RollDice(1, 20);


            if (d20 % 2 == 0)
            {
                return ActionEnum.Attack;
            }

            return ActionEnum.Move;
        }

        /// <summary>
        /// Critical Miss Problem
        /// </summary>
        public override bool DetermineCriticalMissProblem(PlayerInfoModel attacker)
        {
            return base.DetermineCriticalMissProblem(attacker);
        }

        /// <summary>
        /// See if the Battle Settings will Override the Hit
        /// Return the Override for the HitStatus
        /// </summary>
        public override HitStatusEnum BattleSettingsOverride(PlayerInfoModel Attacker)
        {
            return base.BattleSettingsOverride(Attacker);
        }

        /// <summary>
        /// Return the Override for the HitStatus
        /// </summary>
        public override HitStatusEnum BattleSettingsOverrideHitStatusEnum(HitStatusEnum myEnum)
        {
            return base.BattleSettingsOverrideHitStatusEnum(myEnum);
        }

        /// <summary>
        /// Apply the Damage to the Target
        /// </summary>
        public override bool ApplyDamage(PlayerInfoModel Target)
        {
            return base.ApplyDamage(Target);
        }

        /// <summary>
        /// Calculate the Attack, return if it hit or missed.
        /// </summary>
        public override HitStatusEnum CalculateAttackStatus(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            return base.CalculateAttackStatus(Attacker, Target);
        }

        /// <summary>
        /// Calculate Experience
        /// Level up if needed
        /// </summary>
        public override bool CalculateExperience(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            return base.CalculateExperience(Attacker, Target);
        }

        /// <summary>
        /// If Dead process Target Died
        /// </summary>
        public override bool RemoveIfDead(PlayerInfoModel Target)
        {
            return base.RemoveIfDead(Target);
        }

        /// <summary>
        /// Use the Ability. Since abilities in game are board based and not player based,
        /// We need to trigger the effects in the TurnEngine rather than in PlayerInfoModel like the 
        /// default abilities. The reason being, we need to access other players on the board when the effect
        /// is triggered.
        /// </summary>
        public override bool UseAbility(PlayerInfoModel Attacker)
        {
            //Logic to check if ability is available

            /*Cast ability

            var ability = Attacker.UseAbility(EngineSettings.CurrentActionAbility);
            switch (ability)
            {
                case AbilityEnum.Reduce:
                    break;

            }*/
            var ability = Attacker.Job;
            switch(ability) 
            {
                case CharacterJobEnum.Nerd:
                    NerdAbility(Attacker);
                    break;

                case CharacterJobEnum.Athlete:
                    AthelteAbility(Attacker);
                    break;

                case CharacterJobEnum.Goth:
                    GothAbility(Attacker);
                    break;

                case CharacterJobEnum.Skater:
                    SkaterAbility(Attacker);
                    break;

                case CharacterJobEnum.Procrastinator:
                    ProcrastinatorAbility(Attacker);
                    break;

                case CharacterJobEnum.ClassClown:
                    ClassClownAbility(Attacker);
                    break;
              
            }
            return true;
        }

        /// <summary>
        /// Trigger Nerd Ability
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool NerdAbility(PlayerInfoModel Attacker) 
        {
            return true;
        }

        /// <summary>
        /// Trigger Athelte Ability
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool AthelteAbility(PlayerInfoModel Attacker)
        {
            return true;
        }


        /// <summary>
        /// Trigger Goth Ability
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool GothAbility(PlayerInfoModel Attacker)
        {
            return true;
        }


        /// <summary>
        /// Trigger Skater Ability
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool SkaterAbility(PlayerInfoModel Attacker)
        {
            return true;
        }
        
        /// <summary>
        /// Trigger Procrastinator Ability
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool ProcrastinatorAbility(PlayerInfoModel Attacker)
        {
            return true;
        }


        /// <summary>
        /// Trigger Class Clown Ability
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool ClassClownAbility(PlayerInfoModel Attacker)
        {
            return true;
        }

        /// <summary>
        /// Attack as a Turn
        /// 
        /// Pick who to go after
        /// 
        /// Determine Attack Score
        /// Determine DefenseScore
        /// 
        /// Do the Attack
        /// 
        /// </summary>
        public override bool Attack(PlayerInfoModel Attacker)
        {
            return base.Attack(Attacker);
        }

        /// <summary>
        /// Decide which to attack
        /// </summary>
        public override PlayerInfoModel AttackChoice(PlayerInfoModel data)
        {
            return base.AttackChoice(data);
        }
    }
}