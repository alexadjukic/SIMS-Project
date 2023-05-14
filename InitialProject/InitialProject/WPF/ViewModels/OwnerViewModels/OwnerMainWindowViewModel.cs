using InitialProject.Commands;
using InitialProject.WPF.Views.OwnerViews;
using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InitialProject.WPF.Views;
using System.Windows.Controls;
using InitialProject.Repositories;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class OwnerMainWindowViewModel : ViewModelBase
    {
        #region PROPERTIES
        private readonly User _user;
        private readonly Window _ownerMainWindow;

        public String Username { get; set; }

        private Page _selectedPage;
        public Page SelectedPage 
        {
            get
            {
                return _selectedPage;
            }
            set
            {
                if (_selectedPage != value)
                {
                    _selectedPage = value;
                    OnPropertyChanged(nameof(SelectedPage));
                }
            }
        }
        #endregion

        public OwnerMainWindowViewModel(Window ownerMainWindow, User user) 
        {
            _ownerMainWindow = ownerMainWindow;
            _user = user;
            SelectedPage = new MyAccommodationsPage(_user);

            Username = user.Username;

            CloseWindowCommand = new RelayCommand(CloseWindowCommand_Execute);
            SeeMyAccommodationsCommand = new RelayCommand(SeeMyAccommodationsCommand_Execute);
            SeeMyReservationsCommand = new RelayCommand(SeeMyReservationsCommand_Execute);
            SeeMyRequestsCommand = new RelayCommand(SeeMyRequestsCommand_Execute);
            SeeMyReviewsCommand = new RelayCommand(SeeMyReviewsCommand_Execute);
            SeeForumsCommand = new RelayCommand(SeeForumsCommand_Execute);
            SeeMyRenovationsCommand = new RelayCommand(SeeMyRenovationsCommand_Execute);
            SeeMyProfileCommand = new RelayCommand(SeeMyProfileCommand_Execute);
            SeeNotificationsCommand = new RelayCommand(SeeNotificationsCommand_Execute);
        }

        #region COMMANDS
        public RelayCommand? CloseWindowCommand { get; }
        public RelayCommand? SeeMyAccommodationsCommand { get; }
        public RelayCommand? SeeMyReservationsCommand { get; }
        public RelayCommand? SeeMyRequestsCommand { get; }
        public RelayCommand? SeeMyReviewsCommand { get; }
        public RelayCommand? SeeForumsCommand { get; }
        public RelayCommand? SeeMyRenovationsCommand { get; }
        public RelayCommand? SeeMyProfileCommand { get; }
        public RelayCommand? SeeNotificationsCommand { get; }

        public void CloseWindowCommand_Execute(object? parameter)
        {
            SignInForm signIn = new SignInForm();
            signIn.Show();
            _ownerMainWindow.Close();
        }

        public void SeeMyAccommodationsCommand_Execute(object? parameter)
        {
            SelectedPage = new MyAccommodationsPage(_user);
        }

        public void SeeMyReservationsCommand_Execute(object? parameter)
        {
            SelectedPage = new MyReservationsOverviewPage(_user);
        }

        public void SeeMyRequestsCommand_Execute(object? parameter)
        {
            SelectedPage = new RequestsOverview(_user.Id);
        }

        public void SeeMyReviewsCommand_Execute(object? parameter)
        {
            SelectedPage = new RatedGuestsOverview(_user.Id);
        }

        public void SeeForumsCommand_Execute(object? parameter)
        {
            SelectedPage = new ForumsOverviewPage();
        }

        public void SeeMyRenovationsCommand_Execute(object? parameter)
        {
            SelectedPage = new MyRenovationsOverviewPage();
        }

        public void SeeMyProfileCommand_Execute(object? parameter)
        {
            SelectedPage = new MyProfilePage();
        }

        public void SeeNotificationsCommand_Execute(object? parameter)
        {
            SelectedPage = new NotificationsOverviewPage();
        }
        #endregion
    }
}
