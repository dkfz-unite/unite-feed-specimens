using System.Text.Json.Serialization;
using Unite.Data.Entities.Specimens.Lines.Enums;

namespace Unite.Specimens.Feed.Web.Models.Base;

public record LineModel
{
    [JsonPropertyName("cells_species")]
    public CellsSpecies? CellsSpecies { get; init; }

    [JsonPropertyName("cells_type")]
    public CellsType? CellsType { get; init; }

    [JsonPropertyName("cells_culture_type")]
    public CellsCultureType? CellsCultureType { get; init; }


    [JsonPropertyName("info")]
    public LineInfoModel Info { get; init; }
}
