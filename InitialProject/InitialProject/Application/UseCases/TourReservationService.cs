using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class TourReservationService
    {
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly TourService _tourService;
        private readonly UserService _userService;
        private readonly VoucherService _voucherService;

        public TourReservationService()
        {
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
            _tourService = new TourService();
            _userService = new UserService();
            _voucherService = new VoucherService();
        }

        public TourReservation GetById(int id)
        {
            var tourReservation = _tourReservationRepository.GetById(id);
            tourReservation.Tour = _tourService.GetById(tourReservation.TourId);
            tourReservation.User = _userService.GetById(tourReservation.UserId);
            return tourReservation;
        }

        public TourReservation GetByTourIdAndUserId(int tourId, int userId)
        {
            var reservations = _tourReservationRepository.GetAll();
            foreach (var reservation in reservations)
            {
                if(reservation.TourId == tourId && reservation.UserId == userId)
                {
                    reservation.Tour = _tourService.GetById(reservation.TourId);
                    reservation.User = _userService.GetById(reservation.UserId);
                    return reservation;
                }
            }
            return null;
        }

        public IEnumerable<TourReservation> GetAllByTour(Tour tour)
        {
            var reservations = _tourReservationRepository.GetAll().Where(r => r.TourId == tour.Id);
            foreach (var reservation in reservations)
            {
                reservation.Tour = tour;
                reservation.User = _userService.GetById(reservation.UserId);
            }
            return reservations;
        }

        public void DeleteAllReservationsForCancelledTour(Tour tour)
        {
            var reservations = _tourReservationRepository.GetAll().Where(r => r.TourId == tour.Id);
            foreach (var reservation in reservations)
            {
                reservation.User = _userService.GetById(reservation.UserId);
                _voucherService.Create(reservation.User);
                _tourReservationRepository.Delete(reservation);
            }
        }

        public void SaveReservation(int selectedTourId, int userId, int numberOfGuests, int age)
        {
            _tourReservationRepository.Save(selectedTourId, userId, numberOfGuests, age);
        }
    }
}
