using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels
{
    public class TourStatisticsSelectionViewModel : ViewModelBase
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

        private readonly Window _tourStatisticsSelectionView;

        private readonly TourService _tourService;
        #endregion
        public TourStatisticsSelectionViewModel(Window tourStatisticsSelectionView)
        {
            _tourStatisticsSelectionView = tourStatisticsSelectionView;

            _tourService = new TourService();

            PastTours = new ObservableCollection<Tour>(_tourService.GetPastTours());

            OpenStatsCommand = new RelayCommand(OpenStatsCommand_Execute, OpenStatsCommand_CanExecute);
            CloseWindowCommand = new RelayCommand(CloseWindowCommand_Execute);
            MostVisitedTourCommand = new RelayCommand(MostVisitedTourCommand_Execute);
        }

        #region COMMANDS
        public RelayCommand OpenStatsCommand { get; }
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand MostVisitedTourCommand { get; }

        public void MostVisitedTourCommand_Execute(object? parameter)
        {
            var mostVisitedTourView = new MostVisitedTourView();
            mostVisitedTourView.Show();
        }

        public void OpenStatsCommand_Execute(object? parameter)
        {
            var tourStatisticsView = new TourStatisticsView(SelectedTour);
            tourStatisticsView.Show();
        }

        public bool OpenStatsCommand_CanExecute(object? parameter)
        {
            return SelectedTour is not null;
        }

        public void CloseWindowCommand_Execute(object? parameter)
        {
            _tourStatisticsSelectionView.Close();
        } 
        #endregion
    }
}
