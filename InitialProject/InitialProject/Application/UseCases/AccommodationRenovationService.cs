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
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly ILocationRepository _locationRepository;

        public AccommodationRenovationService()
        {
            _accommodationRenovationRepository = Injector.CreateInstance<IAccommodationRenovationRepository>();
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            _locationRepository = Injector.CreateInstance<ILocationRepository>();
        }

        public AccommodationRenovation Save(Accommodation accommodation, DateTime startDate, DateTime endDate, int renovationLenght, string comment, AccommodationRenovation.RenovationStatus status)
        {
            return _accommodationRenovationRepository.Save(accommodation, startDate, endDate, renovationLenght, comment, status);
        }

        public List<AccommodationRenovation> GetAll()
        {
            return _accommodationRenovationRepository.GetAll();
        }

        internal IEnumerable<AccommodationRenovation> GetByAccommodationId(int accommodationId)
        {
            return _accommodationRenovationRepository.GetAll().FindAll(ar => ar.AccommodationId == accommodationId);
        }

        internal IEnumerable<AccommodationRenovation> GetByOwnerId(int ownerId)
        {
            var ownersRenovations = _accommodationRenovationRepository.GetAll();

            ownersRenovations = LoadAccommodations(ownersRenovations);

            return ownersRenovations.FindAll(or => or.Accommodation.OwnerId == ownerId);
        }

        private List<AccommodationRenovation> LoadAccommodations(IEnumerable<AccommodationRenovation> ownersRenovations)
        {
            var updatedOwnerRenovations = new List<AccommodationRenovation>();

            foreach (var ownerRenovation in ownersRenovations)
            {
                ownerRenovation.Accommodation = _accommodationRepository.GetById(ownerRenovation.AccommodationId);
                ownerRenovation.Accommodation.Location = _locationRepository.GetById(ownerRenovation.Accommodation.LocationId);
                updatedOwnerRenovations.Add(ownerRenovation);
            }

            return updatedOwnerRenovations;
        }

        public void Update(AccommodationRenovation updatedRenovation)
        {
            _accommodationRenovationRepository.Update(updatedRenovation);
        }
    }
}
