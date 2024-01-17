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
            Medium = source.Medium
        };
    }
}
