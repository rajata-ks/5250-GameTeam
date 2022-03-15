using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Game.Views
{
    /// <summary>
    /// Selecting Characters for the Game
    /// 
    /// 
    /// Mike's game allows duplicate characters in a party (6 Mikes can all fight)
    /// If you do not allow duplicates, change the code below
    /// Instead of using the database list directly make a copy of it in the viewmodel
    /// Then have on select of the database remove the character from that list and add to the part list
    /// Have select from the party list remove it from the party list and add it to the database list
    ///
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickCharactersPage : ContentPage
    {
        public CharacterModel currentItem;
        public int itemcount;
        HashSet<CharacterModel> characterSet;

        // Empty Constructor for UTs
        public PickCharactersPage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Index Page
        /// 
        /// Get the CharacterIndexView Model
        /// </summary>
        public PickCharactersPage()
        {
            InitializeComponent();

            BindingContext = BattleEngineViewModel.Instance;
            //BindingContext = BattleEngineViewModel.Instance;


            currentItem = BattleEngineViewModel.Instance.DatabaseCharacterList.FirstOrDefault();
            if (currentItem != null)
            {
                CharacterImage.Source = currentItem.ImageURI;
                CharacterImage2.Source = currentItem.ImageURI;
                CharacterDescription.Text = currentItem.Description;
                CharacterText.Text = currentItem.Name;
                CharacterLevel.Text = currentItem.Level.ToString();
            }
            itemcount = BattleEngineViewModel.Instance.DatabaseCharacterList.Count;

            // Clear the Database List and the Party List to start
            if (BattleEngineViewModel.Instance.PartyCharacterList != null)
            {
                BattleEngineViewModel.Instance.PartyCharacterList.Clear();
            }
            characterSet = new HashSet<CharacterModel>();

            CharacterModel data = BattleEngineViewModel.Instance.DatabaseCharacterList.FirstOrDefault();
            if (data != null)
            {
                CharacterImage.Source = data.ImageURI;
            }
            UpdateNextButtonState();
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnPartyCharacterItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            CharacterModel data = args.SelectedItem as CharacterModel;
            if (data == null)
            {
                return;
            }

            // Manually deselect Character.
            PartyListView.SelectedItem = null;

            // Remove the character from the list
            _ = BattleEngineViewModel.Instance.PartyCharacterList.Remove(data);

            //Remove from set
            characterSet.Remove(data);
            UpdateNextButtonState();
        }


        /// <summary>
        /// Next Button is based on the count
        /// 
        /// If no selected characters, disable
        /// 
        /// Show the Count of the party
        /// 
        /// </summary>
        public void UpdateNextButtonState()
        {
            // If no characters disable Next button
            NextButton.IsEnabled = true;

            var currentCount = BattleEngineViewModel.Instance.PartyCharacterList?.Count();
            if (currentCount == 0)
            {
                NextButton.IsEnabled = false;
            }


        }

        public void OnDatabaseCharacterItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            CharacterModel data = args.SelectedItem as CharacterModel;
            if (data == null)
            {
                return;
            }



            // Don't add more than the party max
            if (BattleEngineViewModel.Instance.PartyCharacterList.Count() < BattleEngineViewModel.Instance.Engine.EngineSettings.MaxNumberPartyCharacters)
            {
                BattleEngineViewModel.Instance.PartyCharacterList.Add(data);
            }

            UpdateNextButtonState();
        }



        /// <summary>
        /// Jump to the Battle
        /// 
        /// Its Modal because don't want user to come back...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void NextButton_Clicked(object sender, EventArgs e)
        {
           
            CreateEngineCharacterList();

            await Navigation.PushModalAsync(new NavigationPage(new ShowMonstersPage()));
        }

        /// <summary>
        /// Select party Character
        /// 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SelectButton_Clicked(object sender, EventArgs e)
        {
           

            if (BattleEngineViewModel.Instance.PartyCharacterList.Count() < BattleEngineViewModel.Instance.Engine.EngineSettings.MaxNumberPartyCharacters)
            {
              
                if (!characterSet.Contains(this.currentItem))
                {
                    if (currentItem != null)
                    {
                        BattleEngineViewModel.Instance.PartyCharacterList.Add(currentItem);
                        characterSet.Add(this.currentItem);
                    }
                }
            }

            UpdateNextButtonState();

        }

        /// <summary>
        /// Enable True of False the Image Arrows
        /// Based on the image in the list
        /// </summary>
        /// <returns></returns>
        public bool EnableImageArrowButtons()
        {
            LeftArrowButton.IsEnabled = true;
            RightArrowButton.IsEnabled = true;

            var ImageIndexCurrent = BattleEngineViewModel.Instance.DatabaseCharacterList.IndexOf(this.currentItem);

            if (ImageIndexCurrent < 1)
            {
                LeftArrowButton.IsEnabled = false;
            }

            if (ImageIndexCurrent >= itemcount - 1)
            {
                RightArrowButton.IsEnabled = false;
            }

            return true;
        }

        /// <summary>
        /// Shift Image to the Left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LeftArrow_Clicked(object sender, EventArgs e)
        {
            _ = ChangeImageByIncrement(-1);
        }

        /// <summary>
        /// Shift Image to the Right
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RightArrow_Clicked(object sender, EventArgs e)
        {
            _ = ChangeImageByIncrement(1);
        }


        /// <summary>
        /// Move the Image left or Right
        /// </summary>
        /// <param name="increment"></param>
        public int ChangeImageByIncrement(int increment)
        {
            // Find the Index for the current Image
            var ImageIndexCurrent = BattleEngineViewModel.Instance.DatabaseCharacterList.IndexOf(currentItem);

            // Amount to move
            var indexNew = ImageIndexCurrent + increment;

            if (indexNew >= itemcount)
            {
                indexNew = itemcount - 1;
            }

            if (indexNew <= 0)
            {
                indexNew = 0;
            }

            // Increment or Decrement to change the to a different image
            currentItem = BattleEngineViewModel.Instance.DatabaseCharacterList.ElementAt(indexNew);
            CharacterImage.Source = currentItem.ImageURI;
            CharacterImage2.Source = currentItem.ImageURI;
            CharacterDescription.Text = currentItem.Description;
            CharacterText.Text = currentItem.Name;
            CharacterLevel.Text = currentItem.Level.ToString();



            return indexNew;
        }


        /// <summary>
        /// Clear out the old list and make the new list
        /// </summary>
        public void CreateEngineCharacterList()
        {
            // Clear the currett list
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Clear();

            // Load the Characters into the Engine
            foreach (var data in BattleEngineViewModel.Instance.PartyCharacterList)
            {
                data.CurrentHealth = data.GetMaxHealthTotal;
                BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(data));
            }
        }



        /// <summary>
        /// the on appearing method to handel push
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (BattleEngineViewModel.Instance.PartyCharacterList == null)
            {
                BattleEngineViewModel.Instance.PartyCharacterList = new ObservableCollection<CharacterModel> { new CharacterModel() };
                _ = Navigation.PopModalAsync();
            }
        }
    }
}