using System.Text.Json.Serialization;
using Unite.Data.Entities.Specimens.Enums;

namespace Unite.Specimens.Feed.Web.Models;

public record InterventionsDataModel
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


    [JsonPropertyName("data")]
    public Base.InterventionModel[] Data { get; set; }
}
