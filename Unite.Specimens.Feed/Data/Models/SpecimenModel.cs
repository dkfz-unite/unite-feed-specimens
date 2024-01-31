namespace Unite.Specimens.Feed.Data.Models;

public class SpecimenModel
{
    public string ReferenceId { get; set; }
    public DateOnly? CreationDate { get; set; }
    public int? CreationDay { get; set; }

    public SpecimenModel Parent { get; set; }

    public DonorModel Donor { get; set; }

    public MolecularDataModel MolecularData { get; set; }
    public InterventionModel[] Interventions { get; set; }
    public DrugScreeningModel[] DrugScreenings { get; set; }
}
