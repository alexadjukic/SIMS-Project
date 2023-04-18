using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }
        public string Name  { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }


        public Voucher() {}

        public Voucher(string name, DateTime expiryDate, int userId)
        {
            Name = name;
            ExpiryDate = expiryDate;
            UserId = userId;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                ExpiryDate.ToString(),
                UserId.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            ExpiryDate = DateTime.Parse(values[2]);
            UserId = int.Parse(values[3]);
        }

    }
}
