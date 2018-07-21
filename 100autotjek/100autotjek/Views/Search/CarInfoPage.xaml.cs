using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using _100autotjek.Models;
using _100autotjek.ViewModels.Search;

namespace _100autotjek.Views.Search
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CarInfoPage : ContentPage
	{
		public CarInfoPage (CarInfoViewModel viewModel)
		{
		    NavigationPage.SetBackButtonTitle(this, string.Empty);

            InitializeComponent ();
		    viewModel.Navigation = Navigation;

		    BindingContext = viewModel;
		}
	}
}