using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class AccommodationAvailabilityService
    {
        private readonly IAccommodationReservationRepository _accommodationReservationRepository;

        public AccommodationAvailabilityService()
        {
            _accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
        }

        public string IsAvailable(DateTime newStartDate, DateTime newEndDate, int reservationId, int accommodationId)
        {
            List<DateTime> allSingleDates = FindDatesBetween(newStartDate, newEndDate);

            foreach (var date in allSingleDates)
            {
                if (!IsSingleDateAvailable(date, reservationId, accommodationId))
                {
                    return "no";
                }
            }

            return "yes";
        }

        private bool IsSingleDateAvailable(DateTime date, int reservationId, int accommodationId)
        {
            //_accommodationReservations = _serializer.FromCSV(FilePath);
            foreach (var accommodationReservation in _accommodationReservationRepository.GetAll())
            {
                if (accommodationReservation.Id != reservationId && accommodationReservation.AccommodationId == accommodationId)
                {
                    if (FindDatesBetween(accommodationReservation.StartDate, accommodationReservation.EndDate).Contains(date))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private List<DateTime> FindDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> resultingDates = new List<DateTime>();

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                resultingDates.Add(date);
            }

            return resultingDates;
        }

    }
}
