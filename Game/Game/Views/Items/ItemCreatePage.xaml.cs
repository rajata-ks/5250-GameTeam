﻿using Game.Models;
using Game.ViewModels;

using System;
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
        public ItemCreatePage()
        {
            InitializeComponent();

            this.ViewModel.Data = new ItemModel();
            BindingContext = this.ViewModel;
            this.ViewModel.Title = "Create";

            //Defaults bools
            nameValid = true;
            descriptionValid = true;
            imageValid = true;
            attributeValid = true;
            locationValid = true;

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = ViewModel.Data.Location.ToString();
            AttributePicker.SelectedItem = ViewModel.Data.Attribute.ToString();
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

            if (validateErrorLocationEnum(ViewModel.Data.Location) == false)
            {
                return;
            }

            if (validateErrorAttributeEnum(ViewModel.Data.Attribute) == false)
            {
                return;
            }

            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

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
        /// Catch the change to the Stepper for Range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Range_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            RangeValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the stepper for Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Value_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the stepper for Damage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Damage_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DamageValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Validate the entry for name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Name_TextChanged(object sender, ValueChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(NameEntry.Text))
            {
                NameLabel.TextColor = Color.Red;
                NameLabel.Text = "Name*";
                nameValid = false;

                return;
            }

            if (String.IsNullOrWhiteSpace(NameEntry.Text))
            {
                NameLabel.TextColor = Color.Red;
                NameLabel.Text = "Name*";
                nameValid = false;

                return;
            }

            NameLabel.TextColor = Color.Black;
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
            if (String.IsNullOrEmpty(DescriptionEntry.Text))
            {
                DescriptionLabel.TextColor = Color.Red;
                DescriptionLabel.Text = "Description*";
                descriptionValid = false;

                return;
            }

            if (String.IsNullOrWhiteSpace(DescriptionEntry.Text))
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
            if (String.IsNullOrEmpty(ImageEntry.Text))
            {
                ImageLabel.TextColor = Color.Red;
                imageValid = false;
                return;
            }

            if (String.IsNullOrWhiteSpace(ImageEntry.Text))
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
        /// Validate and change UI Element if invalid
        /// Can't save unknown
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool validateErrorAttributeEnum(AttributeEnum data)
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
        private bool validateErrorLocationEnum(ItemLocationEnum data)
        {
            if (data == ItemLocationEnum.Unknown)
            {
                LocationLabel.TextColor = Color.Red;
                LocationLabel.Text = "Location*";
                return locationValid = false;
            }

            return locationValid = true;
        }
    }
}