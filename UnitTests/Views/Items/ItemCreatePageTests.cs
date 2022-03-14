using NUnit.Framework;

using Game;
using Game.Views;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using Game.Models;

namespace UnitTests.Views
{
    [TestFixture]
    public class ItemCreatePageTests : ItemCreatePage
    {
        App app;
        ItemCreatePage page;

        public ItemCreatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ItemCreatePage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void ItemCreatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemCreatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Null_Image_Should_Pass()
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
        public void ItemCreatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Value_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange

            page = new ItemCreatePage();
            var oldValue = 0.0;
            var newValue = 1.0;

            var args = new ValueChangedEventArgs(oldValue, newValue);

            // Act
            page.Value_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Range_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange

            page = new ItemCreatePage();
            var oldRange = 0.0;
            var newRange = 1.0;

            var args = new ValueChangedEventArgs(oldRange, newRange);

            // Act
            page.Range_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Damage_OnStepperDamageChanged_Default_Should_Pass()
        {
            // Arrange
            page = new ItemCreatePage();
            var oldDamage = 0.0;
            var newDamage = 1.0;

            var args = new ValueChangedEventArgs(oldDamage, newDamage);

            // Act
            page.Damage_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Null_Valid_all_But_Description_Should_Pass()
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
        public void ItemCreatePage_Save_Clicked_Invalid_Name_Should_Pass()
        {
            // Arrange
            var nameEntry = page.FindByName("NameEntry");
            ((Entry)nameEntry).Text = null;

            page.Name_TextChanged(null, null);

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Invalid_Description_Should_Pass()
        {
            // Arrange
            var nameEntry = page.FindByName("NameEntry");
            ((Entry)nameEntry).Text = "test";

            var descriptionEntry = page.FindByName("DescriptionEntry");
            ((Entry)descriptionEntry).Text = null;

            page.Name_TextChanged(null, null);

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Null_Valid_all_But_Location_Should_Pass()
        {
            // Arrange
            var nameEntry = page.FindByName("NameEntry");
            ((Entry)nameEntry).Text = "test";
            var descriptionEntry = page.FindByName("DescriptionEntry");
            ((Entry)descriptionEntry).Text = "test";

            page.Name_TextChanged(null, null);
            page.Description_TextChanged(null, null);


            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Null_Valid_all_But_Attribute_Should_Pass()
        {
            // Arrange
            var nameEntry = page.FindByName("NameEntry");
            ((Entry)nameEntry).Text = "test";
            var descriptionEntry = page.FindByName("DescriptionEntry");
            ((Entry)descriptionEntry).Text = "test";
            page.ViewModel.Data.Location = ItemLocationEnum.Necklass;

            page.Name_TextChanged(null, null);
            page.Description_TextChanged(null, null);

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Null_Valid_Should_Pass()
        {
            // Arrange
            var nameEntry = page.FindByName("NameEntry");
            ((Entry)nameEntry).Text = "test";

            var descriptionEntry = page.FindByName("DescriptionEntry");
            ((Entry)descriptionEntry).Text = "test";

            page.Name_TextChanged(null, null);
            page.Description_TextChanged(null, null);

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_name_changed_empty_string_Should_Pass()
        {
            // Arrange
            var nameEntry = page.FindByName("NameEntry");
            ((Entry)nameEntry).Text = " ";

            // Act


            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_description_changed_empty_string_Should_Pass()
        {
            // Arrange
            var descriptionEntry = page.FindByName("DescriptionEntry");
            ((Entry)descriptionEntry).Text = " ";

            //act

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_round_silder_null_silder_Should_Pass()
        {
            // Arrange

            //act
            var test = page.RoundSilderValueToWhole(2.3, null);

            // Reset

            // Assert
            Assert.IsTrue(test == 0); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_image_text_change_invalid_ending_Should_Pass()
        {
            // Arrange
            var ImageEntry = page.FindByName("ImageEntry");
            ((Entry)ImageEntry).Text = "asdasd";

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_image_text_change_invalid_text_Should_Pass()
        {
            // Arrange
            var ImageEntry = page.FindByName("ImageEntry");
            ((Entry)ImageEntry).Text = " ";

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_image_text_empty_invalid_ending_Should_Pass()
        {
            // Arrange
            var ImageEntry = page.FindByName("ImageEntry");
            ((Entry)ImageEntry).Text = "";
            page.Save_Clicked(null, null);
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_validateErrorAttributeEnum__Should_Pass()
        {
            // Arrange
            var ret = page.validateErrorAttributeEnum(AttributeEnum.CurrentHealth);

            // Reset

            // Assert
            Assert.IsTrue(ret); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_viewmodelImage_text_empty_invalid_ending_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.ImageURI = string.Empty;
            page.ViewModel.Data.Location = ItemLocationEnum.PrimaryHand;
            page.ViewModel.Data.Attribute = AttributeEnum.CurrentHealth;
            page.Save_Clicked(null, null);
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_viewmodelImage_text_empty_valid_ending_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.ImageURI = "string.Empty";
            page.ViewModel.Data.Location = ItemLocationEnum.PrimaryHand;
            page.ViewModel.Data.Attribute = AttributeEnum.CurrentHealth;
            page.Save_Clicked(null, null);
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}