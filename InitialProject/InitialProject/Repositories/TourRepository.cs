using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace InitialProject.Repositories
{
    public class TourRepository : Serializer<Tour>, ITourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";

        private List<Tour> _tours;

        public TourRepository()
        {
            _tours = FromCSV(FilePath);
        }
        public override string[] GetCSVValues(Tour obj)
        {
            string[] csvValues = { obj.Id.ToString(), obj.Name, obj.LocationId.ToString(), obj.Description, obj.Language, obj.MaxGuests.ToString(), obj.StartTime.ToString(), obj.Duration.ToString(), obj.CoverImageUrl, obj.GuideId.ToString(), obj.Status.ToString() };
            return csvValues;
        }

        public override Tour GetObject(string[] csvValues)
        {
            Tour tour = new Tour();

            tour.Id = int.Parse(csvValues[0]);
            tour.Name = csvValues[1];
            tour.LocationId = int.Parse(csvValues[2]);
            tour.Description = csvValues[3];
            tour.Language = csvValues[4];
            tour.MaxGuests = int.Parse(csvValues[5]);
            tour.StartTime = DateTime.Parse(csvValues[6]);
            tour.Duration = int.Parse(csvValues[7]);
            tour.CoverImageUrl = csvValues[8];
            tour.GuideId = int.Parse(csvValues[9]);
            tour.Status = Enum.Parse<TourStatus>(csvValues[10]);

            return tour;
        }

        public List<Tour> GetAll()
        {
            return FromCSV(FilePath);
        }

        public Tour Save(Tour tour)
        {
            tour.Id = NextId();
            _tours = FromCSV(FilePath);
            _tours.Add(tour);
            ToCSV(FilePath, _tours);
            return tour;
        }

        public int NextId()
        {
            _tours = FromCSV(FilePath);
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(t => t.Id) + 1;
        }

        public void Delete(Tour tour)
        {
            _tours = FromCSV(FilePath);
            Tour found = _tours.Find(t => t.Id == tour.Id);
            _tours.Remove(found);
            ToCSV(FilePath, _tours);
        }

        public Tour Update(Tour tour)
        {
            _tours = FromCSV(FilePath);
            Tour current = _tours.Find(t => t.Id == tour.Id);
            int index = _tours.IndexOf(current);
            _tours.Remove(current);
            _tours.Insert(index, tour);
            ToCSV(FilePath, _tours);
            return tour;
        }

        public Tour Create(string name, Location location, int locationId, string description, string language, int maxGuests, DateTime startTime, double duration, string coverImageUrl, int guideId)
        {
            _tours = FromCSV(FilePath);
            Tour newTour = new Tour(NextId(), name, location, locationId, description, language, maxGuests, startTime, duration, coverImageUrl, guideId, TourStatus.NOT_STARTED);
            _tours.Add(newTour);
            ToCSV(FilePath, _tours);
            return newTour;
        }

    }
}
