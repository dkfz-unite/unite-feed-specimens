namespace Unite.Specimens.Feed.Data.Specimens.Models;

public class OrganoidModel : SpecimenModel
{
    public int? ImplantedCellsNumber { get; set; }
    public bool? Tumorigenicity { get; set; }
    public string Medium { get; set; }

    public OrganoidInterventionModel[] Interventions { get; set; }
}
