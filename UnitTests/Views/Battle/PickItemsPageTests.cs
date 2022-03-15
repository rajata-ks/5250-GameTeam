using NUnit.Framework;

using Game;
using Game.Views;
using Xamarin.Forms.Mocks;
using Xamarin.Forms;
using Game.Models;
using System.Linq;
using Game.ViewModels;
using System.Collections.Generic;

namespace UnitTests.Views
{
    [TestFixture]
    public class PickItemsPageTests
    {
        App app;
        PickItemsPage page;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;
            var character = new PlayerInfoModel(new CharacterModel());
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(character);
            page = new PickItemsPage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void PickItemsPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PickItemsPage_CloseButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.CloseButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_ClosePopup_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.ClosePopup_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_GetItemToDisplay_Null_Should_Pass()
        {
            // Arrange
            // Act
            var ret = page.GetItemToDisplayPool(null);

            // Reset

            // Assert
            Assert.AreEqual(typeof(StackLayout), ret.GetType());
        }

        [Test]
        public void PickItemsPage_GetItemToDisplay_empty_id_Should_Pass()
        {
            // Arrange
            var item = new ItemModel();
            item.Id = string.Empty;
            // Act
            var ret = page.GetItemToDisplayPool(item);

            // Reset

            // Assert
            Assert.AreEqual(typeof(StackLayout), ret.GetType());
        }

        [Test]
        public void PickItemsPage_GetItemToDisplay_Should_Pass()
        {
            // Arrange
            var item = new ItemModel();

            // Act
            var ret = page.GetItemToDisplayPool(item);

            // Reset

            // Assert
            Assert.AreEqual(typeof(StackLayout), ret.GetType());
        }

        [Test]
        public void PickItemsPage_Item_ShowPopup_Default_Should_Pass()
        {
            // Arrange
            var item = page.GetItemToDisplayPool(new ItemModel());

            // Act
            var itemButton = item.Children.FirstOrDefault(m => m.GetType().Name.Equals("Button"));
            var dataImage = item.Children[0];
            ((ImageButton)dataImage).PropagateUpClicked();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_Item_ShowPopup_Defaults_Should_Pass()
        {
            // Arrange

            var item = page.GetItemToDisplay(ItemLocationEnum.Head);

            // Act
            var itemButton = item.Children.FirstOrDefault(m => m.GetType().Name.Equals("Button"));

            _ = page.ShowPopup(ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_Item_ShowPopup_Location_Match_Should_Pass()
        {
            // Arrange

            var item = page.GetItemToDisplay(ItemLocationEnum.Head);
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Add(new ItemModel() { Location = ItemLocationEnum.Head});
            // Act
            var itemButton = item.Children.FirstOrDefault(m => m.GetType().Name.Equals("Button"));

            _ = page.ShowPopup(ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_GetItemToDisplay_Click_Button_Valid_Should_Pass()
        {
            // Arrange
            var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
            page.ViewModel.Data.Head = item.Id;
            var StackItem = page.GetItemToDisplay(ItemLocationEnum.PrimaryHand);
            var dataImage = StackItem.Children[0];

            // Act
            ((ImageButton)dataImage).PropagateUpClicked();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_GetItemToDisplay_Click_Buttons_Valid_Should_Pass()
        {
            // Arrange
            var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
            var StackItem = page.GetItemToDisplayPool(item);
            var dataImage = StackItem.Children[0];

            // Act
            ((ImageButton)dataImage).PropagateUpClicked();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_DrawDroppedItems_Valid_Should_Pass()
        {
            // Arrange
            var flexview = page.FindByName("ItemListFoundFrame") as FlexLayout;
            flexview.Children.Add(new StackLayout());
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Add(new ItemModel());

            // Act
            page.DrawDroppedItems();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_ClosePoolPopup_Clicked_Valid_Should_Pass()
        {
            // Arrange

            // Act
            page.ClosePoolPopup_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


        [Test]
        public void PickItemsPage_OnPartyCharacterSelected_Clicked_Valid_Should_Pass()
        {
            // Arrange
            var data = new ItemModel();

            var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(data, 0);
            // Act
            page.OnPartyCharacterSelected(null, selectedCharacterChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_AddItemsToDisplay_Should_Pass()
        {
            // Arrange

            // Act
            page.AddItemsToDisplay();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void PickItemsPage_OnPopupItemSelected_Should_Pass()
        {
            // Arrange
            var data = new ItemModel();

            var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(data, 0);
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList = new List<ItemModel>();
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Add(data);

            // Act
            page.OnPopupItemSelected(null, selectedCharacterChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
        [Test]
        public void PickItemsPage_OnPopupItemSelected_Different_location_Should_Pass()
        {
            // Arrange
            var data = new ItemModel();
            var datas = new ItemModel();

            var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(data, 0);
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList = new List<ItemModel>();
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Add(datas);

            // Act
            page.OnPopupItemSelected(null, selectedCharacterChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


    }
}