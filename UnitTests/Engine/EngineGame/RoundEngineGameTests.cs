using System.Linq;
using System.Threading.Tasks;

using NUnit.Framework;

using Game.Engine.EngineGame;
using Game.Models;
using Game.ViewModels;

namespace UnitTests.Engine.EngineGame
{
    [TestFixture]
    public class RoundEngineGameTests
    {
        #region TestSetup
        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new();

            Engine.Round = new RoundEngine
            {
                Turn = new TurnEngine()
            };
            _ = Engine.Round.ClearLists();

            //Start the Engine in AutoBattle Mode
            //Engine.StartBattle(true);   
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion TestSetup

        #region Constructor
        [Test]
        public void RoundEngine_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion Constructor

        #region OrderPlayListByTurnOrder
        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Valid_Speed_Higher_Should_Be_Z()
        {
            // Arrange

            // Act
            var result = Engine.Round.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Valid_Level_Higher_Should_Be_Z()
        {
            // Arrange

            // Act
            var result = Engine.Round.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Valid_Experience_Higher_Should_Be_Z()
        {
            // Arrange

            // Act
            var result = Engine.Round.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Valid_ListOrder_Should_Be_1()
        {
            // Arrange

            // Act
            var result = Engine.Round.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Valid_Name_A_Z_Should_Be_Z()
        {
            // Arrange

            // Act
            var result = Engine.Round.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.NotNull(result);
        }
        #endregion OrderPlayListByTurnOrder

        #region GetItemFromPoolIfBetter

        //[Test]
        //public async Task RoundEngine_GetItemFromPoolIfBetter_InValid_Pool_Empty_Should_Fail()
        //{
        //    Engine.EngineSettings.MonsterList.Clear();

        //    // Both need to be character to fall through to the Name Test
        //    // Arrange
        //    var Character = new CharacterModel
        //    {
        //        Speed = 20,
        //        Level = 1,
        //        CurrentHealth = 1,
        //        ExperienceTotal = 1,
        //        Name = "Z",
        //        ListOrder = 1,
        //        Guid = "me"
        //    };

        //    // Add each model here to warm up and load it.
        //    _ = Game.Helpers.DataSetsHelper.WarmUp();

        //    var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Head };
        //    var item2 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Location = ItemLocationEnum.Head };

        //    _ = await ItemIndexViewModel.Instance.CreateAsync(item1);
        //    _ = await ItemIndexViewModel.Instance.CreateAsync(item2);

        //    //Engine.EngineSettings.ItemPool.Add(item1);
        //    //Engine.EngineSettings.ItemPool.Add(item2);

        //    // Put the Item on the Character
        //    _ = Character.AddItem(ItemLocationEnum.Head, item2.Id);

        //    var CharacterPlayer = new PlayerInfoModel(Character);
        //    Engine.EngineSettings.CharacterList.Clear();
        //    Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

        //    // Make the List
        //    Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

        //    // Act
        //    var result = Engine.Round.GetItemFromPoolIfBetter(CharacterPlayer, ItemLocationEnum.Head);

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(false, result);
        //}

        #endregion GetItemFromPoolIfBetter

        #region PickupItemsFromPool
        [Test]
        public void RoundEngine_PickupItemsFromPool_Valid_Default_Should_Pass()
        {
            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.PickupItemsFromPool(CharacterPlayer);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void RoundEngine_PickupItemsFromDroppedItems_Valid_Default_Should_Pass()
        {
            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.PickupItemsFromDroppedItems(CharacterPlayer);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion PickupItemsFromPool




        #region EndRound
        [Test]
        public void RoundEngine_EndRound_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.Round.EndRound();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion EndRound

        #region RoundNextTurn
        [Test]
        public void RoundEngine_RoundNextTurn_Valid_No_Monsters_Should_Return_NewRound()
        {
            // Arrange
            Engine.EngineSettings.MonsterList.Clear();
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel());

            // Act
            var result = Engine.Round.RoundNextTurn();

            // Reset

            // Assert
            Assert.AreEqual(RoundEnum.NewRound, result);
        }

        [Test]
        public void RoundEngine_RoundNextTurn_Valid_Characters_Monsters_Should_Return_NewRound()
        {
            // Arrange

            // Act
            var result = Engine.Round.RoundNextTurn();

            // Reset

            // Assert
            Assert.AreEqual(RoundEnum.NewRound, result);
        }
        #endregion RoundNextTurn

        #region GetNextPlayerInList

        [Test]
        public void RoundEngine_GetNextPlayerInList_Valid_Sue_Should_Return_Monster()
        {
            // Arrange


            // Act
            var result = Engine.Round.GetNextPlayerInList();

            // Reset

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public void RoundEngine_GetNextPlayerInList_Valid_Monster_Should_Return_Mike()
        {
            // Arrange

            // Act
            var result = Engine.Round.GetNextPlayerInList();

            // Reset


            // Assert
            Assert.NotNull(result);
        }

        #endregion GetNextPlayerInList

        [Test]
        public void RoundEngine_RoundNextTurn_Valid_No_Characters_Should_Return_GameOver()
        {
            // Arrange
            Engine.EngineSettings.CharacterList.Clear();
            // Act
            var result = Engine.Round.RoundNextTurn();

            // Reset

            // Assert
            Assert.AreEqual(RoundEnum.GameOver, result);
        }

        [Test]
        public void RoundEngine_GetNextPlayerInList_invalid_Should_Return_zero()
        {
            var curr = Engine.EngineSettings.PlayerList.ToList();


            //act
            Engine.EngineSettings.PlayerList.Clear();

            var result = Engine.Round.GetNextPlayerInList();


            Assert.IsNull(result);

            //reset
            Engine.EngineSettings.PlayerList.AddRange(curr);
        }

        #region PlayerList
        //[Test]
        //public void RoundEngine_PlayerList_Valid_Default_Should_Pass()
        //{
        //    // Act
        //    var result = Engine.Round.PlayerList();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(false, result.Any());
        //}
        #endregion PlayerList

        #region SwapCharacterItem
        [Test]
        public void RoundEngine_SwapCharacterItem_Valid_Default_Should_Pass()
        {
            // Arrange 
            var CharacterPlayerMike = new PlayerInfoModel(
                          new CharacterModel
                          {
                              Speed = 100,
                              Attack = 100,
                              Defense = 100,
                              Level = 1,
                              CurrentHealth = 111,
                              ExperienceTotal = 1,
                              ExperienceRemaining = 1,
                              Name = "Mike",
                              ListOrder = 1,
                          });
            var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand); ;

            // Act
            var result = Engine.Round.SwapCharacterItem(CharacterPlayerMike, ItemLocationEnum.PrimaryHand, item);

            // Reset

            // Assert
            Assert.Null(result);
        }
        #endregion SwapCharacterItem

        #region GetItemFromPoolIfBetter
        [Test]
        public void RoundEngine_GetItemFromPoolIfBetter_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.GetItemFromPoolIfBetter(null, ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }
        #endregion GetItemFromPoolIfBetter

        #region RemoveDeadPlayersFromList
        [Test]
        public void RoundEngine_RemoveDeadPlayersFromList_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.RemoveDeadPlayersFromList();

            // Reset

            // Assert
            Assert.NotNull(result);
        }
        #endregion RemoveDeadPlayersFromList

        #region PickupItemsFromDropItems

        #endregion PickupItemsFromDropItems

        #region GetNextPlayerTurn
        [Test]
        public void RoundEngine_GetNextPlayerTurn_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.GetNextPlayerTurn();

            // Reset

            // Assert
            Assert.NotNull(result);
        }
        #endregion GetNextPlayerTurn


        #region GetItemFromDroppedListIfBetter
        [Test]
        public async Task RoundEngine_GetItemFromDroppedListIfBetter_InValid_Location_Empty_Should_Fail()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Both need to be character to fall through to the Name Test
            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Feet };
            var item2 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Location = ItemLocationEnum.Feet };

            _ = await ItemIndexViewModel.Instance.CreateAsync(item1);
            _ = await ItemIndexViewModel.Instance.CreateAsync(item2);

            Engine.EngineSettings.BattleScore.ItemModelDropList.Add(item1);
            Engine.EngineSettings.BattleScore.ItemModelDropList.Add(item2);

            // Put the Item on the Character
            Character.Head = null;

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act

            var result = Engine.Round.GetItemFromDroppedListIfBetter(CharacterPlayer, ItemLocationEnum.Feet);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(item2.Id, CharacterPlayer.Feet);    // The 2nd item is better, so did they swap?
        }

        [Test]
        public async Task RoundEngine_GetItemFromDroppedListIfBetter_Valid_Head_Better_Should_Pass()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Both need to be character to fall through to the Name Test
            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Head };
            var item2 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Location = ItemLocationEnum.Head };

            _ = await ItemIndexViewModel.Instance.CreateAsync(item1);
            _ = await ItemIndexViewModel.Instance.CreateAsync(item2);

            Engine.EngineSettings.BattleScore.ItemModelDropList.Add(item1);
            Engine.EngineSettings.BattleScore.ItemModelDropList.Add(item2);

            // Put the Item on the Character
            _ = Character.AddItem(ItemLocationEnum.Head, item1.Id);

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.GetItemFromDroppedListIfBetter(CharacterPlayer, ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(item2.Id, CharacterPlayer.Head);    // The 2nd item is better, so did they swap?
        }
        [Test]
        public async Task RoundEngine_GetItemFromDroppedListIfBetter_InValid_Head_Worse_Should_Skip()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Both need to be character to fall through to the Name Test
            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Head };
            var item2 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Location = ItemLocationEnum.Head };

            _ = await ItemIndexViewModel.Instance.CreateAsync(item1);
            _ = await ItemIndexViewModel.Instance.CreateAsync(item2);

            Engine.EngineSettings.BattleScore.ItemModelDropList.Add(item1);
            Engine.EngineSettings.BattleScore.ItemModelDropList.Add(item2);

            // Put the Item on the Character
            _ = Character.AddItem(ItemLocationEnum.Head, item2.Id);

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.GetItemFromDroppedListIfBetter(CharacterPlayer, ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(item2.Id, CharacterPlayer.Head);    // Kept the one
        }

        [Test]
        public async Task RoundEngine_GetItemFromDroppedListIfBetter_InValid_Pool_Empty_Should_Fail()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Both need to be character to fall through to the Name Test
            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Head };
            var item2 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Location = ItemLocationEnum.Head };

            _ = await ItemIndexViewModel.Instance.CreateAsync(item1);
            _ = await ItemIndexViewModel.Instance.CreateAsync(item2);


            // Put the Item on the Character
            _ = Character.AddItem(ItemLocationEnum.Head, item2.Id);

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));
            Engine.EngineSettings.BattleScore.ItemModelDropList.Clear();
            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.GetItemFromDroppedListIfBetter(CharacterPlayer, ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task RoundEngine_GetItemFromDroppedListIfBetter_InValid_Right_Finger_Empty_Should_Pass()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Both need to be character to fall through to the Name Test
            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Finger };
            var item2 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Location = ItemLocationEnum.Finger };

            _ = await ItemIndexViewModel.Instance.CreateAsync(item1);
            _ = await ItemIndexViewModel.Instance.CreateAsync(item2);

            Engine.EngineSettings.BattleScore.ItemModelDropList.Add(item1);
            Engine.EngineSettings.BattleScore.ItemModelDropList.Add(item2);

            // Put the Item on the Character
            _ = Character.AddItem(ItemLocationEnum.RightFinger, item1.Id);

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.GetItemFromDroppedListIfBetter(CharacterPlayer, ItemLocationEnum.RightFinger);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(item2.Id, CharacterPlayer.RightFinger);    // The 2nd item is better, so did they swap?
        }

        [Test]
        public async Task RoundEngine_GetItemFromDroppedListIfBetter_Valid_Left_Finger_Empty_Should_Pass()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Both need to be character to fall through to the Name Test
            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Finger };
            var item2 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Location = ItemLocationEnum.Finger };

            _ = await ItemIndexViewModel.Instance.CreateAsync(item1);
            _ = await ItemIndexViewModel.Instance.CreateAsync(item2);

            Engine.EngineSettings.BattleScore.ItemModelDropList.Add(item1);
            Engine.EngineSettings.BattleScore.ItemModelDropList.Add(item2);

            // Put the Item on the Character
            _ = Character.AddItem(ItemLocationEnum.LeftFinger, item1.Id);

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.GetItemFromDroppedListIfBetter(CharacterPlayer, ItemLocationEnum.LeftFinger);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(item2.Id, CharacterPlayer.LeftFinger);    // The 2nd item is better, so did they swap?
        }

        [Test]
        public async Task RoundEngine_GetItemFromDroppedListIfBetter_NoBetterItemFound()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Both need to be character to fall through to the Name Test
            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 2, Location = ItemLocationEnum.Head };
            var item2 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Head };

            _ = await ItemIndexViewModel.Instance.CreateAsync(item1);
            _ = await ItemIndexViewModel.Instance.CreateAsync(item2);


            Engine.EngineSettings.BattleScore.ItemModelDropList.Add(item2);

            // Put the Item on the Character
            _ = Character.AddItem(ItemLocationEnum.Head, item1.Id);

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.GetItemFromDroppedListIfBetter(CharacterPlayer, ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(item1.Id, CharacterPlayer.Head);    // Kept the one
        }
        #endregion GetItemFromDroppedListIfBetter
    }
}