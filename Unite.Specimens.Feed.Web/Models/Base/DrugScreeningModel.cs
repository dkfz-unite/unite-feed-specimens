namespace Unite.Specimens.Feed.Web.Models.Base;

public record DrugScreeningModel
{
    private string _drug;
    private double? _minConcentration;
    private double? _maxConcentration;
    private double? _dss;
    private double? _dssSelective;
    private double? _absIC25;
    private double? _absIC50;
    private double? _absIC75;


    /// <summary>
    /// Drug name
    /// </summary>
    public string Drug { get => _drug?.Trim(); init => _drug = value; }

    /// <summary>
    /// Minimal tested concentration in nano molar
    /// </summary>
    public double? MinConcentration { get => _minConcentration; init => _minConcentration = value; }

    /// <summary>
    /// Maximal concentration tested in nano molar
    /// </summary>
    public double? MaxConcentration { get => _maxConcentration; init => _maxConcentration = value; }

    /// <summary>
    /// Drug sensitivity score (asymmetric)
    /// </summary>
    public double? Dss { get => _dss; init => _dss = value; }

    /// <summary>
    /// Drug sensitivity score (asymmetric) selective
    /// </summary>
    public double? DssSelective { get => _dssSelective; init => _dssSelective = value; }

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
}
