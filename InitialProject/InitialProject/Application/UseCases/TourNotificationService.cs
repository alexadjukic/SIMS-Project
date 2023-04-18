using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.UseCases
{
    public class TourNotificationService
    {
        private readonly ITourNotificationRepository _tourNotificationRepository;
        public TourNotificationService()
        {
            _tourNotificationRepository = Injector.CreateInstance<ITourNotificationRepository>();
        }

        public IEnumerable<TourNotification> GetNotificationsByUser(int userId)
        {
            return _tourNotificationRepository.GetAllByUserId(userId);
        }
    }
}
