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
using System.Windows.Input;

namespace InitialProject.WPF.ViewModels
{
    public class YourToursViewModel : ViewModelBase
    {
		private Tour _selectedPastTour;
		public Tour SelectedPastTour
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

		private Tour _selectedFutureTour;
		public Tour SelectedFutureTour
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
					CancelTourCommand.Tour = value;
					OnPropertyChanged(nameof(SelectedFutureTour));

				}
			}
		}

		public ObservableCollection<Tour> PastTours { get; set; }
        public ObservableCollection<Tour> FutureTours { get; set; }

		private TourRepository _tourRepository;

		public CancelTourCommand CancelTourCommand { get; }
		public CloseWindowCommand CloseWindowCommand { get; }

        public YourToursViewModel(Window yourToursView)
        {
			_tourRepository = new TourRepository();
			PastTours = new ObservableCollection<Tour>();
            FutureTours = new ObservableCollection<Tour>(_tourRepository.GetAll());

			CancelTourCommand = new CancelTourCommand(this);
			CloseWindowCommand = new CloseWindowCommand(yourToursView);
        }
		public void LoadFutureTours()
		{
			_tourRepository.Update(SelectedFutureTour);
			FutureTours.Clear();
			foreach (var tour in _tourRepository.GetAll())
			{
                FutureTours.Add(tour);
			}
		}
    }
}
