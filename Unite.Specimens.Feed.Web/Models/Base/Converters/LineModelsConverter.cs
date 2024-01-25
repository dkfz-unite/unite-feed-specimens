namespace Unite.Specimens.Feed.Web.Models.Base.Converters;

public class LineModelsConverter
{
    public Data.Models.LineModel Convert(in string referenceId, in LineModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new Data.Models.LineModel
        {
            ReferenceId = referenceId,
            CellsSpecies = source.CellsSpecies,
            CellsType = source.CellsType,
            CellsCultureType = source.CellsCultureType,

            Info = Convert(source.Info)
        };
    }


    private static Data.Models.LineInfoModel Convert(in LineInfoModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new Data.Models.LineInfoModel
        {
            Name = source.Name,
            DepositorName = source.DepositorName,
            DepositorEstablishment = source.DepositorEstablishment,
            EstablishmentDate = source.EstablishmentDate,
            PubMedLink = source.PubMedLink,
            AtccLink = source.AtccLink,
            ExPasyLink = source.ExPasyLink
        };
    }
}
