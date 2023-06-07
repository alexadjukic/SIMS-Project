using InitialProject.Application.UseCases;
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

		private readonly TourService _tourService;
		private readonly TourReservationService _tourReservationService;

		private readonly User _guide;

		private readonly Stack<Tuple<ReversibleCommand, object?>> _commandStack;
        #endregion

        public YourToursViewModel(User guide, Stack<Tuple<ReversibleCommand, object?>> commandStack)
        {
			_guide = guide;
			_commandStack = commandStack;

			_tourService = new TourService();
			_tourReservationService = new TourReservationService();

			PastTours = new ObservableCollection<Tour>();
            FutureTours = new ObservableCollection<Tour>();

			CancelTourCommand = new ReversibleCommand(CancelTourCommand_Execute, CancelTourCommand_CanExecute, CancelTourCommand_Reverse);

			LoadFutureTours();
			LoadPastTours();
        }
		public void LoadFutureTours()
		{
			FutureTours.Clear();
			foreach (var tour in _tourService.GetFutureToursByGuide(_guide))
			{
                FutureTours.Add(tour);
			}
		}

        public void LoadPastTours()
        {
            PastTours.Clear();
            foreach (var tour in _tourService.GetPastToursByGuide(_guide))
            {
                PastTours.Add(tour);
            }
        }

        #region COMMANDS
        public ReversibleCommand CancelTourCommand { get; }
        public void CancelTourCommand_Execute(object? parameter)
		{
			if (MessageBox.Show("Are you sure you want to cancel this tour? You can undo this action later but all the reservations will remain deleted and guests will keep their vouchers!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No) == MessageBoxResult.No) return;
			_tourService.CancelTour(SelectedFutureTour);
			_tourReservationService.DeleteAllReservationsForCancelledTour(SelectedFutureTour);
			LoadFutureTours();

			_commandStack.Push(new Tuple<ReversibleCommand, object?>(CancelTourCommand, parameter));
		}

		public bool CancelTourCommand_CanExecute(object? parameter)
		{
			Tour? tour = parameter as Tour;
            return tour is not null && tour.Status != TourStatus.CANCELED && tour.StartTime.Subtract(DateTime.Now) > TimeSpan.FromDays(2);
        }

		public void CancelTourCommand_Reverse(object? parameter)
		{
			Tour? tour = parameter as Tour;
			_tourService.UncancelTour(tour);
			LoadFutureTours();
		}
        #endregion
    }
}
