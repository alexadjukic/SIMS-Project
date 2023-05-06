﻿using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.Guest1Views;
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
        #region PROPERTIES
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
        private readonly AccommodationRatingService _ratingService;
        private readonly AccommodationNotificationService _accommodationNotificationService;
        #endregion

        public ReservationsViewModel(Window reservationsView, int guestId)
        {
            _reservationsView = reservationsView;
            _reservationService = new AccommodationReservationService();
            _ratingService = new AccommodationRatingService();
            _accommodationNotificationService = new AccommodationNotificationService();
            _guestId = guestId;

            Reservations = new ObservableCollection<AccommodationReservation>();

            CancelReservationCommand = new RelayCommand(CancelReservationCommand_Execute, CancelReservationCommand_CanExecute);
            RateYourStayCommand = new RelayCommand(RateYourStayCommand_Execute, RateYourStayCommand_CanExecute);
            ChangeReservationCommand = new RelayCommand(ChangeReservationCommand_Execute, ChangeReservationCommand_CanExecute);
            ViewAllChangeRequestsCommand = new RelayCommand(ViewAllChangeRequestsCommand_Execute);

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

        private bool IsReservationRated()
        {
            return _ratingService.FindAccommodationRatingByReservationId(SelectedReservation.Id) != null;
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
                _accommodationNotificationService.NotifyUser($"{SelectedReservation.Guest.Username} has cancelled the reservation for {SelectedReservation.Accommodation.Name}.", _guestId, SelectedReservation.Accommodation.OwnerId);
                LoadReservations();
            }
        }

        public bool CancelReservationCommand_CanExecute(object? parameter)
        {
            return SelectedReservation is not null && (SelectedReservation.StartDate - DateTime.Now.Date).Days >= SelectedReservation.Accommodation.MinDaysBeforeCancel;
        }

        public void RateYourStayCommand_Execute(object? parameter)
        {
            MainWindow.mainWindow.MainPreview.Content = new AccommodationRatingFormPage(new AccommodationRatingFormViewModel(_reservationsView, SelectedReservation));
        }

        public bool RateYourStayCommand_CanExecute(object? parameter)
        {
            return SelectedReservation is not null && (DateTime.Now - SelectedReservation.EndDate).Days < 6 
                && DateTime.Compare(DateTime.Now.Date, SelectedReservation.EndDate.Date) >= 0
                && !IsReservationRated();
        }

        public void ChangeReservationCommand_Execute(object? parameter)
        {
            ReservationChangeView reservationChangeView = new ReservationChangeView(SelectedReservation);
            reservationChangeView.Show();
        }

        public bool ChangeReservationCommand_CanExecute(object? parameter)
        {
            return SelectedReservation is not null && (SelectedReservation.StartDate - DateTime.Now.Date).Days > 0;
        }

        public void ViewAllChangeRequestsCommand_Execute(object? parameter)
        {
            ReservationChangeRequestsView reservationChangeRequestsView = new ReservationChangeRequestsView(_guestId);
            reservationChangeRequestsView.Show();
        }
        #endregion
    }
}
