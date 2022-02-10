using System;

namespace Unite.Specimens.Feed.Data.Specimens.Models
{
    public class OrganoidInterventionModel
    {
        public string Type { get; set; }
        public string Details { get; set; }
        public DateTime? StartDate { get; set; }
        public int? StartDay { get; set; }
        public DateTime? EndDate { get; set; }
        public int? DurationDays { get; set; }
        public string Results { get; set; }
    }
}
