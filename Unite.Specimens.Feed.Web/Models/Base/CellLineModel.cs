using Unite.Data.Entities.Specimens.Cells.Enums;

namespace Unite.Specimens.Feed.Web.Models.Base;

public record CellLineModel
{
    public Species? Species { get; init; }
    public CellLineType? Type { get; init; }
    public CellLineCultureType? CultureType { get; init; }

    public CellLineInfoModel Info { get; init; }
}
