﻿using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModels
{
    public class YourToursViewModel : ViewModelBase
    {
        #region PROPERTIES

		private Tour? _selectedPastTour;
		public Tour? SelectedPastTour
		{
			get
			{
				return _selectedPastTour;
			}
			set
			{
				if (_selectedPastTour != value)
				{
					_selectedPastTour = value;
					OnPropertyChanged(nameof(SelectedPastTour));
				}
			}
		}

		private Tour? _selectedFutureTour;
		public Tour? SelectedFutureTour
		{
			get
			{
				return _selectedFutureTour;
			}
			set
			{
				if (_selectedFutureTour != value)
				{
					_selectedFutureTour = value;
					OnPropertyChanged(nameof(SelectedFutureTour));

				}
			}
		}

		public ObservableCollection<Tour> PastTours { get; set; }
        public ObservableCollection<Tour> FutureTours { get; set; }

        private readonly Page _yourToursView;
		private readonly TourService _tourService;
		private readonly TourReservationService _tourReservationService;
        #endregion

        public YourToursViewModel(Page yourToursView)
        {
			_yourToursView = yourToursView;
			_tourService = new TourService();
			_tourReservationService = new TourReservationService();

			PastTours = new ObservableCollection<Tour>();
            FutureTours = new ObservableCollection<Tour>();

			CancelTourCommand = new RelayCommand(CancelTourCommand_Execute, CancelTourCommand_CanExecute);
			CloseWindowCommand = new RelayCommand(CloseWindowCommand_Execute);

			LoadFutureTours();
			LoadPastTours();
        }
		public void LoadFutureTours()
		{
			FutureTours.Clear();
			foreach (var tour in _tourService.GetFutureTours())
			{
                FutureTours.Add(tour);
			}
		}

        public void LoadPastTours()
        {
            PastTours.Clear();
            foreach (var tour in _tourService.GetPastTours())
            {
                PastTours.Add(tour);
            }
        }

        #region COMMANDS
        public RelayCommand CancelTourCommand { get; }
		public RelayCommand CloseWindowCommand { get; }
        public void CancelTourCommand_Execute(object? parameter)
		{
			_tourService.CancelTour(SelectedFutureTour);
			_tourReservationService.DeleteAllReservationsForCancelledTour(SelectedFutureTour);
			LoadFutureTours();
		}

		public bool CancelTourCommand_CanExecute(object? parameter)
		{
			Tour? tour = parameter as Tour;
            return tour is not null && tour.Status != TourStatus.CANCELED && tour.StartTime.Subtract(DateTime.Now).TotalHours > 48;
        }

		public void CloseWindowCommand_Execute(object? parameter)
		{
			//_yourToursView.Close();
		}
        #endregion
    }
}
