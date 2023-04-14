using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class TourReservation : ISerializable
    {
        public int Id { get; set; }
        public Tour Tour { get; set; }
        public int TourId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int? NumberOfPeople { get; set; }

        public TourReservation() { }

        public TourReservation(int id, int tourId, int userId, int? numberOfPeople)
        {
            Id = id;
            TourId = tourId;
            UserId = userId;
            NumberOfPeople = numberOfPeople;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                UserId.ToString(),
                NumberOfPeople.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            UserId = int.Parse(values[2]);
            NumberOfPeople = int.Parse(values[3]);
        }
    }
}
