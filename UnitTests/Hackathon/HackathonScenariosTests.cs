using NUnit.Framework;

using Game.Models;
using System.Threading.Tasks;
using Game.ViewModels;
using Game.Helpers;

namespace Scenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {
        #region TestSetup
        readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        [SetUp]
        public void Setup()
        {
            // Put seed data into the system for all tests
            _ = BattleEngineViewModel.Instance.Engine.Round.ClearLists();

            //Start the Engine in AutoBattle Mode
            _ = BattleEngineViewModel.Instance.Engine.StartBattle(false);

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalHit = false;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalMiss = false;
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion TestSetup

        #region Scenario0
        [Test]
        public void HakathonScenario_Scenario_0_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      <Describe the scenario>
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert


            // Act
            var result = EngineViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion Scenario0

        #region Scenario1
        [Test]
        public async Task HackathonScenario_Scenario_1_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      1
            *      
            * Description: 
            *      Make a Character called Mike, who dies in the first round
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go last...
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);

            // Set Monster Conditions

            // Auto Battle will add the monsters

            // Monsters always hit
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, EngineViewModel.Engine.EngineSettings.PlayerList.Find(m => m.Name.Equals("Mike")));
            Assert.AreEqual(1, EngineViewModel.Engine.EngineSettings.BattleScore.RoundCount);
        }
        #endregion Scenario1


        #region Scenario2
        [Test]
        public void HackathonScenario_Scenario_2_Doug_Always_Misses__Should_Miss()
        {
            /* 
            * Scenario Number:  
            *      2
            *      
            * Description: 
            *      Everyone loves to have Doug as part of their adventure party, however Doug has yet to 
            *      work out the fine motor skills needed for a good attack, and always misses.  Update the 
            *      game engine to force Doug to always miss. Doug can do other action such as move or use 
            *      abilities as appropriate.
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      Added condition in TurnEngine.cs to check if Attack name is Doug. If the Attacker name is Doug,
            *      attack will miss.
            * 
            * Test Algrorithm:
            *      Create Character named Doug
            *      Create a Monster to attack
            *      Set Character name to Doug.
            *      
            *  
            *      Startup Battle
            *      Make Doug Attack.
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify that Dougs attack was a Miss.
            *  
            */
            // Arrange
            var PlayerInfo = new PlayerInfoModel();
            PlayerInfo.Name = "Doug";

            EngineViewModel.Engine.EngineSettings.MonsterList.Add(new PlayerInfoModel(new MonsterModel()));

            // Act
            var result = EngineViewModel.Engine.Round.Turn.Attack(PlayerInfo);

            // Reset
            _ = EngineViewModel.Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreEqual(HitStatusEnum.Miss, EngineViewModel.Engine.EngineSettings.BattleMessagesModel.HitStatus);
        }

        #endregion Scenario2

        #region Scenario5
        [Test]
        public void HackathonScenario_Scenario_5_Critical_Miss()
        {
            /* 
            * Scenario Number:  
            *      5
            *      
            * Description: 
            *      Oops, I can’t believe I missed. On a roll of 1, not only will the attacker miss the target, but 
            *      something bad happens.
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      Added condition in TurnEngine.cs to check if Attack name is Doug. If the Attacker name is Doug,
            *      attack will miss.
            * 
            * Test Algrorithm:
            *      Create Character named Doug
            *      Create a Monster to attack
            *      Set Character name to Doug.
            *      
            *  
            *      Startup Battle
            *      Make Doug Attack.
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify that Dougs attack was a Miss.
            *  
            */
            // Arrange
            var PlayerInfo = new PlayerInfoModel();
            PlayerInfo.Name = "Doug";

            EngineViewModel.Engine.EngineSettings.MonsterList.Add(new PlayerInfoModel(new MonsterModel()));
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalMiss = true;


            // Act
            var result = EngineViewModel.Engine.Round.Turn.Attack(PlayerInfo);

            // Reset
            _ = EngineViewModel.Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreEqual(HitStatusEnum.Miss, EngineViewModel.Engine.EngineSettings.BattleMessagesModel.HitStatus);
        }

        #endregion Scenario5

        #region Scenario17
        [Test]
        public void HackathonScenario_Scenario_17_Default_Setting_False_Should_NOT_Zombie_Valid_Roll_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      17
            *      
            * Description: 
            *    17.	Sleepless Zombies in Seattle
                    When you kill a monster, there is a chance it returns from the dead and continues attacking as a zombie monster.  
                    Set the alive back to True, set HP to ½ original HP, update Name to Zombie <Monster> and continue the battle.
                    Set a debug setting to enable the feature, and a % chance for occurrence.


            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      Add a new setting on battlesettingpage.xaml and the cs file
            *      baseplayermodel.cs
            *      battlesettingpage model
            * 
            * Test Algrorithm:
            *      Create Character 
            *      create a dead monster 
            *  
            *      Startup Battle
            *      run a turn where attack is character and defend is monster
            *      monster dies, removed from list
            * 
            * Test Conditions:
            *      monster alive = false
            *      current health = 0
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Monster is dead
            *      Verify Monster name does not contains "zombie"
            *      Verify monster current health 0
            *       verify current monster list is empty
            *       
            */

            //Arrange

            // Set Character Conditions
            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 10,
                                Level = 1,
                                Attack = 100,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);

            // Set Monster Conditions
            //make a dead monster
            var monsterTest = new PlayerInfoModel(
                           new MonsterModel
                           {
                               Speed = -1,
                               Alive = false,
                               Level = 1,
                               Attack = 1,
                               CurrentHealth = 1,
                               ExperienceTotal = 1,
                               ExperienceRemaining = 1,
                               Name = "test",
                               MaxHealth = 20
                           });
            EngineViewModel.Engine.EngineSettings.MonsterList.Clear();
            EngineViewModel.Engine.EngineSettings.MonsterList.Add(monsterTest);

            //force dice roll 20
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(20);

            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.AllowZombieMode = false;

            // Act
            //character attacks, monster defend
            var result = BattleEngineViewModel.Instance.Engine.Round.Turn.TurnAsAttack(CharacterPlayerMike, monsterTest);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            var postBattleMonster = BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList;
            var deadMonster = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.MonsterModelDeathList.Find(x => x.Name == monsterTest.Name);
            Assert.AreEqual(true, result);
            Assert.AreEqual(false, deadMonster.Alive);
            Assert.AreEqual(false, deadMonster.Name.Contains("Zombie"));
            Assert.AreEqual(0, deadMonster.CurrentHealth);
            Assert.IsEmpty(postBattleMonster);
        }

        [Test]
        public void HackathonScenario_Scenario_17_Setting_On_Should_Zombie_Valid_Roll_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      17
            *      
            * Description: 
            *    17.	Sleepless Zombies in Seattle
                    When you kill a monster, there is a chance it returns from the dead and continues attacking as a zombie monster.  
                    Set the alive back to True, set HP to ½ original HP, update Name to Zombie <Monster> and continue the battle.
                    Set a debug setting to enable the feature, and a % chance for occurrence.


            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      Add a new setting on battlesettingpage.xaml and the cs file
            *      baseplayermodel.cs
            *      battlesettingpage model
            * 
            * Test Algrorithm:
            *      Create Character 
            *      create a dead monster 
            *  
            *      Startup Battle
            *      run a turn where attack is character and defend is monster
            *      monster dies, and become a zombie
            * 
            * Test Conditions:
            *      monster alive = true
            *      current health = half of max health
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Monster is alive
            *      Verify Monster name contains "zombie"
            *      Verify monster current health is half of max
            *  
            */

            //Arrange

            // Set Character Conditions
            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 10,
                                Level = 1,
                                Attack = 100,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);

            // Set Monster Conditions
            //make a dead monster
            var monsterTest = new PlayerInfoModel(
                           new MonsterModel
                           {
                               Speed = -1,
                               Alive = false,
                               Level = 1,
                               Attack = 1,
                               CurrentHealth = 1,
                               ExperienceTotal = 1,
                               ExperienceRemaining = 1,
                               Name = "test",
                               MaxHealth = 20
                           });
            EngineViewModel.Engine.EngineSettings.MonsterList.Clear();
            EngineViewModel.Engine.EngineSettings.MonsterList.Add(monsterTest);

            //force dice roll 20
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(20);

            var oldSetting = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.AllowZombieMode;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.AllowZombieMode = true;


            // Act
            //character attacks, monster defend
            var result = BattleEngineViewModel.Instance.Engine.Round.Turn.TurnAsAttack(CharacterPlayerMike, monsterTest);

            // Reset
            _ = DiceHelper.DisableForcedRolls();
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.AllowCriticalHit = oldSetting;

            // Assert
            var postBattleMonster = BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Find(x => x.Name == monsterTest.Name);

            Assert.AreEqual(true, result);
            Assert.AreEqual(true, postBattleMonster.Alive);
            Assert.IsTrue(postBattleMonster.Name.Contains("Zombie"));
            Assert.AreEqual(monsterTest.MaxHealth / 2, postBattleMonster.CurrentHealth);
        }
        #endregion Scenario17

        #region Scenario19
        [Test]
        public void HackathonScenario_Scenario_19_Setting_On_Should_apply_bonus_damage_Valid_Roll_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      17
            *      
            * Description: 
            *     I feel good…
                    When everyone feels good, all characters get a D20 added to their Attack Score, Monsters get a D20 subtracted to their defense.
                    Add a debug switch to control the setting and a % chance.
                    When the % happens output “I feel good”, and if audio is enabled, play a clip from James Brown

            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      Add a new setting, logic for zombie set on current health and name
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions
            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 10,
                                Level = 1,
                                Attack = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);

            // Set Monster Conditions
            //make a dead monster
            var monsterTest = new PlayerInfoModel(
                           new MonsterModel
                           {
                               Speed = -1,
                               Alive = false,
                               Level = 1,
                               Attack = 1,
                               CurrentHealth = 1,
                               ExperienceTotal = 1,
                               ExperienceRemaining = 1,
                               Name = "test",
                               MaxHealth = 20,
                               Defense = 100
                           });
            EngineViewModel.Engine.EngineSettings.MonsterList.Clear();
            EngineViewModel.Engine.EngineSettings.MonsterList.Add(monsterTest);

            //force dice roll 20
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(20);

            var oldSetting = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.AllowZombieMode;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.AllowZombieMode = true;


            // Act
            //character attacks, monster defend
            var result = BattleEngineViewModel.Instance.Engine.Round.Turn.TurnAsAttack(CharacterPlayerMike, monsterTest);

            // Reset
            _ = DiceHelper.DisableForcedRolls();
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.AllowCriticalHit = oldSetting;

            // Assert
            var postBattleMonster = BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Find(x => x.Name == monsterTest.Name);

            Assert.AreEqual(true, result);
            Assert.AreEqual(true, postBattleMonster.Alive);
            Assert.IsTrue(postBattleMonster.Name.Contains("Zombie"));
            //Assert.IsTrue(BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.AttackStatus.Contains("I feel good"));
            Assert.AreEqual(monsterTest.MaxHealth / 2, postBattleMonster.CurrentHealth);
        }
        #endregion Scenario19
    }
}