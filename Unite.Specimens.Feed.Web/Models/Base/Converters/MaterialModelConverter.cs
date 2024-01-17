using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Base.Converters;


public class MaterialModelConverter
{
    public DataModels.MaterialModel Convert(in string referenceId, in MaterialModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new DataModels.MaterialModel
        {
            ReferenceId = referenceId,
            Type = source.Type.Value,
            TumorType = source.TumorType,
            Source = source.Source
        };
    }
}
