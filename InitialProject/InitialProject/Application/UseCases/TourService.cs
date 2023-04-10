using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Application.UseCases
{
    public class TourService
    {
        private readonly ITourRepository _tourRepository;
        private readonly ILocationRepository _locationRepository;

        public TourService()
        {
            _tourRepository = Injector.CreateInstance<ITourRepository>();
            _locationRepository = Injector.CreateInstance<ILocationRepository>();
        }

        public IEnumerable<Tour> GetFutureTours()
        {
            var futureTours = _tourRepository.GetAll().Where(t => t.StartTime.Subtract(DateTime.Now).TotalHours > 0);
            LoadLocations(futureTours);
            return futureTours;
        }

        public IEnumerable<Tour> GetPastTours()
        {
            var pastTours = _tourRepository.GetAll().Where(t => t.StartTime.Subtract(DateTime.Now).TotalHours < 0);
            LoadLocations(pastTours);
            return pastTours;
        }

        public void CancelTour(Tour tour)
        {
            tour.Status = TourStatus.CANCELED;
            _tourRepository.Update(tour);
        }

        private void LoadLocations(IEnumerable<Tour> tours)
        {
            foreach (var tour in tours)
            {
                tour.Location = _locationRepository.GetById(tour.LocationId);
            }
        }
    }
}
