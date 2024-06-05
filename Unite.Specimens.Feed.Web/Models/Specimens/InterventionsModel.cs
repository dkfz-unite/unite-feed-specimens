using System.Text.Json.Serialization;
using Unite.Data.Entities.Specimens.Enums;
using Unite.Specimens.Feed.Web.Models.Base;

namespace Unite.Specimens.Feed.Web.Models.Specimens;

public record InterventionsModel
{
    private string _donorId;
    private string _specimenId;
    private SpecimenType? _specimenType;


    [JsonPropertyName("donor_id")]
    public string DonorId { get => _donorId?.Trim(); set => _donorId = value; }

    [JsonPropertyName("specimen_id")]
    public string SpecimenId { get => _specimenId?.Trim(); set => _specimenId = value; }

    [JsonPropertyName("specimen_type")]
    public SpecimenType? SpecimenType { get => _specimenType; set => _specimenType = value; }

    [JsonPropertyName("entries")]
    public InterventionModel[] Entries { get; set; }
}
