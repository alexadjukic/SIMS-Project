using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
    /// Interaction logic for Guest2TourOverview.xaml
    /// </summary>
    public partial class Guest2TourOverview : Window, INotifyPropertyChanged
    {
        public User LoggedUser { get; set; }
        public readonly TourRepository _tourRepository;
        public readonly LocationRepository _locationRepository;
        public readonly TourImageRepository _tourImageRepository;

        private ObservableCollection<Tour> _tours;
        public ObservableCollection<Tour> Tours 
        { 
            get => _tours;
            set
            {
                if (_tours != value)
                {
                    _tours = value;
                    OnPropertyChanged("Tours");
                }
            } 
        }

        private ObservableCollection<string> _countries;
        public ObservableCollection<string> Countries 
        { 
            get=>_countries;
            set
            {
                if (_countries != value)
                {
                    _countries = value;
                    OnPropertyChanged("Countries");
                }
            } 
        }

        private ObservableCollection<string> _cities;
        public ObservableCollection<string> Cities 
        { 
            get=>_cities;
            set
            {
                if(value != _cities)
                {
                    _cities = value;
                    OnPropertyChanged("Cities");
                }
            } 
        }

        private ObservableCollection<string> _languages;
        public ObservableCollection<string> Languages 
        { 
            get => _languages;
            set
            {
                if (value != _languages)
                {
                    _languages = value;
                    OnPropertyChanged("Languages");
                }
            }
        }

        private Tour _tour;
        public Tour SelectedTour 
        { 
            get => _tour;
            set
            {
                if(_tour != value)
                {
                    _tour = value;
                    OnPropertyChanged("SelectedTour");
                }
            } 
        }

        private string _country;
        public string SelectedCountry
        {
            get => _country;
            set
            {
                if (_country != value)
                {
                    _country = value;
                    OnPropertyChanged("SelectedCountry");
                }
            }
        }

        private string _city;
        public string SelectedCity
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged("SelectedCity");
                }
            }
        }

        private string _tourDuration;
        public string TourDuration 
        { 
            get => _tourDuration;
            set
            {
                if(_tourDuration != value)
                {
                    _tourDuration = value;
                    OnPropertyChanged("TourDuration");
                }
            } 
        }

        private string _language;
        public string SelectedLanguage 
        { 
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged("SelectedLanguage");
                }
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Guest2TourOverview(TourRepository tourRepository, LocationRepository locationRepository, TourImageRepository tourImageRepository)
        {
            InitializeComponent();
            DataContext = this;
            _tourRepository = tourRepository;
            _locationRepository = locationRepository;
            _tourImageRepository = tourImageRepository;
            Countries = new ObservableCollection<string>();
            Cities = new ObservableCollection<string>();
            //Tours = new ObservableCollection<Tour>();
            ShowInitialOptions();
        }

        private void ShowInitialOptions()
        {
            foreach (var location in _locationRepository.GetAll())
            {
                if (!Countries.Contains(location.Country))
                {
                    Countries.Add(location.Country);
                }
            }
            
            Languages = new ObservableCollection<string> {"Serbian", "Hungarian", "German", "Thai", "French", "Italian", "Turkish", "Chinese", "Bulgarian", "Swedish", "Finish", "Croatian", "Bosnian", "Japanese", "Eren Yeager", "Danish", "English", "Romanian", "Greek", "Albanian", "Ukranian", "Russian", "Slovenian", "Slovakian", "Belgian", "Dutch", "Portuguese", "Spanish", "Lithuanian", "Estonian"};                                     
            
            Tours = new ObservableCollection<Tour>();
            foreach (var tour in _tourRepository.GetAll())
            {
                foreach(var location in _locationRepository.GetAll())
                {
                    if(location.Id == tour.LocationId)
                    {
                        tour.Location = _locationRepository.GetById(location.Id);
                        //tour.Location.Country = location.Country;
                        //tour.Location.City = location.City;
                    }
                }
                Tours.Add(tour);
            }
        }

        private void ComboBoxCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxCity.IsEnabled = true;
            Cities.Clear();
            
            foreach(var location in _locationRepository.GetAll())
            {
                if (SelectedCountry == location.Country)
                {
                    Cities.Add(location.City);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
