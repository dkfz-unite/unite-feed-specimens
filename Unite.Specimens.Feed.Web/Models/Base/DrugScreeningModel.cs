using System.Text.Json.Serialization;

namespace Unite.Specimens.Feed.Web.Models.Base;

public record DrugScreeningModel
{
    private string _drug;
    private double? _dss;
    private double? _dssSelective;
    private double? _gof;
    private double? _minConcentration;
    private double? _maxConcentration;
    private double? _absIC25;
    private double? _absIC50;
    private double? _absIC75;
    private double? _pI1;
    private double? _pI2;
    private double? _pI3;
    private double? _pI4;
    private double? _pI5;


    /// <summary>
    /// Drug name
    /// </summary>
    public string Drug { get => _drug?.Trim(); init => _drug = value; }

    /// <summary>
    /// Drug sensitivity score (asymmetric)
    /// </summary>
    public double? Dss { get => _dss; init => _dss = value; }

    /// <summary>
    /// Drug sensitivity score (asymmetric) selective
    /// </summary>
    public double? DssSelective { get => _dssSelective; init => _dssSelective = value; }

    /// <summary>
    /// Drug goodness of fit score
    /// </summary>
    public double? Gof { get => _gof; init => _gof = value; }

    /// <summary>
    /// Minimal tested concentration in nano molar
    /// </summary>
    public double? MinConcentration { get => _minConcentration; init => _minConcentration = value; }

    /// <summary>
    /// Maximal concentration tested in nano molar
    /// </summary>
    public double? MaxConcentration { get => _maxConcentration; init => _maxConcentration = value; }

    /// <summary>
    /// Average concentration at 25% inhibition
    /// </summary>
    public double? AbsIC25 { get => _absIC25; init => _absIC25 = value; }

    /// <summary>
    /// Average concentration at 50% inhibition
    /// </summary>
    public double? AbsIC50 { get => _absIC50; init => _absIC50 = value; }

    /// <summary>
    /// Average concentration at 75% inhibition
    /// </summary>
    public double? AbsIC75 { get => _absIC75; init => _absIC75 = value; }

    /// <summary>
    /// Percent inhibition at 1st step of concentration
    /// </summary>
    public double? PI1 { get => _pI1; set => _pI1 = value; }

    /// <summary>
    /// Percent inhibition at 2nd step of concentration
    /// </summary>
    public double? PI2 { get => _pI2; set => _pI2 = value; }

    /// <summary>
    /// Percent inhibition at 3rd step of concentration
    /// </summary>
    public double? PI3 { get => _pI3; set => _pI3 = value; }

    /// <summary>
    /// Percent inhibition at 4th step of concentration
    /// </summary>
    public double? PI4 { get => _pI4; set => _pI4 = value; }

    /// <summary>
    /// Percent inhibition at 5th step of concentration
    /// </summary>
    public double? PI5 { get => _pI5; set => _pI5 = value; }

    [JsonIgnore]
    public double[] Inhibition => GetInhibition();


    private double[] GetInhibition()
    {
        var inhibition = new List<double>();

        if (PI1 != null)
            inhibition.Add(PI1.Value);

        if (PI2 != null)
            inhibition.Add(PI2.Value);

        if (PI3 != null)
            inhibition.Add(PI3.Value);

        if (PI4 != null)
            inhibition.Add(PI4.Value);

        if (PI5 != null)
            inhibition.Add(PI5.Value);

        return inhibition.Any() ? inhibition.ToArray() : null;
    }
}
