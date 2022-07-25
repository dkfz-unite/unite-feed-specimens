using Unite.Data.Entities.Specimens.Tissues.Enums;

namespace Unite.Specimens.Feed.Web.Models.Base;

public record TissueModel
{
    private string _source;


    public TissueType? Type { get; init; }
    public TumorType? TumorType { get; init; }
    public string Source { get => _source?.Trim(); init => _source = value; }
}
