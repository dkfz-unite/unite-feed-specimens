namespace Unite.Specimens.Feed.Web.Models.Base.Converters;

public class OrganoidModelsConverter
{
    public Data.Models.OrganoidModel Convert(in string referenceId, in OrganoidModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new Data.Models.OrganoidModel
        {
            ReferenceId = referenceId,
            ImplantedCellsNumber = source.ImplantedCellsNumber,
            Tumorigenicity = source.Tumorigenicity,
            Medium = source.Medium
        };
    }
}
