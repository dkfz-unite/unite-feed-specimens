namespace Unite.Specimens.Feed.Data.Models;

public class InterventionModel
{
    public string Type { get; set; }
    public string Details { get; set; }
    public DateOnly? StartDate { get; set; }
    public int? StartDay { get; set; }
    public DateOnly? EndDate { get; set; }
    public int? DurationDays { get; set; }
    public string Results { get; set; }
}
