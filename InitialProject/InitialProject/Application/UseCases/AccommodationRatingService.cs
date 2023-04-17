using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class AccommodationRatingService
    {
        private readonly IAccommodationRatingRepository _accommodationRatingRepository;
        private readonly IAccommodationRatingImageRepository _accommodationRatingImageRepository;

        public AccommodationRatingService()
        {
            _accommodationRatingRepository = Injector.CreateInstance<IAccommodationRatingRepository>();
            _accommodationRatingImageRepository = Injector.CreateInstance<IAccommodationRatingImageRepository>();
        }

        public AccommodationRating FindAccommodationRatingByReservationId(int reservationId)
        {
            AccommodationRating accommodationRating = _accommodationRatingRepository.FindByReservationId(reservationId);
            return accommodationRating;
        }

        /*public int CalculateNumberOfRatings(int ownerId)
        {
            int numberOfRatings = _accommodationRatingRepository.CalculateNumberOfRatings(ownerId);
            return numberOfRatings;
        }

        internal double CalculateTotalRating(int ownerId)
        {
            double totalRating = _accommodationRatingRepository.CalculateTotalRating(ownerId);
            return totalRating;
        }

        public void SetOwnerRole(int ownerId)
        {
            int numberOfRatings = CalculateNumberOfRatings(ownerId);
            double totalRating = CalculateTotalRating(ownerId);

            _userRepository.SetOwnerRole(ownerId, numberOfRatings, totalRating);
            _accommodationRepository.SetSuperOwnerMark(ownerId, numberOfRatings, totalRating);
        }*/

        public void SaveImage(string url, int accommodationRatingId)
        {
            _accommodationRatingImageRepository.Save(url, accommodationRatingId);
        }

        public AccommodationRating SaveAccommodationRating(int cleanliness, int correctness, string comment, int reservationId, int ownerId, int raterId) 
        {
            return _accommodationRatingRepository.Save(cleanliness, correctness, comment, reservationId, ownerId, raterId);
        }
    }
}
