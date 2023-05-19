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

        public TourRequestService()
        {
            _tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
            _locationRepository = Injector.CreateInstance<ILocationRepository>();
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
    }
}
