using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using _100autotjek.Models;
using _100autotjek.Services;
using _100autotjek.ViewModels.CarsForSaleItem;
using _100autotjek.ViewModels.DocItem;

namespace _100autotjek.Views.CarsForSaleItem
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CarSelectionPage : ContentPage
	{
		public CarSelectionPage ()
		{
		    NavigationPage.SetBackButtonTitle(this, string.Empty);

            InitializeComponent ();
		    var time = new TimeSpan(1, 1, 1);

            var viewModel = new CarSelectionViewModel
            {
                Navigation = Navigation,
                ListOfCars = CarSelectionList,
                SearchBar = SearchBar
            };
            
		    PullToRefresh.Refreshing += viewModel.OnRefreshingListOfCars;
		    SearchBar.TextChanged += viewModel.OnSearchCar;
		    CarSelectionList.ItemDoubleTapped += viewModel.OnCarDoubleTapped;

		    BindingContext = viewModel;
		}
	}
}