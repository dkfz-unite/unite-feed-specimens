using Unite.Essentials.Extensions;

using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Base.Converters;


public class InterventionModelConverter
{
    public DataModels.InterventionModel Convert(in InterventionModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new DataModels.InterventionModel
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

    public DataModels.InterventionModel[] Convert(in InterventionModel[] sources)
    {
        return sources.IsNotEmpty() ? sources.Select(source => Convert(source)).ToArray() : null;
    }
}
