using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using _100autotjek.Models;

namespace _100autotjek.ViewModels.Search
{
    public class CarInfoViewModel
    {
        public CarInfoViewModel()
        {
            BackCommand = new Command(Back);
            MainMenuCommand = new Command(MainMenu);   
        }

        public Car Car { get; set; }
        public string FullCarName { get; set; }
        public ICommand MainMenuCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public INavigation Navigation { get; set; }

        private async void MainMenu() => await Navigation.PopToRootAsync(true);
        private async void Back() => await Navigation.PopAsync(true);
    }
}
