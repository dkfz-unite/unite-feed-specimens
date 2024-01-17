using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Base.Converters;


public class LineModelConverter
{
    public DataModels.LineModel Convert(in string referenceId, in LineModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new DataModels.LineModel
        {
            ReferenceId = referenceId,
            CellsSpecies = source.CellsSpecies,
            CellsType = source.CellsType,
            CellsCultureType = source.CellsCultureType,

            Info = Convert(source.Info)
        };
    }


    private static DataModels.LineInfoModel Convert(in LineInfoModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new DataModels.LineInfoModel
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
