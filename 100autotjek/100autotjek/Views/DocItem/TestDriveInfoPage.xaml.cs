using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using _100autotjek.Models;
using _100autotjek.ViewModels;
using _100autotjek.ViewModels.DocItem;

namespace _100autotjek.Views.DocItem
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestDriveInfoPage : ContentPage
	{
		public TestDriveInfoPage(TestDriveInfoViewModel viewModel)
		{
		    NavigationPage.SetBackButtonTitle(this, string.Empty);

            InitializeComponent ();
		    
            viewModel.Navigation = Navigation;

		    BindingContext = viewModel;
        }
	        
        private void ValidateDateAndTime(object sender, PropertyChangingEventArgs e)
	    {
            if (EndDate.Date <= StartDate.Date && EndTime.Time <= StartTime.Time)
            {
                EndDate.Date = StartDate.Date;
                var hour = StartTime.Time.Add(TimeSpan.FromHours(1)).Hours;
                EndTime.Time = new TimeSpan(hour, EndTime.Time.Minutes, EndTime.Time.Seconds);
            }
        }
	}
}