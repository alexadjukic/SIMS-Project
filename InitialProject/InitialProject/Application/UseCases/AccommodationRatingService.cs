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
        public AccommodationRatingService()
        {
            _accommodationRatingRepository = Injector.CreateInstance<IAccommodationRatingRepository>();
        }

        public AccommodationRating FindAccommodationRatingByReservationId(int reservationId)
        {
            AccommodationRating accommodationRating = _accommodationRatingRepository.FindByReservationId(reservationId);
            return accommodationRating;
        }
    }
}
