using System.Text.Json.Serialization;

namespace Unite.Specimens.Feed.Web.Models;

public class XenograftInterventionDataFlatModel : Base.XenograftInterventionModel
{
    private string _donorId;
    private string _specimenId;
    
    [JsonPropertyName("donor_id")]
    public string DonorId { get => _donorId?.Trim(); set => _donorId = value; }

    [JsonPropertyName("specimen_id")]
    public string SpecimenId { get => _specimenId?.Trim(); set => _specimenId = value; }
}
