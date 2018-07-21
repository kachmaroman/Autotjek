using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using _100autotjek.ViewModels.CarsForSaleItem;

namespace _100autotjek.Views.CarsForSaleItem
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditCarInfoPage : ContentPage
	{
        public EditCarInfoPage (EditCarInfoViewModel viewModel)
		{
			InitializeComponent ();

		    viewModel.Navigation = Navigation;

		    BindingContext = viewModel;
		}
	}
}