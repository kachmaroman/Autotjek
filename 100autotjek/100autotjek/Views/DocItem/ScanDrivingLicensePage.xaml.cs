using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using _100autotjek.Localize;
using _100autotjek.ViewModels;
using _100autotjek.ViewModels.DocItem;

namespace _100autotjek.Views.DocItem
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanDrivingLicensePage : ContentPage
	{
		public ScanDrivingLicensePage ()
		{
		    NavigationPage.SetBackButtonTitle(this, string.Empty);

            InitializeComponent();
		    
            BindingContext = new ScanDrivingLicenseViewModel {Navigation = Navigation};
		}

        private void OnCPRNumberChanged(object sender, TextChangedEventArgs e)
	    {
	        var limit = 11;

            if (sender is Entry entry)
            {
                var cprNumber = entry.Text;

                if (cprNumber.Length == 7 && !cprNumber.Contains("-"))
                {
                    entry.Text = cprNumber.Insert(cprNumber.Length - 1, "-");
                }

                if (cprNumber.Length > limit)
                {
                    entry.Text = cprNumber.Remove(cprNumber.Length - 1);
                }
            }
        }
    }
}