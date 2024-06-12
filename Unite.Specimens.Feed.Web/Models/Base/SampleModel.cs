using System.Text.Json.Serialization;
using Unite.Data.Entities.Specimens.Analysis.Enums;
using Unite.Data.Entities.Specimens.Enums;

namespace Unite.Specimens.Feed.Web.Models.Base;

public class SampleModel
{
    private string _donorId;
    private string _specimenId;
    private SpecimenType? _specimenType;
    private AnalysisType? _analysisType;
    private DateOnly? _analysisDate;
    private int? _analysisDay;

    [JsonPropertyName("donor_id")]
    public virtual string DonorId { get => _donorId?.Trim(); set => _donorId = value; }

    [JsonPropertyName("specimen_id")]
    public virtual string SpecimenId { get => _specimenId?.Trim(); set => _specimenId = value; }

    [JsonPropertyName("specimen_type")]
    public virtual SpecimenType? SpecimenType { get => _specimenType; set => _specimenType = value; }

    [JsonPropertyName("analysis_type")]
    public AnalysisType? AnalysisType { get => _analysisType; set => _analysisType = value; }

    [JsonPropertyName("analysis_date")]
    public DateOnly? AnalysisDate { get => _analysisDate; set => _analysisDate = value; }

    [JsonPropertyName("analysis_day")]
    public int? AnalysisDay { get => _analysisDay; set => _analysisDay = value; }
}
