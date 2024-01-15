using System.Text.Json.Serialization;
using Unite.Specimens.Feed.Web.Models.Base.Enums;

namespace Unite.Specimens.Feed.Web.Models;

public record DrugScreeningsDataModel
{
    private string _donorId;
    private string _specimenId;
    private SpecimenType _specimenType;


    [JsonPropertyName("donor_id")]
    public string DonorId { get => _donorId?.Trim(); init => _donorId = value; }

    [JsonPropertyName("specimen_id")]
    public string SpecimenId { get => _specimenId?.Trim(); init => _specimenId = value; }

    [JsonPropertyName("specimen_type")]
    public SpecimenType SpecimenType { get => _specimenType; init => _specimenType = value; }


    [JsonPropertyName("data")]
    public Base.DrugScreeningModel[] Data { get; init; }
}
