using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
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
        #endregion

        public AccommodationMonthlyStatisticsOverviewViewModel(Accommodation selectedAccommodation, AccommodationYearStatistic selectedYearStatistic)
        {
            SelectedAccommodation = selectedAccommodation;
            SelectedYearStatistics = selectedYearStatistic;
        }

        #region COMMANDS
        #endregion
    }
}
