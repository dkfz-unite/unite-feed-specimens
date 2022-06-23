using Unite.Data.Entities.Specimens.Tissues.Enums;

namespace Unite.Specimens.Feed.Web.Services.Specimens;

public class TissueModel
{
    public TissueType? Type { get; set; }
    public TumorType? TumorType { get; set; }
    public string Source { get; set; }


    public void Sanitise()
    {
        Source = Source?.Trim();
    }
}
