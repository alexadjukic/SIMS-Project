using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class PointRepository
    {
        private const string FilePath = "../../../Resources/Data/points.csv";

        private readonly Serializer<Point> _serializer;

        private List<Point> _points;

        public PointRepository()
        {
            _serializer = new Serializer<Point>();
            _points = _serializer.FromCSV(FilePath);
        }

        public List<Point> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Point Save(Point point)
        {
            point.Id = NextId();
            _points = _serializer.FromCSV(FilePath);
            _points.Add(point);
            _serializer.ToCSV(FilePath, _points);
            return point;
        }

        public int NextId()
        {
            _points = _serializer.FromCSV(FilePath);
            if (_points.Count < 1)
            {
                return 1;
            }
            return _points.Max(p => p.Id) + 1;
        }

        public void Delete(Point point)
        {
            _points = _serializer.FromCSV(FilePath);
            Point found = _points.Find(p => p.Id == point.Id);
            _points.Remove(found);
            _serializer.ToCSV(FilePath, _points);
        }

        public Point Update(Point point)
        {
            _points = _serializer.FromCSV(FilePath);
            Point current = _points.Find(p => p.Id == point.Id);
            int index = _points.IndexOf(current);
            _points.Remove(current);
            _points.Insert(index, point);
            _serializer.ToCSV(FilePath, _points);
            return point;
        }

        public Point Create(string name, bool active, Tour tour, int tourId)
        {
            _points = _serializer.FromCSV(FilePath);
            Point newPoint = new Point(NextId(), name, active, tour, tourId);
            _points.Add(newPoint);
            _serializer.ToCSV(FilePath, _points);
            return newPoint;
        }
    }
}
