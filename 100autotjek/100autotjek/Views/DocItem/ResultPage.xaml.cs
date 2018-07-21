using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using _100autotjek.ViewModels;
using _100autotjek.ViewModels.DocItem;

namespace _100autotjek.Views.DocItem
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResultPage : ContentPage
	{
		public ResultPage (ResultViewModel viewModel)
		{
		    NavigationPage.SetBackButtonTitle(this, string.Empty);

            InitializeComponent ();

		    viewModel.Navigation = Navigation;

		    BindingContext = viewModel;
		}
    }
}