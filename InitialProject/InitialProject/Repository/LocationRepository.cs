using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class LocationRepository
    {
        private const string FilePath = "../../../Resources/Data/locations.csv";

        private readonly Serializer<Location> _serializer;

        private List<Location> _locations;

        public LocationRepository()
        {
            _serializer = new Serializer<Location>();
            _locations = _serializer.FromCSV(FilePath);
        }

        public List<Location> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<string> GetAllCountries()
        {
            List<string> countries = new List<string>();

            foreach (var location in _locations)
            {
                countries.Add(location.Country);
            }

            countries = countries.Distinct().ToList();

            return countries;
        }

        public Location Save(string city, string country)
        {
            int id = NextId();

            Location location = new Location(id, city, country);
            _locations.Add(location);
            _serializer.ToCSV(FilePath, _locations);
            return location;
        }

        public int NextId()
        {
            _locations = _serializer.FromCSV(FilePath);
            if (_locations.Count < 1)
            {
                return 1;
            }

            return _locations.Max(c => c.Id) + 1;
        }

        public Location GetById(int id)
        {
            Location Location = new Location();
            foreach(Location location in _locations)
            {
                if(location.Id == id)
                {
                    return location;
                }
            }
            return null; 
        }

    }
}
