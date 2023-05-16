using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using InitialProject.WPF.Views;
using InitialProject.WPF.Views.Guest2Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.Guest2ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public User LoggedUser { get; set; }

        private readonly Window _guest2MenuView;
        
        public MenuViewModel(Window guest2MenuView, User user)
        {
            _guest2MenuView = guest2MenuView;
            LoggedUser = user;

            CloseWindowCommand = new RelayCommand(CloseWindowCommand_Execute);
            LogOutCommand = new RelayCommand(LogOutCommand_Execute);
            OpenNotificationsCommand = new RelayCommand(OpenNotificationsCommand_Execute);
            ShowVouchersCommand = new RelayCommand(ShowVouchersCommand_Execute);
            ShowToursViewCommand = new RelayCommand(ShowToursViewCommand_Execute);
            ShowReservedToursCommand = new RelayCommand(ShowReservedToursCommand_Execute);
        }

        #region COMMANDS
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand LogOutCommand {  get; }
        public RelayCommand OpenNotificationsCommand { get; }
        public RelayCommand ShowVouchersCommand { get; }
        public RelayCommand ShowToursViewCommand { get; }
        public RelayCommand ShowReservedToursCommand { get; }
        
        public void ShowReservedToursCommand_Execute(object? parameter)
        {
            ReservedToursView reservedToursView = new ReservedToursView(LoggedUser);
            reservedToursView.Show();
        }
        public void ShowToursViewCommand_Execute(object? parameter)
        {
            Guest2TourView guest2TourView = new Guest2TourView(LoggedUser);
            guest2TourView.Show();
        }

        public void ShowVouchersCommand_Execute(object? parameter)
        {
            VouchersView vouchersView = new VouchersView(LoggedUser);
            vouchersView.Show();
            //_guest2MenuView.Close();
        }

        public void OpenNotificationsCommand_Execute(object? parameter)
        {
            TourNotificationsView tourNotificationsView = new TourNotificationsView(LoggedUser);
            tourNotificationsView.Show();
            //_guest2MenuView.Close();
        }
        public void LogOutCommand_Execute(object? parameter)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            //_guest2MenuView.Close();
        }
        public void CloseWindowCommand_Execute(object? parameter)
        {
            _guest2MenuView.Close();
        }
        #endregion
    }
}
