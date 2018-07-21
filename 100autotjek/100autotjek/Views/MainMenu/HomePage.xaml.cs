using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace _100autotjek.Views.MainMenu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
		    NavigationPage.SetBackButtonTitle(this, string.Empty);

            InitializeComponent();
        }
    }
}