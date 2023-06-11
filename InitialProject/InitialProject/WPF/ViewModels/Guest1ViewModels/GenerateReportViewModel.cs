using InitialProject.Application.UseCases;
using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModels.Guest1ViewModels
{
    public class GenerateReportViewModel : ViewModelBase
    {
        #region PROPERTIES
        private DateTime _selectedStartDate;
        public DateTime SelectedStartDate
        {
            get
            {
                return _selectedStartDate;
            }
            set
            {
                if (_selectedStartDate != value)
                {
                    _selectedStartDate = value;
                    OnPropertyChanged(nameof(SelectedStartDate));
                }
            }
        }
        private DateTime _selectedEndDate;
        public DateTime SelectedEndDate
        {
            get
            {
                return _selectedEndDate;
            }
            set
            {
                if (_selectedEndDate != value)
                {
                    _selectedEndDate = value;
                    OnPropertyChanged(nameof(SelectedEndDate));
                }
            }
        }
        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public User LoggedUser { get; set; }

        private readonly AccommodationReservationService _accommodationReservationService;
        #endregion
        public GenerateReportViewModel(User user)
        {
            _accommodationReservationService = new AccommodationReservationService();

            Status = "Scheduled";
            LoggedUser = user;
        }

        #region COMMANDS
        #endregion
    }
}
