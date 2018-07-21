using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using _100autotjek.Localize;

namespace _100autotjek.Views.MainMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPage : ContentView
    {
        public MainMenuPage()
        {
            InitializeMenuPage();
        }

        private void InitializeMenuPage()
        {
            var mainMenu = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)}
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
                },
            };

            mainMenu.Children.Add(new MainMenuItem(AppResources.MainMenuSearch,      "SearchIcon"),      0, 0);
            mainMenu.Children.Add(new MainMenuItem(AppResources.MainMenuDoc,         "DocIcon"),         1, 0);
            mainMenu.Children.Add(new MainMenuItem(AppResources.MainMenuSticker,     "StickerIcon"),     0, 1);
            mainMenu.Children.Add(new MainMenuItem(AppResources.MainMenuCarsForSale, "CarsForSaleIcon"), 1, 1);
            mainMenu.Children.Add(new MainMenuItem(AppResources.MainMenuNumberPlate, "NumberPlateIcon"), 0, 2);
            mainMenu.Children.Add(new MainMenuItem(AppResources.MainMenuPhone,       "PhoneIcon"),       1, 2);

            Content = mainMenu;
        }
    }
}