﻿using NUnit.Framework;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class MonsterUpdatePageTests : MonsterUpdatePage
    {
        App app;
        MonsterUpdatePage page;

        public MonsterUpdatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new MonsterUpdatePage(new GenericViewModel<MonsterModel>(new MonsterModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void MonsterUpdatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MonsterUpdatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_Save_Clicked_Null_Image_Should_Pass()
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
        public void MonsterUpdatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_GetItemToDisplay_ShowPopup_Valid_Should_Pass()
        {
            // Arrange
            var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
            page.ViewModel.Data.PrimaryHand = item.Id;
            var StackItem = page.GetItemToDisplay(ItemLocationEnum.PrimaryHand);
            var dataImage = StackItem.Children[0];

            // Act
            ((ImageButton)dataImage).PropagateUpClicked();

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void ItemCreatePage_Speed_OnStepperValueChanged_Valid_Should_Pass()
        {
            // Arrange


            // Act
            page.Speed_OnStepperValueChanged(null, new ValueChangedEventArgs(0, 1));

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Attack_OnStepperValueChanged_Valid_Should_Pass()
        {
            // Arrange


            // Act
            page.Attack_OnStepperValueChanged(null, new ValueChangedEventArgs(0, 1));

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Def_OnStepperValueChanged_Valid_Should_Pass()
        {
            // Arrange


            // Act
            page.Defense_OnStepperValueChanged(null, new ValueChangedEventArgs(0, 1));

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_OnPopupItemSelected_Clicked_Default_Should_Pass()
        {
            // Arrange

            var data = new ItemModel();

            var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(data, 0);

            // Act
            page.OnPopupItemSelected(null, selectedCharacterChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_OnPopupItemSelected_Clicked_Null_Should_Fail()
        {
            // Arrange

            var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(null, 0);

            // Act
            page.OnPopupItemSelected(null, selectedCharacterChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_round_silder_null_silder_Should_Pass()
        {
            // Arrange

            //act
            var test = page.RoundSilderValueToWhole(2.3, null);

            // Reset

            // Assert
            Assert.IsTrue(test == 0); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_close_popup_clicked_Should_Pass()
        {
            // Arrange

            //act
            page.ClosePopup_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_Save_Clicked_Null_Valid_all_But_Description_Should_Pass()
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
        public void MonsterUpdatePage_Save_Clicked_Invalid_Name_Should_Pass()
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
        public void MonsterUpdatePage_Save_Clicked_Invalid_Description_Should_Pass()
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
        public void MonsterUpdatePage_Save_Clicked_Null_Valid_all_But_Location_Should_Pass()
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
        public void MonsterUpdatePage_Save_Clicked_Null_Valid_all_But_Attribute_Should_Pass()
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
        public void MonsterUpdatePage_Save_Clicked_Null_Valid_Should_Pass()
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
        public void MonsterUpdatePage_name_changed_empty_string_Should_Pass()
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
        public void MonsterUpdatePage_description_changed_empty_string_Should_Pass()
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
        public void MonsterUpdatePage_image_text_change_invalid_ending_Should_Pass()
        {
            // Arrange
            var ImageEntry = page.FindByName("ImageEntry");
            ((Entry)ImageEntry).Text = "asdasd";
          
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_image_text_change_invalid_text_Should_Pass()
        {
            // Arrange
            var ImageEntry = page.FindByName("ImageEntry");
            ((Entry)ImageEntry).Text = " ";

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_location_changed_Should_Pass()
        {
            // Arrange
            var LocationPicker = page.FindByName("LocationPicker");
            var data = ((Picker)LocationPicker).Items;

            //act
            for (int i = 0; i < data.Count; i++)
            {
                ((Picker)LocationPicker).SelectedIndex = i;
               page.Location_Changed(null, null);
            }
            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}