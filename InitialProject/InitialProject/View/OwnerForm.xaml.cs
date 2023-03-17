using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for OwnerForm.xaml
    /// </summary>
    public partial class OwnerForm : Window
    {
        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;
        private readonly AccommodationImageRepository _imageRepository;
        private readonly AccommodationReservationRepository _reservationRepository;
        private readonly UserRepository _userRepository;

        private int _ownerId;
        public OwnerForm(AccommodationRepository accommodationRepository, LocationRepository locationRepository, AccommodationImageRepository imageRepository, User user, AccommodationReservationRepository reservationRepository, UserRepository userRepository)
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationRepository = accommodationRepository;
            _locationRepository = locationRepository;
            _imageRepository = imageRepository;
            LabelWelcomeUser.Content = "Welcome " + user.Username;
            _ownerId = user.Id;
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
        }

        private void ButtonRegistrateAccommodation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationRegistrationForm accommodationRegistration = new AccommodationRegistrationForm(_accommodationRepository, _locationRepository, _imageRepository, _ownerId);
            accommodationRegistration.Show();
        }

        private void ButtonSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignInForm signIn = new SignInForm();
            signIn.Show();
            this.Close();
        }

        private void ButtonRateGuest_Click(object sender, RoutedEventArgs e)
        {
            GuestsOverview guestsOverview = new GuestsOverview(_ownerId, _reservationRepository, _accommodationRepository, _userRepository);
            guestsOverview.Show();
        }
    }
}
