using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.Guest1Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.Guest1ViewModels
{
    public class RenovationSuggestionViewModel : ViewModelBase
    {
        #region PROPERTIES
        private string _comment;
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }

        private bool[] _urgencyModeArray = new bool[] { true, false, false, false, false };
        public bool[] UrgencyModeArray
        {
            get { return _urgencyModeArray; }
        }
        public int UrgencySelectedMode
        {
            get { return Array.IndexOf(_urgencyModeArray, true);  }
        }

        public AccommodationReservation SelectedReservation { get; }
        private readonly AccommodationRenovationSuggestionService _accommodationRenovationSuggestionService;
        #endregion

        public RenovationSuggestionViewModel(AccommodationReservation reservation)
        {
            _accommodationRenovationSuggestionService = new AccommodationRenovationSuggestionService();
            SelectedReservation = reservation;
            SubmitReviewCommand = new RelayCommand(SubmitReviewCommand_Execute);
        }

        #region COMMANDS
        public RelayCommand SubmitReviewCommand { get; }

        public void SubmitReviewCommand_Execute(object? parameter)
        {
            _accommodationRenovationSuggestionService.SaveSuggestion(Comment, UrgencySelectedMode + 1, SelectedReservation.Id, SelectedReservation.Accommodation.OwnerId, SelectedReservation.GuestId);
            MainWindow.mainWindow.MainPreview.Content = new ReservationsPage(new ReservationsViewModel(SelectedReservation.GuestId));
        }
        #endregion
    }
}
