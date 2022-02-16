using Game.Models;
using Game.ViewModels;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// Create Item
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemCreatePage : ContentPage
    {
        // The item to create
        public GenericViewModel<ItemModel> ViewModel = new GenericViewModel<ItemModel>();

        // Empty Constructor for UTs
        public ItemCreatePage(bool UnitTest) { }

        //Silder increment size
        private readonly int SliderStepSize = 1;

        //Bools used to validate name.
        private bool nameValid;

        //Bool used to validate description.
        private bool descriptionValid;

        //Bool used to validate image.
        private bool imageValid;

        //Bool used to attribute image.
        private bool attributeValid;

        //Bool used to location image.
        private bool locationValid;

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public ItemCreatePage(ItemIndexViewModel ViewModel = null)
        {
            InitializeComponent();

            this.ViewModel.Data = new ItemModel();

            if (ViewModel != null)
            {
                //initialize the dataset
                this.ViewModel.Dataset = new ObservableCollection<DefaultModel>();
                //to populate the dataset so images can show up
                foreach (var data in ViewModel.Dataset)
                {
                    this.ViewModel.Dataset.Add(new DefaultModel { Name = data.Name, Description = data.Description, ImageURI = data.ImageURI });
                }
            }

            BindingContext = this.ViewModel;
           
            this.ViewModel.Title = "";

            //Defaults bools
            nameValid = false;
            descriptionValid = false;
            attributeValid = false;
            locationValid = false;

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = this.ViewModel.Data.Location.ToString();
            AttributePicker.SelectedItem = this.ViewModel.Data.Attribute.ToString();
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
            validateErrorLocationEnum(ViewModel.Data.Location);
            if (!locationValid)
            {
                return;
            }

            validateErrorAttributeEnum(ViewModel.Data.Attribute);

            if (!attributeValid)
            {
                return;
            }

            //get current carousel item image url to save
            var curImage = (DefaultModel)(carouselItem.CurrentItem);
            ViewModel.Data.ImageURI = curImage.ImageURI;

            MessagingCenter.Send(this, "Create", ViewModel.Data);
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Catch the change to the Stepper for Attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Damage_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            //rounding the value based on increments
            var newStep = RoundSilderValueToWhole(e.NewValue, damageSilder);

            DamageValue.Text = string.Format("{0}", newStep);
        }

        /// <summary>
        /// Catch the change to the Stepper for Defense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Range_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            //rounding the value based on increments
            var newStep = RoundSilderValueToWhole(e.NewValue, rangeSilder);

            RangeValue.Text = string.Format("{0}", newStep);
        }

        /// <summary>
        /// Catch the change to the Stepper for Speed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Value_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            //rounding the value based on increments
            var newStep = RoundSilderValueToWhole(e.NewValue, valueSilder);

            ValueValue.Text = string.Format("{0}", newStep);
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

            NameLabel.TextColor = Color.White;
            NameLabel.Text = "Name";
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
                DescriptionLabel.Text = "Description*";
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

            DescriptionLabel.TextColor = Color.White;
            DescriptionLabel.Text = "Description";
            descriptionValid = true;
        }

        /// <summary>
        /// Validate and change UI Element if invalid
        /// Can't save unknown
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool validateErrorAttributeEnum(AttributeEnum data)
        {
            if (data == AttributeEnum.Unknown)
            {
                AttributeLabel.TextColor = Color.Red;
                AttributeLabel.Text = "Attribute*";
                return attributeValid = false;
            }

            return attributeValid = true;
        }

        /// <summary>
        /// Validate and change UI Element if invalid
        /// Can't save unknown
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool validateErrorLocationEnum(ItemLocationEnum data)
        {
            if (data == ItemLocationEnum.Unknown)
            {
                LocationLabel.TextColor = Color.Red;
                LocationLabel.Text = "Location*";
                return locationValid = false;
            }

            return locationValid = true;
        }

        /// <summary>
        /// Round number for the slider
        /// </summary>
        /// <param name="val"></param>
        /// <param name="slide"> silder object</param>
        /// <returns></returns>
        public double RoundSilderValueToWhole(double val, Slider slide)
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