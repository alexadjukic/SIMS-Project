using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.WPF.Views.OwnerViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class AccommodationInfoOverviewViewModel : ViewModelBase
    {
        #region PROPERTIES
        private readonly AccommodationImageService _accommodationImageService;

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

        private Window _accommodationInfoOverview;
        #endregion

        public AccommodationInfoOverviewViewModel(Window accommodationInfoOverview, Accommodation selectedAccommodation) 
        { 
            _accommodationInfoOverview = accommodationInfoOverview;
            SelectedAccommodation = selectedAccommodation;

            _accommodationImageService = new AccommodationImageService();

            Images = new ObservableCollection<String>();

            CloseWindowCommand = new RelayCommand(CloseWindowCommand_Execute);
            PreviousImageCommand = new RelayCommand(PreviousImageCommand_Execute, PreviousImageCommand_CanExecute);
            NextImageCommand = new RelayCommand(NextImageCommand_Execute, NextImageCommand_CanExecute);

            UploadImages();
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
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand NextImageCommand { get; }
        public RelayCommand PreviousImageCommand { get; }

        public void CloseWindowCommand_Execute(object? parameter)
        {
            _accommodationInfoOverview.Close();
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
