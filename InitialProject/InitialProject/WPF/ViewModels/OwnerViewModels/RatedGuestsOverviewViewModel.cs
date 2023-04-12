using InitialProject.Application.UseCases;
using InitialProject.Domain.Models;
using InitialProject.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModels.OwnerViewModels
{
    public class RatedGuestsOverviewViewModel : ViewModelBase
    {
        private readonly Window _ratedGuestsOverview;
        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly int _ownerId;
        private readonly AccommodationRepository _accommodationRepository;
        private readonly UserRepository _userRepository;

        public ObservableCollection<AccommodationReservation> RatedReservations { get; set; }

        public RatedGuestsOverviewViewModel(Window ratedGuestsOverview, int ownerId, AccommodationRepository accommodationRepository, UserRepository userRepository)
        {
            _ratedGuestsOverview = ratedGuestsOverview;
            _accommodationReservationService = new AccommodationReservationService();
            _ownerId = ownerId;
            _accommodationRepository = accommodationRepository;
            _userRepository = userRepository;

            RatedReservations = new ObservableCollection<AccommodationReservation>();

            LoadRatedReservations();
        }

        public void LoadRatedReservations()
        {
            RatedReservations.Clear();

            foreach (var reservation in _accommodationReservationService.GetRatedReservations(_ownerId, _accommodationRepository, _userRepository))
            {
                RatedReservations.Add(reservation);
            }
        }
    }
}
