namespace Unite.Specimens.Feed.Web.Models.Base;

public class OrganoidInterventionModel
{
    private string _type;
    private string _details;
    private DateTime? _startDate;
    private int? _startDay;
    private DateTime? _endDate;
    private int? _durationDays;
    private string _results;


    public string Type { get => _type?.Trim(); set => _type = value; }
    public string Details { get => _details?.Trim(); set => _details = value; }
    public DateTime? StartDate { get => _startDate; set => _startDate = value; }
    public int? StartDay { get => _startDay; set => _startDay = value; }
    public DateTime? EndDate { get => _endDate; set => _endDate = value; }
    public int? DurationDays { get => _durationDays; set => _durationDays = value; }
    public string Results { get => _results?.Trim(); set => _results = value; }
}
