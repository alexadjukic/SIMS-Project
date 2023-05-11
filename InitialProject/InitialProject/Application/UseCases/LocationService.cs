using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    class LocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService()
        {
            _locationRepository = Injector.CreateInstance<ILocationRepository>();
        }

        public IEnumerable<Location> GetAll()
        {
            return _locationRepository.GetAll();
        }

        public Location GetByCountryAndCity(string country, string city)
        {
            return _locationRepository.GetAll().Find(l => l.Country == country && l.City == city);
        }

        public IEnumerable<string> GetAllCountries()
        {
            return _locationRepository.GetAll().Select(l => l.Country).ToHashSet();
        }
    }
}
