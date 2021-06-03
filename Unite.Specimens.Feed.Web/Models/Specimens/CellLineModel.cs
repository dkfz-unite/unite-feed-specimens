using Unite.Data.Entities.Specimens.Cells.Enums;

namespace Unite.Specimens.Feed.Web.Models.Specimens
{
    public class CellLineModel
    {
        public CellLineType? Type { get; set; }
        public Species? Species { get; set; }

        public CellLineInfoModel Info { get; set; }

        public void Sanitise()
        {
            Info?.Sanitise();
        }
    }
}
