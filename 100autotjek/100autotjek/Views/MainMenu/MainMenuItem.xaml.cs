using System;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using _100autotjek.Localize;
using _100autotjek.Models;
using _100autotjek.Services;
using _100autotjek.ViewModels.CarsForSaleItem;
using _100autotjek.ViewModels.Search;
using _100autotjek.Views.CarsForSaleItem;
using _100autotjek.Views.DocItem;
using _100autotjek.Views.Search;

namespace _100autotjek.Views.MainMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuItem : ContentView
    {
        private readonly TestDriveInfoService _testDriveInfoService = new TestDriveInfoService();

        public MainMenuItem(string btnName, string btnImage)
        {
            InitializeComponent();

            BtnName.Text = btnName;
            BtnImage.Source = btnImage;
            BtnImage.ClassId = btnImage;
        }

        private async void OnPickMenuItem(object sender, EventArgs e)
        {
            if (sender is Image btnImage)
            {
                var index = btnImage.ClassId.IndexOf("Icon", StringComparison.Ordinal);
                var btnName = btnImage.ClassId.Substring(0, index);
                ContentPage selectedPage = null;

                switch (btnName)
                {
                    case "Search":
                        selectedPage = new SearchCarPage(new SearchCarViewModel{IsNewCar = false});
                        break;
                    case "Doc":
                        selectedPage = new ScanDrivingLicensePage();
                        break;
                    case "Sticker":

                        break;
                    case "CarsForSale":
                        selectedPage = new CarSelectionPage();
                        break;
                    case "NumberPlate":

                        break;
                    case "Phone":

                        break;
                }

                BtnImage.IsEnabled = false;

                if (selectedPage != null)
                {
                    await Navigation.PushAsync(selectedPage, true);
                }

                BtnImage.IsEnabled = true;
            }
        }
    }
}