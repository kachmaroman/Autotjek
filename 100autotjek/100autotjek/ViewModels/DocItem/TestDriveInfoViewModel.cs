using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EmailValidation;
using Xamarin.Forms;
using _100autotjek.Localize;
using _100autotjek.Models;
using _100autotjek.Services;
using _100autotjek.Views;
using _100autotjek.Views.DocItem;

namespace _100autotjek.ViewModels.DocItem
{
    public class TestDriveInfoViewModel
    {
        public TestDriveInfoViewModel()
        {
            ContinueCommand = new Command(Continue);
            MainMenuCommand = new Command(NavigateToMainMenu);
        }

        public INavigation Navigation { get; set; }
        public ICommand ContinueCommand { get; set; }
        public ICommand MainMenuCommand { get; set; }
        public IEnumerable<TestNumberPlate> TestNumberPlates { get; set; }
        public TestDriveInfo TestDriveInfo { get; set; }
        public TestNumberPlate SelectedPlateNumber { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        private async void NavigateToMainMenu() => await Navigation.PopToRootAsync(true);

        private void SetDateTime()
        {
            var year = TestDriveInfo.StartDate.Year;
            var month = TestDriveInfo.StartDate.Month;
            var day = TestDriveInfo.StartDate.Day;
            var hour = StartTime.Hours;
            var minute = StartTime.Minutes;

            TestDriveInfo.StartDate = new DateTime(year, month, day, hour, minute, 0);

            year = TestDriveInfo.EndDate.Year;
            month = TestDriveInfo.EndDate.Month;
            day = TestDriveInfo.EndDate.Day;
            hour = EndTime.Hours;
            minute = EndTime.Minutes;

            TestDriveInfo.EndDate = new DateTime(year, month, day, hour, minute, 0);
        }

        private bool IsDriverInfoValid()
        {
            bool IsEmailValid(string value)
            {
                if (string.IsNullOrEmpty(value))
                    return false;

                value = value.Trim();

                return EmailValidator.Validate(value);
            }

            return SelectedPlateNumber != null && IsEmailValid(TestDriveInfo.Email) && !string.IsNullOrWhiteSpace(TestDriveInfo.Phone);
        }

        private async Task DisplayErrorMessage(string title, string message, string cancel) =>
            await Application.Current.MainPage.DisplayAlert($"{title}", $"{message}", $"{cancel}");

        private async void Continue()
        {
            if (!IsDriverInfoValid())
            {
                await DisplayErrorMessage($"{AppResources.Alert_DriverInfoInvalidErrorTitle}",
                                          $"{AppResources.Alert_DriverInfoInvalidErrorMessage}",
                                          $"{AppResources.Alert_Cancel}");

                return;
            }

            TestDriveInfo.PlateId = SelectedPlateNumber.Id;

            SetDateTime();

            var listOfCarsViewModel = new ListOfCarsViewModel
            {
                TestDriveInfo = TestDriveInfo
            };

            await listOfCarsViewModel.RefreshListOfCarsAsync();
            var listOfCarsPage = new ListOfCarsPage(listOfCarsViewModel);

            await Navigation.PushAsync(listOfCarsPage, true);
        }
    }
}
