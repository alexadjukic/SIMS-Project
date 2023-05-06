using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using InitialProject.WPF.ViewModels;
using InitialProject.WPF.ViewModels.Guest1ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace InitialProject.WPF.Views.Guest1Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _themeButton;
        public string ThemeButton
        {
            get => _themeButton;
            set
            {
                if (_themeButton != value)
                {
                    _themeButton = value;
                    OnPropertyChanged("ThemeButton");
                }
            }
        }
        public static MainWindow mainWindow;

        public User LoggedUser { get; set; }
        public readonly AccommodationRepository _accommodationRepository;
        public readonly LocationRepository _locationRepository;
        public readonly AccommodationImageRepository _accommodationImageRepository;
        public readonly AccommodationReservationRepository _accommodationReservationRepository;
        public readonly UserRepository _userRepository;

        public MainWindow(User user, AccommodationRepository accommodationRepository, LocationRepository locationRepository, AccommodationImageRepository accommodationImageRepository, AccommodationReservationRepository accommodationReservationRepository, UserRepository userRepository)
        {
            InitializeComponent();
            this.DataContext = this;
            mainWindow = this;

            _accommodationRepository = accommodationRepository;
            _locationRepository = locationRepository;
            _accommodationImageRepository = accommodationImageRepository;
            _accommodationReservationRepository = accommodationReservationRepository;
            _userRepository = userRepository;

            LoggedUser = user;
            ThemeButton = "OFF";

            MainPreview.Content = new AccommodationsPage(LoggedUser, _accommodationRepository, _locationRepository, _accommodationImageRepository, _accommodationReservationRepository, _userRepository);
        }

        private void AccommodationsButton_Click(object sender, RoutedEventArgs e)
        {
            MainPreview.Content = new AccommodationsPage(LoggedUser, _accommodationRepository, _locationRepository, _accommodationImageRepository, _accommodationReservationRepository, _userRepository);
        }

        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }

        private void ReservationsButton_Click(object sender, RoutedEventArgs e)
        {
            MainPreview.Content = new ReservationsPage(new ReservationsViewModel(this, LoggedUser.Id));
        }

        private void DarkTheme_Checked(object sender, RoutedEventArgs e)
        {
            var app = (App)System.Windows.Application.Current;
            app.ChangeTheme(new Uri("Resources/Themes/DarkTheme.xaml", UriKind.Relative));
            ThemeButton = "ON";
        }

        private void DarkTheme_Unchecked(object sender, RoutedEventArgs e)
        {
            var app = (App)System.Windows.Application.Current;
            app.ChangeTheme(new Uri("Resources/Themes/LightTheme.xaml", UriKind.Relative));
            ThemeButton = "OFF";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
