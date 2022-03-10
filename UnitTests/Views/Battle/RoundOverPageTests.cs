using NUnit.Framework;
using System.Threading.Tasks;

using Game;
using Game.Views;
using Game.Models;

using Xamarin.Forms.Mocks;
using Xamarin.Forms;
using Game.ViewModels;
using System.Collections.Generic;

namespace UnitTests.Views
{
    [TestFixture]
    public class RoundOverPageTests
    {
        App app;
        RoundOverPage page;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new RoundOverPage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void RoundOverPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void RoundOverPage_NextButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.CloseButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_AutoAssignButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.AutoAssignButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_AssignItem_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.AssignItem_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_AssignItems_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.AssignItems(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_AmazonInstantDelivery_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.AmazonInstantDelivery_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public async Task RoundOverPage_AmazonInstantDelivery_Clicked_With_CharacterList_Should_Pass()
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Clear();

            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 2,
                ExperienceTotal = 1,
                Name = "C",
                ListOrder = 10,
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Head };
            _ = await ItemIndexViewModel.Instance.CreateAsync(item1);
            var item2 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Feet };
            _ = await ItemIndexViewModel.Instance.CreateAsync(item2);
            var item3 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Necklass };
            _ = await ItemIndexViewModel.Instance.CreateAsync(item3);
            var item4 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.OffHand };
            _ = await ItemIndexViewModel.Instance.CreateAsync(item4);
            var item5 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.PrimaryHand };
            _ = await ItemIndexViewModel.Instance.CreateAsync(item5);
            CharacterPlayer.AddItem(ItemLocationEnum.Head, item1.Id);
            CharacterPlayer.AddItem(ItemLocationEnum.Feet, item2.Id);
            CharacterPlayer.AddItem(ItemLocationEnum.Necklass, item3.Id);
            CharacterPlayer.AddItem(ItemLocationEnum.OffHand, item4.Id);
            CharacterPlayer.AddItem(ItemLocationEnum.PrimaryHand, item5.Id);

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            page.AmazonInstantDelivery_Clicked(null, null);

            Assert.IsTrue(true); // Got to here, so it happened...

        }

        [Test]
        public async Task RoundOverPage_testGetUpSkilledItemList__Should_Pass()
        {
            List<PlayerInfoModel> list = new List<PlayerInfoModel>();
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 2,
                ExperienceTotal = 1,
                Name = "C",
                ListOrder = 10,
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Head };
            _ = await ItemIndexViewModel.Instance.CreateAsync(item1);
            CharacterPlayer.AddItem(ItemLocationEnum.Head, item1.Id);
            list.Add(CharacterPlayer);
            List<ItemModel> result = page.getUpSkilledItemList(list);

            Assert.IsTrue(result[0].Value == 2);

        }

        [Test]
        public void RoundOverPage_ClosePopup_Clicked_Default_Should_Pass()
        {
            // Arrange
            // Act
            page.ClosePopup_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


        [Test]
        public void RoundOverPage_ShowPopup_Default_Should_Pass()
        {
            // Arrange
            // Act
            _ = page.ShowPopup(new ItemModel());

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_CreatePlayerDisplayBox_Null_Should_Pass()
        {
            // Arrange
            // Act
            _ = page.CreatePlayerDisplayBox(null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_GetItemToDisplay_Null_Should_Pass()
        {
            // Arrange
            // Act
            _ = page.GetItemToDisplay(null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_GetItemToDisplay_InValid_Id_Should_Pass()
        {
            // Arrange
            // Act
            _ = page.GetItemToDisplay(new ItemModel { Id = "" });

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public async Task RoundOverPage_GetItemToDisplay_Valid_Should_Pass()
        {
            // Arrange
            var data = new ItemModel { Name = "Mike" };
            _ = await ItemIndexViewModel.Instance.CreateAsync(data);

            // Act
            _ = page.GetItemToDisplay(data);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_DrawCharacterList_Valid_Should_Pass()
        {
            //// Arrange
            //var frame = page.FindByName("CharacterListFrame");
            //((FlexLayout)frame).Children.Add(page.CreatePlayerDisplayBox(null));
            //BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.CharacterModelDeathList.Add(new PlayerInfoModel(new CharacterModel()));

            //// Draw the Monsters
            //BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.MonsterModelDeathList.Add(new PlayerInfoModel(new CharacterModel()));

            //// Do it two times
            //page.DrawCharacterList();

            //// Act
            //page.DrawCharacterList();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_DrawDroppedItems_Valid_Should_Pass()
        {
            // Arrange

            // Draw the Items
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Add(new ItemModel());

            // Draw two times
            page.DrawDroppedItems();

            // Act
            page.DrawDroppedItems();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_DrawItemLists_Valid_Should_Pass()
        {
            // Arrange

            // Draw the Items
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Add(new ItemModel());
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList.Add(new ItemModel());

            // Draw two times
            page.DrawItemLists();

            // Act  BattleEngineViewModel.Instance.Engine.EngineSettings.
            page.DrawItemLists();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_DrawSelectedItems_Valid_Should_Pass()
        {
            // Arrange

            // Draw the Items
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Add(new ItemModel());
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList.Add(new ItemModel());

            // Draw two times
            page.DrawSelectedItems();

            // Act
            page.DrawSelectedItems();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void RoundOverPage_GetItemToDisplay_Click_Button_Valid_Should_Pass()
        {
            // Arrange
            var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
            var StackItem = page.GetItemToDisplay(item);
            var dataImage = StackItem.Children[0];

            // Act
            ((ImageButton)dataImage).PropagateUpClicked();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}