using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class TourReviewService
    {
        private readonly ITourReviewRepository _tourReviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICheckpointArrivalRepository _checkpointArrivalRepository;
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly ICheckpointRepository _checkpointRepository;

        public TourReviewService()
        {
            _tourReviewRepository = Injector.CreateInstance<ITourReviewRepository>();
            _userRepository = Injector.CreateInstance<IUserRepository>();
            _checkpointArrivalRepository = Injector.CreateInstance<ICheckpointArrivalRepository>();
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
            _checkpointRepository = Injector.CreateInstance<ICheckpointRepository>();
        }

        public IEnumerable<TourReview> GetReviewsByTour(Tour tour)
        {
            List<TourReview> reviews = new();
            foreach(var review in _tourReviewRepository.GetAll())
            {
                var arrival = _checkpointArrivalRepository.GetById(review.Id);
                var reservation = _tourReservationRepository.GetById(arrival.ReservationId);
                reservation.User = _userRepository.GetById(reservation.UserId);
                arrival.Checkpoint = _checkpointRepository.GetById(arrival.CheckpointId);
                if (reservation.TourId == tour.Id)
                {
                    arrival.Reservation = reservation;
                    review.Arrival = arrival;
                    reviews.Add(review);
                }
            }

            return reviews;
        }
    }
}
