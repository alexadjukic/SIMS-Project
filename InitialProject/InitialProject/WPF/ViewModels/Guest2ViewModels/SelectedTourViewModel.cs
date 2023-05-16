﻿using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using InitialProject.WPF.Views;
using InitialProject.WPF.Views.Guest2Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.Guest2ViewModels
{
    public class SelectedTourViewModel : ViewModelBase
    {
        #region PROPERTIES

        User LoggedUser { get; set; }
        

        private ObservableCollection<Tour> _availableTours;
        public ObservableCollection<Tour> AvailableTours
        {
            get => _availableTours;
            set
            {
                if (value != _availableTours)
                {
                    _availableTours = value;
                    OnPropertyChanged(nameof(AvailableTours));
                }
            }
        }

        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {
                if (_selectedTour != value)
                {
                    _selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }

        private int? _numberOfNewGuests;
        public int? NumberOfNewGuests
        {
            get => _numberOfNewGuests;
            set
            {
                if (_numberOfNewGuests != value)
                {
                    _numberOfNewGuests = value;
                    OnPropertyChanged(nameof(NumberOfNewGuests));
                }
            }
        }

        private int? _availableSlots;
        public int? AvailableSlots
        {
            get => _availableSlots;
            set
            {
                if (_availableSlots != value)
                {
                    _availableSlots = value;
                    OnPropertyChanged(nameof(AvailableSlots));
                }
            }
        }

        private readonly Window _selectedTourView;
        private readonly VoucherService _voucherService;
        private readonly TourService _tourService;
        private readonly TourReservationService _tourReservationService;
        #endregion

        public SelectedTourViewModel(Window selectedTourView, User user, Tour selectedTour)
        {
            _selectedTourView = selectedTourView;
            LoggedUser = user;
            SelectedTour = selectedTour;

            _voucherService = new VoucherService();
            _tourService = new TourService();
            _tourReservationService = new TourReservationService();

            CancelCommand = new RelayCommand(CancelCommand_Execute);
            ReserveTourCommand = new RelayCommand(ReserveTourCommand_Execute);
            ShowReservedToursCommand = new RelayCommand(ShowReservedToursCommand_Execute);
            OpenNotificationsCommand = new RelayCommand(OpenNotificationsCommand_Execute);
            ShowVouchersCommand = new RelayCommand(ShowVouchersCommand_Execute);
            ShowToursViewCommand = new RelayCommand(ShowToursViewCommand_Execute);

        }

        public void UseVoucher()
        {
            UseVoucherView useVoucherView = new UseVoucherView(LoggedUser);
            useVoucherView.Show();
        }

        public void OfferOtherTours()
        {
            AlternativeTourOffersView alternativeTourOffersView = new AlternativeTourOffersView(LoggedUser, SelectedTour);
            alternativeTourOffersView .Show();
            _selectedTourView.Close();
        }

        private void MakeNewReservation()
        {
            TourReservation tourReservation = new TourReservation();
            _tourReservationService.SaveReservation(SelectedTour.Id, LoggedUser.Id, (int)NumberOfNewGuests, 0); //to-do: add option to enter age
            SelectedTour.MaxGuests = SelectedTour.MaxGuests - (int)NumberOfNewGuests;
            _tourService.UpdateTour(SelectedTour);
        }

        #region COMMANDS

        public RelayCommand CancelCommand { get; }
        public RelayCommand ReserveTourCommand { get; }
        public RelayCommand ShowToursViewCommand { get; }
        public RelayCommand ShowReservedToursCommand { get; }
        public RelayCommand OpenNotificationsCommand { get; }
        public RelayCommand ShowVouchersCommand { get; }

        public void OpenNotificationsCommand_Execute(object? parameter)
        {
            TourNotificationsView tourNotificationsView = new TourNotificationsView(LoggedUser);
            tourNotificationsView.Show();
            _selectedTourView.Close();
        }

        public void ShowVouchersCommand_Execute(object? parameter)
        {
            VouchersView vouchersView = new VouchersView(LoggedUser);
            vouchersView.Show();
            _selectedTourView.Close();
        }

        public void ShowReservedToursCommand_Execute(object? parameter)
        {
            ReservedToursView reservedToursView = new ReservedToursView(LoggedUser);
            reservedToursView.Show();
            _selectedTourView.Close();
        }

        public void ShowToursViewCommand_Execute(object? parameter)
        {
            Guest2TourView guest2TourView = new Guest2TourView(LoggedUser);
            guest2TourView.Show();
            _selectedTourView.Close();
        }

        public void ReserveTourCommand_Execute(object? parameter)
        {
            if (SelectedTour.MaxGuests - NumberOfNewGuests < 0)
            {
                MessageBox.Show("There is no enough slots for this reservation!");
                OfferOtherTours();
            }
            else if (NumberOfNewGuests == 0 || NumberOfNewGuests == null)
            {
                MessageBox.Show("This field can't be empty!");
            }
            else
            {
                UseVoucher();
                MakeNewReservation();
                _selectedTourView.Close();
            }
        }

        public void CancelCommand_Execute(object? parameter)
        {
            Guest2TourView guest2TourView = new Guest2TourView(LoggedUser);
            guest2TourView.Show();
            _selectedTourView.Close();
        }

        #endregion
    }
}