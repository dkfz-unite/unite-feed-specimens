using System;
using Unite.Data.Entities.Specimens.Tissues.Enums;

namespace Unite.Specimens.Feed.Web.Models.Specimens
{
    public class TissueModel
    {
        public TissueType? Type { get; set; }
        public TumorType? TumorType { get; set; }
        public DateTime? ExtractionDate { get; set; }
        public string Source { get; set; }


        public void Sanitise()
        {
            Source = Source?.Trim();
        }
    }
}
