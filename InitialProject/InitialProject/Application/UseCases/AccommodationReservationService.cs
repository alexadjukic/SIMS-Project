﻿using InitialProject.Domain.Models;
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
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly ILocationRepository _locationRepository;

        public AccommodationReservationService()
        {
            _accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
            _ratingRepository = Injector.CreateInstance<IRatingRepository>();
            _userRepository = Injector.CreateInstance<IUserRepository>();
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            _locationRepository = Injector.CreateInstance<ILocationRepository>();
        }

        public IEnumerable<AccommodationReservation> GetRatedReservations(int ownerId, AccommodationRepository accommodationRepository, UserRepository userRepository)
        {
            var ownerReservations = _accommodationReservationRepository.GetAllByOwnerId(ownerId, accommodationRepository, userRepository);

            var ratedReservations = RemoveUnratedReservations(ownerReservations);

            ratedReservations = LoadAccommodations(ratedReservations);

            return ratedReservations;
        }

        public IEnumerable<AccommodationReservation> RemoveUnratedReservations(List<AccommodationReservation> ownerReservations)
        {
            var filteredOwnerReservations = new List<AccommodationReservation>();

            foreach (var reservation in ownerReservations)
            {
                //instead of ratingRepository here will be used accommodationRatingRepository
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


            /*public List<AccommodationReservation> LoadGuests(IEnumerable<AccommodationReservation> ratedReservations)
            {
                var updatedRatedReservations = new List<AccommodationReservation>();

                foreach (var reservation in ratedReservations)
                {
                    reservation.Guest = _userRepository.GetById(reservation.GuestId);
                    updatedRatedReservations.Add(reservation);
                }

                return updatedRatedReservations;
            }*/
        
    }
}