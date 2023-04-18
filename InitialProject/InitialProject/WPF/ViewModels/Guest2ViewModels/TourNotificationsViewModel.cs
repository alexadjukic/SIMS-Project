using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.DTOs;
using InitialProject.Domain.Models;
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
    public class TourNotificationsViewModel : ViewModelBase
    {
        #region PROPERTIES

        public User LoggedUser { get; set; }

        private TourNotification _selectedNotification;
        public TourNotification SelectedNotification
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

        public ObservableCollection<TourNotification> TourNotifications { get; set; }
        private readonly Window _tourNotificationsView;
        private readonly TourNotificationService _tourNotificationService;

        #endregion

        public TourNotificationsViewModel(Window tourNotificationsView, User user)
        {
            _tourNotificationsView = tourNotificationsView;
            _tourNotificationService = new TourNotificationService();
            LoggedUser = user;
            TourNotifications = new ObservableCollection<TourNotification>();
            LoadNotifications();
            ViewNotificationCommand = new RelayCommand(ViewNotificationCommand_Execute);
        }

        public void LoadNotifications()
        {
            TourNotifications = _tourNotificationService.GetNotificationsByUser(LoggedUser.Id);
        }

        #region COMMANDS
        public RelayCommand ViewNotificationCommand { get; }

        public void ViewNotificationCommand_Execute(object? parameter)
        {
            SelectedTourNotificationView selectedTourNotificationView = new SelectedTourNotificationView(LoggedUser, SelectedNotification);
            selectedTourNotificationView.Show();
            _tourNotificationsView.Close();
        }
        #endregion
    }
}
