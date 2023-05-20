using InitialProject.Application.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using InitialProject.WPF.Views;
using InitialProject.WPF.Views.Guest2Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.Guest2ViewModels
{
    public class AlternativeTourOffersViewModel : ViewModelBase
    {
        #region PROPERTIES

        User LoggedUser { get; set; }
       
        private ObservableCollection<Tour> _alternativeTours;
        public ObservableCollection<Tour> AlternativeTours
        {
            get => _alternativeTours;
            set
            {
                if (value != _alternativeTours)
                {
                    _alternativeTours = value;
                    OnPropertyChanged(nameof(AlternativeTours));
                }
            }
        }

        private Tour _alternativeSelectedTour;
        public Tour AlternativeSelectedTour
        {
            get => _alternativeSelectedTour;
            set
            {
                if (value != _alternativeSelectedTour)
                {
                    _alternativeSelectedTour = value;
                    OnPropertyChanged(nameof(AlternativeSelectedTour));
                }
            }
        }

        public Tour PreviouslySelectedTour { get; set; }

        private readonly Window _alternativeTourOffersView;
        private readonly TourService _tourService;
        private readonly LocationService _locationService;
        #endregion
        public AlternativeTourOffersViewModel(Window alternativeTourOffersView, User user, Tour previouslySelectedTour)
        {
            _alternativeTourOffersView = alternativeTourOffersView;
            LoggedUser = user;
            PreviouslySelectedTour = previouslySelectedTour;
            AlternativeTours = new ObservableCollection<Tour>();

            _tourService = new TourService();
            _locationService = new LocationService();

            CancelCommand = new RelayCommand(CancelCommand_Execute);
            ChooseCommand = new RelayCommand(ChooseCommand_Execute);
            ShowReservedToursCommand = new RelayCommand(ShowReservedToursCommand_Execute);
            OpenNotificationsCommand = new RelayCommand(OpenNotificationsCommand_Execute);
            ShowVouchersCommand = new RelayCommand(ShowVouchersCommand_Execute);
            ShowToursViewCommand = new RelayCommand(ShowToursViewCommand_Execute);
            ShowTourRequestsCommand = new RelayCommand(ShowTourRequestsCommand_Execute);
            ShowStatisticsCommand = new RelayCommand(ShowStatisticsCommand_Execute);

            ShowAlternativeTourOptions();
        }

        public void ShowAlternativeTourOptions()
        {
            foreach (var tour in _tourService.GetTours())
            {
                FillAlternativeTours(tour);
            }
        }

        public void FillAlternativeTours(Tour tour)
        {
            foreach (var location in _locationService.GetLocations())
            {
                if (location.Id == tour.LocationId)
                {
                    tour.Location = _locationService.GetLocationById(location.Id);
                }
            }
            if (PreviouslySelectedTour.LocationId == tour.LocationId && PreviouslySelectedTour.Id != tour.Id)
            {
                if (tour.Status == TourStatus.ACTIVE || tour.Status == TourStatus.NOT_STARTED)
                {
                    AlternativeTours.Add(tour);
                }
            }
        }

        #region COMMANDS

        public RelayCommand CancelCommand { get; }
        public RelayCommand ChooseCommand { get; }
        public RelayCommand ShowToursViewCommand { get; }
        public RelayCommand ShowReservedToursCommand { get; }
        public RelayCommand OpenNotificationsCommand { get; }
        public RelayCommand ShowVouchersCommand { get; }
        public RelayCommand ShowTourRequestsCommand { get; }
        public RelayCommand ShowStatisticsCommand { get; }

        public void ShowStatisticsCommand_Execute(object? parameter)
        {
            RequestedTourStatisticsView requestedTourStatisticsView = new RequestedTourStatisticsView(LoggedUser);
            requestedTourStatisticsView.Show();
            _alternativeTourOffersView.Close();
        }

        public void ShowTourRequestsCommand_Execute(object? parameter)
        {
            TourRequestFormView tourRequestFormView = new TourRequestFormView(LoggedUser);
            tourRequestFormView.Show();
            _alternativeTourOffersView.Close();
        }

        public void OpenNotificationsCommand_Execute(object? parameter)
        {
            TourNotificationsView tourNotificationsView = new TourNotificationsView(LoggedUser);
            tourNotificationsView.Show();
            _alternativeTourOffersView.Close();
        }

        public void ShowVouchersCommand_Execute(object? parameter)
        {
            VouchersView vouchersView = new VouchersView(LoggedUser);
            vouchersView.Show();
            _alternativeTourOffersView.Close();
        }

        public void ShowReservedToursCommand_Execute(object? parameter)
        {
            ReservedToursView reservedToursView = new ReservedToursView(LoggedUser);
            reservedToursView.Show();
            _alternativeTourOffersView.Close();
        }

        public void ShowToursViewCommand_Execute(object? parameter)
        {
            Guest2TourView guest2TourView = new Guest2TourView(LoggedUser);
            guest2TourView.Show();
            _alternativeTourOffersView.Close();
        }

        public void ChooseCommand_Execute(object? parameter)
        {
            if (AlternativeSelectedTour == null)
            {
                return;
            }
            SelectedTourView selectedTourView = new SelectedTourView(AlternativeSelectedTour, LoggedUser);
            selectedTourView.Show();
            _alternativeTourOffersView.Close();
        }
        public void CancelCommand_Execute(object? parameter)
        {
            Guest2TourView guest2TourView = new Guest2TourView(LoggedUser);
            guest2TourView.Show();

            _alternativeTourOffersView.Close();
        }

        #endregion
    }
}
