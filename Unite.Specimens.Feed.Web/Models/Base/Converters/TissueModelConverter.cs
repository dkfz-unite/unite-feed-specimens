using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Base.Converters;


public class TissueModelConverter
{
    public DataModels.TissueModel Convert(in string referenceId, in TissueModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new DataModels.TissueModel
        {
            ReferenceId = referenceId,
            Type = source.Type.Value,
            TumorType = source.TumorType,
            Source = source.Source
        };
    }
}
