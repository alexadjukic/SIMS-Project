using InitialProject.Application.UseCases;
using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class AccommodationMonthlyStatisticsOverviewViewModel : ViewModelBase
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

        private int _mostTakenMonth;
        public int MostTakenMonth
        {
            get
            {
                return _mostTakenMonth;
            }
            set
            {
                if (_mostTakenMonth != value)
                {
                    _mostTakenMonth = value;
                    OnPropertyChanged(nameof(MostTakenMonth));
                }

            }
        }

        public ObservableCollection<AccommodationMonthStatistics> AccommodationMonthStatistics { get; set; }

        private readonly AccommodationMonthStatisticsService _accommodationMonthStatisticsService;
        private readonly AccommodationReservationService _accommodationReservationService;
        #endregion

        public AccommodationMonthlyStatisticsOverviewViewModel(Accommodation selectedAccommodation, AccommodationYearStatistic selectedYearStatistic)
        {
            SelectedAccommodation = selectedAccommodation;
            SelectedYearStatistics = selectedYearStatistic;

            AccommodationMonthStatistics = new ObservableCollection<AccommodationMonthStatistics>();

            _accommodationMonthStatisticsService = new AccommodationMonthStatisticsService();
            _accommodationReservationService = new AccommodationReservationService();

            LoadMonthStatistics();
            FindMostTakenMonth();
        }

        private void LoadMonthStatistics()
        {
            foreach (var monthStatistic in _accommodationMonthStatisticsService.GetAllByYearStatistic(SelectedYearStatistics.Id))
            {
                AccommodationMonthStatistics.Add(monthStatistic);
            }
        }

        private void FindMostTakenMonth()
        {
            MostTakenMonth = 1;
            int maxNumberOfTakenDays = 0;

            foreach (var monthStatistics in _accommodationMonthStatisticsService.GetAllByYearStatistic(SelectedYearStatistics.Id))
            {
                int numberOfTakenDaysInMonth = _accommodationReservationService.GetNumberOfTakenDaysInMonthByAccommodationId(monthStatistics.Month, SelectedYearStatistics);

                if (numberOfTakenDaysInMonth > maxNumberOfTakenDays)
                {
                    maxNumberOfTakenDays = numberOfTakenDaysInMonth;
                    MostTakenMonth = monthStatistics.Month;
                }
            }
            
        }

        #region COMMANDS
        #endregion
    }
}
