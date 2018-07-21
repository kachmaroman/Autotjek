using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SignaturePad.Forms;
using Xamarin.Forms;
using _100autotjek.Helpers;
using _100autotjek.Localize;
using _100autotjek.Models;
using _100autotjek.Services;
using _100autotjek.Views;
using _100autotjek.Views.DocItem;

namespace _100autotjek.ViewModels.DocItem
{
    public class SignatureViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isLoading;
        private bool _isSendBtnClicked;

        private readonly TestDriveInfoService _testDriveInfoService;

        public INavigation Navigation { get; set; }
        public ICommand SaveTestDriveCommand { get; set; }
        public ICommand MainMenuCommand { get; set; }
        public TestDriveInfo TestDriveInfo { get; set; }
        public string SelectedCarInfo { get; set; }
        public SignaturePadView Signature { get; set; }

        public SignatureViewModel()
        {
            _testDriveInfoService = new TestDriveInfoService();

            SaveTestDriveCommand = new Command(SaveTestDriveAsync);
            MainMenuCommand = new Command(NavigateToMainMenu);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public bool IsSigned => Signature.Points.Count() > 10;

        private async void SaveTestDriveAsync()
        {
            if (!IsSigned) return;

            if (_isSendBtnClicked) return;

            _isSendBtnClicked = true;

            var imageData = await GetSignatureDataAsync(SignatureImageFormat.Png);

            TestDriveInfo.ImageData = imageData;

            IsLoading = true;
            var statusCode = await _testDriveInfoService.AddTestDriveAsync(TestDriveInfo);
            IsLoading = false;

            string imageResult;
            string messageResult;

            if (statusCode == HttpStatusCode.OK)
            {
                imageResult = (string)Application.Current.Resources["HappyDocIcon"];
                messageResult = AppResources.ResultSuccessMessage;
            }
            else
            {
                imageResult = (string)Application.Current.Resources["UnhappyDocIcon"];
                messageResult = AppResources.ResultErrorMessage;
            }

            var resultViewModel = new ResultViewModel
            {
                ImageResult = imageResult,
                MessageResult = messageResult
            };

            var resultPage = new ResultPage(resultViewModel);

            await Navigation.PushAsync(resultPage, true);

            _isSendBtnClicked = false;
        }

        public async Task<byte[]> GetSignatureDataAsync(SignatureImageFormat format)
        {
            using (var imageStream = await Signature.GetImageStreamAsync(format, new Size(100, 100)))
            {
                return await ImageConverter.ToBytesAsync(imageStream);
            }
        }

        private async void NavigateToMainMenu() => await Navigation.PopToRootAsync(true);
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

