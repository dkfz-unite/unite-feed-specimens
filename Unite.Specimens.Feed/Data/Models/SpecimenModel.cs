using Unite.Data.Entities.Specimens.Enums;

namespace Unite.Specimens.Feed.Data.Models;

public abstract class SpecimenModel
{
    public string ReferenceId { get; set; }
    public DateOnly? CreationDate { get; set; }
    public int? CreationDay { get; set; }
    public Category? Category { get; set; }
    public TumorType? TumorType { get; set; }
    public byte? TumorGrade { get; set; }

    public SpecimenModel Parent { get; set; }
    public DonorModel Donor { get; set; }


    public TumorClassificationModel TumorClassification { get; set; }
    public MolecularDataModel MolecularData { get; set; }
    public InterventionModel[] Interventions { get; set; }
}
