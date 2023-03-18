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
    /// Interaction logic for GuestsOverview.xaml
    /// </summary>
    public partial class GuestsOverview : Window
    {
        public int _ownerId;

        public ObservableCollection<AccommodationReservation> Reservations { get; set; }

        public readonly AccommodationReservationRepository _reservationRepository;
        public readonly AccommodationRepository _accommodationRepository;
        public readonly UserRepository _userRepository;
        public readonly RatingRepository _ratingRepository;

        public AccommodationReservation SelectedReservation { get; set; }


        public GuestsOverview(int ownerId, AccommodationReservationRepository accommodationReservationRepository, AccommodationRepository accommodationRepository, UserRepository userRepository, RatingRepository ratingRepository)
        {
            InitializeComponent();
            this.DataContext = this;
            _ownerId = ownerId;
            _reservationRepository = accommodationReservationRepository;
            _accommodationRepository = accommodationRepository;
            _userRepository = userRepository;
            _ratingRepository = ratingRepository;

            LoadReservations();
        }

        public void LoadReservations()
        {
            List<AccommodationReservation> _ownerReservations = _reservationRepository.GetAllByOwnerId(_ownerId, _accommodationRepository, _userRepository);

            Reservations = new ObservableCollection<AccommodationReservation>(_reservationRepository.GetAllByOwnerId(_ownerId, _accommodationRepository, _userRepository));
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataGridGuests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                if (!IsDateExpired) 
                { 
                    ButtonRate.IsEnabled = true;
                }
            }
        }

        private void ButtonRate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null && !IsDateExpired && _ratingRepository.GetAll().Find(r => r.ReservationId == SelectedReservation.Id) == null)
            {
                RatingGuestForm ratingGuestForm = new RatingGuestForm(_ratingRepository, SelectedReservation, _ownerId);
                ratingGuestForm.Show();
            }
            else if (IsDateExpired)
            {
                MessageBox.Show("Selected reservation can't be rated", "It's been more than 5 days", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (_ratingRepository.GetAll().Find(r => r.ReservationId == SelectedReservation.Id) != null)
            {
                MessageBox.Show("Selected reservation can't be rated", "It is already rated", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public bool IsDateExpired
        {
            get
            {
                if ((DateTime.Now - SelectedReservation.EndDate).Days < 5)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
