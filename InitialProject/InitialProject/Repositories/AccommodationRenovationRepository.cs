using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InitialProject.Domain.Models.AccommodationRenovation;

namespace InitialProject.Repositories
{
    public class AccommodationRenovationRepository : IAccommodationRenovationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationRenovations.csv";

        private readonly Serializer<AccommodationRenovation> _serializer;

        private List<AccommodationRenovation> _accommodationRenovations;

        public AccommodationRenovationRepository()
        {
            _serializer = new Serializer<AccommodationRenovation>();
            _accommodationRenovations = _serializer.FromCSV(FilePath);
        }

        public List<AccommodationRenovation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public AccommodationRenovation Save(Accommodation accommodation, DateTime startDate, DateTime endDate, int renovationLenght, string comment, RenovationStatus status)
        {
            int id = NextId();

            AccommodationRenovation accommodationRenovation = new AccommodationRenovation(id, accommodation, startDate, endDate, renovationLenght, comment, status);
            _accommodationRenovations.Add(accommodationRenovation);
            SaveAllRenovations();
            return accommodationRenovation;
        }

        public void SaveAllRenovations()
        {
            _serializer.ToCSV(FilePath, _accommodationRenovations);
        }

        public int NextId()
        {
            _accommodationRenovations = _serializer.FromCSV(FilePath);

            if (_accommodationRenovations.Count < 1)
            {
                return 1;
            }

            return _accommodationRenovations.Max(c => c.Id) + 1;
        }
    }
}
