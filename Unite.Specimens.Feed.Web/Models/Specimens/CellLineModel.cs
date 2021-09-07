using Unite.Data.Entities.Specimens.Cells.Enums;

namespace Unite.Specimens.Feed.Web.Services.Specimens
{
    public class CellLineModel
    {
        public Species? Species { get; set; }
        public CellLineType? Type { get; set; }
        public CellLineCultureType? CultureType { get; set; }
        public string PassageNumber { get; set; }

        public CellLineInfoModel Info { get; set; }

        public void Sanitise()
        {
            Info?.Sanitise();
        }
    }
}
