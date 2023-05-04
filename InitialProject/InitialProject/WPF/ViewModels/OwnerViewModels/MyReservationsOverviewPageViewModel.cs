using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.WPF.Views;
using InitialProject.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class MyReservationsOverviewPageViewModel : ViewModelBase
    {
        #region
        private AccommodationReservation _selectedReservation;
        public AccommodationReservation SelectedReservation
        {
            get
            {
                return _selectedReservation;
            }
            set
            {
                if (value != _selectedReservation)
                {
                    _selectedReservation = value;
                    OnPropertyChanged(nameof(SelectedReservation));
                }
            }
        }

        private readonly Page _myReservationsOverviewPage;
        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly RatingService _ratingService;

        private readonly RatingRepository _ratingRepository;

        private readonly User _owner;

        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        #endregion

        public MyReservationsOverviewPageViewModel(Page myReservationsOverviewPage, User user)
        {
            _myReservationsOverviewPage = myReservationsOverviewPage;
            _accommodationReservationService = new AccommodationReservationService();
            _ratingService = new RatingService();

            _ratingRepository = new RatingRepository();

            _owner = user;

            AccommodationReservations = new ObservableCollection<AccommodationReservation>();

            LoadReservations();

            SeeReviewCommand = new RelayCommand(SeeReviewCommand_Execute, SeeReviewCommand_CanExecute);
            ReviewCommand = new RelayCommand(ReviewCommand_Execute, ReviewCommand_CanExecute);
        }

        public void LoadReservations()
        {
            AccommodationReservations.Clear();

            foreach (var accommodationReservation in _accommodationReservationService.GetAllByOwnerId(_owner.Id))
            {
                AccommodationReservations.Add(accommodationReservation);
            }
        }

        #region COMMANDS
        public RelayCommand SeeReviewCommand { get; }
        public RelayCommand ReviewCommand { get; }

        public bool SeeReviewCommand_CanExecute(object? parameter)
        {
            return SelectedReservation is not null && _ratingService.FindRatingByReservationId(SelectedReservation.Id) != null;
        }

        public void SeeReviewCommand_Execute(object? parameter)
        {
            RatingOverviewWindow ratingOverviewWindow = new RatingOverviewWindow(SelectedReservation);
            ratingOverviewWindow.Show();
        }

        public bool ReviewCommand_CanExecute(object? parameter)
        {
            return SelectedReservation is not null && _ratingService.FindRatingByReservationId(SelectedReservation.Id) == null;
        }

        public void ReviewCommand_Execute(object? parameter)
        {
            if ((DateTime.Now - SelectedReservation.EndDate).Days < 0)
            {
                MessageBox.Show("Selected reservation can't be rated", "Guest hasn't left yet", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else if (SelectedReservation != null && (DateTime.Now - SelectedReservation.EndDate).Days <= 5 && _ratingRepository.GetAll().Find(r => r.ReservationId == SelectedReservation.Id) == null)
            {
                RatingGuestForm ratingGuestForm1 = new RatingGuestForm(_ratingRepository, SelectedReservation, _owner.Id);
                ratingGuestForm1.ShowDialog();
                return;
            }
            else if ((DateTime.Now - SelectedReservation.EndDate).Days > 5)
            {
                MessageBox.Show("Selected reservation can't be rated", "It's been more than 5 days", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else if (_ratingRepository.GetAll().Find(r => r.ReservationId == SelectedReservation.Id) != null)
            {
                MessageBox.Show("Selected reservation can't be rated", "It is already rated", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            RatingGuestForm ratingGuestForm = new RatingGuestForm(_ratingRepository, SelectedReservation, _owner.Id);
            ratingGuestForm.Show();
        }
        #endregion
    }
}
