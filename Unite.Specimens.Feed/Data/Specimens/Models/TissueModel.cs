using System;
using Unite.Data.Entities.Specimens.Tissues.Enums;

namespace Unite.Specimens.Feed.Data.Specimens.Models
{
    public class TissueModel : SpecimenModel
    {
        public TissueType Type { get; set; }
        public TumourType? TumourType { get; set; }
        public DateTime? ExtractionDate { get; set; }
        public string Source { get; set; }
    }
}
