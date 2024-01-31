using Unite.Data.Entities.Specimens.Xenografts.Enums;

namespace Unite.Specimens.Feed.Data.Models;

public class XenograftModel : SpecimenModel
{
    public string MouseStrain { get; set; }
    public int? GroupSize { get; set; }
    public ImplantType? ImplantType { get; set; }
    public ImplantLocation? ImplantLocation { get; set; }
    public int? ImplantedCellsNumber { get; set; }
    public bool? Tumorigenicity { get; set; }
    public TumorGrowthForm? TumorGrowthForm { get; set; }
    public int? SurvivalDaysFrom { get; set; }
    public int? SurvivalDaysTo { get; set; }
}
