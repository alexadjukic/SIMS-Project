using InitialProject.Domain.Models;
using InitialProject.Repositories;
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

namespace InitialProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for GuideMenu.xaml
    /// </summary>
    public partial class GuideMenu : Window
    {
        private TourRepository _tourRepository;
        private TourImageRepository _tourImageRepository;
        private LocationRepository _locationRepository;
        private CheckpointRepository _checkpointRepository;
        private User _guide;

        public string _welcomeMessage;
        public string WelcomeMessage { get; set; }

        public GuideMenu(TourRepository tourRepository, TourImageRepository tourImageRepository, LocationRepository locationRepository, CheckpointRepository checkpointRepository, User guide)
        {
            InitializeComponent();
            this.DataContext = this;

            _tourRepository = tourRepository;
            _tourImageRepository = tourImageRepository;
            _locationRepository = locationRepository;
            _checkpointRepository = checkpointRepository;
            _guide = guide;
            WelcomeMessage = String.Format("Welcome {0}", _guide.Username);
        }

        private void ButtonCreateTour_Click(object sender, RoutedEventArgs e)
        {
            TourCreationForm tourCreationForm = new TourCreationForm(_tourRepository, _tourImageRepository, _locationRepository, _checkpointRepository, _guide);
            tourCreationForm.Show();
        }

        private void ButtonTodaysTours_Click(object sender, RoutedEventArgs e)
        {
            TodaysToursView todaysToursView = new TodaysToursView(_guide);
            todaysToursView.Show();
        }

        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }

        private void ButtonYourTours_Click(object sender, RoutedEventArgs e)
        {
            YourToursView yourToursView = new YourToursView();
            yourToursView.Show();
        }

        private void ButtonStatistics_Click(object sender, RoutedEventArgs e)
        {
            var tourStatisticsSelectionView = new TourStatisticsSelectionView();
            tourStatisticsSelectionView.Show();
        }

        private void ButtonReviews_Click(object sender, RoutedEventArgs e)
        {
            var tourReviewsView = new TourReviewsView();
            tourReviewsView.Show();
        }
    }
}
