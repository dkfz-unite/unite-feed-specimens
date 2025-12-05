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
            Family = source.Family,
            Class = source.Class,
            Subclass = source.Subclass
        };
    }
}
