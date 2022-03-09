using Game.Models;
using Game.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickItemsPage : ContentPage
    {
        //Parallel list used to get characters from list view.
        public List<PlayerInfoModel> characterList;

        // Hold the current location selected
        public ItemLocationEnum PopupLocationEnum = ItemLocationEnum.Unknown;
        // The Character to create
        public GenericViewModel<PlayerInfoModel> ViewModel { get; set; }

        //Keep track of current character index
        public int currentCharacterIndex;
        /// <summary>
        /// Constructor
        /// </summary>
        public PickItemsPage()
        {
            InitializeComponent();

            characterList = new List<PlayerInfoModel>();
            ViewModel = new GenericViewModel<PlayerInfoModel>();
            currentCharacterIndex = 0;
            DrawCharacterList();
            AddItemsToDisplay();
            DrawDroppedItems();
        }


        /// <summary>
        /// Populate the list with all the characters.
        /// </summary>
        public void DrawCharacterList()
        {

            foreach (PlayerInfoModel character in BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList)
            {
                characterList.Add(character);
            }
            PartyListView.ItemsSource = characterList;

            //Display first character at the start
            PlayerInfoModel data = characterList[0];
            CharacterImage.Source = data.ImageURI;
        }

        public void OnPartyCharacterSelected(object s, SelectedItemChangedEventArgs e)
        {
            Test.Text = e.SelectedItemIndex.ToString();
            PlayerInfoModel data = characterList[e.SelectedItemIndex];
            currentCharacterIndex = e.SelectedItemIndex;
            CharacterImage.Source = data.ImageURI;

            AddItemsToDisplay();

        }

        /// <summary>
        /// Quit the Battle
        /// 
        /// Quitting out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CloseButton_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }

        #region CharaterItems
        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
        public void OnPopupItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ItemModel data = args.SelectedItem as ItemModel;
            if (data == null)
            {
                return;
            }

            _ = ViewModel.Data.AddItem(PopupLocationEnum, data.Id);

            //Find item and remove it from the drop pool
            foreach (var item in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList)
            {
                if (data.Id == item.Id)
                {
                    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Remove(item);
                    break;
                }
            }
            AddItemsToDisplay();

            //Update Item pool
            DrawDroppedItems();
            ClosePopup();
        }

        /// <summary>
        /// Show the Popup for Selecting Items
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool ShowPopup(ItemLocationEnum location)
        {
            PopupItemSelector.IsVisible = true;

            PopupLocationLabel.Text = "Items for :";
            PopupLocationValue.Text = location.ToMessage();

            // Make a fake item for None
            var NoneItem = new ItemModel
            {
                Id = null, // will use null to clear the item
                Guid = "None", // how to find this item amoung all of them
                ImageURI = "icon_cancel.png",
                Name = "None",
                Description = "None"
            };

            List<ItemModel> itemList = new List<ItemModel>
            {
                NoneItem
            };

            // Add the rest of the items to the list

            //Add character item
            var data = ViewModel.Data.GetItemByLocation(location);
            if (data != null)
            {
                itemList.Add(data);
            }

            //Add items from pool
            foreach (var item in BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Distinct())
            {
                if (item.Location == location)
                {
                    itemList.Add(item);
                }
            }


            // Populate the list with the items
            PopupLocationItemListView.ItemsSource = itemList;

            // Remember the location for this popup
            PopupLocationEnum = location;

            return true;
        }

        /// <summary>
        /// When the user clicks the close in the Popup
        /// hide the view
        /// show the scroll view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopup_Clicked(object sender, EventArgs e)
        {
            ClosePopup();
        }

        /// <summary>
        /// Close the popup
        /// </summary>
        public void ClosePopup()
        {
            PopupItemSelector.IsVisible = false;
        }

        /// <summary>
        /// Show the Items the Character has
        /// </summary>
        public void AddItemsToDisplay()
        {
            var FlexList = ItemBox.Children.ToList();
            foreach (var data in FlexList)
            {
                _ = ItemBox.Children.Remove(data);
            }

            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Head));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Necklass));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.PrimaryHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.OffHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.RightFinger));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.LeftFinger));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Feet));
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay(ItemLocationEnum location)
        {
            // Get the Item, if it exist show the info
            // If it does not exist, show a Plus Icon for the location

            // Defualt Image is the Plus
            var ImageSource = "icon_add.png";
            ViewModel.Data = characterList[currentCharacterIndex];
            var data = ViewModel.Data.GetItemByLocation(location);
            if (data == null)
            {
                data = new ItemModel { Location = location, ImageURI = ImageSource };
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"],
                Source = data.ImageURI
            };

            // Add a event to the user can click the item and see more
            ItemButton.Clicked += (sender, args) => ShowPopup(location);

            // Add the Display Text for the item
            var ItemLabel = new Label
            {
                Text = location.ToMessage(),
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                Padding = 3,
                Style = (Style)Application.Current.Resources["ItemImageLabelBox"],
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    ItemButton,
                    ItemLabel
                },
            };

            return ItemStack;
        }

        #endregion CharacterItems

        #region ItemPool
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
                ItemListFoundFrame.Children.Add(GetItemToDisplayPool(data));
            }
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplayPool(ItemModel item)
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
                ItemButton.Clicked += (sender, args) => ShowPoolPopup(data);
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
        /// Show the Popup for the Item
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ShowPoolPopup(ItemModel data)
        {
            PopupPoolItem.IsVisible = true;
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
        public void ClosePoolPopup_Clicked(object sender, EventArgs e)
        {
            PopupPoolItem.IsVisible = false;
        }
        #endregion ItemPool
    }
}