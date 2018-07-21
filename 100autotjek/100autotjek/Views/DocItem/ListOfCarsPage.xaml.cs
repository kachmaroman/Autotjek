using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using _100autotjek.Models;
using _100autotjek.ViewModels;
using _100autotjek.ViewModels.DocItem;

namespace _100autotjek.Views.DocItem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOfCarsPage : ContentPage
    {
        public ListOfCarsPage(ListOfCarsViewModel viewModel)
        {
            NavigationPage.SetBackButtonTitle(this, string.Empty);

            InitializeComponent();

            viewModel.Navigation = Navigation;
            viewModel.ListOfCars = ListOfCars;
            viewModel.SearchBar = SearchBar;

            BindingContext = viewModel;

            SearchBar.TextChanged += viewModel.OnSearchCar;
            ListOfCars.ItemDoubleTapped += viewModel.OnCarDoubleTapped;
            PullToRefresh.Refreshing += viewModel.OnRefreshingListOfCars;
        }
    }
}
