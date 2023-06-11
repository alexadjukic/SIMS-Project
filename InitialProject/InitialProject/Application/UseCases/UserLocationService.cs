using InitialProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class UserLocationService
    {
        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly LocationService _locationService;

        public UserLocationService()
        {
            _accommodationReservationService = new AccommodationReservationService();
            _locationService = new LocationService();
        }

        public bool WasUserOnThisLocation(int guestId, int locationId, int ownerId)
        {
            if (guestId == ownerId)
            {
                return true;
            }

            IEnumerable<AccommodationReservation> guestsReservations = _accommodationReservationService.GetGuestsReservations(guestId);

            guestsReservations = _accommodationReservationService.LoadAccommodations(guestsReservations);

            return guestsReservations.FirstOrDefault(gr => gr.Accommodation.LocationId == locationId) != null;
        }
    }
}
