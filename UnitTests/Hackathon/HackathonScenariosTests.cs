﻿using NUnit.Framework;

using Game.Models;
using System.Threading.Tasks;
using Game.ViewModels;

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
    }
}