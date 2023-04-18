using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class RequestsOverviewViewModel : ViewModelBase
    {
        #region PROPERTIES
        private Request _selectedRequest;
        public Request SelectedRequest
        {
            get
            { 
                return _selectedRequest; 
            }
            set
            {
                if (value != _selectedRequest)
                {
                    _selectedRequest = value;
                    OnPropertyChanged(nameof(SelectedRequest));
                }
            }
        }

        public ObservableCollection<Request> Requests { get; set; }

        private readonly RequestService _requestService;
        private readonly Window _requestsOverview;
        private readonly ManageRequestService _manageRequestService;
        private readonly AccommodationNotificationService _accommodationNotificationService;
        private readonly int _ownerId;
        #endregion

        public RequestsOverviewViewModel(Window requestsOverview, int ownerId)
        {
            _requestsOverview = requestsOverview;
            _requestService = new RequestService();
            _manageRequestService = new ManageRequestService();
            _accommodationNotificationService = new AccommodationNotificationService();
            _ownerId = ownerId;

            Requests = new ObservableCollection<Request>();
            LoadOnHoldRequests();

            CloseWindowCommand = new RelayCommand(CloseWindowCommand_Execute);
            DeclineRequestCommand = new RelayCommand(DeclineRequestCommand_Execute, DeclineRequestCommand_CanExecute);
            AcceptedRequestCommand = new RelayCommand(AcceptedRequestCommand_Execute, AcceptedRequestCommand_CanExecute);
            
        }

        public void LoadOnHoldRequests()
        {
            Requests.Clear();

            foreach (var request in _requestService.GetOnHoldRequests())
            {
                if (request.Reservation.Accommodation.OwnerId == _ownerId)
                {
                    Requests.Add(request);
                }
            }
        }

        #region COMMANDS
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand DeclineRequestCommand { get; }
        public RelayCommand AcceptedRequestCommand { get; }

        public void CloseWindowCommand_Execute(object? parameter)
        {
            _requestsOverview.Close();
        }

        public bool DeclineRequestCommand_CanExecute(object? parameter)
        {
            return SelectedRequest is not null;
        }

        public void AcceptedRequestCommand_Execute(object? parameter)
        {
            _manageRequestService.AcceptRequest(SelectedRequest);
            _accommodationNotificationService.NotifyUser($"Date change request for {SelectedRequest.Reservation.Accommodation.Name} is accepted.", _ownerId, SelectedRequest.Reservation.GuestId);
            LoadOnHoldRequests();
        }

        public bool AcceptedRequestCommand_CanExecute(object? parameter)
        {
            int razlikaUDanima = 0;

            if (SelectedRequest != null)
            {
                razlikaUDanima = (SelectedRequest.Reservation.StartDate - DateTime.Now.Date).Days;
            }
            return SelectedRequest is not null && razlikaUDanima >= SelectedRequest.Reservation.Accommodation.MinDaysBeforeCancel;
        }

        public void DeclineRequestCommand_Execute(object? parameter)
        {
            _manageRequestService.DeclineRequest(SelectedRequest);
            _accommodationNotificationService.NotifyUser($"Date change request for {SelectedRequest.Reservation.Accommodation.Name} is declined.", _ownerId, SelectedRequest.Reservation.GuestId);
            LoadOnHoldRequests();
        }
        #endregion
    }
}
