using InitialProject.Model;
using InitialProject.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationRegistrationForm.xaml
    /// </summary>
    public partial class AccommodationRegistrationForm : Window
    {
        public User LoggedInUser { get; set; }

        public Accommodation SelectedAccommodation { get; set; }

        private readonly AccommodationRepository _repository;
        private readonly LocationRepository _locationRepository;
        private readonly AccommodationImageRepository _imageRepository;

        private int _imageNumber;
        private int _ownerId;

        private string _accommodationName;

        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (_country != value)
                {
                    _country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        private string _type;

        public string Type
        {
            get => _type;
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        private string _capacity;

        public string Capacity
        {
            get => _capacity;
            set
            {
                if (_capacity != value)
                {
                    _capacity = value;
                    OnPropertyChanged("Capacity");
                }
            }
        }

        private string _minDaysForStay;

        public string MinDaysForStay
        {
            get => _minDaysForStay;
            set
            {
                if (_minDaysForStay != value)
                {
                    _minDaysForStay = value;
                    OnPropertyChanged("MinDaysForStay");
                }
            }
        }

        private string _minDaysBeforeCancel;

        public string MinDaysBeforeCancel
        {
            get => _minDaysBeforeCancel;
            set
            {
                if (_minDaysBeforeCancel != value)
                {
                    _minDaysBeforeCancel = value;
                    OnPropertyChanged("MinDaysBeforeCancel");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccommodationRegistrationForm(AccommodationRepository repository, LocationRepository locationRepository, AccommodationImageRepository imageRepository, int ownerId)
        {
            InitializeComponent();
            this.DataContext = this;
            _repository = repository;
            _locationRepository = locationRepository;
            _imageRepository = imageRepository; 
            _imageNumber = 0;
            _ownerId = ownerId;
            MinDaysBeforeCancel = "1";
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            _imageRepository.RemovePicturesForCanceledAccommodation();
            this.Close();
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            int accommodationId = _repository.Save(AccommodationName, City, Country, Type, Capacity, MinDaysForStay, MinDaysBeforeCancel, _ownerId, _locationRepository).Id;
            _imageRepository.AddAccommodationId(accommodationId);
            this.Close();
        }

        private void AccommodationRegistrationLoaded(object sender, RoutedEventArgs e)
        {
            List<string> countries = _locationRepository.GetAllCountries();
            ComboBoxCountry.ItemsSource = countries;
        }

        private void ComboBoxCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsValid)
            {
                ButtonRegister.IsEnabled = true;
            }
            else
            {
                ButtonRegister.IsEnabled = false;
            }

            ComboBoxCity.IsEnabled = true;
            ComboBoxCity.Items.Clear();

            List<string> comboBoxCityItems = new List<string>();

            List<string> cities = new List<string>();

            foreach (var location in _locationRepository.GetAll())
            {
                if (location.Country == ComboBoxCountry.SelectedItem.ToString())
                {
                    cities.Add(location.City);
                }
            }

            comboBoxCityItems = cities;


            foreach (var comboCity in comboBoxCityItems)
            {
                ComboBoxCity.Items.Add(comboCity);
            }
        }

        private void ButtonAddImages_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;)|*.jpg; *.jpeg; *.gif; *.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                TestTextBox.Text = openFileDialog.FileName;
                Uri fileUri = new Uri(openFileDialog.FileName);
                UploadedPicture.Source = new BitmapImage(fileUri);
            }
        }

        private void ButtonSaveImage_Click(object sender, RoutedEventArgs e)
        {

            File.Copy(TestTextBox.Text, System.IO.Path.Combine("../../../Resources/Images", System.IO.Path.GetFileName(TestTextBox.Text)), true);
            LabelPictureSaved.Content = "Image added, if you want to add more images click button 'Add images'";

            _imageNumber++;
            _imageRepository.Save(System.IO.Path.GetFileName(TestTextBox.Text), -1);

            if (IsValid)
            {
                ButtonRegister.IsEnabled = true;
            }
            else
            {
                ButtonRegister.IsEnabled = false;
            }
        }

        private void TestTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsValid)
            {
                ButtonRegister.IsEnabled = true;
            }
            else
            {
                ButtonRegister.IsEnabled = false;
            }
        }

        private Regex _Number = new Regex(@"^[1-9][0-9]*$");

        public bool IsValid
        {
            get
            { 
                if (AccommodationName == null || AccommodationName == "")
                {
                    return false;
                }
                else if (Country == null)
                {
                    return false;
                }
                else if (City == null)
                {
                    return false;
                }
                else if (Type == null)
                {
                    return false;
                }
                else if (Capacity == null || Capacity == "")
                {
                    return false;
                }
                else if (MinDaysForStay == null || MinDaysForStay == "")
                {
                    return false;
                }
                else if (MinDaysBeforeCancel == null || MinDaysBeforeCancel == "")
                {
                    return false;
                }
                else if (_imageNumber == 0)
                {
                    return false;
                }

                Match capacityMatch = _Number.Match(Capacity);
                Match minDaysForStay = _Number.Match(MinDaysForStay);
                Match minDayBeforeCancel = _Number.Match(MinDaysBeforeCancel);

                if (!capacityMatch.Success || !minDaysForStay.Success || !minDayBeforeCancel.Success)
                {
                    return false;
                }

                return true;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsValid)
            {
                ButtonRegister.IsEnabled = true;
            }
            else
            {
                ButtonRegister.IsEnabled = false;
            }
        }
    }
}
