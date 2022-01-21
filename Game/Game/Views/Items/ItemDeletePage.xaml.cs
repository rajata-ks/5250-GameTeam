using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDeletePage : ContentPage
    {
        // View Model for Item
        readonly GenericViewModel<ItemModel> viewModel;

        // Empty Constructor for UTs
        public ItemDeletePage(bool UnitTest) { }
        private string updateTitle;

        // Constructor for Delete takes a view model of what to delete
        public ItemDeletePage(GenericViewModel<ItemModel> data)
        {
            InitializeComponent();

            BindingContext = this.viewModel = data;

            updateTitle = data.Title;

            this.viewModel.Title = "Delete Item";
        }

        /// <summary>
        /// Deletes item and sends user to Item Index View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Delete", viewModel.Data);
            await Navigation.PushModalAsync(new NavigationPage(new ItemIndexPage()));
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            this.viewModel.Title = updateTitle;

            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Trap the Back Button on the Phone
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            // Add your code here...
            return true;
        }
    }
}