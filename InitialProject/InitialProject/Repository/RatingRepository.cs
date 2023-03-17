using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class RatingRepository
    {
        private const string FilePath = "../../../Resources/Data/ratings.csv";

        private readonly Serializer<Rating> _serializer;

        private List<Rating> _ratings;

        public RatingRepository()
        {
            _serializer = new Serializer<Rating>();
            _ratings = _serializer.FromCSV(FilePath);
        }

        public List<Rating> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Rating Save(Rating rating)
        {
            rating.Id = NextId();
            _ratings = _serializer.FromCSV(FilePath);
            _ratings.Add(rating);
            _serializer.ToCSV(FilePath, _ratings);
            return rating;
        }

        public int NextId()
        {
            _ratings = _serializer.FromCSV(FilePath);

            if (_ratings.Count < 1)
            {
                return 1;
            }

            return _ratings.Max(t => t.Id) + 1;
        }
    }
}
