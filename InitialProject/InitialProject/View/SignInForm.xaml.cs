﻿using InitialProject.Forms;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace InitialProject
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserRepository _repository;
        
        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository(); 
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);

            if (user != null)
            {
                if (user.Role == "owner")
                {
                    if (user.Password == txtPassword.Password)
                    {
                        AccommodationRegistrationForm accommodationRegistration = new AccommodationRegistrationForm(_accommodationRepository, _locationRepository);
                        accommodationRegistration.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong password!");
                    }
                }
                else
                {
                    MessageBox.Show("Wrong username!");
                }
            }

            /*if (user != null)
            {
                if(user.Password == txtPassword.Password)
                {
                    CommentsOverview commentsOverview = new CommentsOverview(user);
                    commentsOverview.Show();
                    Close();
                } 
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
            */
            
        }
    }
}
