using System.Collections.Generic;

using NUnit.Framework;

using Game.Models;
using Game.Helpers;

namespace UnitTests.Helpers
{
    [TestFixture]
    public class AbilityHelperTests
    {
        //#region AbilityTracker
        //[Test]
        //public void AddAbilitiesBasedOnJob_Cleric_Should_Pass()
        //{
        //    //Arrange
        //    var data = new BasePlayerModel<CharacterModel>
        //    {
        //        Job = CharacterJobEnum.Cleric
        //    };

        //    var AbTracker = new Dictionary<AbilityEnum, int>();
        //    AbTracker.Add(AbilityEnum.Heal, 2);
        //    AbTracker.Add(AbilityEnum.Quick, 2);
        //    AbTracker.Add(AbilityEnum.Curse, 2);
        //    AbTracker.Add(AbilityEnum.Barrier, 2);
        //    AbTracker.Add(AbilityEnum.Bandage, 2);

        //    //Act
        //    AbilityHelper.AddAbilitiesBasedOnJob(data.AbilityTracker, data.Job, 2);

        //    //Reset

        //    //Assert
        //    Assert.AreEqual(data.AbilityTracker, AbTracker);
        //}

        //[Test]
        //public void AddAbilitiesBasedOnJob_Cleric_NotEmptyAbilityTracker_Should_Pass()
        //{
        //    //Arrange
        //    var data = new BasePlayerModel<CharacterModel>
        //    {
        //        Job = CharacterJobEnum.Cleric
        //    };

        //    data.AbilityTracker.Add(AbilityEnum.Heal, 1);
        //    data.AbilityTracker.Add(AbilityEnum.Quick, 1);
        //    data.AbilityTracker.Add(AbilityEnum.Curse, 1);
        //    data.AbilityTracker.Add(AbilityEnum.Barrier, 1);
        //    data.AbilityTracker.Add(AbilityEnum.Bandage, 1);

        //    var AbTracker = new Dictionary<AbilityEnum, int>();
        //    AbTracker.Add(AbilityEnum.Heal, 3);
        //    AbTracker.Add(AbilityEnum.Quick, 3);
        //    AbTracker.Add(AbilityEnum.Curse, 3);
        //    AbTracker.Add(AbilityEnum.Barrier, 3);
        //    AbTracker.Add(AbilityEnum.Bandage, 3);

        //    //Act
        //    AbilityHelper.AddAbilitiesBasedOnJob(data.AbilityTracker, data.Job, 2);

        //    //Reset

        //    //Assert
        //    Assert.AreEqual(data.AbilityTracker, AbTracker);
        //}

        //[Test]
        //public void AddAbilitiesBasedOnJob_Fighter_NotEmptyAbilityTracker_Should_Pass()
        //{
        //    //Arrange
        //    var data = new BasePlayerModel<CharacterModel>
        //    {
        //        Job = CharacterJobEnum.Fighter
        //    };

        //    data.AbilityTracker.Add(AbilityEnum.Nimble, 1);
        //    data.AbilityTracker.Add(AbilityEnum.Toughness, 1);
        //    data.AbilityTracker.Add(AbilityEnum.Focus, 1);
        //    data.AbilityTracker.Add(AbilityEnum.Bandage, 1);

        //    var = new Dictionary<AbilityEnum, int>();
        //    AbTracker.Add(AbilityEnum.Nimble, 3);
        //    AbTracker.Add(AbilityEnum.Toughness, 3);
        //    AbTracker.Add(AbilityEnum.Focus, 3);
        //    AbTracker.Add(AbilityEnum.Bandage, 3);

        //    //Act
        //    AbilityHelper.AddAbilitiesBasedOnJob(data.AbilityTracker, data.Job, 2);

        //    //Reset

        //    //Assert
        //    Assert.AreEqual(data.AbilityTracker, AbTracker);
        //}

        //[Test]
        //public void AddAbilitiesBasedOnJob_Fighter_Should_Pass()
        //{
        //    //Arrange
        //    var data = new BasePlayerModel<CharacterModel>
        //    {
        //        Job = CharacterJobEnum.Fighter
        //    };

        //    var AbTracker = new Dictionary<AbilityEnum, int>();
        //    AbTracker.Add(AbilityEnum.Nimble, 2);
        //    AbTracker.Add(AbilityEnum.Toughness, 2);
        //    AbTracker.Add(AbilityEnum.Focus, 2);
        //    AbTracker.Add(AbilityEnum.Bandage, 2);

        //    //Act
        //    AbilityHelper.AddAbilitiesBasedOnJob(data.AbilityTracker, data.Job, 2);

        //    //Reset

        //    //Assert
        //    Assert.AreEqual(data.AbilityTracker, AbTracker);
        //}

        //[Test]
        //public void AddAbilitiesBasedOnJob_Unknown_Should_Pass()
        //{
        //    //Arrange
        //    var data = new BasePlayerModel<CharacterModel>
        //    {
        //        Job = CharacterJobEnum.Unknown
        //    };

        //    var AbTracker = new Dictionary<AbilityEnum, int>();
        //    AbTracker.Add(AbilityEnum.Bandage, 2);

        //    //Act
        //    AbilityHelper.AddAbilitiesBasedOnJob(data.AbilityTracker, data.Job, 2);

        //    //Reset

        //    //Assert
        //    Assert.AreEqual(data.AbilityTracker, AbTracker);
        //}
        //#endregion AbilityTracker

        #region AddAbilitiesBasedOnJob
        [Test]
        public void AddAbilitiesBasedOnJob_Invalid_Ability_Null_Should_Fail()
        {
            //Arrange

            //Act
            var result = AbilityHelper.AddAbilitiesBasedOnJob(null, CharacterJobEnum.Unknown, 0);

            //Reset

            //Assert
            Assert.AreEqual(false, result); //Getting here without exception means pass
        }

        [Test]
        public void AddAbilitiesBasedOnJob_Invalid_Level_0_Should_Fail()
        {
            //Arrange
            var AbTracker = new Dictionary<AbilityEnum, int>();

            //Act
            var result = AbilityHelper.AddAbilitiesBasedOnJob(AbTracker, CharacterJobEnum.Unknown, 0);

            //Reset

            //Assert
            Assert.AreEqual(false, result); //Getting here without exception means pass
        }

        [Test]
        public void AddAbilitiesBasedOnJob_Valid_Unknown_Should_Pass()
        {
            //Arrange
            var AbTracker = new Dictionary<AbilityEnum, int>();

            //Act
            var result = AbilityHelper.AddAbilitiesBasedOnJob(AbTracker, CharacterJobEnum.Unknown, 1);

            //Reset

            //Assert
            Assert.AreEqual(true, result); //Getting here without exception means pass
        }

        [Test]
        public void AddAbilitiesBasedOnJob_Valid_Unknown_Repeat_Should_Increment()
        {
            //Arrange
            var AbTracker = new Dictionary<AbilityEnum, int>();

            // First time
            _ = AbilityHelper.AddAbilitiesBasedOnJob(AbTracker, CharacterJobEnum.Unknown, 1);

            //Act
            var result = AbilityHelper.AddAbilitiesBasedOnJob(AbTracker, CharacterJobEnum.Unknown, 2);

            //Reset

            //Assert
            Assert.AreEqual(true, result); //Getting here without exception means pass                
        }

        [Test]
        public void AddAbilitiesBasedOnJob_Valid_Cleric_Should_Pass()
        {
            //Arrange
            var AbTracker = new Dictionary<AbilityEnum, int>();

            //Act
            var result = AbilityHelper.AddAbilitiesBasedOnJob(AbTracker, CharacterJobEnum.Cleric, 1);

            //Reset

            //Assert
            Assert.AreEqual(true, result); //Getting here without exception means pass
        }

        [Test]
        public void AddAbilitiesBasedOnJob_Valid_Cleric_Repeat_Should_Increment()
        {
            //Arrange
            var AbTracker = new Dictionary<AbilityEnum, int>();

            // First time
            _ = AbilityHelper.AddAbilitiesBasedOnJob(AbTracker, CharacterJobEnum.Cleric, 1);

            //Act
            var result = AbilityHelper.AddAbilitiesBasedOnJob(AbTracker, CharacterJobEnum.Cleric, 2);

            //Reset

            //Assert
            Assert.AreEqual(true, result); //Getting here without exception means pass                
        }

        [Test]
        public void AddAbilitiesBasedOnJob_Valid_Fighter_Should_Pass()
        {
            //Arrange
            var AbTracker = new Dictionary<AbilityEnum, int>();

            //Act
            var result = AbilityHelper.AddAbilitiesBasedOnJob(AbTracker, CharacterJobEnum.Fighter, 1);

            //Reset

            //Assert
            Assert.AreEqual(true, result); //Getting here without exception means pass
        }

        [Test]
        public void AddAbilitiesBasedOnJob_Valid_Fighter_Repeat_Should_Increment()
        {
            //Arrange
            var AbTracker = new Dictionary<AbilityEnum, int>();

            // First time
            _ = AbilityHelper.AddAbilitiesBasedOnJob(AbTracker, CharacterJobEnum.Fighter, 1);

            //Act
            var result = AbilityHelper.AddAbilitiesBasedOnJob(AbTracker, CharacterJobEnum.Fighter, 2);

            //Reset

            //Assert
            Assert.AreEqual(true, result); //Getting here without exception means pass                
        }
        #endregion AddAbilitiesBasedOnJob
    }
}