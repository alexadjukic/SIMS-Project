using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class AccommodationStatisticsOverviewWindowViewModel : ViewModelBase
    {
        #region PROPERTIES
        private Accommodation _selectedAccommodation;
        public Accommodation SelectedAccommodation
        {
            get
            {
                return _selectedAccommodation;
            }
            set
            {
                if (_selectedAccommodation != value)
                {
                    _selectedAccommodation = value;
                    OnPropertyChanged(nameof(SelectedAccommodation));
                }

            }
        }

        private int _mostTakenYear;
        public int MostTakenYear
        {
            get
            {
                return _mostTakenYear;
            }
            set
            {
                if (_mostTakenYear != value)
                {
                    _mostTakenYear = value;
                    OnPropertyChanged(nameof(MostTakenYear));
                }

            }
        }

        private AccommodationYearStatistic _selectedYearStatistics;
        public AccommodationYearStatistic SelectedYearStatistics
        {
            get
            {
                return _selectedYearStatistics;
            }
            set
            {
                if (_selectedYearStatistics != value)
                {
                    _selectedYearStatistics = value;
                    OnPropertyChanged(nameof(SelectedYearStatistics));
                }

            }
        }

        public ObservableCollection<AccommodationYearStatistic> AccommodationYearStatistics { get; set; }

        private readonly AccommodationYearStatisticsService _accommodationYearStatisticsService;
        private readonly AccommodationReservationService _accommodationReservationService;
        #endregion

        public AccommodationStatisticsOverviewWindowViewModel(Accommodation selectedAccommodation)
        {
            SelectedAccommodation = selectedAccommodation;

            _accommodationYearStatisticsService = new AccommodationYearStatisticsService();
            _accommodationReservationService = new AccommodationReservationService();

            AccommodationYearStatistics = new ObservableCollection<AccommodationYearStatistic>();

            ShowMonthlyStatisticsCommand = new RelayCommand(ShowMonthlyStatisticsCommand_Execute);

            LoadYearStatistics();
            FindMostTakenYear();
        }

        private void LoadYearStatistics()
        {
            foreach(var yearStatistics in _accommodationYearStatisticsService.GetAllByAccommodationId(SelectedAccommodation.Id))
            {
                AccommodationYearStatistics.Add(yearStatistics);
            }
        }

        private void FindMostTakenYear()
        {
            MostTakenYear = 0;
            int maxNumberOfTakenDays = 0;
            
            foreach (var yearStatistics in _accommodationYearStatisticsService.GetAllByAccommodationId(SelectedAccommodation.Id))
            {
                int numberOfTakenDaysInYear = _accommodationReservationService.GetNumberOfTakenDaysInYearByAccommodationId(yearStatistics.Year, yearStatistics.AccommodationId);

                if (numberOfTakenDaysInYear > maxNumberOfTakenDays)
                {
                    maxNumberOfTakenDays = numberOfTakenDaysInYear;
                    MostTakenYear = yearStatistics.Year;
                }
            }
        }

        #region COMMANDS
        public RelayCommand ShowMonthlyStatisticsCommand { get; }

        public void ShowMonthlyStatisticsCommand_Execute(object? parameter)
        {
            AccommodationMonthlyStatisticsOverview accommodationMonthlyStatisticsOverview = new AccommodationMonthlyStatisticsOverview(SelectedAccommodation, SelectedYearStatistics);
            accommodationMonthlyStatisticsOverview.Show();
        }
        #endregion
    }
}
