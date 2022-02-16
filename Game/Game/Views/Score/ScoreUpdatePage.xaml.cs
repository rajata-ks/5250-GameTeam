﻿using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// Score Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreUpdatePage : ContentPage
    {
        // View Model for Score
        public readonly GenericViewModel<ScoreModel> ViewModel;

        // Constructor for Unit Testing
        public ScoreUpdatePage(bool UnitTest) { }
        
        //Bool used to validate name.
        private bool nameValid;

        //Bool used to validate battle.
        private bool battleValid;

        //Bool used to validate date.
        private bool dateValid;

        //Bool used to validate experience.
        private bool experienceValid;

        //Bool used to validate monster.
        private bool monsterValid;

        //Bool used to validate score.
        private bool scoreValid;

        /// <summary>
        /// Constructor that takes and existing data Score
        /// </summary>
        public ScoreUpdatePage(GenericViewModel<ScoreModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            //Default bools
            nameValid = true;
            battleValid = true;
            dateValid = true;
            experienceValid = true;
            monsterValid = true;
            scoreValid = true;
            this.ViewModel.Title = "Update " + data.Title;
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            if (!nameValid)
            {
                return;
            }
            if (!battleValid)
            {
                return;
            }

            if (!dateValid)
            {
                return;
            }

            if (!experienceValid)
            {
                return;
            }

            if (!monsterValid)
            {
                return;
            }

            if (!scoreValid)
            {
                return;
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
            _ = await Navigation.PopModalAsync();
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
        /// Validate the entry for battle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Battle_TextChanged(object sender, ValueChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(BattleEntry.Text))
            {
                BattleLabel.TextColor = Color.Red;
                BattleLabel.Text = "Battle*";
                battleValid = false;
                return;
            }
            if (!isNumeric(BattleEntry.Text))
            {
                BattleLabel.TextColor = Color.Red;
                BattleLabel.Text = "Battle*";
                battleValid = false;
                return;
            }

            BattleLabel.TextColor = Color.White;
            BattleLabel.Text = "Battle";
            battleValid = true;
        }

        /// <summary>
        /// Validate the entry for Date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Date_TextChanged(object sender, ValueChangedEventArgs e)
        {
            //Used to validate DateTime.
            DateTime temp;

            if (string.IsNullOrEmpty(DateEntry.Text))
            {
                DateLabel.TextColor = Color.Red;
                DateLabel.Text = "Date*";
                dateValid = false;
                return;
            }
            if (!DateTime.TryParse(DateEntry.Text, out temp))
            {
                DateLabel.TextColor = Color.Red;
                DateLabel.Text = "Date*";
                dateValid = false;
                return;
            }

            DateLabel.TextColor = Color.White;
            DateLabel.Text = "Date";
            dateValid = true;
        }

        /// <summary>
        /// Validate the entry for Experience.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Experience_TextChanged(object sender, ValueChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(ExperienceEntry.Text))
            {
                ExperienceLabel.TextColor = Color.Red;
                ExperienceLabel.Text = "Experience*";
                experienceValid = false;
                return;
            }
            if (!isNumeric(ExperienceEntry.Text))
            {
                ExperienceLabel.TextColor = Color.Red;
                ExperienceLabel.Text = "Experience*";
                experienceValid = false;
                return;
            }

            ExperienceLabel.TextColor = Color.White;
            ExperienceLabel.Text = "Experience";
            experienceValid = true;
        }

        /// <summary>
        /// Validate the entry for Monster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name=""></param>
        public void Monster_TextChanged(object sender, ValueChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(MonsterEntry.Text))
            {
                MonsterLabel.TextColor = Color.Red;
                MonsterLabel.Text = "Monster*";
                monsterValid = false;
                return;
            }
            if (!isNumeric(MonsterEntry.Text))
            {
                MonsterLabel.TextColor = Color.Red;
                MonsterLabel.Text = "Monster*";
                monsterValid = false;
                return;
            }

            MonsterLabel.TextColor = Color.White;
            MonsterLabel.Text = "Monster";
            monsterValid = true;
        }

        /// <summary>
        /// Checks if the string is only numeric values.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool isNumeric(string s)
        {
            foreach (char c in s)
            {
                if (!(c >= '0' && c <= '9'))
                {
                    return false;
                }
            }

            return true;
        }
    }
}