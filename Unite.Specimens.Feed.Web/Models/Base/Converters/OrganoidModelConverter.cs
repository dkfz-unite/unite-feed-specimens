using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Base.Converters;


public class OrganoidModelConverter
{
    public DataModels.OrganoidModel Convert(in string referenceId, in OrganoidModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new DataModels.OrganoidModel
        {
            ReferenceId = referenceId,
            ImplantedCellsNumber = source.ImplantedCellsNumber,
            Tumorigenicity = source.Tumorigenicity,
            Medium = source.Medium,

            Interventions = Convert(source.Interventions)
        };
    }


    private static DataModels.OrganoidInterventionModel[] Convert(in OrganoidInterventionModel[] sources)
    {
        return sources?.Length > 0 ? sources.Select(intervention => Convert(intervention)).ToArray() : null;
    }

    private static DataModels.OrganoidInterventionModel Convert(in OrganoidInterventionModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new DataModels.OrganoidInterventionModel
        {
            Type = source.Type,
            Details = source.Details,
            StartDate = FromDateTime(source.StartDate),
            StartDay = source.StartDay,
            EndDate = FromDateTime(source.EndDate),
            DurationDays = source.DurationDays,
            Results = source.Results
        };
    }

    private static DateOnly? FromDateTime(in DateTime? date)
    {
        return date != null ? DateOnly.FromDateTime(date.Value) : null;
    }
}
