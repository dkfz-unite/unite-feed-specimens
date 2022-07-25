using Unite.Specimens.Feed.Web.Models.Base.Enums;

namespace Unite.Specimens.Feed.Web.Models;

public record SpecimenDataModel
{
    private string _id;
    private string _parentId;
    private SpecimenType? _parentType;
    private string _donorId;
    private DateTime? _creationDate;
    private int? _creationDay;


    public string Id { get => _id?.Trim(); init => _id = value; }
    public string ParentId { get => _parentId?.Trim(); init => _parentId = value; }
    public SpecimenType? ParentType { get => _parentType; init => _parentType = value; }
    public string DonorId { get => _donorId?.Trim(); init => _donorId = value; }
    public DateTime? CreationDate { get => _creationDate; init => _creationDate = value; }
    public int? CreationDay { get => _creationDay; init => _creationDay = value; }

    public Base.TissueModel Tissue { get; init; }
    public Base.CellLineModel CellLine { get; init; }
    public Base.OrganoidModel Organoid { get; init; }
    public Base.XenograftModel Xenograft { get; init; }

    public Base.MolecularDataModel MolecularData { get; init; }
    public Base.DrugScreeningModel[] DrugScreeningData { get; init; }
}
