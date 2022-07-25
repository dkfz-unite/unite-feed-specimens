namespace Unite.Specimens.Feed.Web.Models.Base;

public class OrganoidModel
{
    private int? _implantedCellsNumber;
    private bool? _tumorigenicity;
    private string _medium;


    public int? ImplantedCellsNumber { get => _implantedCellsNumber; set => _implantedCellsNumber = value; }
    public bool? Tumorigenicity { get => _tumorigenicity; set => _tumorigenicity = value; }
    public string Medium { get => _medium?.Trim(); set => _medium = value; }

    public OrganoidInterventionModel[] Interventions { get; set; }
}
