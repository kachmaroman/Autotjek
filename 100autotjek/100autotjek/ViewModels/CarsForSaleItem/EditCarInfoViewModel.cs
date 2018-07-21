using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using _100autotjek.Helpers;
using _100autotjek.Localize;
using _100autotjek.Models;
using _100autotjek.Services;
using _100autotjek.Views.CarsForSaleItem;

namespace _100autotjek.ViewModels.CarsForSaleItem
{
    public class EditCarInfoViewModel
    {
        private readonly TestDriveInfoService _testNumberPlateService;

        public EditCarInfoViewModel()
        {
            _testNumberPlateService = new TestDriveInfoService();

            MainMenuCommand = new Command(NavigateToMainMenu);
            UpdateCarCommand = new Command(UpdateCar);
            LoadCarImageCommand = new Command(LoadCarImage);
        }

        public bool IsNewCar { get; set; }
        public Car Car { get; set; }
        public string FullCarName { get; set; }
        public INavigation Navigation { get; set; }
        public ICommand MainMenuCommand { get; set; }
        public ICommand UpdateCarCommand { get; set; }
        public ICommand LoadCarImageCommand { get; set; }

        private async void NavigateToMainMenu() => await Navigation.PopToRootAsync(true);

        private async void UpdateCar()
        {
            HttpStatusCode statusCode;

            if (!IsNewCar)
            {
                statusCode = await _testNumberPlateService.UpdateCarAsync(Car);
            }
            else
            {
                statusCode = await _testNumberPlateService.AddCarAsync(Car);
            }

            if (statusCode == HttpStatusCode.OK)
            {
                await Navigation.PopAsync();
            }
        }

        public void LoadCarImage(object item)
        {
            if (!(item is Image carImage)) return;

            Device.BeginInvokeOnMainThread(async () =>
            {
                var mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Small
                });

                if (mediaFile != null)
                {
                    Car.ImageData = await ImageConverter.ToBytesAsync(mediaFile.GetStream());

                    carImage.Source = ImageSource.FromStream(() =>
                    {
                        var stream = mediaFile.GetStream();
                        mediaFile.Dispose();

                        return stream;
                    });
                }
            });
        }
    }
}
