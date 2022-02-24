using System.Linq;
using System.Collections.ObjectModel;

using NUnit.Framework;

using Xamarin.Forms.Mocks;

using Game.ViewModels;
using Game.Models;
using Game.Views;
using Game;
using Xamarin.Forms;

namespace UnitTests.Views.Battle
{
    internal class ShowMonstersPageTest
    {

        App app;
        ShowMonstersPage page;


        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ShowMonstersPage();

        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void ShowMonstersPage_BattleButton_Clicked_Default_Should_Pass()
        {
            // Get the current valute

            // Act
            page.BattleButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}
