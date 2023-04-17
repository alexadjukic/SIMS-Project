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
    public class RequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IAccommodationReservationRepository _accommodationReservationRepository;
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IUserRepository _userRepository;

        public RequestService()
        {
            _requestRepository = Injector.CreateInstance<IRequestRepository>();
            _accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            _locationRepository = Injector.CreateInstance<ILocationRepository>();
            _userRepository = Injector.CreateInstance<IUserRepository>();
        }

        public IEnumerable<Request> GetOnHoldRequests()
        {
            var onHoldRequests = _requestRepository.GetAll();

            onHoldRequests = LoadOnHoldRequests(onHoldRequests);

            onHoldRequests = LoadReservations(onHoldRequests);
            return onHoldRequests;
        }

        private List<Request> LoadOnHoldRequests(List<Request> onHoldRequests)
        {
            var updatedOnHoldRequests = new List<Request>();

            foreach (var request in onHoldRequests)
            {
                if (request.Status == RequestStatus.ON_HOLD)
                {
                    updatedOnHoldRequests.Add(request);
                }
            }

            return updatedOnHoldRequests;
        }

        private List<Request> LoadReservations(IEnumerable<Request> onHoldRequests)
        {
            var updatedOnHoldRequests = new List<Request>();

            foreach (var request in onHoldRequests)
            {
                request.Reservation = _accommodationReservationRepository.GetById(request.ReservationId);
                request.Reservation = LoadReservation(request.Reservation);
                request.IsAvailable = _accommodationReservationRepository.IsAvailable(request.NewStartDate, request.NewEndDate, request.ReservationId, request.Reservation.AccommodationId);
                updatedOnHoldRequests.Add(request);
            }

            return updatedOnHoldRequests;
        }

        private AccommodationReservation LoadReservation(AccommodationReservation accommodationReservation)
        {
            var updatedAccommodationReservation = LoadAccommodation(accommodationReservation);
            updatedAccommodationReservation.Guest = _userRepository.GetById(updatedAccommodationReservation.GuestId);
            return updatedAccommodationReservation;
        }

        private AccommodationReservation LoadAccommodation(AccommodationReservation accommodationReservation)
        {
            var updatedReservation = accommodationReservation;

            updatedReservation.Accommodation = _accommodationRepository.GetById(accommodationReservation.AccommodationId);

            updatedReservation.Accommodation.Location = _locationRepository.GetById(updatedReservation.Accommodation.LocationId);

            return updatedReservation;
        }

        public void DeclineRequest(Request selectedRequest)
        {
            _requestRepository.DeclineRequest(selectedRequest);
        }

        internal void AcceptRequest(Request selectedRequest)
        {
            _requestRepository.AcceptRequest(selectedRequest);
            _accommodationReservationRepository.AcceptRequest(selectedRequest);
        }

        public void CreateRequest(DateTime newStartDate, DateTime newEndDate, AccommodationReservation reservation)
        {
            _requestRepository.Save(newStartDate, newEndDate, RequestStatus.ON_HOLD, reservation);
        }
    }
}
