using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using FFImageLoading.Cache;
using FFImageLoading.Forms;

namespace _100autotjek.Models
{
    public class Car : INotifyPropertyChanged
    {
        #region Fields
        private decimal price;
        private double run;
        private string equipment;
        private byte[] imageData;
        private string imageUrl;
        #endregion

        #region Properties
        public long Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double Run
        {
            get => run;
            set
            {
                run = value;
                OnPropertyChanged(nameof(Run));
            }
        }
        public decimal Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
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
        public string Equipment
        {
            get => equipment;
            set
            {
                equipment = value;
                OnPropertyChanged(nameof(Equipment));
            }
        }
        public string ImageUrl
        {
            get => imageUrl;
            set
            {
                imageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }
        }
        public string Variant { get; set; }
        public string ShortDescription => $"{Model} {Variant}";
        public DateTime LastRegistrationDate { get; set; }
        public DateTime ServiceCheckDate { get; set; }
        #endregion

        #region Interface Member
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
