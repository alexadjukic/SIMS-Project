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
        private TourReview _review;
		public TourReview Review
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

        public ObservableCollection<TourReview> Reviews { get; set; }

		private readonly Window _toursUserReviewsView;
        private readonly Tour _tour;
        private readonly TourReviewService _tourReviewService;
        #endregion

        public ToursUserReviewsViewModel(Window toursUserReviewsView, Tour tour)
        {
			_toursUserReviewsView = toursUserReviewsView;
            _tour = tour;

            _tourReviewService = new();
            Reviews = new();

			OpenReviewCommand = new RelayCommand(OpenReviewCommand_Execute, OpenReviewCommand_CanExecute);
			CloseWindowCommand = new RelayCommand(CloseWindowCommand_Execute);

            LoadReviews();
        }

        private void LoadReviews()
        {
            Reviews.Clear();
            foreach (var review in _tourReviewService.GetReviewsByTour(_tour))
            {
                Reviews.Add(review);
            }
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
