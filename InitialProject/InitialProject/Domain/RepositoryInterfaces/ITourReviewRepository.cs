using InitialProject.Domain.Models;
using System.Collections.Generic;

namespace InitialProject.Domain.RepositoryInterfaces
{
    public interface ITourReviewRepository
    {
        public List<TourReview> GetAll();
        public TourReview Save(TourReview review);
        public int NextId();
        public void Delete(TourReview review);
        public TourReview Update(TourReview review);
    }
}
