using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace InitialProject.Application.UseCases
{
    public class TourReviewImageService
    {
        private readonly ITourReviewImageRepository _tourReviewImageRepository;

        public TourReviewImageService()
        {
            _tourReviewImageRepository = Injector.CreateInstance<ITourReviewImageRepository>();
        }

        public void SaveImage(string url, int reviewId)
        {
            _tourReviewImageRepository.Save(url, reviewId);
        }
    }
}
