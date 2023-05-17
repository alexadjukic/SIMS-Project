using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class GuestRatingService
    {
        private readonly IGuestRatingRepository _ratingRepository;
        public GuestRatingService() 
        { 
            _ratingRepository = Injector.CreateInstance<IGuestRatingRepository>();
        }

        public GuestRating FindRatingByReservationId(int reservationId)
        {
            GuestRating rating = _ratingRepository.GetByReservationId(reservationId);

            return rating;
        }
    }
}
