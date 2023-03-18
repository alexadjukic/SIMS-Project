using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly RatingRepository _ratingRepository;

        public int _numberOfUnratedGuests;

        private int _ownerId;
        public OwnerForm(AccommodationRepository accommodationRepository, LocationRepository locationRepository, AccommodationImageRepository imageRepository, User user, AccommodationReservationRepository reservationRepository, UserRepository userRepository, RatingRepository ratingRepository)
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
            _ratingRepository = ratingRepository;

            _numberOfUnratedGuests = 0;

            foreach (var reservation in _reservationRepository.GetAll())
            {
                int reservationId = reservation.Id;
                Accommodation foundAccommodation = _accommodationRepository.GetAll().Find(a => a.Id == reservation.AccommodationId);
                if (foundAccommodation != null)
                {
                    List<Rating> ratings = _ratingRepository.GetAll();
                    Rating foundRating = new Rating();

                    foreach (var rating in ratings)
                    {
                        if (rating.ReservationId == reservation.Id)
                        {
                            foundRating = rating;
                        }
                    }

                    if (foundAccommodation.OwnerId == _ownerId && foundRating.Id == 0)
                    {
                        RatingGuestReminderForm ratingGuestReminderForm = new RatingGuestReminderForm(_ownerId, _reservationRepository, _accommodationRepository, _userRepository, _ratingRepository);
                        ratingGuestReminderForm.Show();
                        break;
                    }
                }
            }
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
            GuestsOverview guestsOverview = new GuestsOverview(_ownerId, _reservationRepository, _accommodationRepository, _userRepository, _ratingRepository);
            guestsOverview.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           /*foreach (var reservation in _reservationRepository.GetAll())
           {
               if (_accommodationRepository.GetAll().Find(a => a.Id == reservation.AccommodationId) != null)
               {
                   if (_accommodationRepository.GetAll().Find(a => a.Id == reservation.AccommodationId).OwnerId == _ownerId && _ratingRepository.GetAll().Find(r => r.ReservationId == reservation.Id) == null)
                   {
                       RatingGuestReminderForm ratingGuestReminderForm = new RatingGuestReminderForm(_ownerId, _reservationRepository, _accommodationRepository, _userRepository);
                       ratingGuestReminderForm.Show();
                       break;
                   }
               }

           }*/
        }
    }
}
