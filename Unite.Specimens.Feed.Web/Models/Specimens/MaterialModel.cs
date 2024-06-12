using System.Text.Json.Serialization;
using Unite.Data.Entities.Specimens.Materials.Enums;

namespace Unite.Specimens.Feed.Web.Models.Specimens;

public record MaterialModel : Base.SpecimenModel
{
    private string _source;

    [JsonPropertyName("type")]
    public MaterialType? Type { get; init; }

    [JsonPropertyName("tumor_type")]
    public TumorType? TumorType { get; init; }

    [JsonPropertyName("source")]
    public string Source { get => _source?.Trim(); init => _source = value; }
}
