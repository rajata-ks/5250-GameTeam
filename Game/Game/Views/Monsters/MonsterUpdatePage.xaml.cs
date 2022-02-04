﻿using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;

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

        ///// <summary>
        ///// Randomize the Monster, keep the level the same
        ///// </summary>
        ///// <returns></returns>
        //public bool RandomizeMonster()
        //{
        //    // Randomize Name
        //    ViewModel.Data.Name = RandomPlayerHelper.GetMonsterName();
        //    ViewModel.Data.Description = RandomPlayerHelper.GetMonsterDescription();

        //    // Randomize the Attributes
        //    ViewModel.Data.Attack = RandomPlayerHelper.GetAbilityValue();
        //    ViewModel.Data.Speed = RandomPlayerHelper.GetAbilityValue();
        //    ViewModel.Data.Defense = RandomPlayerHelper.GetAbilityValue();

        //    ViewModel.Data.Difficulty = RandomPlayerHelper.GetMonsterDifficultyValue();

        //    ViewModel.Data.ImageURI = RandomPlayerHelper.GetMonsterImage();

        //    ViewModel.Data.UniqueItem = RandomPlayerHelper.GetMonsterUniqueItem();

        //    _ = UpdatePageBindingContext();

        //    return true;
        //}

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