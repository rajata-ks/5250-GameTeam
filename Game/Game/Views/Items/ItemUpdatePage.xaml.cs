using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;
using System.Collections.ObjectModel;
using System.Linq;

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

            this.ViewModel = data;

            //clear list
            if (this.ViewModel.Dataset != null)
            {
                this.ViewModel.Dataset.Clear();
            }

            //initialize the dataset
            this.ViewModel.Dataset = new ObservableCollection<DefaultModel>();

            //get all data
            var dataList = ItemIndexViewModel.Instance.Dataset;

            //to populate the dataset so images can show up
            foreach (var datas in dataList)
            {
                this.ViewModel.Dataset.Add(new DefaultModel { Name = datas.Name, Description = datas.Description, ImageURI = datas.ImageURI, Id = datas.Id });
            }

            BindingContext = this.ViewModel = data;

            //set carousel to the current item image
            carouselItem.CurrentItem = this.ViewModel.Dataset.Where(x => x.Id == data.Data.Id).FirstOrDefault();

            //set title
            this.ViewModel.Title = "Update " + data.Title;

            //Set bools to true.
            nameValid = true;
            descriptionValid = true;
            imageValid = true;

            ItemModelCopy = new ItemModel(data.Data);

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = ItemLocationEnumExtensions.ToMessage(data.Data.Location);
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

        private void carouselItem_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            //get current carousel item image url to save
            var cur = e.CurrentItem as DefaultModel;
            ViewModel.Data.ImageURI = cur.ImageURI;
        }
    }
}