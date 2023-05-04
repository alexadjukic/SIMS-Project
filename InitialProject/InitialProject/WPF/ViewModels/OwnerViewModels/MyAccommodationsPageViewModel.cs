using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.Repositories;
using InitialProject.WPF.Views;
using InitialProject.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class MyAccommodationsPageViewModel : ViewModelBase
    {
        #region PROPERTIES
        private readonly Page _myAccommodationsPage;

        private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;
        private readonly AccommodationImageRepository _accommodationImageRepository;
        private readonly UserRepository _userRepository;

        private readonly AccommodationService _accommodationService;
        private readonly AccommodationImageService _accommodationImageService;

        private readonly User _user;

        private Accommodation _selectedAccommodation;
        public Accommodation SelectedAccommodation
        {
            get
            {
                return _selectedAccommodation;
            }
            set
            {
                if (_selectedAccommodation != value)
                {
                    _selectedAccommodation = value;
                    OnPropertyChanged(nameof(SelectedAccommodation));
                    UploadImages();
                }
            }
        }

        private string _currentImage;
        public string CurrentImage
        {
            get
            {
                return _currentImage;
            }
            set
            {
                if (_currentImage != value)
                {
                    _currentImage = value;
                    OnPropertyChanged(nameof(CurrentImage));
                }
            }
        }

        private ObservableCollection<String> _images;

        public ObservableCollection<String> Images 
        {
            get
            {
                return _images;
            }
            set
            {
                if (_images != value)
                {
                    _images = value;
                    OnPropertyChanged(nameof(Images));
                }
            }
        }

        public ObservableCollection<Accommodation> MyAccommodations { get; set; }

        private Window _ownerMainWindow;
        #endregion


        public MyAccommodationsPageViewModel(Page myAccommodationsPage, User user, Window ownerMainWindow)
        {
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            _userRepository = new UserRepository();
            _accommodationImageRepository = new AccommodationImageRepository();

            _accommodationService = new AccommodationService();
            _accommodationImageService = new AccommodationImageService();

            _myAccommodationsPage = myAccommodationsPage;
            _ownerMainWindow = ownerMainWindow;

            _user = user;

            Images = new ObservableCollection<String>();
            MyAccommodations = new ObservableCollection<Accommodation>();

            CreateNewAccommodationCommand = new RelayCommand(CreateNewAccommodationCommand_Execute);
            NextImageCommand = new RelayCommand(NextImageCommand_Execute, NextImageCommand_CanExecute);
            PreviousImageCommand = new RelayCommand(PreviousImageCommand_Execute, PreviousImageCommand_CanExecute);

            LoadAccommodations();
            UploadImages();
        }

        public void LoadAccommodations()
        {
            MyAccommodations.Clear();

            foreach (var accommodation in _accommodationService.GetByOwnerId(_user.Id))
            {
                MyAccommodations.Add(accommodation);
            }
        }

        public void UploadImages()
        {
            Images.Clear();

            if (SelectedAccommodation != null)
            {
                foreach (var image in _accommodationImageService.GetAllByAccommodationId(SelectedAccommodation.Id))
                {
                    Images.Add(image.Url);
                }
                
            }

            if (Images.Count > 0)
            {
                CurrentImage = Images[0];
            }
        }

        #region COMMANDS
        public RelayCommand? CreateNewAccommodationCommand { get; }
        public RelayCommand NextImageCommand { get; }
        public RelayCommand PreviousImageCommand { get; }

        public void CreateNewAccommodationCommand_Execute(object? parameter)
        {
            AccommodationRegistrationForm accommodationRegistration = new AccommodationRegistrationForm(_accommodationRepository, _locationRepository, _accommodationImageRepository, _user.Id, _userRepository);
            accommodationRegistration.Show();
            _ownerMainWindow.Close();
        }

        public void PreviousImageCommand_Execute(object? prameter)
        {
            for (int i = 0; i < Images.Count; i++)
            {
                if (CurrentImage == Images[i])
                {
                    CurrentImage = Images[i - 1];
                    return;
                }
            }
        }

        public bool PreviousImageCommand_CanExecute(object? parameter)
        {
            return CurrentImage != null && CurrentImage != Images.First();
        }

        public void NextImageCommand_Execute(object? parameter)
        {
            for (int i = 0; i < Images.Count; i++)
            {
                if (CurrentImage == Images[i])
                {
                    CurrentImage = Images[i + 1];
                    return;
                }
            }
        }

        public bool NextImageCommand_CanExecute(object? parameter)
        {
            return CurrentImage != null && CurrentImage != Images.Last();
        }
        #endregion
    }
}
