﻿using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views;
using System.Collections.ObjectModel;
using System.Windows;

namespace InitialProject.WPF.ViewModels
{
    public class TourReviewsViewModel : ViewModelBase
    {
        #region PROPERTIES
        private Tour? _selectedTour;
        public Tour? SelectedTour
        {
            get
            {
                return _selectedTour;
            }
            set
            {
                if (_selectedTour != value)
                {
                    _selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }

        public ObservableCollection<Tour> PastTours { get; set; }

        private readonly TourService _tourService;
        #endregion

        public TourReviewsViewModel()
        {
            _tourService = new TourService();

            PastTours = new();

            OpenReviewListCommand = new RelayCommand(OpenReviewListCommand_Execute, OpenReviewListCommand_CanExecute);

            LoadPastTours();
        }

        public void LoadPastTours()
        {
            foreach (var tour in _tourService.GetPastTours())
            {
                PastTours.Add(tour);
            }
        }

        #region COMMANDS
        public RelayCommand OpenReviewListCommand { get; }

        public void OpenReviewListCommand_Execute(object? parameter)
        {
            var toursUserReviewsView = new ToursUserReviewsView(SelectedTour);
            toursUserReviewsView.Show();
        }

        public bool OpenReviewListCommand_CanExecute(object? parameter)
        {
            return SelectedTour is not null && SelectedTour.Status != TourStatus.CANCELED;
        }
        #endregion
    }
}
