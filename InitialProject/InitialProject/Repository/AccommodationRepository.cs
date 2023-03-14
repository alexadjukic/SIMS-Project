using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class AccommodationRepository
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

        public Accommodation Save(string name, string city, string country, string type, string capacity, string minDaysForStay, string minDaysBeforeCancel, LocationRepository _repositoryLocation)
        {
            int id = NextId();

            Location location = new Location();

            location.City = city;
            location.Country = country;
            foreach (Location locationSearch in _repositoryLocation.GetAll())
            {
                if (locationSearch.City != location.City)
                    continue;
                if (locationSearch.Country != location.Country)
                    continue;

                location = locationSearch;
            }

            if (location.Id == 0)
            {
                location = _repositoryLocation.Save(location.City, location.Country);
            }

            AccommodationType Type;

            switch(type)
            {
                case "apartment":
                    Type = AccommodationType.apartment;
                    break;
                case "house":
                    Type = AccommodationType.house;
                    break;
                case "cottage":
                    Type = AccommodationType.cottage;
                    break;
                default:
                    Type = 0;
                    break;
            }

            Accommodation accommodation = new Accommodation(id, name, location, Type, Convert.ToInt32(capacity), Convert.ToInt32(minDaysForStay), Convert.ToInt32(minDaysBeforeCancel));

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

    }
}
