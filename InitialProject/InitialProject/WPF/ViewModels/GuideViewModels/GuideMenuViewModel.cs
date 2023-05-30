using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.WPF.ViewModels.GuideViewModels
{
    public class GuideMenuViewModel : ViewModelBase
    {
		#region PROPERTIES
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged(nameof(CurrentViewModel));
                }
            }
        }

        public User Guide { get; set; }
        public string Username { get; set; }

        #endregion

        public GuideMenuViewModel(User guide)
		{
            Username = guide.Username;
            Guide = guide;

            CurrentViewModel = new YourToursViewModel(Guide);

            CreateNewTourCommand = new RelayCommand(CreateNewTourCommand_Execute, CreateNewTourCommand_CanExecute);
            CreateMostWantedTourCommand = new RelayCommand(CreateMostWantedTourCommand_Execute, CreateMostWantedTourCommand_CanExecute);
            YourToursCommand = new RelayCommand(YourToursCommand_Execute, YourToursCommand_CanExecute);
            TodaysToursCommand = new RelayCommand(TodaysToursCommand_Execute, TodaysToursCommand_CanExecute);
            TourRequestsCommand = new RelayCommand(TourRequestsCommand_Execute, TourRequestsCommand_CanExecute);
            ComplexTourRequestsCommand = new RelayCommand(ComplexTourRequestsCommand_Execute, ComplexTourRequestsCommand_CanExecute);
            YourTourStatisticsCommand = new RelayCommand(YourTourStatisticsCommand_Execute, YourTourStatisticsCommand_CanExecute);
            TourRequestStatisticsCommand = new RelayCommand(TourRequestStatisticsCommand_Execute, TourRequestStatisticsCommand_CanExecute);
            ReviewsCommand = new RelayCommand(ReviewsCommand_Execute, ReviewsCommand_CanExecute);
            SettingsCommand = new RelayCommand(SettingsCommand_Execute, SettingsCommand_CanExecute);
        }

        #region COMMANDS
		public RelayCommand CreateNewTourCommand { get; }
		public RelayCommand CreateMostWantedTourCommand { get; }
		public RelayCommand YourToursCommand { get; }
		public RelayCommand TodaysToursCommand { get; }
		public RelayCommand TourRequestsCommand { get; }
		public RelayCommand ComplexTourRequestsCommand { get; }
		public RelayCommand YourTourStatisticsCommand { get; }
		public RelayCommand TourRequestStatisticsCommand { get; }
		public RelayCommand ReviewsCommand { get; }
		public RelayCommand SettingsCommand { get; }
		public RelayCommand LogOutCommand { get; }
        public RelayCommand CloseCommand { get; }

		public void CreateNewTourCommand_Execute(object? parameter)
		{
            CurrentViewModel = new CreateNewTourViewModel(Guide);
		}

		public bool CreateNewTourCommand_CanExecute(object? parameter)
		{
            return CurrentViewModel.GetType() != typeof(CreateNewTourViewModel);
		}

        public void CreateMostWantedTourCommand_Execute(object? parameter)
        {
            CurrentViewModel = new CreateMostWantedTourViewModel(Guide);
        }

        public bool CreateMostWantedTourCommand_CanExecute(object? parameter)
        {
            return CurrentViewModel.GetType() != typeof(CreateMostWantedTourViewModel);
        }

        public void YourToursCommand_Execute(object? parameter)
        {
            CurrentViewModel = new YourToursViewModel(Guide);
        }

        public bool YourToursCommand_CanExecute(object? parameter)
        {
            return CurrentViewModel.GetType() != typeof(YourToursViewModel);
        }

        public void TodaysToursCommand_Execute(object? parameter)
        {
            CurrentViewModel = new TodaysToursViewModel(Guide);
        }

        public bool TodaysToursCommand_CanExecute(object? parameter)
        {
            return CurrentViewModel.GetType() != typeof(TodaysToursViewModel);
        }

        public void TourRequestsCommand_Execute(object? parameter)
        {
            CurrentViewModel = new GuideTourRequestsViewModel(Guide);
        }

        public bool TourRequestsCommand_CanExecute(object? parameter)
        {
            return CurrentViewModel.GetType() != typeof(GuideTourRequestsViewModel);
        }

        public void ComplexTourRequestsCommand_Execute(object? parameter)
        {

        }

        public bool ComplexTourRequestsCommand_CanExecute(object? parameter)
        {
            return true;
        }

        public void YourTourStatisticsCommand_Execute(object? parameter)
        {
            CurrentViewModel = new YourTourStatisticsViewModel(Guide);
        }

        public bool YourTourStatisticsCommand_CanExecute(object? parameter)
        {
            return CurrentViewModel.GetType() != typeof(YourTourStatisticsViewModel);
        }

        public void TourRequestStatisticsCommand_Execute(object? parameter)
        {
            CurrentViewModel = new TourRequestStatisticsViewModel();
        }

        public bool TourRequestStatisticsCommand_CanExecute(object? parameter)
        {
            return CurrentViewModel.GetType() != typeof(TourRequestStatisticsViewModel);
        }

        public void ReviewsCommand_Execute(object? parameter)
        {
            CurrentViewModel = new TourReviewsViewModel(Guide);
        }

        public bool ReviewsCommand_CanExecute(object? parameter)
        {
            return CurrentViewModel.GetType() != typeof(TourReviewsViewModel);
        }

        public void SettingsCommand_Execute(object? parameter)
        {

        }

        public bool SettingsCommand_CanExecute(object? parameter)
        {
            return true;
        }
        #endregion
    }
}
