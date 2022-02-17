using NUnit.Framework;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class ScoreUpdatePageTests : ScoreUpdatePage
    {
        App app;
        ScoreUpdatePage page;

        public ScoreUpdatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ScoreUpdatePage(new GenericViewModel<ScoreModel>(new ScoreModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void ScoreUpdatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ScoreUpdatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_Save_Clicked_Null_Image_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.ImageURI = null;

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
        [Test]
        public void ScoreUpdatePage_Save_Clicked_Null_Valid_all_But_Name_Should_Pass()
        {
            // Arrange
            var nameEntry = page.FindByName("NameEntry");
            ((Entry)nameEntry).Text = "";
            var battleEntry = page.FindByName("BattleEntry");
            ((Entry)battleEntry).Text = "0";
            var DateEntry = page.FindByName("DateEntry");
            ((Entry)DateEntry).Text = "1/11";
            var ExperienceEntry = page.FindByName("ExperienceEntry");
            ((Entry)ExperienceEntry).Text = "0";
            var MonsterEntry = page.FindByName("MonsterEntry");
            ((Entry)MonsterEntry).Text = "0";
            var ScoreEntry = page.FindByName("ScoreEntry");
            ((Entry)ScoreEntry).Text = "0";
            page.Name_TextChanged(null, null);
            page.Battle_TextChanged(null, null);
            page.Date_TextChanged(null, null);
            page.Experience_TextChanged(null, null);
            page.Monster_TextChanged(null, null);
            page.Score_TextChanged(null, null);

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_Save_Clicked_Null_Valid_all_But_Battle_Should_Pass()
        {
            // Arrange
            var nameEntry = page.FindByName("NameEntry");
            ((Entry)nameEntry).Text = "Text";
            var battleEntry = page.FindByName("BattleEntry");
            ((Entry)battleEntry).Text = "";
            var DateEntry = page.FindByName("DateEntry");
            ((Entry)DateEntry).Text = "1/11";
            var ExperienceEntry = page.FindByName("ExperienceEntry");
            ((Entry)ExperienceEntry).Text = "0";
            var MonsterEntry = page.FindByName("MonsterEntry");
            ((Entry)MonsterEntry).Text = "0";
            var ScoreEntry = page.FindByName("ScoreEntry");
            ((Entry)ScoreEntry).Text = "0";
            page.Name_TextChanged(null, null);
            page.Battle_TextChanged(null, null);
            page.Date_TextChanged(null, null);
            page.Experience_TextChanged(null, null);
            page.Monster_TextChanged(null, null);
            page.Score_TextChanged(null, null);

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_Save_Clicked_Null_Valid_all_But_Date_Should_Pass()
        {
            // Arrange
            var nameEntry = page.FindByName("NameEntry");
            ((Entry)nameEntry).Text = "Text";
            var battleEntry = page.FindByName("BattleEntry");
            ((Entry)battleEntry).Text = "0";
            var DateEntry = page.FindByName("DateEntry");
            ((Entry)DateEntry).Text = "";
            var ExperienceEntry = page.FindByName("ExperienceEntry");
            ((Entry)ExperienceEntry).Text = "0";
            var MonsterEntry = page.FindByName("MonsterEntry");
            ((Entry)MonsterEntry).Text = "0";
            var ScoreEntry = page.FindByName("ScoreEntry");
            ((Entry)ScoreEntry).Text = "0";
            page.Name_TextChanged(null, null);
            page.Battle_TextChanged(null, null);
            page.Date_TextChanged(null, null);
            page.Experience_TextChanged(null, null);
            page.Monster_TextChanged(null, null);
            page.Score_TextChanged(null, null);

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_Save_Clicked_Null_Valid_all_But_Experience_Should_Pass()
        {
            // Arrange
            var nameEntry = page.FindByName("NameEntry");
            ((Entry)nameEntry).Text = "Text";
            var battleEntry = page.FindByName("BattleEntry");
            ((Entry)battleEntry).Text = "0";
            var DateEntry = page.FindByName("DateEntry");
            ((Entry)DateEntry).Text = "1/11";
            var ExperienceEntry = page.FindByName("ExperienceEntry");
            ((Entry)ExperienceEntry).Text = "";
            var MonsterEntry = page.FindByName("MonsterEntry");
            ((Entry)MonsterEntry).Text = "0";
            var ScoreEntry = page.FindByName("ScoreEntry");
            ((Entry)ScoreEntry).Text = "0";
            page.Name_TextChanged(null, null);
            page.Battle_TextChanged(null, null);
            page.Date_TextChanged(null, null);
            page.Experience_TextChanged(null, null);
            page.Monster_TextChanged(null, null);
            page.Score_TextChanged(null, null);

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
        [Test]
        public void ItemUpdatePage_Save_Clicked_Null_Valid_all_But_Monster_Should_Pass()
        {
            // Arrange
            var nameEntry = page.FindByName("NameEntry");
            ((Entry)nameEntry).Text = "Text";
            var battleEntry = page.FindByName("BattleEntry");
            ((Entry)battleEntry).Text = "0";
            var DateEntry = page.FindByName("DateEntry");
            ((Entry)DateEntry).Text = "1/11";
            var ExperienceEntry = page.FindByName("ExperienceEntry");
            ((Entry)ExperienceEntry).Text = "0";
            var MonsterEntry = page.FindByName("MonsterEntry");
            ((Entry)MonsterEntry).Text = "";
            var ScoreEntry = page.FindByName("ScoreEntry");
            ((Entry)ScoreEntry).Text = "0";
            page.Name_TextChanged(null, null);
            page.Battle_TextChanged(null, null);
            page.Date_TextChanged(null, null);
            page.Experience_TextChanged(null, null);
            page.Monster_TextChanged(null, null);
            page.Score_TextChanged(null, null);

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_Save_Clicked_Null_Valid_all_But_Score_Should_Pass()
        {
            // Arrange
            var nameEntry = page.FindByName("NameEntry");
            ((Entry)nameEntry).Text = "Text";
            var battleEntry = page.FindByName("BattleEntry");
            ((Entry)battleEntry).Text = "0";
            var DateEntry = page.FindByName("DateEntry");
            ((Entry)DateEntry).Text = "1/11";
            var ExperienceEntry = page.FindByName("ExperienceEntry");
            ((Entry)ExperienceEntry).Text = "0";
            var MonsterEntry = page.FindByName("MonsterEntry");
            ((Entry)MonsterEntry).Text = "0";
            var ScoreEntry = page.FindByName("ScoreEntry");
            ((Entry)ScoreEntry).Text = "";
            page.Name_TextChanged(null, null);
            page.Battle_TextChanged(null, null);
            page.Date_TextChanged(null, null);
            page.Experience_TextChanged(null, null);
            page.Monster_TextChanged(null, null);
            page.Score_TextChanged(null, null);

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_Save_Clicked_Null_Valid_all_But_Description_Should_Pass()
        {
            // Arrange
            var nameEntry = page.FindByName("NameEntry");
            ((Entry)nameEntry).Text = "test";

            page.Name_TextChanged(null, null);

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_name_changed_empty_string_Should_Pass()
        {
            // Arrange
            var nameEntry = page.FindByName("NameEntry");
            ((Entry)nameEntry).Text = " ";
            page.Name_TextChanged(null, null);
            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_Battle_changed_empty_string_Should_Pass()
        {
            // Arrange
            var BattleEntry = page.FindByName("BattleEntry");
            ((Entry)BattleEntry).Text = " ";
            page.Name_TextChanged(null, null);
            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
        [Test]
        public void ScoreUpdatePage_Date_changed_empty_string_Should_Pass()
        {
            // Arrange
            var dateEntry = page.FindByName("DateEntry");
            ((Entry)dateEntry).Text = " ";
            page.Name_TextChanged(null, null);
            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_Experience_changed_empty_string_Should_Pass()
        {
            // Arrange
            var experienceEntry = page.FindByName("ExperienceEntry");
            ((Entry)experienceEntry).Text = " ";
            page.Name_TextChanged(null, null);
            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
        [Test]
        public void ScoreUpdatePage_Monster_changed_empty_string_Should_Pass()
        {
            // Arrange
            var monsterEntry = page.FindByName("MonsterEntry");
            ((Entry)monsterEntry).Text = " ";
            page.Name_TextChanged(null, null);
            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
        [Test]
        public void ScoreUpdatePage_Score_changed_empty_string_Should_Pass()
        {
            // Arrange
            var scoreEntry = page.FindByName("ScoreEntry");
            ((Entry)scoreEntry).Text = " ";
            page.Name_TextChanged(null, null);
            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_image_text_change_invalid_ending_Should_Pass()
        {
            // Arrange
            var ImageEntry = page.FindByName("ImageEntry");
            ((Entry)ImageEntry).Text = "asdasd";

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_image_text_change_invalid_text_Should_Pass()
        {
            // Arrange
            var ImageEntry = page.FindByName("ImageEntry");
            ((Entry)ImageEntry).Text = " ";

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_image_text_change_empty_string_text_Should_Pass()
        {
            // Arrange
            var ImageEntry = page.FindByName("ImageEntry");
            ((Entry)ImageEntry).Text = string.Empty;

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}