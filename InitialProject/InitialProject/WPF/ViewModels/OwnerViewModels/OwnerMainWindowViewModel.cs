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
            SelectedPage = new MyAccommodationsPage(_user, _ownerMainWindow);

            Username = user.Username;

            CloseWindowCommand = new RelayCommand(CloseWindowCommand_Execute);
            SeeMyAccommodationsCommand = new RelayCommand(SeeMyAccommodationsCommand_Execute);
        }

        #region COMMANDS
        public RelayCommand? CloseWindowCommand { get; }
        public RelayCommand? SeeMyAccommodationsCommand { get; }

        public void CloseWindowCommand_Execute(object? parameter)
        {
            SignInForm signIn = new SignInForm();
            signIn.Show();
            _ownerMainWindow.Close();
        }

        public void SeeMyAccommodationsCommand_Execute(object? parameter)
        {
            SelectedPage = new MyAccommodationsPage(_user, _ownerMainWindow);
        }
        #endregion
    }
}
