using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class AccommodationRenovationService
    {
        private readonly IAccommodationRenovationRepository _accommodationRenovationRepository;

        public AccommodationRenovationService()
        {
            _accommodationRenovationRepository = Injector.CreateInstance<IAccommodationRenovationRepository>();
        }

        public AccommodationRenovation Save(Accommodation accommodation, DateTime startDate, DateTime endDate, int renovationLenght, string comment, AccommodationRenovation.RenovationStatus status)
        {
            return _accommodationRenovationRepository.Save(accommodation, startDate, endDate, renovationLenght, comment, status);
        }

        public List<AccommodationRenovation> GetAll()
        {
            return _accommodationRenovationRepository.GetAll();
        }
    }
}
