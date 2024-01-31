using Unite.Essentials.Extensions;

namespace Unite.Specimens.Feed.Web.Models.Base.Converters;

public class InterventionModelsConverter
{
    public Data.Models.InterventionModel Convert(in InterventionModel source)
    {
        if (source == null)
        {
            return null;
        }

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

    public Data.Models.InterventionModel[] Convert(in InterventionModel[] sources)
    {
        return sources.IsNotEmpty() ? sources.Select(source => Convert(source)).ToArray() : null;
    }
}
