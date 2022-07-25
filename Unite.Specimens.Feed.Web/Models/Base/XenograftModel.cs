using Unite.Data.Entities.Specimens.Xenografts.Enums;

namespace Unite.Specimens.Feed.Web.Models.Base;

public record XenograftModel
{
    private string _mouseStrain;
    private int? _groupSize;
    private ImplantType? _implantType;
    private TissueLocation? _tissueLocation;
    private int? _implantedCellsNumber;
    private bool? _tumorigenicity;
    private TumorGrowthForm? _tumorGrowthForm;
    private string _survivalDays;


    public string MouseStrain { get => _mouseStrain?.Trim(); init => _mouseStrain = value; }
    public int? GroupSize { get => _groupSize; init => _groupSize = value; }
    public ImplantType? ImplantType { get => _implantType; init => _implantType = value; }
    public TissueLocation? TissueLocation { get => _tissueLocation; init => _tissueLocation = value; }
    public int? ImplantedCellsNumber { get => _implantedCellsNumber; init => _implantedCellsNumber = value; }
    public bool? Tumorigenicity { get => _tumorigenicity; init => _tumorigenicity = value; }
    public TumorGrowthForm? TumorGrowthForm { get => _tumorGrowthForm; init => _tumorGrowthForm = value; }
    public string SurvivalDays { get => _survivalDays?.Trim(); init => _survivalDays = value; }

    public XenograftInterventionModel[] Interventions { get; init; }
}
