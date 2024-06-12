namespace Unite.Specimens.Feed.Web.Models.Base.Converters;

public class InterventionModelConverter
{
    public Data.Models.InterventionModel Convert(InterventionModel source)
    {
        if (source == null)
            return null;

        return new Data.Models.InterventionModel
        {
            Type = source.Type,
            Details = source.Details,
            StartDate = source.StartDate,
            StartDay = source.StartDay,
            EndDate = source.EndDate,
            DurationDays = source.DurationDays,
            Results = source.Results
        };
    }
}
