﻿using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace InitialProject.WPF.ViewModels
{
    public class CreateNewTourViewModel : ViewModelBase
    {
        #region PROPERTIES
        private string _tourName;
        public string TourName
        {
            get => _tourName;

            set
            {
                if (_tourName != value)
                {
                    _tourName = value;
                    OnPropertyChanged(nameof(TourName));
                }
            }
        }

        private string _selectedCountry;
        public string SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                if (_selectedCountry != value)
                {
                    _selectedCountry = value;
                    OnPropertyChanged(nameof(SelectedCountry));
                }
            }
        }

        private string _selectedCity;
        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                if (_selectedCity != value)
                {
                    _selectedCity = value;
                    OnPropertyChanged(nameof(SelectedCity));
                }
            }
        }

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged(nameof(SelectedLanguage));
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (_maxGuests != value)
                {
                    _maxGuests = value;
                    OnPropertyChanged(nameof(MaxGuests));
                }
            }
        }

        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }

        private DateTime _datePickerSelectedDate;
        public DateTime DatePickerSelectedDate
        {
            get => _datePickerSelectedDate;
            set
            {
                if (_datePickerSelectedDate != value)
                {
                    _datePickerSelectedDate = value;
                    OnPropertyChanged(nameof(DatePickerSelectedDate));
                }
            }
        }

        private DateTime _dataGridSelectedDate;
        public DateTime DataGridSelectedDate
        {
            get => _dataGridSelectedDate;
            set
            {
                if (_dataGridSelectedDate != value)
                {
                    _dataGridSelectedDate = value;
                    OnPropertyChanged(nameof(DataGridSelectedDate));
                }
            }
        }
        
        private string _enteredCheckpointName;
        public string EnteredCheckpointName
        {
            get => _enteredCheckpointName;
            set
            {
                if (_enteredCheckpointName != value)
                {
                    _enteredCheckpointName = value;
                    OnPropertyChanged(nameof(EnteredCheckpointName));
                }
            }
        }

        private string _selectedCheckpointName;
        public string SelectedCheckpointName
        {
            get => _selectedCheckpointName;
            set
            {
                if (_selectedCheckpointName != value)
                {
                    _selectedCheckpointName = value;
                    OnPropertyChanged(nameof(SelectedCheckpointName));
                }
            }
        }

        private BitmapImage _selectedImage;
        public BitmapImage SelectedImage
        {
            get => _selectedImage;
            set
            {
                if (_selectedImage != value)
                {
                    _selectedImage = value;
                    OnPropertyChanged(nameof(SelectedImage));
                }
            }
        }

        private BitmapImage _coverImage;
        public BitmapImage CoverImage
        {
            get
            {
                return _coverImage;
            }
            set
            {
                if (_coverImage != value)
                {
                    _coverImage = value;
                    OnPropertyChanged(nameof(CoverImage));
                }
            }
        }

        private bool _isCoverImageSelected;
        public bool IsCoverImageSelected
        {
            get
            {
                return _isCoverImageSelected;
            }
            set
            {
                if (_isCoverImageSelected != value)
                {
                    _isCoverImageSelected = value;
                    OnPropertyChanged(nameof(IsCoverImageSelected));
                }
            }
        }

        private bool _isNextButtonVisible;
        public bool IsNextButtonVisible
        {
            get
            {
                return _isNextButtonVisible;
            }
            set
            {
                if (_isNextButtonVisible != value)
                {
                    _isNextButtonVisible = value;
                    OnPropertyChanged(nameof(IsNextButtonVisible));
                }
            }
        }

        private bool _isConfirmButtonVisible;
        public bool IsConfirmButtonVisible
        {
            get
            {
                return _isConfirmButtonVisible;
            }
            set
            {
                if (_isConfirmButtonVisible != value)
                {
                    _isConfirmButtonVisible = value;
                    OnPropertyChanged(nameof(IsConfirmButtonVisible));
                }
            }
        }

        private bool _isGeneralTabSelected;
        public bool IsGeneralTabSelected
        {
            get
            {
                return _isGeneralTabSelected;
            }
            set
            {
                if (_isGeneralTabSelected != value)
                {
                    _isGeneralTabSelected = value;
                    OnPropertyChanged(nameof(IsGeneralTabSelected));
                }
            }
        }

        private bool _isDateTimeTabSelected;
        public bool IsDateTimeTabSelected
        {
            get
            {
                return _isDateTimeTabSelected;
            }
            set
            {
                if (_isDateTimeTabSelected != value)
                {
                    _isDateTimeTabSelected = value;
                    OnPropertyChanged(nameof(IsDateTimeTabSelected));
                }
            }
        }

        private bool _isCheckpointsTabSelected;
        public bool IsCheckpointsTabSelected
        {
            get
            {
                return _isCheckpointsTabSelected;
            }
            set
            {
                if (_isCheckpointsTabSelected != value)
                {
                    _isCheckpointsTabSelected = value;
                    OnPropertyChanged(nameof(IsCheckpointsTabSelected));
                }
            }
        }

        private bool _isImagesTabSelected;
        public bool IsImagesTabSelected
        {
            get
            {
                return _isImagesTabSelected;
            }
            set
            {
                if (_isImagesTabSelected != value)
                {
                    _isImagesTabSelected = value;
                    OnPropertyChanged(nameof(IsImagesTabSelected));
                }
            }
        }


        public ObservableCollection<DateTime> Dates { get; set; }
        public List<string> Countries { get; set; }
        public ObservableCollection<string> Cities { get; set; }
        public ObservableCollection<string> Languages { get; set; }
        public ObservableCollection<string> CheckpointNames { get; set; }
        public List<BitmapImage> Images { get; set; }

        private readonly TourService _tourService;
        private readonly LocationService _locationService;
        private readonly CheckpointService _checkpointService;
        private readonly TourImageService _tourImageService;

        private readonly User _guide;
        #endregion

        public CreateNewTourViewModel(User guide)
        {
            _guide = guide;

            _tourService = new TourService();
            _locationService = new LocationService();
            _checkpointService = new CheckpointService();
            _tourImageService = new TourImageService();

            Dates = new ObservableCollection<DateTime>();
            Countries = new List<string>();
            Cities = new ObservableCollection<string>();
            Languages = new ObservableCollection<string>() { "Afrikaans", "Albanian", "Amharic", "Arabic", "Armenian", "Assamese", "Azerbaijani", "Basque", "Belarusian", "Bengali", "Bosnian", "Bulgarian", "Burmese", "Catalan", "Cebuano", "Chichewa", "Chinese (Mandarin)", "Corsican", "Croatian", "Czech", "Danish", "Dutch", "English", "Esperanto", "Estonian", "Finnish", "French", "Frisian", "Galician", "Georgian", "German", "Greek", "Gujarati", "Haitian Creole", "Hausa", "Hawaiian", "Hebrew", "Hindi", "Hmong", "Hungarian", "Icelandic", "Igbo", "Indonesian", "Irish", "Italian", "Japanese", "Javanese", "Kannada", "Kazakh", "Khmer", "Kinyarwanda", "Korean", "Kurdish (Kurmanji)", "Kyrgyz", "Lao", "Latin", "Latvian", "Lithuanian", "Luxembourgish", "Macedonian", "Malagasy", "Malay", "Malayalam", "Maltese", "Maori", "Marathi", "Mongolian", "Myanmar (Burmese)", "Nepali", "Norwegian", "Odia (Oriya)", "Pashto", "Persian", "Polish", "Portuguese", "Punjabi", "Romanian", "Russian", "Samoan", "Scots Gaelic", "Serbian", "Sesotho", "Shona", "Sindhi", "Sinhala", "Slovak", "Slovenian", "Somali", "Spanish", "Sundanese", "Swahili", "Swedish", "Tagalog (Filipino)", "Tajik", "Tamil", "Tatar", "Telugu", "Thai", "Turkish", "Turkmen", "Ukrainian", "Urdu", "Uyghur", "Uzbek", "Vietnamese", "Welsh", "Xhosa", "Yiddish", "Yoruba", "Zulu" };
            CheckpointNames = new ObservableCollection<string>();
            Images = new List<BitmapImage>();

            DatePickerSelectedDate = DateTime.Now;
            IsGeneralTabSelected = true;
            IsDateTimeTabSelected = false;
            IsCheckpointsTabSelected = false;
            IsImagesTabSelected = false;
            IsNextButtonVisible = true;
            IsConfirmButtonVisible = false;

            LoadCountries();

            AddDateCommand = new RelayCommand(AddDateCommand_Execute, AddDateCommand_CanExecute);
            RemoveDateCommand = new RelayCommand(RemoveDateCommand_Execute, RemoveDateCommand_CanExecute);
            AddCheckpointCommand = new RelayCommand(AddCheckpointCommand_Execute, AddCheckpointCommand_CanExecute);
            RemoveCheckpointCommand = new RelayCommand(RemoveCheckpointCommand_Execute, RemoveCheckpointCommand_CanExecute);
            AddImageCommand = new RelayCommand(AddImageCommand_Execute);
            RemoveImageCommand = new RelayCommand(RemoveImageCommand_Execute, RemoveImageCommand_CanExecute);
            SetAsCoverCommand = new RelayCommand(SetAsCoverCommand_Execute, SetAsCoverCommand_CanExecute);
            PreviousImageCommand = new RelayCommand(PreviousImageCommand_Execute, PreviousImageCommand_CanExecute);
            NextImageCommand = new RelayCommand(NextImageCommand_Execute, NextImageCommand_CanExecute);
            ConfirmCommand = new RelayCommand(ConfirmCommand_Execute, ConfirmCommand_CanExecute);
            NextCommand = new RelayCommand(NextCommand_Execute, NextCommand_CanExecute);
            PreviousCommand = new RelayCommand(PreviousCommand_Execute, PreviousCommand_CanExecute);
            LoadCitiesCommand = new RelayCommand(LoadCitiesCommand_Execute);
            TabControlSelectionChangedCommand = new RelayCommand(TabControlSelectionChangedCommand_Execute);
        }

        private void LoadCountries()
        {
            Countries = _locationService.GetAllCountries().ToList();
            Countries.Sort();
        }

        private void AddImage(string fileName)
        {
            Uri uri = new Uri(fileName);
            BitmapImage newImage = new BitmapImage(uri);

            if (SelectedImage == null)
            {
                Images.Add(newImage);
            }
            else
            {
                Images.Insert(Images.IndexOf(SelectedImage) + 1, newImage);
            }
            SelectedImage = newImage;
            IsCoverImageSelected = false;
        }

        #region COMMANDS
        public RelayCommand AddDateCommand { get; }
        public RelayCommand RemoveDateCommand { get; }
        public RelayCommand AddCheckpointCommand { get; }
        public RelayCommand RemoveCheckpointCommand { get; }
        public RelayCommand AddImageCommand { get; }
        public RelayCommand RemoveImageCommand { get; }
        public RelayCommand SetAsCoverCommand { get; }
        public RelayCommand PreviousImageCommand { get; }
        public RelayCommand NextImageCommand { get; }
        public RelayCommand ConfirmCommand { get; }
        public RelayCommand NextCommand { get; }
        public RelayCommand PreviousCommand { get; }
        public RelayCommand LoadCitiesCommand { get; }
        public RelayCommand TabControlSelectionChangedCommand { get; }

        public void AddDateCommand_Execute(object? parameter)
        {
            Dates.Add(DatePickerSelectedDate);
        }

        public bool AddDateCommand_CanExecute(object? parameter)
        {
            return !Dates.Contains(DatePickerSelectedDate) && DatePickerSelectedDate.CompareTo(DateTime.Now) > 0;
        }

        public void RemoveDateCommand_Execute(object? parameter)
        {
            DateTime date = (DateTime)parameter;
            Dates.Remove(date);
        }

        public bool RemoveDateCommand_CanExecute(object? parameter)
        {
            return parameter is not null;
        }

        public void AddCheckpointCommand_Execute(object? parameter)
        {
            CheckpointNames.Add(EnteredCheckpointName);
            EnteredCheckpointName = "";
        }

        public bool AddCheckpointCommand_CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(EnteredCheckpointName) && !CheckpointNames.Contains(EnteredCheckpointName);
        }

        public void RemoveCheckpointCommand_Execute(object? parameter)
        {
            string checkpointName = parameter as string;
            CheckpointNames.Remove(checkpointName);
        }

        public bool RemoveCheckpointCommand_CanExecute(object? parameter)
        {
            return parameter is not null;
        }

        public void AddImageCommand_Execute(object? parameter)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";

            if ((bool)dlg.ShowDialog()) AddImage(dlg.FileName);
        }

        public void RemoveImageCommand_Execute(object? parameter)
        {
            var index = Images.IndexOf(SelectedImage);
            if (SelectedImage == Images.Last()) index--;
            Images.Remove(SelectedImage);
            if (Images.Count == 0)
            {
                SelectedImage = null;
            }
            else
            {
                SelectedImage = Images[index];
            }
            IsCoverImageSelected = SelectedImage == CoverImage;
        }

        public bool RemoveImageCommand_CanExecute(object? parameter)
        {
            return SelectedImage is not null;
        }

        public void SetAsCoverCommand_Execute(object? parameter)
        {
            CoverImage = SelectedImage;
            IsCoverImageSelected = true;
        }

        public bool SetAsCoverCommand_CanExecute(object? parameter)
        {
            return SelectedImage is not null && SelectedImage != CoverImage;
        }

        public void PreviousImageCommand_Execute(object? prameter)
        {
            for (int i = 0; i < Images.Count; i++)
            {
                if (SelectedImage == Images[i])
                {
                    SelectedImage = Images[i - 1];
                    IsCoverImageSelected = SelectedImage == CoverImage;
                    return;
                }
            }
        }

        public bool PreviousImageCommand_CanExecute(object? parameter)
        {
            return Images.Count != 0 && SelectedImage != Images.First();
        }

        public void NextImageCommand_Execute(object? parameter)
        {
            for (int i = 0; i < Images.Count; i++)
            {
                if (SelectedImage == Images[i])
                {
                    SelectedImage = Images[i + 1];
                    IsCoverImageSelected = SelectedImage == CoverImage;
                    return;
                }
            }
        }

        public bool NextImageCommand_CanExecute(object? parameter)
        {
            return Images.Count != 0 && SelectedImage != Images.Last();
        }

        public void ConfirmCommand_Execute(object? parameter)
        {
            CoverImage ??= Images.First();
            foreach (var startTime in Dates)
            {
                Tour tour = _tourService.Create(TourName, SelectedCountry, SelectedCity, Description, SelectedLanguage, MaxGuests, startTime, Duration, CoverImage.ToString(), _guide.Id);
                foreach (var checkpointName in CheckpointNames)
                {
                    _checkpointService.Create(checkpointName, tour);
                }

                foreach (var image in Images)
                {
                    _tourImageService.Create(image.ToString(), tour);
                }
            }
        }

        public bool ConfirmCommand_CanExecute(object? parameter)
        {
            return IsImagesTabSelected;
        }

        public void NextCommand_Execute(object? parameter)
        {
            if (IsGeneralTabSelected)
            {
                IsGeneralTabSelected = false;
                IsDateTimeTabSelected = true;
            }
            else if (IsDateTimeTabSelected)
            {
                IsDateTimeTabSelected = false;
                IsCheckpointsTabSelected = true;
            }
            else if (IsCheckpointsTabSelected)
            {
                IsCheckpointsTabSelected = false;
                IsImagesTabSelected = true;
                IsConfirmButtonVisible = true;
                IsNextButtonVisible = false;
            }
        }

        public bool NextCommand_CanExecute(object? parameter)
        {
            return !IsImagesTabSelected;
        }

        public void PreviousCommand_Execute(object? parameter)
        {
            if (IsDateTimeTabSelected)
            {
                IsDateTimeTabSelected = false;
                IsGeneralTabSelected = true;
            }
            else if (IsCheckpointsTabSelected)
            {
                IsCheckpointsTabSelected = false;
                IsDateTimeTabSelected = true;
            }
            else if (IsImagesTabSelected)
            {
                IsImagesTabSelected = false;
                IsCheckpointsTabSelected = true;
                IsConfirmButtonVisible = false;
                IsNextButtonVisible = true;
            }
        }

        public bool PreviousCommand_CanExecute(object? parameter)
        {
            return !IsGeneralTabSelected;
        }

        public void LoadCitiesCommand_Execute(object? parameter)
        {
            Cities.Clear();
            foreach (var location in _locationService.GetAll())
            {
                if (location.Country != SelectedCountry) continue;
                Cities.Add(location.City);
            }
        }

        public void TabControlSelectionChangedCommand_Execute(object? parameter)
        {
            if (IsImagesTabSelected)
            {
                IsNextButtonVisible = false;
                IsConfirmButtonVisible = true;
            }
            else
            {
                IsNextButtonVisible = true;
                IsConfirmButtonVisible = false;
            }
        }
        #endregion
    }
}