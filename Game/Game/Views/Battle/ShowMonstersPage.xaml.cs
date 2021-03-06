using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowMonstersPage : ContentPage
    {
        //bool check for modal after push
        public bool ReturnedFromModalPage = false;

        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        public ShowMonstersPage(bool UnitTest) { ReturnedFromModalPage = true; }


        public ShowMonstersPage()
        {
            InitializeComponent();

            BindingContext = EngineViewModel;

        }

        /// <summary>
        /// Jump to the Battle
        /// 
        /// Its Modal because don't want user to come back...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void BattleButton_Clicked(object sender, EventArgs e)
        {

            //await Navigation.PushModalAsync(new BattlePage());

            ReturnedFromModalPage = false;
            await Navigation.PushModalAsync(new NavigationPage(new BattlePage()));
            ReturnedFromModalPage = true;
        }


        /// <summary>
        /// the on appearing method to handel push
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (ReturnedFromModalPage)
            {
                ReturnedFromModalPage = false;
                _ = await Navigation.PopModalAsync();
            }
        }


    }
}