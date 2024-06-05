using System.Text.Json.Serialization;

namespace Unite.Specimens.Feed.Web.Models.Specimens;

public record OrganoidModel : Base.SpecimenModel
{
    private int? _implantedCellsNumber;
    private bool? _tumorigenicity;
    private string _medium;

    [JsonPropertyName("implanted_cells_number")]
    public int? ImplantedCellsNumber { get => _implantedCellsNumber; set => _implantedCellsNumber = value; }

    [JsonPropertyName("tumorigenicity")]
    public bool? Tumorigenicity { get => _tumorigenicity; set => _tumorigenicity = value; }

    [JsonPropertyName("medium")]
    public string Medium { get => _medium?.Trim(); set => _medium = value; }
}
