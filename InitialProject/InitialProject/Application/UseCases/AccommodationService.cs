﻿using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class AccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly ILocationRepository _locationRepository;

        public AccommodationService()
        {
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            _locationRepository = Injector.CreateInstance<ILocationRepository>();
        }

        public IEnumerable<Accommodation> GetByOwnerId(int id)
        {
            List<Accommodation> myAccommodations = new List<Accommodation>();

            foreach (var accommodation in _accommodationRepository.GetAll())
            {
                if (accommodation.OwnerId == id)
                {
                    accommodation.Location = LoadLocation(accommodation.LocationId);
                    myAccommodations.Add(accommodation);
                }
            }

            return myAccommodations;
        }

        public Location LoadLocation(int locationId)
        {
            return _locationRepository.GetById(locationId);
        }
    }
}