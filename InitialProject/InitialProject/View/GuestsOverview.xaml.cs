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
        int _ownerId;

        public ObservableCollection<AccommodationReservation> Reservations { get; set; }

        public readonly AccommodationReservationRepository _reservationRepository;
        public readonly AccommodationRepository _accommodationRepository;
        public readonly UserRepository _userRepository;

        public AccommodationReservation SelectedReservation { get; set; }


        public GuestsOverview(int ownerId, AccommodationReservationRepository accommodationReservationRepository, AccommodationRepository accommodationRepository, UserRepository userRepository)
        {
            InitializeComponent();
            this.DataContext = this;
            _ownerId = ownerId;
            _reservationRepository = accommodationReservationRepository;
            _accommodationRepository = accommodationRepository;
            _userRepository = userRepository;


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
                if (DateTime.Now.Day - SelectedReservation.EndDate.Day < 5) 
                { 
                    ButtonRate.IsEnabled = true;
                }
            }
        }



    }
}
