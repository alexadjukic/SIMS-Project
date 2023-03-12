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

        public Accommodation Save(string name, string location, string type, string capacity, string minDaysForStay, string minDaysBeforeCancel, LocationRepository _repositoryLocation)
        {
            int id = NextId();

            int i = 0;
            int j = 0;
            Location loc = new Location();
            foreach (char c in location)
            {
                if (c == ',')
                    i++;
                else if (i == 0)
                {
                    loc.City += c;

                }
                else if (i == 1)
                {
                    loc.Country += c;
                }
            }
            loc.City = loc.City.Trim();
            loc.Country = loc.Country.Trim();

            foreach (Location locationSearch in _repositoryLocation.GetAll())
            {
                if (locationSearch.City != loc.City)
                    continue;
                if (locationSearch.Country != loc.Country)
                    continue;

                loc = locationSearch;
            }

            if (loc.Id == 0)
            {
                loc = _repositoryLocation.Save(loc.City, loc.Country);
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

            Accommodation accommodation = new Accommodation(id, name, loc, Type, Convert.ToInt32(capacity), Convert.ToInt32(minDaysForStay), Convert.ToInt32(minDaysBeforeCancel));

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
