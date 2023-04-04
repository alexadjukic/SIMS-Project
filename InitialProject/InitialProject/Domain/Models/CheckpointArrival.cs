using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace InitialProject.Domain.Models
{
    public class CheckpointArrival : ISerializable
    {
        public int Id { get; set; }
        public int CheckpointId { get; set; }
        public int UserId { get; set; }

        public CheckpointArrival()
        {

        }

        public CheckpointArrival(int id, int checkpointId, int userId)
        {
            Id = id;
            CheckpointId = checkpointId;
            UserId = userId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), CheckpointId.ToString(), UserId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            CheckpointId = int.Parse(values[1]);
            UserId = int.Parse(values[2]);
        }
    }
}
