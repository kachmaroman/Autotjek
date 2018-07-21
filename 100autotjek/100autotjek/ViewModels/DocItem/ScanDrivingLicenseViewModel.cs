using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using _100autotjek;
using System.Linq;
using System.Text.RegularExpressions;
using _100autotjek.Localize;
using _100autotjek.Models;
using _100autotjek.OCR;
using _100autotjek.Services;
using _100autotjek.Views;
using _100autotjek.Views.DocItem;

namespace _100autotjek.ViewModels.DocItem
{
    public class ScanDrivingLicenseViewModel : INotifyPropertyChanged
    {
        public const string CprNumberTemplate = @"\d{6}-\d{4}";

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly TestDriveInfoService _testDriveInfoService;

        private bool _isLoading;
        private bool _isCameraBtnClicked;
        private bool _isContinueBtnClicked;

        public ScanDrivingLicenseViewModel()
        {
            _testDriveInfoService = new TestDriveInfoService();

            IsLoading = false;
            TestDriveInfo = new TestDriveInfo
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            TakePictureCommand = new Command(TakePicture);
            ContinueCommand = new Command(Continue);
            MainMenuCommand = new Command(NavigateToMainMenu);
        }

        public TestDriveInfo TestDriveInfo { get; set; }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public INavigation Navigation { get; set; }
        public ICommand TakePictureCommand { get; set; }
        public ICommand ContinueCommand { get; set; }
        public ICommand MainMenuCommand { get; set; }


        private async Task<bool> CheckPermissionsAsync(params Permission[] permissions)
        {
            foreach (var permission in permissions)
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);

                if (status != PermissionStatus.Granted)
                {
                    return await Task.FromResult(false);
                }
            }

            return await Task.FromResult(true);
        }

        private bool IsCPRNumberCorrect(string cprNumber) =>
            !string.IsNullOrEmpty(cprNumber) && Regex.IsMatch(cprNumber, $"^{CprNumberTemplate}$");

        private async void TakePicture()
        {
            if (_isCameraBtnClicked) return;

            _isCameraBtnClicked = true;

            await CrossMedia.Current.Initialize();

            var isCameraAvailable = CrossMedia.Current.IsCameraAvailable;

            if (!isCameraAvailable)
            {
                await Application.Current.MainPage.DisplayAlert(AppResources.Alert_NoCameraErrorTitle,
                                                                AppResources.Alert_NoCameraErrorMessage,
                                                                AppResources.Alert_Cancel);

                return;
            }

            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

            var isGranted = await CheckPermissionsAsync(Permission.Camera);

            if (!isGranted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);

                cameraStatus = results[Permission.Camera];

                if (cameraStatus != PermissionStatus.Granted)
                {
                    var isOpen = await Application.Current.MainPage.DisplayAlert(AppResources.Alert_PermissionErrorTitle,
                                                                                 AppResources.Alert_PermissionErrorMessage,
                                                                                 AppResources.Alert_EnableCameraAccess,
                                                                                 AppResources.Alert_Cancel);

                    _isCameraBtnClicked = false;

                    if (!isOpen) return;

                    CrossPermissions.Current.OpenAppSettings();

                    isGranted = await CheckPermissionsAsync(Permission.Camera);

                    if (!isGranted) return;
                }
            }

            var cameraOptions = new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Small,
                SaveToAlbum = false,
                DefaultCamera = CameraDevice.Rear,
            };

            var mediaFile = await CrossMedia.Current.TakePhotoAsync(cameraOptions);

            if (mediaFile != null)
            {
                IsLoading = true;

                try
                {
                    TestDriveInfo.SocialSecurityNumber = await TextRecognizer.GetCPRNumber(mediaFile.GetStream(), CprNumberTemplate);
                   
                    if (string.IsNullOrEmpty(TestDriveInfo.SocialSecurityNumber))
                    {
                        await Application.Current.MainPage.DisplayAlert($"{AppResources.Alert_CPRNumberErrorTitle}",
                                                                        $"{AppResources.Alert_CPRNumberErrorMessage}",
                                                                        $"{AppResources.Alert_Cancel}");
                    }
                }
                catch
                {
                    TestDriveInfo.SocialSecurityNumber = string.Empty;

                    await Application.Current.MainPage.DisplayAlert($"{AppResources.Alert_InternetConnectionErrorTitle}",
                                                                    $"{AppResources.Alert_InternetConnectionErrorMessage}",
                                                                    $"{AppResources.Alert_Cancel}");
                }
                finally
                {
                    mediaFile.Dispose();
                    IsLoading = false;
                }
            }

            _isCameraBtnClicked = false;
        }

        private async void Continue()
        {
            if (_isContinueBtnClicked || _isCameraBtnClicked) return;

            _isContinueBtnClicked = true;

            if (!IsCPRNumberCorrect(TestDriveInfo.SocialSecurityNumber))
            {
                await Application.Current.MainPage.DisplayAlert($"{AppResources.Alert_CPRNumberErrorTitle}",
                                                                $"{AppResources.Alert_CPRNumberErrorMessage}",
                                                                $"{AppResources.Alert_Cancel}");

                _isContinueBtnClicked = false;

                return;
            }

            IsLoading = true;
            _isCameraBtnClicked = true;

            var driverInfo = await _testDriveInfoService.GetDriverInfoAsync(TestDriveInfo.SocialSecurityNumber);

            if (driverInfo != null)
            {
                TestDriveInfo.FirstName = driverInfo.FirstName;
                TestDriveInfo.LastName = driverInfo.LastName;
                TestDriveInfo.Phone = driverInfo.Phone;
                TestDriveInfo.Address = driverInfo.Address;
                TestDriveInfo.Email = driverInfo.Email;
                TestDriveInfo.PostCode = driverInfo.PostCode;
                TestDriveInfo.City = driverInfo.City;
            }
            else
            {
                await DisplayErrorMessage($"{AppResources.Alert_DriverInfoLoadErrorTitle}",
                                          $"{AppResources.Alert_DriverInfoLoadErrorMessage}",
                                          $"{AppResources.Alert_Cancel}");
            }

            var numberPlates = await _testDriveInfoService.GetNumberPlatesAsync(5);

            if (numberPlates != null)
            {
                var driverViewModel = new TestDriveInfoViewModel
                {
                    TestDriveInfo = TestDriveInfo,
                    TestNumberPlates = numberPlates
                };

                var driverInfoPage = new TestDriveInfoPage(driverViewModel);

                await Navigation.PushAsync(driverInfoPage, true);
            }
            else
            {
                await DisplayErrorMessage($"{AppResources.Alert_NumberPlatesLoadErrorTitle}",
                                          $"{AppResources.Alert_NumberPlatesLoadErrorMessage}",
                                          $"{AppResources.Alert_Cancel}");
            }

            IsLoading = false;
            _isContinueBtnClicked = false;
            _isCameraBtnClicked = false;
        }

        private async Task DisplayErrorMessage(string title, string message, string cancel) =>
            await Application.Current.MainPage.DisplayAlert($"{title}", $"{message}", $"{cancel}");

        private async void NavigateToMainMenu() => await Navigation.PopToRootAsync(true);

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
