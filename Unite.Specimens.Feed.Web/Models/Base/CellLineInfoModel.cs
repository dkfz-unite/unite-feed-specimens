namespace Unite.Specimens.Feed.Web.Models.Base;

public record CellLineInfoModel
{
    private string _name;
    private string _depositorName;
    private string _depositorEstablishment;
    private DateTime? _establishmentDate;

    private string _pubMedLink;
    private string _atccLink;
    private string _exPasyLink;


    /// <summary>
    /// Publicly known cell line name
    /// </summary>
    public string Name { get => _name?.Trim(); init => _name = value; }

    /// <summary>
    /// Depositor name
    /// </summary>
    public string DepositorName { get => _depositorName?.Trim(); init => _depositorName = value; }

    /// <summary>
    /// Establishment, where the cell line was produced
    /// </summary>
    public string DepositorEstablishment { get => _depositorEstablishment?.Trim(); init => _depositorEstablishment = value; }

    /// <summary>
    /// Date when the cell line was produced
    /// </summary>
    public DateTime? EstablishmentDate { get => _establishmentDate; init => _establishmentDate = value; }


    /// <summary>
    /// Link to the cell line in PubMed
    /// </summary>
    public string PubMedLink { get => _pubMedLink?.Trim(); init => _pubMedLink = value; }

    /// <summary>
    /// Link to the cell line in ATCC
    /// </summary>
    public string AtccLink { get => _atccLink?.Trim(); init => _atccLink = value; }

    /// <summary>
    /// Link to the cell line in ExPasy
    /// </summary>
    public string ExPasyLink { get => _exPasyLink?.Trim(); init => _exPasyLink = value; }
}
