using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignaturePad.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using _100autotjek.Helpers;
using _100autotjek.Models;
using _100autotjek.ViewModels;
using _100autotjek.ViewModels.DocItem;

namespace _100autotjek.Views.DocItem
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignaturePage : ContentPage
	{
		public SignaturePage (SignatureViewModel viewModel)
		{
		    NavigationPage.SetBackButtonTitle(this, string.Empty);

            InitializeComponent ();

		    viewModel.Navigation = Navigation;
		    viewModel.Signature = Signature;

		    BindingContext = viewModel;
		}
	}
}