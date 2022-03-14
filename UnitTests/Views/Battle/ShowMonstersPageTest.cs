using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms.Mocks;
using Xamarin.Forms;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

namespace UnitTests.Views.Battle
{
    internal class ShowMonstersPageTest : ShowMonstersPage
    {

        App app;
        ShowMonstersPage page;
        public ShowMonstersPageTest() : base(true) { }


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
        public void ShowMonstersPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ShowMonstersPage_BattleButton_Clicked_Default_Should_Pass()
        {
            // Act
            page.BattleButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


        [Test]
        public void ShowMonsterPage_OnAppearing_Should_Pass()
        {
            // set character list
            page.ReturnedFromModalPage = true;

            // Act
            OnAppearing();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}
