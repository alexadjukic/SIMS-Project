using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels
{
    public class CheckpointArrivalViewModel : ViewModelBase
    {
        #region PROPERTIES
        private User _selectedArrivedGuest;
        public User SelectedArrivedGuest
        {
            get => _selectedArrivedGuest;
            set
            {
                if (_selectedArrivedGuest != value)
                {
                    _selectedArrivedGuest = value;
                    OnPropertyChanged(nameof(SelectedArrivedGuest));
                }
            }
        }

        private User _selectedUnarrivedGuest;
        public User SelectedUnarrivedGuest
        {
            get => _selectedUnarrivedGuest;
            set
            {
                if (_selectedUnarrivedGuest != value)
                {
                    _selectedUnarrivedGuest = value;
                    OnPropertyChanged(nameof(SelectedUnarrivedGuest));
                }
            }
        }

        public ObservableCollection<User> ArrivedGuests { get; set; }
        public ObservableCollection<User> UnarrivedGuests { get; set; }

        private Checkpoint _checkpoint;
        private Tour _tour;

        private readonly Window _checkpointArrivalView;
        private readonly TourReservationService _tourReservationService;
        private readonly CheckpointArrivalService _checkpointArrivalService;
        private readonly TourNotificationService _tourNotificationService;
        #endregion

        public CheckpointArrivalViewModel(Checkpoint checkpoint, Tour tour, Window checkpointArrivalView)
        {
            _checkpointArrivalView = checkpointArrivalView;

            _tourReservationService = new TourReservationService();
            _checkpointArrivalService = new CheckpointArrivalService();
            _tourNotificationService = new TourNotificationService();

            _checkpoint = checkpoint;
            _tour = tour;

            ArrivedGuests = new ObservableCollection<User>();
            UnarrivedGuests = new ObservableCollection<User>();
            LoadData();

            RemoveGuestCommand = new RelayCommand(RemoveGuestCommand_Execute, RemoveGuestCommand_CanExecute);
            AddGuestCommand = new RelayCommand(AddGuestCommand_Execute, AddGuestCommand_CanExecute);
            OkCommand = new RelayCommand(OkCommand_Execute, OkCommand_CanExecute);
        }

        private void LoadData()
        {
            LoadArrivedGuests();
            LoadUnarrivedGuests();
        }

        private void LoadUnarrivedGuests()
        {
            UnarrivedGuests.Clear();
            foreach (var tourReservation in _tourReservationService.GetAllByTour(_tour))
            {
                if (_checkpointArrivalService.GetByReservation(tourReservation) == null)
                {
                    UnarrivedGuests.Add(tourReservation.User);
                }
            }
        }

        private void LoadArrivedGuests()
        {
            ArrivedGuests.Clear();
            foreach (var tourReservation in _tourReservationService.GetAllByTour(_tour))
            {
                if (_checkpointArrivalService.GetByReservationAndCheckpoint(tourReservation, _checkpoint) != null)
                {
                    ArrivedGuests.Add(tourReservation.User);
                }
            }
        }

        private void DeleteRemovedArrivals()
        {
            foreach (var arrival in _checkpointArrivalService.GetAllByCheckpoint(_checkpoint))
            {
                if (ArrivedGuests.FirstOrDefault(g => g == arrival.Reservation.User) == null)
                {
                    _checkpointArrivalService.Delete(arrival);
                }
            }
        }

        private void CreateNewArrivals()
        {
            var existingArrivals = _checkpointArrivalService.GetAllByCheckpoint(_checkpoint);
            foreach (var user in ArrivedGuests)
            {
                if (existingArrivals.FirstOrDefault(a => a.Reservation.User == user) == null)
                {
                    var arrival = _checkpointArrivalService.Create(_checkpoint, _tourReservationService.GetByTourIdAndUserId(_tour.Id, user.Id));
                    _tourNotificationService.Create(arrival);
                }
            }
        }

        #region COMMANDS
        public RelayCommand RemoveGuestCommand { get; }
        public RelayCommand AddGuestCommand { get; }
        public RelayCommand OkCommand { get; }

        public void RemoveGuestCommand_Execute(object? parameter)
        {
            UnarrivedGuests.Add(SelectedArrivedGuest);
            ArrivedGuests.Remove(SelectedArrivedGuest);
        }

        public bool RemoveGuestCommand_CanExecute(object? parameter)
        {
            return SelectedArrivedGuest is not null;
        }

        public void AddGuestCommand_Execute(object? parameter)
        {
            ArrivedGuests.Add(SelectedUnarrivedGuest);
            UnarrivedGuests.Remove(SelectedUnarrivedGuest);
        }

        public bool AddGuestCommand_CanExecute(object? parameter)
        {
            return SelectedUnarrivedGuest is not null;
        }

        public void OkCommand_Execute(object? parameter)
        {
            DeleteRemovedArrivals();

            CreateNewArrivals();

            _checkpointArrivalView.Close();
        }

        public bool OkCommand_CanExecute(object? parameter)
        {
            return true;
        } 
        #endregion
    }
}
