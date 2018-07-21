using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace _100autotjek.ViewModels.DocItem
{
    public class ResultViewModel
    {
        public ResultViewModel()
        {
            MainMenuCommand = new Command(NavigateToMainMenu);
        }

        public INavigation Navigation { get; set; }
        public ICommand MainMenuCommand { get; set; }
        public string ImageResult { get; set; }
        public string MessageResult { get; set; }


        private async void NavigateToMainMenu() => await Navigation.PopToRootAsync(true);
    }
}
