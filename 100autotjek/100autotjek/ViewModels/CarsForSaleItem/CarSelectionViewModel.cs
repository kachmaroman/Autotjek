using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Syncfusion.ListView.XForms;
using Syncfusion.SfPullToRefresh.XForms;
using Xamarin.Forms;
using _100autotjek.Localize;
using _100autotjek.Models;
using _100autotjek.Services;
using _100autotjek.ViewModels.Search;
using _100autotjek.Views.CarsForSaleItem;
using _100autotjek.Views.Search;

namespace _100autotjek.ViewModels.CarsForSaleItem
{
    public class CarSelectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isRefreshing;
        private bool isLoading;

        private ObservableCollection<Car> cars;
        private readonly TestDriveInfoService _testDriveInfoService;

        public CarSelectionViewModel()
        {
            _testDriveInfoService = new TestDriveInfoService();

            MainMenuCommand = new Command(NavigateToMainMenu);
            RemoveCarCommand = new Command(RemoveCarFromList);
            AddNewCarCommand = new Command(AddNewCar);

            LoadListOfCars();
        }

        public ObservableCollection<Car> Cars
        {
            get => cars;
            set
            {
                cars = value;
                OnPropertyChanged(nameof(Cars));
            }
        }

        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(Cars));
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
    
        public INavigation Navigation { get; set; }
        public ICommand RemoveCarCommand { get; set; }
        public ICommand MainMenuCommand { get; set; }
        public ICommand AddNewCarCommand { get; set; }
        public SfListView ListOfCars { get; set; }
        public SearchBar SearchBar { get; set; }

        private async void AddNewCar()
        {
            var searchCarViewModel = new SearchCarViewModel {IsNewCar = true};
            var searCarPage = new SearchCarPage(searchCarViewModel);

            await Navigation.PushAsync(searCarPage, true);
        }

        private async void RemoveCarFromList(object item)
        {
            if (item is Car car)
            {
                Cars.Remove(car);
                await _testDriveInfoService.RemoveCarAsync(car.Id);
            }
        }

        public async void OnCarDoubleTapped(object sender, ItemDoubleTappedEventArgs e)
        {
            if (e.ItemData is Car car)
            {
                var editCarInfoViewModel = new EditCarInfoViewModel
                {
                    Car = car,
                    FullCarName = $"{car.Make} {car.Model} {car.Variant}"
                };

                await Navigation.PushAsync(new EditCarInfoPage(editCarInfoViewModel), true);
            }
        }

        public async void OnRefreshingListOfCars(object sender, EventArgs e) => await RefreshListOfCarsAsync();

        public async Task RefreshListOfCarsAsync()
        {
            IsRefreshing = true;

            await Task.Delay(1000);

            var listOfCars = await _testDriveInfoService.GetListOfCarsAsync(5);

            if (listOfCars != null)
            {
                listOfCars = listOfCars.OrderBy(c => c.Make);
                Cars = new ObservableCollection<Car>(listOfCars);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert($"{AppResources.Alert_InternetConnectionErrorTitle}",
                                                                $"{AppResources.Alert_InternetConnectionErrorMessage}", 
                                                                $"{AppResources.Alert_Cancel}");
            }

            IsRefreshing = false;
        }

        public async void LoadListOfCars()
        {
            IsLoading = true;
            await RefreshListOfCarsAsync();
            IsLoading = false;
        }

        public void OnSearchCar(object sender, TextChangedEventArgs e)
        {
            if (ListOfCars.DataSource == null) return;

            ListOfCars.DataSource.Filter = FilterCars;
            ListOfCars.DataSource.RefreshFilter();
        }

        public bool FilterCars(object item)
        {
            if (!(item is Car car))
                return false;

            var value = SearchBar?.Text;

            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                return true;

            value = value.Trim().ToLower();

            return car.Make.ToLower().Contains(value) || car.Model.ToLower().Contains(value);
        }

        private async void NavigateToMainMenu() => await Navigation.PopToRootAsync(true);

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
