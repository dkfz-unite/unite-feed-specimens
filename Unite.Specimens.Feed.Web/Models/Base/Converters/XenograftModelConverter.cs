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
            TissueLocation = source.TissueLocation,
            ImplantedCellsNumber = source.ImplantedCellsNumber,
            Tumorigenicity = source.Tumorigenicity,
            TumorGrowthForm = source.TumorGrowthForm,
            SurvivalDaysFrom = ParseDuration(source.SurvivalDays)?.From,
            SurvivalDaysTo = ParseDuration(source.SurvivalDays)?.To,

            Interventions = Convert(source.Interventions)
        };
    }


    private static DataModels.XenograftInterventionModel[] Convert(in XenograftInterventionModel[] sources)
    {
        return sources?.Length > 0 ? sources.Select(intervention => Convert(intervention)).ToArray() : null;
    }

    private static DataModels.XenograftInterventionModel Convert(in XenograftInterventionModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new DataModels.XenograftInterventionModel
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

    private static DateOnly? FromDateTime(DateTime? date)
    {
        return date != null ? DateOnly.FromDateTime(date.Value) : null;
    }
}
