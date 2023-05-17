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
    public class ManageRequestService
    {
        private readonly IReservationRequestRepository _requestRepository;
        private readonly IAccommodationReservationRepository _accommodationReservationRepository;

        public ManageRequestService() 
        {
            _requestRepository = Injector.CreateInstance<IReservationRequestRepository>();
            _accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
        }

        public void DeclineRequest(ReservationRequest selectedRequest)
        {
            _requestRepository.DeclineRequest(selectedRequest);
        }

        internal void AcceptRequest(ReservationRequest selectedRequest)
        {
            _requestRepository.AcceptRequest(selectedRequest);
            _accommodationReservationRepository.AcceptRequest(selectedRequest);
        }
    }
}
