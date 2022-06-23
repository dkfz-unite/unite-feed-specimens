using Unite.Data.Entities.Specimens.Xenografts.Enums;
using Unite.Data.Extensions;

namespace Unite.Specimens.Feed.Web.Services.Specimens;

public class XenograftModel
{
    public string MouseStrain { get; set; }
    public int? GroupSize { get; set; }
    public ImplantType? ImplantType { get; set; }
    public TissueLocation? TissueLocation { get; set; }
    public int? ImplantedCellsNumber { get; set; }
    public bool? Tumorigenicity { get; set; }
    public TumorGrowthForm? TumorGrowthForm { get; set; }
    public string SurvivalDays { get; set; }

    public XenograftInterventionModel[] Interventions { get; set; }


    public void Sanitise()
    {
        MouseStrain = MouseStrain?.Trim();
        SurvivalDays = SurvivalDays?.Trim();

        Interventions?.ForEach(intervention => intervention.Sanitise());
    }
}
