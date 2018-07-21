using _100autotjek.Views;
using Xamarin.Forms;
using _100autotjek.Views.MainMenu;

namespace _100autotjek
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage())
            {
                BarBackgroundColor = Color.FromHex("#C4D1DB"),
                BarTextColor = Color.FromHex("#293862"),
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
