using Unite.Data.Entities.Specimens.Cells.Enums;

namespace Unite.Specimens.Feed.Data.Specimens.Models
{
    public class CellLineModel : SpecimenModel
    {
        public Species? Species { get; set; }
        public CellLineType? Type { get; set; }
        public CellLineCultureType? CultureType { get; set; }

        public CellLineInfoModel Info { get; set; }
    }
}
