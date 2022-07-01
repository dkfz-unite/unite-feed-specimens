namespace Unite.Specimens.Feed.Web.Services.Specimens;

public class OrganoidInterventionModel
{
    public string Type { get; set; }
    public string Details { get; set; }
    public DateTime? StartDate { get; set; }
    public int? StartDay { get; set; }
    public DateTime? EndDate { get; set; }
    public int? DurationDays { get; set; }
    public string Results { get; set; }


    public void Sanitise()
    {
        Type = Type?.Trim();
        Details = Details?.Trim();
        Results = Results?.Trim();
    }
}
