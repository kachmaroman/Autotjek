using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using _100autotjek.ViewModels.Search;

namespace _100autotjek.Views.Search
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchCarPage : ContentPage
	{
		public SearchCarPage (SearchCarViewModel viewModel)
		{
		    NavigationPage.SetBackButtonTitle(this, string.Empty);

            InitializeComponent ();

		    viewModel.Navigation = Navigation;

		    BindingContext = viewModel;
		}

	    private void OnPlateNumberChanged(object sender, TextChangedEventArgs e)
	    {
	        if (sender is Entry entry)
	        {
	            var input = entry.Text;

	            if (input.Length > 9)
	            {
	               input = input.Remove(input.Length - 1);
	            }

	            entry.Text = input.ToUpper();
	        }
	    }
	}
}