using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public enum AccommodationType {apartman = 1, kuca, koliba};
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public AccommodationType Type { get; set; }
        public int Capacity { get; set; }
        public int MinDaysForStay { get; set; }
        public int MinDaysBeforeCancel { get; set; }

        public Accommodation() { }

        public Accommodation(int id, string name, Location location, AccommodationType type, int capacity, int minDaysForStay, int minDaysBeforeCancel)
        {
            Id = id;
            Name = name;
            Location = location;
            Type = type;
            Capacity = capacity;
            MinDaysForStay = minDaysForStay;
            MinDaysBeforeCancel = minDaysBeforeCancel;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                //Location.ToString(),
                LocationId.ToString(),
                Type.ToString(),
                Capacity.ToString(),
                MinDaysForStay.ToString(),
                MinDaysBeforeCancel.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = Convert.ToInt32(values[2]);
            switch(values[3])
            {
                case "apartman":
                    Type = AccommodationType.apartman;
                    break;
                case "kuca":
                    Type = AccommodationType.kuca;
                    break;
                case "koliba":
                    Type = AccommodationType.koliba;
                    break;
            }
            Capacity = Convert.ToInt32(values[4]);
            MinDaysForStay = Convert.ToInt32(values[5]);
            MinDaysBeforeCancel = Convert.ToInt32(values[6]);
        }
    }

    
}
