using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class TourRequestService
    {
        private readonly ITourRequestRepository _tourRequestRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly LocationService _locationService;

        public TourRequestService()
        {
            _tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
            _locationRepository = Injector.CreateInstance<ILocationRepository>();
            _locationService = new LocationService();
        }

        public IEnumerable<TourRequest> GetAll()
        {
            return _tourRequestRepository.GetAll();
        }

        public Location FillLocation(string country, string city)
        {
            return _locationRepository.GetByCountryAndCity(country, city);
        }

        public void Save(TourRequest tourRequest)
        {
            _tourRequestRepository.Save(tourRequest);
        }

        public void Update(TourRequest tourRequest)
        {
            _tourRequestRepository.Update(tourRequest);
        }

        public Location GetMostWantedLocation()
        {
            var locationId = _tourRequestRepository.GetAll().GroupBy(r => r.LocationId).OrderByDescending(r => r.Count()).Select(r => r.Key).FirstOrDefault();
            return _locationService.GetLocationById(locationId);
        }

        public string GetMostWantedLanguage()
        {
            return _tourRequestRepository.GetAll().GroupBy(r => r.Language).OrderByDescending(r => r.Count()).Select(r => r.Key).FirstOrDefault();
        }
    }
}
