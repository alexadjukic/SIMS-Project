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
		private Page? _currentPage;
		public Page? CurrentPage
		{
			get
			{
				return _currentPage;
			}
			set
			{
				if (_currentPage != value)
				{
					_currentPage = value;
					OnPropertyChanged(nameof(CurrentPage));
				}
			}
		}

        public string Username { get; set; }

        private readonly Window _guideMenuView;
        #endregion

        public GuideMenuViewModel(Window guideMenuView, User guide)
		{
            _guideMenuView = guideMenuView;
            Username = guide.Username;

            CreateNewTourCommand = new RelayCommand(CreateNewTourCommand_Execute, CreateNewTourCommand_CanExecute);
            CreateMostWantedTourCommand = new RelayCommand(CreateMostWantedTourCommand_Execute, CreateMostWantedTourCommand_CanExecute);
            YourToursCommand = new RelayCommand(YourToursCommand_Execute, YourToursCommand_CanExecute);
            TodaysToursCommand = new RelayCommand(TodaysToursCommand_Execute, TodaysToursCommand_CanExecute);
            TourRequestsCommand = new RelayCommand(TourRequestsCommand_Execute, TourRequestsCommand_CanExecute);
            ComplexTourRequestsCommand = new RelayCommand(ComplexTourRequestsCommand_Execute, ComplexTourRequestsCommand_CanExecute);
            YourTourStatisticsCommand = new RelayCommand(YourTourStatisticsCommand_Execute, YourTourStatisticsCommand_CanExecute);
            ReviewsCommand = new RelayCommand(ReviewsCommand_Execute, ReviewsCommand_CanExecute);
            SettingsCommand = new RelayCommand(SettingsCommand_Execute, SettingsCommand_CanExecute);
            LogOutCommand = new RelayCommand(LogOutCommand_Execute, LogOutCommand_CanExecute);
            CloseCommand = new RelayCommand(CloseCommand_Execute);
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

		}

		public bool CreateNewTourCommand_CanExecute(object? parameter)
		{
			return true;
		}

        public void CreateMostWantedTourCommand_Execute(object? parameter)
        {

        }

        public bool CreateMostWantedTourCommand_CanExecute(object? parameter)
        {
            return true;
        }

        public void YourToursCommand_Execute(object? parameter)
        {
            CurrentPage = new YourToursView();
        }

        public bool YourToursCommand_CanExecute(object? parameter)
        {
            return CurrentPage is null || !CurrentPage.Title.Equals("Your Tours");
        }

        public void TodaysToursCommand_Execute(object? parameter)
        {

        }

        public bool TodaysToursCommand_CanExecute(object? parameter)
        {
            return true;
        }

        public void TourRequestsCommand_Execute(object? parameter)
        {

        }

        public bool TourRequestsCommand_CanExecute(object? parameter)
        {
            return true;
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
            CurrentPage = new YourTourStatisticsView();
        }

        public bool YourTourStatisticsCommand_CanExecute(object? parameter)
        {
            return CurrentPage is null || !CurrentPage.Title.Equals("Your Tour Statistics");
        }

        public void TourRequestStatisticsCommand_Execute(object? parameter)
        {

        }

        public bool TourRequestStatisticsCommand_CanExecute(object? parameter)
        {
            return true;
        }

        public void ReviewsCommand_Execute(object? parameter)
        {
            CurrentPage = new TourReviewsView();
        }

        public bool ReviewsCommand_CanExecute(object? parameter)
        {
            return CurrentPage is null || !CurrentPage.Title.Equals("Reviews");
        }

        public void SettingsCommand_Execute(object? parameter)
        {

        }

        public bool SettingsCommand_CanExecute(object? parameter)
        {
            return true;
        }

        public void LogOutCommand_Execute(object? parameter)
        {
            Window signInForm = new SignInForm();
            signInForm.Show();
            _guideMenuView.Close();
        }

        public bool LogOutCommand_CanExecute(object? parameter)
        {
            return true;
        }

        public void CloseCommand_Execute(object? parameter)
        {
            _guideMenuView.Close();
        }
        #endregion
    }
}
