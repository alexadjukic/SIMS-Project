using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels
{
    public class TodaysToursViewModel : ViewModelBase
    {
        #region PROPERTIES
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

        private Checkpoint _selectedCheckpoint;
        public Checkpoint SelectedCheckpoint
        {
            get => _selectedCheckpoint;
            set
            {
                if (_selectedCheckpoint != value)
                {
                    _selectedCheckpoint = value;
                    OnPropertyChanged(nameof(SelectedCheckpoint));
                }
            }
        }

        private Tour? _activeTour;
        public Tour? ActiveTour
        {
            get => _activeTour;
            set
            {
                if (_activeTour != value)
                {
                    _activeTour = value;
                    OnPropertyChanged(nameof(ActiveTour));
                }
            }
        }

        public ObservableCollection<Tour> TodaysTours { get; set; }
        public ObservableCollection<Checkpoint> Checkpoints { get; set; }

        private readonly User _guide;

        private readonly TourService _tourService;
        private readonly CheckpointService _checkpointService; 
        #endregion

        public TodaysToursViewModel(User guide)
        {
            _tourService = new TourService();
            _checkpointService = new CheckpointService();

            _guide = guide;

            TodaysTours = new ObservableCollection<Tour>();
            Checkpoints = new ObservableCollection<Checkpoint>();

            LoadData();

            ActivateTourCommand = new RelayCommand(ActivateTourCommand_Execute, ActivateTourCommand_CanExecute);
            CompleteCheckpointCommand = new RelayCommand(CompleteCheckpointCommand_Execute, CompleteCheckpointCommand_CanExecute);
            EndTourCommand = new RelayCommand(EndTourCommand_Execute, EndTourCommand_CanExecute);
            GuestListCommand = new RelayCommand(GuestListCommand_Execute, GuestListCommand_CanExecute);
        }

        private void LoadTodaysTours()
        {
            TodaysTours.Clear();
            foreach (var tour in _tourService.GetTodaysToursByGuideId(_guide.Id))
            {
                TodaysTours.Add(tour);
                if (_tourService.IsTourActive(tour))
                {
                    ActiveTour = tour;
                }
            }
        }
        private void LoadCheckpoints()
        {
            Checkpoints.Clear();
            if (ActiveTour == null) return;
            foreach (var checkpoint in _checkpointService.GetAllByTour(ActiveTour))
            {
                Checkpoints.Add(checkpoint);
            }
        }
        
        private void LoadData()
        {
            LoadTodaysTours();
            LoadCheckpoints();
        }

        #region COMMANDS
        public RelayCommand ActivateTourCommand { get; }
        public RelayCommand CompleteCheckpointCommand { get; }
        public RelayCommand EndTourCommand { get; }
        public RelayCommand GuestListCommand { get; }

        public void ActivateTourCommand_Execute(object? parameter)
        {
            _tourService.ActivateTour(SelectedTour);
            LoadData();
        }

        public bool ActivateTourCommand_CanExecute(object? parameter)
        {
            return ActiveTour is null && SelectedTour is not null && SelectedTour.Status == TourStatus.NOT_STARTED;
        }

        public void CompleteCheckpointCommand_Execute(object? parameter)
        {
            var isLastCheckpoint = Checkpoints.IndexOf(SelectedCheckpoint) + 1 == Checkpoints.Count;

            _checkpointService.ActivateCheckpoint(SelectedCheckpoint);

            if (isLastCheckpoint)
            {
                _tourService.FinishTour(ActiveTour);
                ActiveTour = null;
            }

            LoadData();
        }

        public bool CompleteCheckpointCommand_CanExecute(object? parameter)
        {
            return SelectedCheckpoint is not null && ActiveTour is not null;
        }

        public void EndTourCommand_Execute(object? parameter)
        {
            _tourService.FinishTour(ActiveTour);
            ActiveTour = null;
            LoadData();
        }

        public bool EndTourCommand_CanExecute(object? parameter)
        {
            return ActiveTour is not null;
        }

        public void GuestListCommand_Execute(object? parameter)
        {
            /*var checkpointArrivalWindow = new CheckpointArrivalWindow(_tourReservationRepository, _checkpointArrivalRepository, _userRepository, SelectedCheckpoint, ActiveTour);
            checkpointArrivalWindow.Show();*/
        }

        public bool GuestListCommand_CanExecute(object? parameter)
        {
            return SelectedCheckpoint is not null;
        }
        #endregion
    }
}
