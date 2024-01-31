using System.Text.Json.Serialization;

namespace Unite.Specimens.Feed.Web.Models.Base;

public record InterventionModel
{
    private string _type;
    private string _details;
    private DateOnly? _startDate;
    private int? _startDay;
    private DateOnly? _endDate;
    private int? _durationDays;
    private string _results;


    [JsonPropertyName("type")]
    public string Type { get => _type?.Trim(); set => _type = value; }

    [JsonPropertyName("details")]
    public string Details { get => _details?.Trim(); set => _details = value; }

    [JsonPropertyName("start_date")]
    public DateOnly? StartDate { get => _startDate; set => _startDate = value; }

    [JsonPropertyName("start_day")]
    public int? StartDay { get => _startDay; set => _startDay = value; }

    [JsonPropertyName("end_date")]
    public DateOnly? EndDate { get => _endDate; set => _endDate = value; }

    [JsonPropertyName("duration_days")]
    public int? DurationDays { get => _durationDays; set => _durationDays = value; }

    [JsonPropertyName("results")]
    public string Results { get => _results?.Trim(); set => _results = value; }
}
