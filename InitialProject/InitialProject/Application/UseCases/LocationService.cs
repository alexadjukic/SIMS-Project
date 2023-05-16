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
    public class LocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService()
        {
            _locationRepository = Injector.CreateInstance<ILocationRepository>();
        }

        public IEnumerable<Location> GetLocations()
        {
            return _locationRepository.GetAll();
        }

        public Location GetLocationById(int id)
        {
            return _locationRepository.GetById(id);
        }
    }
}
