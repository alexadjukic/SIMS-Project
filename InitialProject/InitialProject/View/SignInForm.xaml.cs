using InitialProject.Forms;
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
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _userRepository.GetByUsername(Username);

            if (user != null)
            {
                if (user.Role == UserRole.OWNER)
                {
                    if (user.Password == txtPassword.Password)
                    {
                        OwnerForm ownerForm = new OwnerForm(_accommodationRepository, _locationRepository, _accommodationImageRepository, user);
                        ownerForm.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong password!");
                    }
                }
                else if (user.Role == UserRole.GUEST2)
                {
                    if (user.Password == txtPassword.Password)
                    {
                        Guest2TourOverview guest2TourOverview = new Guest2TourOverview(_tourRepository, _locationRepository, _tourImageRepository);
                        Close();
                        guest2TourOverview.Show();
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
        }
    }
}
