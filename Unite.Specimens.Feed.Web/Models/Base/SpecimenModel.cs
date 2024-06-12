using System.Text.Json.Serialization;
using Unite.Data.Entities.Specimens.Enums;

namespace Unite.Specimens.Feed.Web.Models.Base;

public abstract record SpecimenModel
{
    protected string _id;
    protected string _donorId;
    protected string _parentId;
    protected SpecimenType? _parentType;
    protected DateOnly? _creationDate;
    protected int? _creationDay;

    [JsonPropertyName("id")]
    public virtual string Id { get => _id?.Trim(); set => _id = value; }

    [JsonPropertyName("donor_id")]
    public virtual string DonorId { get => _donorId?.Trim(); set => _donorId = value; }

    [JsonPropertyName("parent_id")]
    public virtual string ParentId { get => _parentId?.Trim(); set => _parentId = value; }

    [JsonPropertyName("parent_type")]
    public virtual SpecimenType? ParentType { get => _parentType; set => _parentType = value; }

    [JsonPropertyName("creation_date")]
    public virtual DateOnly? CreationDate { get => _creationDate; set => _creationDate = value; }

    [JsonPropertyName("creation_day")]
    public virtual int? CreationDay { get => _creationDay; set => _creationDay = value; }

    [JsonPropertyName("molecular_data")]
    public MolecularDataModel MolecularData { get; set; }

    [JsonPropertyName("interventions")]
    public InterventionModel[] Interventions { get; set; }
}
