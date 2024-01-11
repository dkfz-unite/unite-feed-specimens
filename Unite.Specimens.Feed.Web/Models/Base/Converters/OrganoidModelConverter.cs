using Unite.Essentials.Extensions;

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
        return sources.IsNotEmpty() ? sources.Select(intervention => Convert(intervention)).ToArray() : null;
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
            StartDate = source.StartDate,
            StartDay = source.StartDay,
            EndDate = source.EndDate,
            DurationDays = source.DurationDays,
            Results = source.Results
        };
    }
}
