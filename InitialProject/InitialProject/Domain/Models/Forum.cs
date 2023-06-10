using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace InitialProject.Domain.Models
{
    public class Forum : ISerializable
    {
        public int Id { get; set; }
        public bool Open { get; set; }
        public int LocationId { get; set; }
        public int CreatorId { get; set; }

        public Forum() { }

        public Forum(int id, bool open, int locationId, int creatorId)
        {
            Id = id;
            Open = open;
            LocationId = locationId;
            CreatorId = creatorId;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Open.ToString(),
                LocationId.ToString(),
                CreatorId.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Open = Convert.ToBoolean(values[1]);
            LocationId = Convert.ToInt32(values[1]);
            CreatorId = Convert.ToInt32(values[2]);
        }
    }
}
