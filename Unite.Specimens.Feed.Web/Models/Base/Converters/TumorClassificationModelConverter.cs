namespace Unite.Specimens.Feed.Web.Models.Base.Converters;

public class TumorClassificationModelConverter
{
    public Data.Models.TumorClassificationModel Convert(TumorClassificationModel source)
    {
        if (source == null)
            return null;

        return new Data.Models.TumorClassificationModel
        {
            Superfamily = source.Superfamily,
            SuperfamilyScore = source.SuperfamilyScore,
            Family = source.Family,
            FamilyScore = source.FamilyScore,
            Class = source.Class,
            ClassScore = source.ClassScore,
            Subclass = source.Subclass,
            SubclassScore = source.SubclassScore
        };
    }
}
