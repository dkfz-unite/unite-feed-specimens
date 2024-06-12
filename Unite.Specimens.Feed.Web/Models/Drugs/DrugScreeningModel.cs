using System.Text.Json.Serialization;

namespace Unite.Specimens.Feed.Web.Models.Drugs;

public record DrugScreeningModel
{
    private string _drug;
    private double? _gof;
    private double? _dss;
    private double? _dsss;
    private double? _minDose;
    private double? _maxDose;
    private double? _dose25;
    private double? _dose50;
    private double? _dose75;
    private double[] _doses;
    private double[] _responses;


    /// <summary>
    /// Drug name
    /// </summary>
    [JsonPropertyName("drug")]
    public string Drug { get => _drug?.Trim(); init => _drug = value; }

    /// <summary>
    /// Drug sensitivity score (asymmetric)
    /// </summary>
    [JsonPropertyName("dss")]
    public double? Dss { get => _dss; init => _dss = value; }

    /// <summary>
    /// Drug sensitivity score (asymmetric) selective
    /// </summary>
    [JsonPropertyName("dsss")]
    public double? DssS { get => _dsss; init => _dsss = value; }

    /// <summary>
    /// Drug goodness of fit score
    /// </summary>
    [JsonPropertyName("gof")]
    public double? Gof { get => _gof; init => _gof = value; }

    /// <summary>
    /// Minimal tested concentration in nano molar
    /// </summary>
    [JsonPropertyName("min_dose")]
    public double? MinDose { get => _minDose; init => _minDose = value; }

    /// <summary>
    /// Maximal concentration tested in nano molar
    /// </summary>
    [JsonPropertyName("max_dose")]
    public double? MaxDose { get => _maxDose; init => _maxDose = value; }

    /// <summary>
    /// Average concentration at 25% inhibition
    /// </summary>
    [JsonPropertyName("dose_25")]
    public double? Dose25 { get => _dose25; init => _dose25 = value; }

    /// <summary>
    /// Average concentration at 50% inhibition
    /// </summary>
    [JsonPropertyName("dose_50")]
    public double? Dose50 { get => _dose50; init => _dose50 = value; }

    /// <summary>
    /// Average concentration at 75% inhibition
    /// </summary>
    [JsonPropertyName("dose_75")]
    public double? Dose75 { get => _dose75; init => _dose75 = value; }

    /// <summary>
    /// Concentration at corresponding inhibition percent
    /// </summary>
    [JsonPropertyName("doses")]
    public double[] Doses { get => _doses; set => _doses = value; }

    /// <summary>
    /// Percent inhibition at corresponding concentration
    /// </summary>
    [JsonPropertyName("responses")]
    public double[] Responses { get => _responses; set => _responses = value; }
}
