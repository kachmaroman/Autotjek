using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Syncfusion.DataSource;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using Syncfusion.SfPullToRefresh.XForms;
using Xamarin.Forms;
using _100autotjek.Models;
using _100autotjek.Services;
using _100autotjek.Views;
using _100autotjek.Views.DocItem;

namespace _100autotjek.ViewModels.DocItem
{
    public class ListOfCarsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isRefreshing;

        private ObservableCollection<Car> _cars;
        private readonly TestDriveInfoService _testDriveInfoService;

        public ListOfCarsViewModel()
        {
            _testDriveInfoService = new TestDriveInfoService();

            MainMenuCommand = new Command(NavigateToMainMenu);
        }

        public ObservableCollection<Car> Cars
        {
            get => _cars;
            set
            {
                _cars = value;
                OnPropertyChanged(nameof(Cars));
            }
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        
        public INavigation Navigation { get; set; }
        public ICommand MainMenuCommand { get; set; }
        public TestDriveInfo TestDriveInfo { get; set; }
        public SfListView ListOfCars { get; set; }
        public SearchBar SearchBar { get; set; }

        public async void SelectedCar(Car car)
        {
            TestDriveInfo.CarId = car.Id;

            var signatureViewModel = new SignatureViewModel
            {
                TestDriveInfo =  TestDriveInfo,
                SelectedCarInfo = $"{car.Make} {car.Model}"
            };

            var signaturePage = new SignaturePage(signatureViewModel);

            await Navigation.PushAsync(signaturePage, true);
        }

        public async Task RefreshListOfCarsAsync()
        {
            var listOfCars = await _testDriveInfoService.GetListOfCarsAsync(5);

            if (listOfCars != null)
            {
                listOfCars = listOfCars.OrderBy(c => c.Make);

                Cars = new ObservableCollection<Car>(listOfCars);
            }
        }
        
        public void OnCarDoubleTapped(object sender, ItemDoubleTappedEventArgs e)
        {
            if (e.ItemData is Car car)
            {
                SelectedCar(car);
            }
        }

        public async void OnRefreshingListOfCars(object sender, EventArgs e)
        {
            IsRefreshing = true;

            await Task.Delay(1000);

            await RefreshListOfCarsAsync();

            IsRefreshing = false;
        }

        public void OnSearchCar(object sender, TextChangedEventArgs e)
        {
            if (ListOfCars.DataSource == null) return;

            ListOfCars.DataSource.Filter = FilterCars;
            ListOfCars.DataSource.RefreshFilter();
        }

        private bool FilterCars(object item)
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
