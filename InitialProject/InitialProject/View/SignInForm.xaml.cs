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

        private readonly UserRepository _userRepository;
        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;
        private readonly AccommodationImageRepository _accommodationImageRepository;
        private readonly TourRepository _tourRepository;
        private readonly TourImageRepository _tourImageRepository;
        private readonly CheckpointRepository _pointRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private readonly RatingRepository _ratingRepository;
        private readonly TourReservationRepository _tourReservationRepository;

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
            _userRepository = new UserRepository();
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository(); 
            _accommodationImageRepository = new AccommodationImageRepository();
            _tourRepository = new TourRepository();
            _tourImageRepository = new TourImageRepository();
            _pointRepository = new CheckpointRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _ratingRepository = new RatingRepository();
            _tourReservationRepository = new TourReservationRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _userRepository.GetByUsername(Username);

            if (user == null)
            {
                MessageBox.Show("Wrong username!");
                return;
            }

            if (!user.Password.Equals(txtPassword.Password))
            {
                MessageBox.Show("Wrong password!");
                return;
            }

            OpenSuitableWindow(user);
        }

        private void OpenSuitableWindow(User user)
        {
            if (user.Role == UserRole.OWNER)
            {
                OwnerForm ownerForm = new OwnerForm(_accommodationRepository, _locationRepository, _accommodationImageRepository, user, _accommodationReservationRepository, _userRepository, _ratingRepository);
                ownerForm.Show();
                Close();
            }
            else if (user.Role == UserRole.GUEST1)
            {
                Guest1AccommodationOverview guest1AccommodationOverview = new Guest1AccommodationOverview(user, _accommodationRepository, _locationRepository, _accommodationImageRepository, _accommodationReservationRepository);
                guest1AccommodationOverview.Show();
                Close();
            }
            else if (user.Role == UserRole.GUEST2)
            {
                Guest2TourOverview guest2TourOverview = new Guest2TourOverview(_tourRepository, _locationRepository, _tourImageRepository, _tourReservationRepository, user);
                guest2TourOverview.Show();
                Close();
            }
            else if (user.Role == UserRole.GUIDE)
            {
                TourCreationForm tourCreationForm = new TourCreationForm(_tourRepository, _tourImageRepository, _locationRepository, _pointRepository, user);
                tourCreationForm.Show();
                Close();
            }
        }
    }
}
