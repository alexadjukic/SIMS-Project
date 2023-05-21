using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views;
using InitialProject.WPF.Views.Guest2Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.Guest2ViewModels
{
    public class RequestedTourNotificationViewModel : ViewModelBase
    {
        #region PROPERTIES
        public User LoggedUser { get; set; }

        private RequestedTourNotification _selectedNotification;
        public RequestedTourNotification SelectedNotification
        {
            get
            {
                return _selectedNotification;
            }
            set
            {
                if (_selectedNotification != value)
                {
                    _selectedNotification = value;
                    OnPropertyChanged(nameof(SelectedNotification));
                }
            }
        }

        public ObservableCollection<RequestedTourNotification> TourNotifications { get; set; }
        private readonly Window _requestedTourNotificationsView;
        private readonly TourService _tourService;
        private readonly RequestedTourNotificationService _requestedTourNotificationService;

        #endregion

        public RequestedTourNotificationViewModel(Window requestedTourNotificationView, User loggedUser)
        {
            LoggedUser = loggedUser;
            _requestedTourNotificationsView = requestedTourNotificationView;
            TourNotifications = new ObservableCollection<RequestedTourNotification>();
            _tourService = new TourService();
            _requestedTourNotificationService = new RequestedTourNotificationService();

            ViewNotificationCommand = new RelayCommand(ViewNotificationCommand_Execute);
            ShowReservedToursCommand = new RelayCommand(ShowReservedToursCommand_Execute);
            OpenNotificationsCommand = new RelayCommand(OpenNotificationsCommand_Execute);
            ShowVouchersCommand = new RelayCommand(ShowVouchersCommand_Execute);
            ShowToursViewCommand = new RelayCommand(ShowToursViewCommand_Execute);
            ShowTourRequestsCommand = new RelayCommand(ShowTourRequestsCommand_Execute);
            ShowStatisticsCommand = new RelayCommand(ShowStatisticsCommand_Execute);
            ShowTourNotificationsCommand = new RelayCommand(ShowTourNotificationsCommand_Execute);
        }

        public void LoadNotifications()
        {
            foreach(var notification in _requestedTourNotificationService.GetAllByLocationOrLanguage(LoggedUser))
            {
                TourNotifications.Add(notification);
            }
        }

        #region COMMANDS

        public RelayCommand ViewNotificationCommand { get; }
        public RelayCommand ShowToursViewCommand { get; }
        public RelayCommand ShowReservedToursCommand { get; }
        public RelayCommand OpenNotificationsCommand { get; }
        public RelayCommand ShowVouchersCommand { get; }
        public RelayCommand ShowTourRequestsCommand { get; }
        public RelayCommand ShowStatisticsCommand { get; }
        public  RelayCommand ShowTourNotificationsCommand { get; }

        public void ShowTourNotificationsCommand_Execute(object? parameter)
        {
            TourNotificationsView tourNotificationsView = new TourNotificationsView(LoggedUser);
            tourNotificationsView.Show();
            _requestedTourNotificationsView.Close();
        }

        public void ShowStatisticsCommand_Execute(object? parameter)
        {
            RequestedTourStatisticsView requestedTourStatisticsView = new RequestedTourStatisticsView(LoggedUser);
            requestedTourStatisticsView.Show();
            _requestedTourNotificationsView.Close();
        }


        public void ShowTourRequestsCommand_Execute(object? parameter)
        {
            TourRequestFormView tourRequestFormView = new TourRequestFormView(LoggedUser);
            tourRequestFormView.Show();
            _requestedTourNotificationsView.Close();
        }

        public void OpenNotificationsCommand_Execute(object? parameter)
        {
            TourNotificationsView tourNotificationsView = new TourNotificationsView(LoggedUser);
            tourNotificationsView.Show();
            _requestedTourNotificationsView.Close();
        }

        public void ShowVouchersCommand_Execute(object? parameter)
        {
            VouchersView vouchersView = new VouchersView(LoggedUser);
            vouchersView.Show();
            _requestedTourNotificationsView.Close();
        }

        public void ShowReservedToursCommand_Execute(object? parameter)
        {
            ReservedToursView reservedToursView = new ReservedToursView(LoggedUser);
            reservedToursView.Show();
            _requestedTourNotificationsView.Close();
        }

        public void ShowToursViewCommand_Execute(object? parameter)
        {
            Guest2TourView guest2TourView = new Guest2TourView(LoggedUser);
            guest2TourView.Show();
            _requestedTourNotificationsView.Close();
        }

        public void ViewNotificationCommand_Execute(object? parameter)
        {
            if (SelectedNotification != null)
            {
                Tour tour = _tourService.GetById(SelectedNotification.TourId);
                SelectedTourView selectedTourView = new SelectedTourView(tour, LoggedUser);
                selectedTourView.Show();
                _requestedTourNotificationsView.Close();
            }
        }

        #endregion
    }
}
