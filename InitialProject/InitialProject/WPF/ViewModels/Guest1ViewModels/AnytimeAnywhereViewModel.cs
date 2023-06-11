using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.DTOs;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.Guest1Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.Guest1ViewModels
{
    public class AnytimeAnywhereViewModel : ViewModelBase
    {
        #region PROPERTIES
        private DateTime _selectedStartDate;
        public DateTime SelectedStartDate
        {
            get
            {
                return _selectedStartDate;
            }
            set
            {
                if (_selectedStartDate != value)
                {
                    _selectedStartDate = value;
                    OnPropertyChanged(nameof(SelectedStartDate));
                }
            }
        }
        private DateTime _selectedEndDate;
        public DateTime SelectedEndDate
        {
            get
            {
                return _selectedEndDate;
            }
            set
            {
                if (_selectedEndDate != value)
                {
                    _selectedEndDate = value;
                    OnPropertyChanged(nameof(SelectedEndDate));
                }
            }
        }
        private string _durationOfStay;
        public string DurationOfStay
        {
            get
            {
                return _durationOfStay;
            }
            set
            {
                if (_durationOfStay != value)
                {
                    _durationOfStay = value;
                    OnPropertyChanged(nameof(DurationOfStay));
                }
            }
        }
        private string _guestNumber;
        public string GuestNumber
        {
            get
            {
                return _guestNumber;
            }
            set
            {
                if (_guestNumber != value)
                {
                    _guestNumber = value;
                    OnPropertyChanged(nameof(GuestNumber));
                }
            }
        }
        private AccommodationInfo _selectedAccommodation;
        public AccommodationInfo SelectedAccommodation
        {
            get
            {
                return _selectedAccommodation;
            }
            set
            {
                if (_selectedAccommodation != value)
                {
                    _selectedAccommodation = value;
                    OnPropertyChanged(nameof(SelectedAccommodation));
                }
            }
        }

        public ObservableCollection<AccommodationInfo> Accommodations { get; set; }
        public User LoggedUser { get; set; }

        private readonly AccommodationService _accommodationService;
        private readonly AccommodationAvailabilityService _accommodationAvailabilityService;
        private readonly AccommodationReservationService _accommodationReservationService;
        #endregion

        public AnytimeAnywhereViewModel(User user)
        {
            _accommodationService = new AccommodationService();
            _accommodationAvailabilityService = new AccommodationAvailabilityService();
            _accommodationReservationService = new AccommodationReservationService();

            LoggedUser = user;

            Accommodations = new ObservableCollection<AccommodationInfo>();

            SearchDatesCommand = new RelayCommand(SearchDatesCommand_Execute);
            MakeReservationCommand = new RelayCommand(MakeReservationCommand_Execute);
        }



        #region COMMANDS
        public RelayCommand SearchDatesCommand { get; }
        public RelayCommand MakeReservationCommand { get; }

        public void SearchDatesCommand_Execute(object? parameter)
        {
            Accommodations.Clear();
            var accommodations = _accommodationAvailabilityService.GetAllAvailableAccommodations(SelectedStartDate, SelectedEndDate, Convert.ToInt32(DurationOfStay));
            foreach (var accommodation in accommodations)
            {
                if (accommodation.MaxGuests >= Convert.ToInt32(GuestNumber) && accommodation.MinReservationDays <= Convert.ToInt32(DurationOfStay))
                {
                    foreach (var date in accommodation.AvailableDates)
                    {
                        Accommodations.Add(new AccommodationInfo(accommodation.Id, accommodation.Name, accommodation.Country, accommodation.City, accommodation.Type, accommodation.MaxGuests, accommodation.MinReservationDays, date.StartDate, date.EndDate, accommodation.Dates));
                    }
                }
            }
        }
        public void MakeReservationCommand_Execute(object? parameter)
        {
            _accommodationReservationService.Save(SelectedAccommodation.StartDate, SelectedAccommodation.EndDate, Convert.ToInt32(DurationOfStay), new Accommodation(), SelectedAccommodation.Id, LoggedUser, LoggedUser.Id);
            MainWindow.mainWindow.MainPreview.Content = new ReservationsPage(new ReservationsViewModel(LoggedUser.Id));
        }
        #endregion
    }
}
