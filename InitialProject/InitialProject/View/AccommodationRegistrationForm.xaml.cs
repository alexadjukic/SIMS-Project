using InitialProject.Model;
using InitialProject.Repository;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationRegistrationForm.xaml
    /// </summary>
    public partial class AccommodationRegistrationForm : Window
    {
        public User LoggedInUser { get; set; }

        public Accommodation SelectedAccommodation { get; set; }

        private readonly AccommodationRepository _repository;
        private readonly LocationRepository _repositoryLocation;

        private string _name;

        public string AccommodationName
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string _location;

        public string Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        private string _type;

        public string Type
        {
            get => _type;
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        private string _capacity;

        public string Capacity
        {
            get => _capacity;
            set
            {
                if (_capacity != value)
                {
                    _capacity = value;
                    OnPropertyChanged("Capacity");
                }
            }
        }

        private string _minDaysForStay;

        public string MinDaysForStay
        {
            get => _minDaysForStay;
            set
            {
                if (_minDaysForStay != value)
                {
                    _minDaysForStay = value;
                    OnPropertyChanged("MinDaysForStay");
                }
            }
        }

        private string _minDaysBeforeCancel;

        public string MinDaysBeforeCancel
        {
            get => _minDaysBeforeCancel;
            set
            {
                if (_minDaysBeforeCancel != value)
                {
                    _minDaysBeforeCancel = value;
                    OnPropertyChanged("MinDaysBeforeCancel");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccommodationRegistrationForm(AccommodationRepository repository, LocationRepository locationRepository)
        {
            InitializeComponent();
            DataContext = this;
            _repository = repository;
            _repositoryLocation = locationRepository;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            _repository.Save(AccommodationName, Location, Type, Capacity, MinDaysForStay, MinDaysBeforeCancel, _repositoryLocation);
            this.Close();
        }
    }
}
