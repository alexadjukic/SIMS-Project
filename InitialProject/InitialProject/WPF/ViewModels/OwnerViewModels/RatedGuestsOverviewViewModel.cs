using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.Repositories;
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
    public class RatedGuestsOverviewViewModel : ViewModelBase
    {
        #region PROPERTIES
        private AccommodationReservation _selectedAccommodationReservation;
        public AccommodationReservation SelectedAccommodationReservation
        {
            get 
            { 
                return _selectedAccommodationReservation;
            }
            set
            {
                if (_selectedAccommodationReservation != value)
                {
                    _selectedAccommodationReservation = value;
                    OnPropertyChanged(nameof(SelectedAccommodationReservation));
                }
            }
        }

        private double _totalRating;
        public double TotalRating
        {
            get
            {
                return _totalRating;
            }
            set
            {
                if (_totalRating != value)
                {
                    _totalRating = value;
                    OnPropertyChanged(nameof(TotalRating));
                }
            }
        }

        private readonly Page _ratedGuestsOverview;
        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly SetOwnerRoleService _setOwnerRoleService;
        private readonly int _ownerId;

        public ObservableCollection<AccommodationReservation> RatedReservations { get; set; }
        #endregion

        public RatedGuestsOverviewViewModel(Page ratedGuestsOverview, int ownerId)
        {
            _ratedGuestsOverview = ratedGuestsOverview;
            _accommodationReservationService = new AccommodationReservationService();
            _setOwnerRoleService = new SetOwnerRoleService();
            _ownerId = ownerId;

            RatedReservations = new ObservableCollection<AccommodationReservation>();

            SeeReviewCommand = new RelayCommand(SeeReviewCommand_Execute, SeeReviewCommand_CanExecute);
            LoadRatedReservations();
            CalculateTotalRating();
        }

        public void LoadRatedReservations()
        {
            RatedReservations.Clear();

            foreach (var reservation in _accommodationReservationService.GetRatedReservations(_ownerId))
            {
                RatedReservations.Add(reservation);
            }
        }

        private void CalculateTotalRating()
        {
            TotalRating = Math.Round(_setOwnerRoleService.CalculateTotalRating(_ownerId), 2);
        }

        #region COMMANDS
        public RelayCommand SeeReviewCommand { get; }
        public RelayCommand CloseWindowCommand { get; }

        public bool SeeReviewCommand_CanExecute(object? parameter)
        {
            return SelectedAccommodationReservation is not null;
        }

        public void SeeReviewCommand_Execute(object? parameter)
        {
            RatingOverviewWindow ratingOverviewWindow = new RatingOverviewWindow(SelectedAccommodationReservation);
            ratingOverviewWindow.Show();
        }
        #endregion
    }
}
