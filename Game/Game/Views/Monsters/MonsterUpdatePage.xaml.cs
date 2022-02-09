using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;
using System.Collections.Generic;
using System.Linq;

namespace Game.Views
{
    /// <summary>
    /// Monster Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterUpdatePage : ContentPage
    {
        // The Monster to create
        public GenericViewModel<MonsterModel> ViewModel { get; set; }

        // Hold the current location selected
        public ItemLocationEnum PopupLocationEnum = ItemLocationEnum.Unknown;

        //Silder increment size
        private readonly int SliderStepSize = 1;

        //Bools used to validate name.
        private bool nameValid;

        //Bool used to validate description.
        private bool descriptionValid;

        //Bool used to validate image.
        private bool imageValid;

        //Int used to keep track of curent index.
        private int difficultyIndex;

        //copy of pre edited data
        private MonsterModel MonsterModelCopy;

        private DifficultyEnum currDifficulty;
        private int locationIndex;

        // Empty Constructor for UTs
        public MonsterUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            //Defaults bools
            nameValid = true;
            descriptionValid = true;
            imageValid = true;

            MonsterModelCopy = new MonsterModel(data.Data);

            //Find what the DifficultyPicker.SelectedIndex should be.
            int currIndex = 0;
            difficultyIndex = 0;
            //Add Difficuly to list
            foreach (string i in Enum.GetNames(typeof(DifficultyEnum)))
            {
                DifficultyPicker.Items.Add(i);
                if(i == this.ViewModel.Data.Difficulty.ToString())
                {
                    difficultyIndex = currIndex;
                }
                currIndex++;
            }

            //Find what the LocationPicker.SelectedIndex should be.
            int currLocationIndex = 0;
            locationIndex = 0;

            //Add item Locations to list
            foreach (string i in Enum.GetNames(typeof(ItemLocationEnum)))
            {
                LocationPicker.Items.Add(i);
                if (i == this.ViewModel.Data.Location.ToString())
                {
                    locationIndex = currLocationIndex;
                }
                currLocationIndex++;
            }


            AddItemsToDisplay();

            _ = UpdatePageBindingContext();
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            // Temp store off the Difficulty
            var difficulty = this.ViewModel.Data.Difficulty;

            // Clear the Binding and reset it
            BindingContext = null;
            BindingContext = this.ViewModel;
            ViewModel.Data.Difficulty = currDifficulty = difficulty;
            DifficultyPicker.SelectedIndex = difficultyIndex;
            return true;
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            //Check to make sure all boxes are filled.
            if (nameValid == false)
            {
                return;
            }

            if (descriptionValid == false)
            {
                return;
            }

            if (imageValid == false)
            {
                return;
            }

            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = new MonsterModel().ImageURI;
            }

            MessagingCenter.Send(this, "Create", ViewModel.Data);

            //Save the difficulty. 
            ViewModel.Data.Difficulty = currDifficulty;

            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {

            //revert changes to original
            ViewModel.Data.Update(MonsterModelCopy);

            _ = await Navigation.PopModalAsync();
        }
        /// <summary>
        /// Changes the diffculty of the Monster.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Difficulty_Changed(object sender, EventArgs e)
        {
            //Change the Difficulty
            currDifficulty = (DifficultyEnum)Enum.Parse(typeof(DifficultyEnum),
                                        DifficultyPicker.Items[DifficultyPicker.SelectedIndex]);
        }

        /// <summary>
        /// Catch the change to the Stepper for Attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Attack_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            var StepValue = SliderStepSize;

            //rounding the value based on increments
            var newStep = RoundSilderValueToWhole(e.NewValue, attackSilder);

            AttackValue.Text = string.Format("{0}", newStep);
        }

        /// <summary>
        /// Catch the change to the Stepper for Defense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Defense_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            //rounding the value based on increments
            var newStep = RoundSilderValueToWhole(e.NewValue, defenseSilder);

            DefenseValue.Text = string.Format("{0}", newStep);
        }

        /// <summary>
        /// Catch the change to the Stepper for Speed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Speed_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            //rounding the value based on increments
            var newStep = RoundSilderValueToWhole(e.NewValue, speedSilder);

            SpeedValue.Text = string.Format("{0}", newStep);
        }


        /// <summary>
        /// Validate the entry for name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Name_TextChanged(object sender, ValueChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameEntry.Text))
            {
                NameLabel.TextColor = Color.Red;
                NameLabel.Text = "Name*";
                nameValid = false;

                return;
            }

            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                NameLabel.TextColor = Color.Red;
                NameLabel.Text = "Name*";
                nameValid = false;

                return;
            }

            NameLabel.TextColor = Color.Black;
            NameLabel.Text = "Name*";
            nameValid = true;
        }

        /// <summary>
        /// Validate the entry for description.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Description_TextChanged(object sender, ValueChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(DescriptionEntry.Text))
            {
                DescriptionLabel.TextColor = Color.Red;
                DescriptionLabel.Text = "Description";
                descriptionValid = false;

                return;
            }

            if (string.IsNullOrWhiteSpace(DescriptionEntry.Text))
            {
                DescriptionLabel.TextColor = Color.Red;
                DescriptionLabel.Text = "Description*";
                descriptionValid = false;

                return;
            }

            DescriptionLabel.TextColor = Color.Black;
            DescriptionLabel.Text = "Description";
            descriptionValid = true;
        }

        /// <summary>
        /// Validate the entry for image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Image_TextChanged(object sender, ValueChangedEventArgs e)
        {

            if (string.IsNullOrEmpty(ImageEntry.Text))
            {
                ImageLabel.TextColor = Color.Red;
                imageValid = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(ImageEntry.Text))
            {
                ImageLabel.TextColor = Color.Red;
                imageValid = false;
                return;
            }

            if (!ImageEntry.Text.EndsWith(".png"))
            {
                ImageLabel.TextColor = Color.Red;
                imageValid = false;
                return;
            }

            ImageLabel.TextColor = Color.Black;
            imageValid = true;

        }

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

            AddItemsToDisplay();

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
            itemList.AddRange(ItemIndexViewModel.Instance.GetLocationItems(location));

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
                Text = "Item",
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

        private double RoundSilderValueToWhole(double val, Slider slide)
        {
            if (slide == null)
            {
                return 0;
            }

            //rounding the value based on increments
            var newStep = Math.Round(val / SliderStepSize);

            return slide.Value = newStep * SliderStepSize;
        }
    }
}