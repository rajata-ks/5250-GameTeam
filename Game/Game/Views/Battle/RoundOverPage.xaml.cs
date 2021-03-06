using Game.Helpers;
using Game.Models;
using Game.Services;
using Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundOverPage : ContentPage
    {
        private int value;

      

        /// <summary>
        /// Constructor
        /// </summary>
        public RoundOverPage()
        {
            InitializeComponent();

            // Update the Found Number
            TotalFound.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Count().ToString();

            // Update the Selected Number, this gets updated later when selected refresh happens
            TotalSelected.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList.Count().ToString();

            DrawCharacterList();

            DrawItemLists();

            DrawMonsterList();

            BindingContext = BattleEngineViewModel.Instance.Engine.EngineSettings;

            var characterImage = BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.FirstOrDefault();
            if (characterImage != null)
            {
                CharacterImage.Source = characterImage.ImageURI;
            }
        }


        /// <summary>
        /// Clear and Add the Characters that survived
        /// </summary>
        public void DrawMonsterList()
        {
            // Clear and Populate the Characters Remaining
           
            foreach (var data in CharacterBox.Children.ToList())
            {
                _ = MonsterBox.Children.Remove(data);
            }

            // Draw the Characters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.MonsterModelDeathList)
            {
                MonsterBox.Children.Add(CreatePlayerDisplayBox(data));
            }
        }

        /// <summary>
        /// Using Amazon Same Battle Delivery, characters get just what they need, when they need it. 
       /// When ASBD is clicked, the dropped items during battle will fit perfectly the open slots of the
        ///character.So, if the character needs shoes, the drop will have shoes in it.This ensures that all
        ///characters quickly have all slots full. 
        ///
        /// Get Items using the HTTP Post command
        /// </summary>
        /// <returns></returns>
        public void AmazonInstantDelivery_Clicked(object sender, EventArgs e)
        {
            var EmptyItemLocation = new SortedDictionary<String, int>();

            foreach (var character in BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList)
            {
                foreach (var name in ItemLocationEnumHelper.GetListItemMessage)
                {
                    if (name.Equals("Any Finger"))
                    {
                        continue;
                    }
                    if (character.GetItemByLocation(ItemLocationEnumHelper.ConvertMessageToEnum(name)) == null)
                    {
                        if (EmptyItemLocation.ContainsKey(name))
                        {

                            int value = EmptyItemLocation[name];
                            value++;

                            EmptyItemLocation.Remove(name);
                            EmptyItemLocation.Add(name, value);
                        }
                        else

                        {
                            EmptyItemLocation.Add(name, 1);
                        }


                    }
                }

            }
            List<ItemModel> dataList = new List<ItemModel>();
            foreach (KeyValuePair<String, int> entry in EmptyItemLocation)
            {
                var number = entry.Value;
                var level = BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Min(m => m.Level); // The Min level of character
                var attribute = AttributeEnum.Unknown;  // Any Attribute
                var location = ItemLocationEnumHelper.ConvertMessageToEnum(entry.Key);    // Any Location
                var random = true;  // Random between 1 and Level
                var updateDataBase = true;  // Add them to the DB

                var category = 6;
                Task<List<ItemModel>> task = Task.Run<List<ItemModel>>(async () => await fetchItemModel(number, level, attribute, location, category, random, updateDataBase));
                dataList.AddRange(task.Result);

            }
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Clear();
            if (dataList.Any())
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.AddRange(dataList);
            }
            else
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.AddRange(getUpSkilledItemList(BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList));
            }

            DrawItemLists();
        }

        ///After all slots are full, then the drop will be of a higher
        ///value than something the character has in any of the slots, thus ensuring a better item.

        public List<ItemModel> getUpSkilledItemList(List<PlayerInfoModel> playerList)
        {
            List<ItemModel> result = new List<ItemModel>();
            foreach (var character in playerList)
            {
                foreach (var name in ItemLocationEnumHelper.GetListItemMessage)
                {
                    if (name.Equals("Any Finger"))
                    {
                        continue;
                    }
                    ItemModel itemModel = character.GetItemByLocation(ItemLocationEnumHelper.ConvertMessageToEnum(name));

                    if (itemModel != null && itemModel.Value <= 20)
                    {
                        itemModel.Value = itemModel.Value + 1;
                        result.Add(itemModel);
                    }
                    
                }

            }
            return result;
        }
        public async Task<List<ItemModel>> fetchItemModel(int number, int level, AttributeEnum attribute, ItemLocationEnum location, int category, bool random, bool updateDataBase)
        {
            List<ItemModel> result = new List<ItemModel>();
            return await ItemService.GetItemsFromServerPostAsync(number, level, attribute, location, category, random, updateDataBase);

        }
        
        /// <summary>
        /// Clear and Add the Characters that survived
        /// </summary>
        public void DrawCharacterList()
        {
            // Clear and Populate the Characters Remaining
            var FlexList = CharacterBox.Children.ToList();
            foreach (var data in FlexList)
            {
                _ = CharacterBox.Children.Remove(data);
            }

            // Draw the Characters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList)
            {
                CharacterBox.Children.Add(CreatePlayerDisplayBox(data));
            }
        }

        /// <summary>
        /// Draw the List of Items
        /// 
        /// The Ones Dropped
        /// 
        /// The Ones Selected
        /// 
        /// </summary>
        public void DrawItemLists()
        {
            DrawDroppedItems();
            DrawSelectedItems();

            // Only need to update the selected, the Dropped is set in the constructor
            TotalSelected.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList.Count().ToString();
        }

        /// <summary>
        /// Add the Dropped Items to the Display
        /// </summary>
        public void DrawDroppedItems()
        {
            // Clear and Populate the Dropped Items
            var FlexList = ItemListFoundFrame.Children.ToList();
            foreach (var data in FlexList)
            {
                _ = ItemListFoundFrame.Children.Remove(data);
            }

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Distinct())
            {
                ItemListFoundFrame.Children.Add(GetItemToDisplay(data));
            }
        }

        /// <summary>
        /// Add the Dropped Items to the Display
        /// </summary>
        public void DrawSelectedItems()
        {
            // Clear and Populate the Dropped Items
            var FlexList = ItemListSelectedFrame.Children.ToList();
            foreach (var data in FlexList)
            {
                _ = ItemListSelectedFrame.Children.Remove(data);
            }

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelSelectList)
            {
                ItemListSelectedFrame.Children.Add(GetItemToDisplay(data));
            }
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay(ItemModel item)
        {
            if (item == null)
            {
                return new StackLayout();
            }

            if (string.IsNullOrEmpty(item.Id))
            {
                return new StackLayout();
            }

            // Defualt Image is the Plus
            var ClickableButton = true;

            var data = ItemIndexViewModel.Instance.GetItem(item.Id);
            if (data == null)
            {
                // Show the Default Icon for the Location
                data = new ItemModel { Name = "Unknown", ImageURI = "icon_cancel.png" };

                // Turn off click action
                ClickableButton = false;
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"],
                Source = data.ImageURI
            };

            if (ClickableButton)
            {
                // Add a event to the user can click the item and see more
                ItemButton.Clicked += (sender, args) => ShowPopup(data);
            }

            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                Padding = 3,
                Style = (Style)Application.Current.Resources["ItemImageBox"],
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    ItemButton,
                },
            };

            return ItemStack;
        }

        /// <summary>
        /// Return a stack layout with the Player information inside
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CreatePlayerDisplayBox(PlayerInfoModel data)
        {
            if (data == null)
            {
                data = new PlayerInfoModel();
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["ImageBattleLargeStyle"],
                Source = data.ImageURI
            };

            // Add the Level
            var PlayerLevelLabel = new Label
            {
                Text = "Level : " + data.Level,
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Add the HP
            var PlayerHPLabel = new Label
            {
                Text = "HP : " + data.GetCurrentHealthTotal,
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            var PlayerNameLabel = new Label()
            {
                Text = data.Name,
                Style = (Style)Application.Current.Resources["ValueStyle"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["PlayerInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children = {
                    PlayerImage,
                    //PlayerNameLabel,
                    
                },
            };

            return PlayerStack;
        }

        /// <summary>
        /// Show the Popup for the Item
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ShowPopup(ItemModel data)
        {
            PopupLoadingView.IsVisible = true;
            PopupItemImage.Source = data.ImageURI;

            PopupItemName.Text = data.Name;
            PopupItemDescription.Text = data.Description;
            PopupItemLocation.Text = data.Location.ToMessage();
            PopupItemAttribute.Text = data.Attribute.ToMessage();
            PopupItemValue.Text = " + " + data.Value.ToString();
            return true;
        }

        /// <summary>
        /// Close the popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopup_Clicked(object sender, EventArgs e)
        {
            PopupLoadingView.IsVisible = false;
        }

        /// <summary>
        /// Closes the Round Over Popup
        /// 
        /// Launches the Next Round Popup
        /// 
        /// Resets the Game Round
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CloseButton_Clicked(object sender, EventArgs e)
        {
            // Reset to a new Round
            _ = BattleEngineViewModel.Instance.Engine.Round.NewRound();

            // Show the New Round Screen
            ShowModalNewRoundPage();
        }

        /// <summary>
        /// Go to the assign item page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       public async void AssignItem_Clicked(object sender, EventArgs e)
       {
            await Navigation.PushModalAsync(new PickItemsPage());
        }


        public void AssignItems(object sender, EventArgs e)
        {
            _ = BattleEngineViewModel.Instance.Engine.Round.PickupDroppedItemsForAllCharacters();
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Clear();
            DrawItemLists();
        }

        /// <summary>
        /// Start next Round, returning to the battle screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AutoAssignButton_Clicked(object sender, EventArgs e)
        {
            // Distribute the Items
            _ = BattleEngineViewModel.Instance.Engine.Round.PickupItemsForAllCharacters();

            // Show what was picked up
            DrawItemLists();
        }

        /// <summary>
        /// Show the Page for New Round
        /// 
        /// Upcomming Monsters
        /// 
        /// </summary>
        public async void ShowModalNewRoundPage()
        {
            _ = await Navigation.PopModalAsync();
        }

    }
}