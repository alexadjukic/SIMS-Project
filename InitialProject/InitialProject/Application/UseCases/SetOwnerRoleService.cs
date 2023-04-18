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
    public class SetOwnerRoleService
    {
        private readonly IAccommodationRatingRepository _accommodationRatingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAccommodationRepository _accommodationRepository;

        public SetOwnerRoleService() 
        {
            _accommodationRatingRepository = Injector.CreateInstance<IAccommodationRatingRepository>();
            _userRepository = Injector.CreateInstance<IUserRepository>();
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
        }

        public int CalculateNumberOfRatings(int ownerId)
        {
            int numberOfRatings = _accommodationRatingRepository.GetAll().Count(ar => ar.OwnerId == ownerId);
            return numberOfRatings;
        }

        public double CalculateTotalRating(int ownerId)
        {
            int numberOfRatings = CalculateNumberOfRatings(ownerId);

            if (numberOfRatings != 0)
            {
                int SumOfRatings = FindSumOfAllRatings(ownerId);
                return (double)SumOfRatings / (2 * (double)numberOfRatings);
            }

            return 0;
        }

        public void SetOwnerRole(int ownerId)
        {
            int numberOfRatings = CalculateNumberOfRatings(ownerId);
            double totalRating = CalculateTotalRating(ownerId);

            _userRepository.SetOwnerRole(ownerId, numberOfRatings, totalRating);
            _accommodationRepository.SetSuperOwnerMark(ownerId, numberOfRatings, totalRating);
        }

        private int FindSumOfAllRatings(int ownerId)
        {
            List<AccommodationRating> accommodationRatingsForOwner = _accommodationRatingRepository.GetAll().FindAll(ar => ar.OwnerId == ownerId);

            int sumRatings = 0;

            foreach (var rating in accommodationRatingsForOwner)
            {
                sumRatings += rating.Cleanliness;
                sumRatings += rating.Correctness;
            }

            return sumRatings;
        }
    }
}
