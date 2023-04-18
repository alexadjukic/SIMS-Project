using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<TourNotification> GetNotificationsByUser(int userId)
        {
            return _tourNotificationRepository.GetAllByUserId(userId);
        }

        public void UpdateNotification(TourNotification notification)
        {
            _tourNotificationRepository.Update(notification);
        }
    }
}
