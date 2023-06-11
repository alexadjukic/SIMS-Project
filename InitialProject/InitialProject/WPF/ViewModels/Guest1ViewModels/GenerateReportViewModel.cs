using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Printing;
using System.Reflection.Metadata;
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

            GenerateReportCommand = new RelayCommand(GenerateReportCommand_Execute);
        }

        #region COMMANDS
        public RelayCommand GenerateReportCommand { get; set; }
        public void GenerateReportCommand_Execute(object? parameter)
        {

        }
        #endregion
    }
}
