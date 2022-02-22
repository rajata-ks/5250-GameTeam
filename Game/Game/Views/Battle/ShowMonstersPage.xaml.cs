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

        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

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

            await Navigation.PushModalAsync(new NavigationPage(new BattlePage()));
            _ = await Navigation.PopAsync();
        }




    }
}