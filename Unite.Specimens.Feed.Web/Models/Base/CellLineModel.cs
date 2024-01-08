using System.Text.Json.Serialization;
using Unite.Data.Entities.Specimens.Cells.Enums;

namespace Unite.Specimens.Feed.Web.Models.Base;

public record CellLineModel
{
    [JsonPropertyName("species")]
    public Species? Species { get; init; }
    [JsonPropertyName("type")]
    public CellLineType? Type { get; init; }
    [JsonPropertyName("culture_type")]
    public CellLineCultureType? CultureType { get; init; }

    [JsonPropertyName("info")]
    public CellLineInfoModel Info { get; init; }
}
