using System.Text.Json.Serialization;
using Unite.Data.Entities.Specimens.Xenografts.Enums;

namespace Unite.Specimens.Feed.Web.Models.Base;

public record XenograftModel
{
    private string _mouseStrain;
    private int? _groupSize;
    private ImplantType? _implantType;
    private ImplantLocation? _implantLocation;
    private int? _implantedCellsNumber;
    private bool? _tumorigenicity;
    private TumorGrowthForm? _tumorGrowthForm;
    private string _survivalDays;


    [JsonPropertyName("mouse_strain")]
    public string MouseStrain { get => _mouseStrain?.Trim(); init => _mouseStrain = value; }

    [JsonPropertyName("group_size")]
    public int? GroupSize { get => _groupSize; init => _groupSize = value; }

    [JsonPropertyName("implant_type")]
    public ImplantType? ImplantType { get => _implantType; init => _implantType = value; }

    [JsonPropertyName("implant_location")]
    public ImplantLocation? ImplantLocation { get => _implantLocation; init => _implantLocation = value; }

    [JsonPropertyName("implanted_cells_number")]
    public int? ImplantedCellsNumber { get => _implantedCellsNumber; init => _implantedCellsNumber = value; }

    [JsonPropertyName("tumorigenicity")]
    public bool? Tumorigenicity { get => _tumorigenicity; init => _tumorigenicity = value; }

    [JsonPropertyName("tumor_growth_form")]
    public TumorGrowthForm? TumorGrowthForm { get => _tumorGrowthForm; init => _tumorGrowthForm = value; }

    [JsonPropertyName("survival_days")]
    public string SurvivalDays { get => _survivalDays?.Trim(); init => _survivalDays = value; }
}
