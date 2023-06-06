using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.OwnerViews;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private string _selectedYear;
        public string SelectedYear
        {
            get
            {
                return _selectedYear;
            }
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged(nameof(SelectedYear));
                }

            }
        }

        public ObservableCollection<AccommodationYearStatistic> AccommodationYearStatistics { get; set; }
        public ObservableCollection<String> Years { get; set; }
        public string[] Labels { get; set; }

        private readonly AccommodationYearStatisticsService _accommodationYearStatisticsService;
        private readonly AccommodationReservationService _accommodationReservationService;

        public SeriesCollection SeriesCollection { get; set; }
        public Func<int, string> Formatter { get; set; }
        private HashSet<int> formattedValues = new HashSet<int>();
        #endregion

        public AccommodationStatisticsOverviewWindowViewModel(Accommodation selectedAccommodation)
        {
            SelectedAccommodation = selectedAccommodation;

            _accommodationYearStatisticsService = new AccommodationYearStatisticsService();
            _accommodationReservationService = new AccommodationReservationService();

            AccommodationYearStatistics = new ObservableCollection<AccommodationYearStatistic>();
            Years = new ObservableCollection<string>();

            ShowMonthlyStatisticsCommand = new RelayCommand(ShowMonthlyStatisticsCommand_Execute, ShowMonthlyStatisticsCommand_CanExecute);
            
            Labels = new string[0];

            LoadColumns();
            LoadYearStatistics();
            FindMostTakenYear();
        }

        private void LoadColumns()
        {
            SeriesCollection = new SeriesCollection() {
                new ColumnSeries
                {
                    Title = "Reservations",
                    Values = new ChartValues<int>(),
                    ColumnPadding = -10
                },
                new ColumnSeries
                {
                    Title = "Canceled Reservations",
                    Values = new ChartValues<int>(),
                    ColumnPadding = -10
                },
                new ColumnSeries
                {
                    Title = "Requests",
                    Values = new ChartValues<int>(),
                    ColumnPadding = -10
                },
                new ColumnSeries
                {
                    Title = "Suggestions",
                    Values = new ChartValues<int>(),
                    ColumnPadding = -10
                }
            };
        }

        private void LoadYearStatistics()
        {
            foreach(var yearStatistics in _accommodationYearStatisticsService.GetAllByAccommodationId(SelectedAccommodation.Id))
            {
                AccommodationYearStatistics.Add(yearStatistics);
                Labels = Labels.Concat(new[] { yearStatistics.Year.ToString() }).ToArray();

                SeriesCollection[0].Values.Add(yearStatistics.NumberOfReservations);
                SeriesCollection[1].Values.Add(yearStatistics.NumberOfDeclinedReservations);
                SeriesCollection[2].Values.Add(yearStatistics.NumberOfChangedReservations);
                SeriesCollection[3].Values.Add(yearStatistics.NumberOfRenovationSuggestions);

                Years.Add(yearStatistics.Year.ToString());
            }

            Formatter = value =>
            {
                if (!formattedValues.Contains(value))
                {
                    formattedValues.Add(value);
                    return value.ToString("N0");
                }
                else
                {
                    return string.Empty;
                }
            };
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

        public bool ShowMonthlyStatisticsCommand_CanExecute(object? parameter)
        {
            return SelectedYear != null;
        }

        public void ShowMonthlyStatisticsCommand_Execute(object? parameter)
        {
            SelectedYearStatistics = _accommodationYearStatisticsService.FindYearStatisticsByYearAndAccommodationId(SelectedYear, SelectedAccommodation.Id);
            AccommodationMonthlyStatisticsOverview accommodationMonthlyStatisticsOverview = new AccommodationMonthlyStatisticsOverview(SelectedAccommodation, SelectedYearStatistics);
            accommodationMonthlyStatisticsOverview.Show();
        }
        #endregion
    }
}
