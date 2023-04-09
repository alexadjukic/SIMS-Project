using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Models
{
    public enum TourStatus
    {
        NOT_STARTED = 1,
        ACTIVE,
        FINISHED
    }
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxGuests { get; set; }
        public DateTime StartTime { get; set; }
        public double Duration { get; set; }
        public string CoverImageUrl { get; set; }
        public int GuideId { get; set; }
        public TourStatus Status { get; set; }

        public Tour() { }
        public Tour(int id, string name, Location location, int locationId, string description, string language, int maxGuests, DateTime startTime, double duration, string coverImageUrl, int guideId, TourStatus status)
        {
            Id = id;
            Name = name;
            Location = location;
            LocationId = locationId;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            StartTime = startTime;
            Duration = duration;
            CoverImageUrl = coverImageUrl;
            GuideId = guideId;
            Status = status;
        }
    }
}
