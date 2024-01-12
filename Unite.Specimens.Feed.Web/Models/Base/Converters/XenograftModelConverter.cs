using Unite.Essentials.Extensions;

using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Base.Converters;


public class XenograftModelConverter
{
    public DataModels.XenograftModel Convert(in string referenceId, in XenograftModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new DataModels.XenograftModel
        {
            ReferenceId = referenceId,
            MouseStrain = source.MouseStrain,
            GroupSize = source.GroupSize,
            ImplantType = source.ImplantType,
            ImplantLocation = source.ImplantLocation,
            ImplantedCellsNumber = source.ImplantedCellsNumber,
            Tumorigenicity = source.Tumorigenicity,
            TumorGrowthForm = source.TumorGrowthForm,
            SurvivalDaysFrom = ParseDuration(source.SurvivalDays)?.From,
            SurvivalDaysTo = ParseDuration(source.SurvivalDays)?.To,

            Interventions = Convert(source.Interventions)
        };
    }


    public DataModels.XenograftInterventionModel[] Convert(in XenograftInterventionModel[] sources)
    {
        return sources.IsNotEmpty() ? sources.Select(intervention => Convert(intervention)).ToArray() : null;
    }

    public DataModels.XenograftInterventionModel Convert(in XenograftInterventionModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new DataModels.XenograftInterventionModel
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

    private static (int From, int To)? ParseDuration(string duration)
    {
        if (string.IsNullOrWhiteSpace(duration))
        {
            return null;
        }

        if (duration.Contains('-'))
        {
            var parts = duration.Split('-');

            var start = int.Parse(parts[0]);
            var end = int.Parse(parts[1]);

            return (start, end);
        }
        else
        {
            var start = int.Parse(duration);
            var end = int.Parse(duration);

            return (start, end);
        }
    }
}
