using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using _100autotjek.Models;
using _100autotjek.Services;
using _100autotjek.ViewModels.CarsForSaleItem;
using _100autotjek.Views.CarsForSaleItem;
using _100autotjek.Views.Search;

namespace _100autotjek.ViewModels.Search
{
    public class SearchCarViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly TestDriveInfoService _testDriveInfoService;

        private string plateNumber;
        private string vinNumber;

        public SearchCarViewModel()
        {
            _testDriveInfoService = new TestDriveInfoService();

            SearchCarCommand = new Command(SearchCar);
            MainMenuCommand = new Command(MainMenu);
        }

        public string PlateNumber
        {
            get => plateNumber;
            set
            {
                plateNumber = value;
                OnPropertyChanged(nameof(PlateNumber));
            }
        }

        public string VinNumber
        {
            get => vinNumber;
            set
            {
                vinNumber = value;
                OnPropertyChanged(nameof(VinNumber));
            }
        }

        public bool IsNewCar { get; set; }
        public Car Car { get; set; }
        public ICommand SearchCarCommand { get; set; }
        public ICommand MainMenuCommand { get; set; }
        public INavigation Navigation { get; set; }

        private async void SearchCar()
        {
            if (!string.IsNullOrEmpty(PlateNumber))
                Car = await _testDriveInfoService.GetCarByNumberAsync(PlateNumber);
            else
                Car = await _testDriveInfoService.GetCarByNumberAsync(VinNumber);

            if (Car != null && !IsNewCar)
            {
                var carInfoViewModel = new CarInfoViewModel
                {
                    Car = Car,
                    FullCarName = $"{Car.Make} {Car.Model}"
                };

                var carInfoPage = new CarInfoPage(carInfoViewModel);

                await Navigation.PushAsync(carInfoPage, true);
            }
            else if (Car != null && IsNewCar)
            {
                var carInfoViewModel = new EditCarInfoViewModel
                {
                    Car = Car,
                    IsNewCar = IsNewCar,
                    FullCarName = $"{Car.Make} {Car.Model} {Car.Variant}"
                };

                var carInfoPage = new EditCarInfoPage(carInfoViewModel);

                await Navigation.PushAsync(carInfoPage, true);
            }
        }

        private async void MainMenu() => await Navigation.PopToRootAsync(true);

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
