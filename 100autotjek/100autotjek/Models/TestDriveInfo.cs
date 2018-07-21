using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _100autotjek.Models
{
    public class TestDriveInfo : INotifyPropertyChanged
    {
        #region Fields
        private string socialSecurityNumber;
        private string phone;
        private string email;
        private string driverLicence = "20999999";

        private long carId;
        private long plateId;
        private DateTime startDate;
        private DateTime endDate;

        private byte[] imageData;
        #endregion

        #region Properties     
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string UserId { get; set; } = "4076e387-4678-43ed-b3b0-a09c35d15f1a";
        public string SocialSecurityNumber
        {
            get => socialSecurityNumber;
            set
            {
                socialSecurityNumber = value;
                OnPropertyChanged(nameof(SocialSecurityNumber));
            }
        }
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string DriverLicence
        {
            get => driverLicence;
            set
            {
                driverLicence = value;
                OnPropertyChanged(nameof(DriverLicence));
            }
        }
        public long CarId
        {
            get => carId;
            set
            {
                carId = value;
                OnPropertyChanged(nameof(CarId));
            }
        }
        public long PlateId
        {
            get => plateId;
            set
            {
                plateId = value;
                OnPropertyChanged(nameof(PlateId));
            }
        }
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        public byte[] ImageData
        {
            get => imageData;
            set
            {
                imageData = value;
                OnPropertyChanged(nameof(ImageData));
            }
        }
        #endregion

        #region Interface Member
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
