namespace Unite.Specimens.Feed.Web.Models.Specimens.Converters;

public class XenograftModelConverter : Base.Converters.SpecimenModelConverter<XenograftModel, Data.Models.XenograftModel>
{
    protected override void Map(in XenograftModel source, ref Data.Models.XenograftModel target)
    {
        base.Map(source, ref target);

        target.MouseStrain = source.MouseStrain;
        target.GroupSize = source.GroupSize;
        target.ImplantType = source.ImplantType;
        target.ImplantLocation = source.ImplantLocation;
        target.ImplantedCellsNumber = source.ImplantedCellsNumber;
        target.Tumorigenicity = source.Tumorigenicity;
        target.TumorGrowthForm = source.TumorGrowthForm;
        target.SurvivalDaysFrom = ParseDuration(source.SurvivalDays)?.From;
        target.SurvivalDaysTo = ParseDuration(source.SurvivalDays)?.To;
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
