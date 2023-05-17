using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{

    public class AccommodationReservationService
    {
        private readonly IAccommodationReservationRepository _accommodationReservationRepository;
        private readonly IGuestRatingRepository _ratingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly ILocationRepository _locationRepository;

        private readonly AccommodationAvailabilityService _accommodationAvailabilityService;

        public AccommodationReservationService()
        {
            _accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
            _ratingRepository = Injector.CreateInstance<IGuestRatingRepository>();
            _userRepository = Injector.CreateInstance<IUserRepository>();
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            _locationRepository = Injector.CreateInstance<ILocationRepository>();

            _accommodationAvailabilityService = new AccommodationAvailabilityService();
        }

        public IEnumerable<AccommodationReservation> GetRatedReservations(int ownerId)
        {
            List<AccommodationReservation> ownerReservations = new List<AccommodationReservation>();
            List<int> accommodationIdsForOwner = _accommodationRepository.AccommodationIdsByOwnerId(ownerId);

            foreach (var reservation in _accommodationReservationRepository.GetAll())
            {
                if (accommodationIdsForOwner.Find(a => a == reservation.AccommodationId) != 0)
                {
                    reservation.Guest = _userRepository.GetAll().Find(g => g.Id == reservation.GuestId);
                    ownerReservations.Add(reservation);
                }
            }

            var ratedReservations = RemoveUnratedReservations(ownerReservations);

            ratedReservations = LoadAccommodations(ratedReservations);

            return ratedReservations;
        }

        public IEnumerable<AccommodationReservation> RemoveUnratedReservations(List<AccommodationReservation> ownerReservations)
        {
            var filteredOwnerReservations = new List<AccommodationReservation>();

            foreach (var reservation in ownerReservations)
            {
                if (_ratingRepository.GetByReservationId(reservation.Id) != null)
                {
                    filteredOwnerReservations.Add(reservation);
                }
            }

            return filteredOwnerReservations;
        }

        public List<AccommodationReservation> LoadAccommodations(IEnumerable<AccommodationReservation> ratedReservations)
        {
            var updatedRatedReservations = new List<AccommodationReservation>();

            foreach (var reservation in ratedReservations)
            {
                reservation.Accommodation = _accommodationRepository.GetById(reservation.AccommodationId);
                reservation.Accommodation.Location = _locationRepository.GetById(reservation.Accommodation.LocationId);
                updatedRatedReservations.Add(reservation);
            }

            return updatedRatedReservations;
        }

        private List<AccommodationReservation> LoadGuests(IEnumerable<AccommodationReservation> oldReservations)
        {
            var updatedReservations = new List<AccommodationReservation>();

            foreach (var reservation in oldReservations)
            {
                reservation.Guest = _userRepository.GetById(reservation.GuestId);
                updatedReservations.Add(reservation);
            }

            return updatedReservations;
        }

        public IEnumerable<AccommodationReservation> GetGuestsReservations(int guestId)
        {
            var guestReservations = _accommodationReservationRepository.GetAllByGuestId(guestId);
            guestReservations = LoadAccommodations(guestReservations);
            guestReservations = LoadGuests(guestReservations);

            return guestReservations;
        }

        public void CancelReservation(AccommodationReservation reservation)
        {
            _accommodationReservationRepository.Remove(reservation);
        }

        public bool IsAccommodationAvailable(DateTime startDate, DateTime endDate, int reservationId, int accommodationId)
        {
            string yesNoanswer = _accommodationAvailabilityService.IsAvailable(startDate, endDate, reservationId, accommodationId);
            if (yesNoanswer.Equals("yes")) return true; else return false;
        }

        public IEnumerable<AccommodationReservation> GetAllByOwnerId(int ownerId)
        {
            List<AccommodationReservation> ownerReservations = new List<AccommodationReservation>();
            List<int> accommodationIdsForOwner = _accommodationRepository.AccommodationIdsByOwnerId(ownerId);

            foreach (var reservation in _accommodationReservationRepository.GetAll())
            {
                if (accommodationIdsForOwner.Find(a => a == reservation.AccommodationId) != 0)
                {
                    reservation.Guest = _userRepository.GetAll().Find(g => g.Id == reservation.GuestId);
                    ownerReservations.Add(reservation);
                }
            }

            ownerReservations = LoadAccommodations(ownerReservations);
            return ownerReservations;
        }

        internal IEnumerable<AccommodationReservation> GetByAccommodationId(int accommodationId)
        {
            return _accommodationReservationRepository.GetAll().FindAll(ar => ar.AccommodationId == accommodationId);
        }
    }
}
