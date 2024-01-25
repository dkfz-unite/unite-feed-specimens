namespace Unite.Specimens.Feed.Web.Models.Base.Converters;

public class MaterialModelsConverter
{
    public Data.Models.MaterialModel Convert(in string referenceId, in MaterialModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new Data.Models.MaterialModel
        {
            ReferenceId = referenceId,
            Type = source.Type.Value,
            TumorType = source.TumorType,
            Source = source.Source
        };
    }
}
