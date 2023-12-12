using System.Text.Json.Serialization;
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


    [JsonPropertyName("id")]
    public string Id { get => _id?.Trim(); init => _id = value; }
    [JsonPropertyName("parent_id")]
    public string ParentId { get => _parentId?.Trim(); init => _parentId = value; }
    [JsonPropertyName("parent_type")]
    public SpecimenType? ParentType { get => _parentType; init => _parentType = value; }
    [JsonPropertyName("donor_id")]
    public string DonorId { get => _donorId?.Trim(); init => _donorId = value; }
    [JsonPropertyName("creation_date")]
    public DateTime? CreationDate { get => _creationDate; init => _creationDate = value; }
    [JsonPropertyName("creation_day")]
    public int? CreationDay { get => _creationDay; init => _creationDay = value; }

    [JsonPropertyName("tissue")]
    public Base.TissueModel Tissue { get; init; }
    public Base.CellLineModel CellLine { get; init; }
    public Base.OrganoidModel Organoid { get; init; }
    public Base.XenograftModel Xenograft { get; init; }

    [JsonPropertyName("molecular_data")]
    public Base.MolecularDataModel MolecularData { get; init; }
    public Base.DrugScreeningModel[] DrugsScreeningData { get; init; }
}
