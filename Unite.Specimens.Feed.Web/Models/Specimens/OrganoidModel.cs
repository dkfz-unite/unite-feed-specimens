using Unite.Data.Extensions;

namespace Unite.Specimens.Feed.Web.Services.Specimens;

public class OrganoidModel
{
    public int? ImplantedCellsNumber { get; set; }
    public bool? Tumorigenicity { get; set; }
    public string Medium { get; set; }

    public OrganoidInterventionModel[] Interventions { get; set; }


    public void Sanitise()
    {
        Medium = Medium?.Trim();

        Interventions?.ForEach(intervention => intervention.Sanitise());
    }
}
