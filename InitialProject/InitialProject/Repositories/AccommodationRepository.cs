using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repositories
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodations.csv";

        private readonly Serializer<Accommodation> _serializer;

        private List<Accommodation> _accommodations;

        public AccommodationRepository()
        {
            _serializer = new Serializer<Accommodation>();
            _accommodations = _serializer.FromCSV(FilePath);
        }

        public List<Accommodation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Accommodation Save(string name, Location location, AccommodationType type, string capacity, string minDaysForStay, string minDaysBeforeCancel, int ownerId, LocationRepository _repositoryLocation)
        {
            int id = NextId();

            Accommodation accommodation = new Accommodation(id, name, location, type, Convert.ToInt32(capacity), Convert.ToInt32(minDaysForStay), Convert.ToInt32(minDaysBeforeCancel), ownerId);

            _accommodations.Add(accommodation);
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }

        public int NextId()
        {
            _accommodations = _serializer.FromCSV(FilePath);
            if (_accommodations.Count < 1)
            {
                return 1;
            }

            return _accommodations.Max(c => c.Id) + 1;
        }

        public List<int> AccommodationIdsByOwnerId(int ownerId)
        {
            _accommodations = _serializer.FromCSV(FilePath);

            List<int> accommodationIdsForOwner = new List<int>();

            foreach (var accommodation in _accommodations)
            {
                if (accommodation.OwnerId == ownerId)
                {
                    accommodationIdsForOwner.Add(accommodation.Id);
                }
            }

            return accommodationIdsForOwner;
        }
    }
}
