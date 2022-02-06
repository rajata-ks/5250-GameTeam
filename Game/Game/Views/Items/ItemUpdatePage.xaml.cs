using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// Item Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemUpdatePage : ContentPage
    {
        // View Model for Item
        public readonly GenericViewModel<ItemModel> ViewModel;

        // Empty Constructor for Tests
        public ItemUpdatePage(bool UnitTest) { }

        //Bools used to validate name.
        private bool nameValid;

        //Bool used to validate description.
        private bool descriptionValid;

        //Bool used to validate image.
        private bool imageValid;

        //copy of pre edited data
        private ItemModel ItemModelCopy;

        //Silder increment size
        private readonly int SliderStepSize = 1;

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public ItemUpdatePage(GenericViewModel<ItemModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            //Set bools to true.
            nameValid = true;
            descriptionValid = true;
            imageValid = true;

            ItemModelCopy = new ItemModel(data.Data);

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = data.Data.Location.ToString();
            AttributePicker.SelectedItem = data.Data.Attribute.ToString();
        }

        /// <summary>
        /// Save calls to Update
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
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

            MessagingCenter.Send(this, "Update", ViewModel.Data);
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        public async void Cancel_Clicked(object sender, EventArgs e)
        {

            //revert changes to original
            ViewModel.Data.Update(ItemModelCopy);

            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Catch the change to the Stepper for Attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Damage_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            var StepValue = SliderStepSize;

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

            ImageLabel.TextColor = Color.White;
            imageValid = true;

        }

        /// <summary>
        /// Round number for the slider
        /// </summary>
        /// <param name="val"></param>
        /// <param name="slide"> silder object</param>
        /// <returns></returns>
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