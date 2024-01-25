namespace Unite.Specimens.Feed.Web.Models.Base.Converters;

public class XenograftModelsConverter
{
    public Data.Models.XenograftModel Convert(in string referenceId, in XenograftModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new Data.Models.XenograftModel
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
            SurvivalDaysTo = ParseDuration(source.SurvivalDays)?.To
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
