using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.Guest2Views;
using InitialProject.WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.Guest2ViewModels
{
    public class TourRequestViewModel : ViewModelBase
    {
        #region PROPERTIES
        public User LoggedUser { get; set; }

        private ObservableCollection<TourRequest> _tourRequests;
        public ObservableCollection<TourRequest> TourRequests
        {
            get => _tourRequests;
            set
            {
                if (value != _tourRequests)
                {
                    _tourRequests = value;
                    OnPropertyChanged(nameof(TourRequests));
                }
            }
        }

        private TourRequest _selectedTourRequest;
        public TourRequest SelectedTourRequest
        {
            get => _selectedTourRequest;
            set
            {
                if (value != _selectedTourRequest)
                {
                    _selectedTourRequest = value;
                    OnPropertyChanged(nameof(SelectedTourRequest));
                }
            }
        }

        private readonly Window _tourRequestView;
        private readonly TourRequestService _tourRequestService;
        private readonly LocationService _locationService;
        private readonly UserService _userService;
        #endregion
        public TourRequestViewModel(Window tourRequestView, User user)
        {
            _tourRequestView = tourRequestView;
            _tourRequestService = new TourRequestService();
            _locationService = new LocationService();
            _userService = new UserService();
            TourRequests = new ObservableCollection<TourRequest>();
            LoggedUser = user;

            ShowReservedToursCommand = new RelayCommand(ShowReservedToursCommand_Execute);
            OpenNotificationsCommand = new RelayCommand(OpenNotificationsCommand_Execute);
            ShowVouchersCommand = new RelayCommand(ShowVouchersCommand_Execute);
            ShowToursViewCommand = new RelayCommand(ShowToursViewCommand_Execute);
            ShowTourRequestFormCommand = new RelayCommand(ShowTourRequestFormCommand_Execute);
            ShowStatisticsCommand = new RelayCommand(ShowStatisticsCommand_Execute);

            LoadRequests();
        }

        public void LoadRequests()
        {
            foreach(var request in _tourRequestService.GetAll())
            {
                request.Location = _locationService.GetLocationById(request.LocationId);
                request.Guide = _userService.GetById(request.GuideId);
                
                if (DateTime.Compare(request.RequestArrivalDate.AddDays(2), DateTime.Now) >= 0)
                {
                    TourRequests.Add(request);
                }
                else
                {
                    request.Status = TourRequestStatus.DECLINED;
                    _tourRequestService.Update(request);
                    TourRequests.Add(request);
                }
            }
        }

        #region COMMANDS
        public RelayCommand ShowToursViewCommand { get; }
        public RelayCommand ShowReservedToursCommand { get; }
        public RelayCommand OpenNotificationsCommand { get; }
        public RelayCommand ShowVouchersCommand { get; }
        public RelayCommand ShowTourRequestFormCommand { get; }
        public RelayCommand ShowStatisticsCommand { get; }

        public void ShowStatisticsCommand_Execute(object? parameter)
        {
            RequestedTourStatisticsView requestedTourStatisticsView = new RequestedTourStatisticsView(LoggedUser);
            requestedTourStatisticsView.Show();
            _tourRequestView.Close();
        }

        public void ShowTourRequestFormCommand_Execute(object? parameter)
        {
            TourRequestFormView tourRequestFormView = new TourRequestFormView(LoggedUser);
            tourRequestFormView.Show();
            _tourRequestView.Close();
        }

        public void OpenNotificationsCommand_Execute(object? parameter)
        {
            TourNotificationsView tourNotificationsView = new TourNotificationsView(LoggedUser);
            tourNotificationsView.Show();
            _tourRequestView.Close();
        }

        public void ShowVouchersCommand_Execute(object? parameter)
        {
            VouchersView vouchersView = new VouchersView(LoggedUser);
            vouchersView.Show();
            _tourRequestView.Close();
        }

        public void ShowReservedToursCommand_Execute(object? parameter)
        {
            ReservedToursView reservedToursView = new ReservedToursView(LoggedUser);
            reservedToursView.Show();
            _tourRequestView.Close();
        }

        public void ShowToursViewCommand_Execute(object? parameter)
        {
            Guest2TourView guest2TourView = new Guest2TourView(LoggedUser);
            guest2TourView.Show();
            _tourRequestView.Close();
        }
        #endregion
    }
}
