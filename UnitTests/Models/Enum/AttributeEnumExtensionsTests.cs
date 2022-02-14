using NUnit.Framework;

using Game.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class AttributeEnumExtensionsTests
    {
        [Test]
        public void AttributeEnumExtensionsTests_ConvertStringToEnum_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnumHelper.ConvertStringToEnum("Unknown");

            // Reset

            // Assert
            Assert.AreEqual(AttributeEnum.Unknown, result);
        }

        [Test]
        public void AttributeEnumExtensionsTests_GetListCharacter_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnumHelper.GetListCharacter;

            // Reset

            // Assert
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void AttributeEnumExtensionsTests_Get_Full_List_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnumHelper.GetListItem;

            // Reset

            // Assert
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void AttributeEnumExtensionsTests_Unknown_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.Unknown.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Unknown", result);
        }

        [Test]
        public void AttributeEnumExtensionsTests_Attack_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.Attack.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Attack", result);
        }

        [Test]
        public void AttributeEnumExtensionsTests_CurrentHealth_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.CurrentHealth.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Current Health", result);
        }

        [Test]
        public void AttributeEnumExtensionsTests_Defense_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.Defense.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Defense", result);
        }

        [Test]
        public void AttributeEnumExtensionsTests_MaxHealth_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.MaxHealth.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Max Health", result);
        }

        [Test]
        public void AttributeEnumExtensionsTests_Speed_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AttributeEnum.Speed.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Speed", result);
        }
    }
}
