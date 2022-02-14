using NUnit.Framework;

using Game.Helpers;

namespace UnitTests.Helpers
{
    [TestFixture]
    public class RollDiceHelperTests
    {
        #region UTDiceLecture

        // TODO: Example for UT Lecture
        //[Test]
        //public void RollDiceHelper_RollDice_Valid_1Time_6sided_Should_Return_1()
        //{
        //    // Arrange

        //    // Act
        //    var result = DiceHelper.RollDice(1, 6);

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(1, result);
        //}

        [Test]
        public void RollDiceHelper_RollDice_Valid_1Time_6sided_Should_Between_1_and_6()
        {
            // Arrange

            // Act
            var result = DiceHelper.RollDice(1, 6);

            // Reset

            // Assert
            Assert.AreEqual(true, result < 7);
            Assert.AreEqual(true, result > 0);
        }

        [Test]
        public void RollDiceHelper_RollDice_Valid_1Time_6sided_Should_Not_Less_1_and_Greater_6()
        {
            // Arrange

            // Act
            var result = DiceHelper.RollDice(1, 6);

            // Reset

            // Assert
            Assert.AreEqual(false, result > 6);
            Assert.AreEqual(false, result < 1);
        }

        [Test]
        public void RollDiceHelper_RollDice_Valid_1Time_6sided_Forced_6_Should_Return_6()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(6);

            // Act
            var result = DiceHelper.RollDice(1, 6);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(6, result);
        }

        [Test]
        public void RollDiceHelper_EnableForcedRolls_Valid_Should_Return_True()
        {
            // Arrange

            // Act
            _ = DiceHelper.EnableForcedRolls();
            var result = DiceHelper.ForceRollsToNotRandom;

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void RollDiceHelper_SetForcedRollValue_Valid_1_Should_Return_1()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();

            // Act
            var result = DiceHelper.SetForcedRollValue(1);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void RollDiceHelper_SetForcedRollValue_Valid_Forced_1_Should_Return_1()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(1);

            // Act
            var result = DiceHelper.RollDice(1, 6);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(1, result);
        }

        //// Example for UT Lecture
        //[Test]
        //public void RollDiceHelper_SetForcedRollValue_InValid_Forced_neg1_Should_Return_0()
        //{
        //    // Arrange
        //    _ = DiceHelper.EnableForcedRolls();
        //    _ = DiceHelper.SetForcedRollValue(-1);

        //    // Act
        //    var result = DiceHelper.RollDice(1, 6);

        //    // Reset
        //    _ = DiceHelper.DisableForcedRolls();

        //    // Assert
        //    Assert.AreEqual(0, result);
        //}

        [Test]
        public void RollDiceHelper_SetForcedRollValue_InValid_Forced_neg1_Should_Return_neg1()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(-1);

            // Act
            var result = DiceHelper.RollDice(1, 6);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void RollDiceHelper_SetForcedRollValue_InValid_Dice_neg1_Should_Return_0()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(1);

            // Act
            var result = DiceHelper.RollDice(-1, 6);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(0, result);
        }

        #endregion UTDiceLecture

        [Test]
        public void RollDiceHelper_RollDice_InValid_NegTime_6sided_Should_Fail()
        {
            // Arrange

            // Act
            var result = DiceHelper.RollDice(-1, 6);

            // Reset

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void RollDiceHelper_RollDice_InValid_1Time_0sided_Should_Fail()
        {
            // Arrange

            // Act
            var result = DiceHelper.RollDice(1, 0);

            // Reset

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void RollDiceHelper_RollDice_InValid_1Time_Negsided_Should_Fail()
        {
            // Arrange

            // Act
            var result = DiceHelper.RollDice(1, -1);

            // Reset

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void RollDiceHelper_SetForcedRollValue_InValid_Forced_neg1_Dice_neg1_Roll_neg1_Should_Return_0()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(-1);

            // Act
            var result = DiceHelper.RollDice(-1, -6);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(0, result);
        }

    }
}