using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace InitialProject.WPF.ViewModels.Guest1ViewModels
{
    public class ReservationsViewModel : ViewModelBase
    {
        #region PROPERITES

        private AccommodationReservation? _selectedReservation;
        public AccommodationReservation? SelectedReservation
        {
            get
            {
                return _selectedReservation;
            }
            set
            {
                if (_selectedReservation != value)
                {
                    _selectedReservation = value;
                    OnPropertyChanged(nameof(SelectedReservation));
                }
            }
        }

        public ObservableCollection<AccommodationReservation> Reservations { get; set; }

        private readonly int _guestId;
        private readonly Window _reservationsView;
        private readonly AccommodationReservationService _reservationService;

        #endregion

        public ReservationsViewModel(Window reservationsView, int guestId)
        {
            _reservationsView = reservationsView;
            _reservationService = new AccommodationReservationService();
            _guestId = guestId;

            Reservations = new ObservableCollection<AccommodationReservation>();

            CancelReservationCommand = new RelayCommand(CancelReservationCommand_Execute, CancelReservationCommand_CanExecute);

            LoadReservations();
        }

        public void LoadReservations()
        {
            Reservations.Clear();
            foreach (var reservation in _reservationService.GetGuestsReservations(_guestId))
            {
                Reservations.Add(reservation);
            }
        }

        #region COMMANDS
        public RelayCommand ChangeReservationCommand { get; }
        public RelayCommand CancelReservationCommand { get; }
        public RelayCommand ViewAllChangeRequestsCommand { get; }
        public RelayCommand RateYourStayCommand { get; }

        public void CancelReservationCommand_Execute(object? parameter)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel your reservation?", "Confirm", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _reservationService.CancelReservation(SelectedReservation);
                LoadReservations();
            }
        }

        public bool CancelReservationCommand_CanExecute(object? parameter)
        {
            return SelectedReservation is not null && (SelectedReservation.StartDate - DateTime.Now.Date).Days >= SelectedReservation.Accommodation.MinDaysBeforeCancel;
        }
        #endregion
    }
}
