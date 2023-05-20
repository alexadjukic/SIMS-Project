using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views;
using InitialProject.WPF.Views.Guest2Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.Guest2ViewModels
{
    public class Guest2TourViewModel : ViewModelBase
    {
        #region PROPERTIES
        public User LoggedUser { get; set; }

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
            get => _countries;
            set
            {
                if (_countries != value)
                {
                    _countries = value;
                    OnPropertyChanged(nameof(Countries));
                }
            }
        }

        private ObservableCollection<string> _cities;
        public ObservableCollection<string> Cities
        {
            get => _cities;
            set
            {
                if (value != _cities)
                {
                    _cities = value;
                    OnPropertyChanged(nameof(Cities));
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
                    OnPropertyChanged(nameof(Languages));
                }
            }
        }

        private Tour _tour;
        public Tour SelectedTour
        {
            get => _tour;
            set
            {
                if (_tour != value)
                {
                    _tour = value;
                    OnPropertyChanged(nameof(SelectedTour));
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
                    OnPropertyChanged(nameof(SelectedCountry));
                    CountrySelectionChanged();
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
                    OnPropertyChanged(nameof(SelectedCity));
                }
            }
        }

        private double? _tourDuration;
        public double? TourDuration
        {
            get => _tourDuration;
            set
            {
                if (_tourDuration != value)
                {
                    _tourDuration = value;
                    OnPropertyChanged(nameof(TourDuration));
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
                    OnPropertyChanged(nameof(SelectedLanguage));
                }
            }
        }

        private int? _maxGuests;
        public int? MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged(nameof(MaxGuests));
                }
            }
        }

        private bool _cityComboBoxIsEnabled;
        public bool CityComboBoxIsEnabled
        {
            get => _cityComboBoxIsEnabled;
            set
            {
                if(value != _cityComboBoxIsEnabled)
                {
                    _cityComboBoxIsEnabled = value;
                    OnPropertyChanged(nameof(CityComboBoxIsEnabled));
                }
            }
        }

        private readonly Window _guest2TourView;
        private readonly TourService _tourService;
        private readonly LocationService _locationService;

        #endregion
        public Guest2TourViewModel(Window guest2TourView, User user)
        {
            _guest2TourView = guest2TourView;
            LoggedUser = user;

            _tourService = new TourService();
            _locationService = new LocationService();
            Countries = new ObservableCollection<string>();
            Cities = new ObservableCollection<string>();
            Tours = new ObservableCollection<Tour>();
            
            ShowInitialTourOptions();

            CloseWindowCommand = new RelayCommand(CloseWindowCommand_Execute);
            ApplyFilterCommand = new RelayCommand(ApplyFilterCommand_Execute);
            ChooseTourCommand = new RelayCommand(ChooseTourCommand_Execute);
            ShowReservedToursCommand = new RelayCommand(ShowReservedToursCommand_Execute);
            OpenNotificationsCommand = new RelayCommand(OpenNotificationsCommand_Execute);
            ShowVouchersCommand = new RelayCommand(ShowVouchersCommand_Execute);
            ShowTourRequestsCommand = new RelayCommand(ShowTourRequestsCommand_Execute);
            ShowStatisticsCommand = new RelayCommand(ShowStatisticsCommand_Execute);
        }

        private void ShowInitialTourOptions()
        {
            CityComboBoxIsEnabled = false;
            FillCountries();
            FillLanguages();

            foreach (var tour in _tourService.GetTours())
            {
                FillTours(tour);
            }
        }

        public void FillCountries()
        {
            foreach(var country in _tourService.GetCountries())
            {
                Countries.Add(country);
            }
        }
        
        public void FillCities()
        {
            foreach (var location in _locationService.GetLocations())
            {
                if (SelectedCountry == location.Country)
                {
                    Cities.Add(location.City);
                }
            }
        }
        public void FillLanguages()
        {
            Languages = new ObservableCollection<string> { "Serbian", "Hungarian", "German", "Thai", "French", "Italian", "Turkish", "Chinese", "Bulgarian", "Swedish", "Finish", "Croatian", "Bosnian", "Japanese", "Eren Yeager", "Danish", "English", "Romanian", "Greek", "Albanian", "Ukranian", "Russian", "Slovenian", "Slovakian", "Belgian", "Dutch", "Portuguese", "Spanish", "Lithuanian", "Estonian" };
        }

        public void FillTours(Tour tour)
        {
            if (tour.Status == TourStatus.ACTIVE || tour.Status == TourStatus.NOT_STARTED)
            {
                foreach (var location in _locationService.GetLocations())
                {
                    if (location.Id == tour.LocationId)
                    {
                        tour.Location = _locationService.GetLocationById(tour.LocationId);
                    }
                }
                Tours.Add(tour);
            }
        }

        public void RemoveTourByDuration(Tour tour)
        {
            if (TourDuration != 0 && TourDuration.ToString() != "")
            {
                if (TourDuration != tour.Duration)
                {
                    Tours.Remove(tour);
                }
            }
        }

        public void RemoveTourByMaxCapacity(Tour tour)
        {
            if (MaxGuests != 0 && MaxGuests.ToString() != "")
            {
                if (MaxGuests > tour.MaxGuests)
                {
                    if (MaxGuests != tour.MaxGuests)
                    {
                        Tours.Remove(tour);
                    }
                }
            }
        }

        public void RemoveTourByLanguage(Tour tour)
        {
            if (SelectedLanguage != null && SelectedLanguage != "")
            {
                if (SelectedLanguage != tour.Language)
                {
                    Tours.Remove(tour);
                }
            }
        }

        public void RemoveTourByCountry(Tour tour)
        {
            if (SelectedCountry != null && SelectedCountry != "")
            {
                if (SelectedCountry != tour.Location.Country)
                {
                    Tours.Remove(tour);
                }
            }
        }

        public void RemoveTourByCity(Tour tour)
        {
            if (SelectedCity != null && SelectedCity != "")
            {
                if (SelectedCity != tour.Location.City)
                {
                    Tours.Remove(tour);
                }
            }
        }

        public bool IsFull(Tour tour)
        {
            if (tour.MaxGuests == 0)
            {
                return true;
            }

            return false;
        }

        public void ShowAlternativeOptions(Tour tour)
        {
            if (IsFull(tour))
            {
                OfferOtherTours();
            }
        }

        public void OfferOtherTours()
        {
            AlternativeTourOffersView alternativeTourOffers = new AlternativeTourOffersView(LoggedUser, SelectedTour);
            alternativeTourOffers.Show();
            _guest2TourView.Close();
        }

        public void CountrySelectionChanged()
        {
            CityComboBoxIsEnabled = true;
            Cities.Clear();
            FillCities();
            if (SelectedCountry == null)
            {
                CityComboBoxIsEnabled = false;
            }
        }

        #region COMMANDS
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand ApplyFilterCommand { get; }
        public RelayCommand ChooseTourCommand { get; }
        public RelayCommand ShowReservedToursCommand { get; }
        public RelayCommand OpenNotificationsCommand { get; }
        public RelayCommand ShowVouchersCommand { get; }
        public RelayCommand ShowTourRequestsCommand { get; }
        public RelayCommand ShowStatisticsCommand { get; }

        public void ShowStatisticsCommand_Execute(object? parameter)
        {
            RequestedTourStatisticsView requestedTourStatisticsView = new RequestedTourStatisticsView(LoggedUser);
            requestedTourStatisticsView.Show();
            _guest2TourView.Close();
        }

        public void ShowTourRequestsCommand_Execute(object? parameter)
        {
            TourRequestFormView tourRequestFormView = new TourRequestFormView(LoggedUser);
            tourRequestFormView.Show();
            _guest2TourView.Close();
        }

        public void OpenNotificationsCommand_Execute(object? parameter)
        {
            TourNotificationsView tourNotificationsView = new TourNotificationsView(LoggedUser);
            tourNotificationsView.Show();
            _guest2TourView.Close();
        }

        public void ShowVouchersCommand_Execute(object? parameter)
        {
            VouchersView vouchersView = new VouchersView(LoggedUser);
            vouchersView.Show();
            _guest2TourView.Close();
        }

        public void ShowReservedToursCommand_Execute(object? parameter)
        {
            ReservedToursView reservedToursView = new ReservedToursView(LoggedUser);
            reservedToursView.Show();
            _guest2TourView.Close();
        }

        public void ChooseTourCommand_Execute(object? parameter)
        {
            if (SelectedTour == null)
            {
                return;
            }
            if (SelectedTour.MaxGuests == 0)
            {
                MessageBox.Show("Selected tour is full, try picking alternative tour on same location.");
                ShowAlternativeOptions(SelectedTour);
            }
            else
            {
                SelectedTourView selectedTourOverview = new SelectedTourView(SelectedTour, LoggedUser);
                selectedTourOverview.Show();
            }
            _guest2TourView.Close();
        }

        public void ApplyFilterCommand_Execute(object? parameter)
        {
            Tours = new ObservableCollection<Tour>();
            foreach (var tour in _tourService.GetTours())
            {
                FillTours(tour);

                foreach (var location in _locationService.GetLocations())
                {
                    if (location.Id == tour.LocationId)
                    {
                        tour.Location = _locationService.GetLocationById(tour.LocationId);
                    }
                }

                RemoveTourByCountry(tour);
                RemoveTourByCity(tour);
                RemoveTourByDuration(tour);
                RemoveTourByMaxCapacity(tour);
                RemoveTourByLanguage(tour);
            }
        }
        public void CloseWindowCommand_Execute(object? parameter)
        {
            _guest2TourView.Close();
        }
        #endregion
    }
}
