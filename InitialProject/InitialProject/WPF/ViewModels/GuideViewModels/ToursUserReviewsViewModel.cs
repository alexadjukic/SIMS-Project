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
    public class ToursUserReviewsViewModel : ViewModelBase
    {
        #region PROPERTIES
        private CheckpointArrival _review;
		public CheckpointArrival Review
		{
			get
			{
				return _review;
			}
			set
			{
				if (_review != value)
				{
					_review = value;
					OnPropertyChanged(nameof(Review));
				}
			}
		}

        public ObservableCollection<CheckpointArrival> Reviews { get; set; }

		private readonly Window _toursUserReviewsView;
        #endregion

        public ToursUserReviewsViewModel(Window toursUserReviewsView)
        {
			_toursUserReviewsView = toursUserReviewsView;

            Reviews = new();

			OpenReviewCommand = new RelayCommand(OpenReviewCommand_Execute, OpenReviewCommand_CanExecute);
			CloseWindowCommand = new RelayCommand(CloseWindowCommand_Execute);
        }

        #region COMMANDS
        public RelayCommand OpenReviewCommand { get; }
        public RelayCommand CloseWindowCommand { get; }

        public void OpenReviewCommand_Execute(object? parameter)
        {
			// TO-DO: implement review window when model is created
        }

        public bool OpenReviewCommand_CanExecute(object? parameter)
        {
			return true;
        }

        public void CloseWindowCommand_Execute(object? parameter)
        {
            _toursUserReviewsView.Close();
        }
        #endregion
    }
}
