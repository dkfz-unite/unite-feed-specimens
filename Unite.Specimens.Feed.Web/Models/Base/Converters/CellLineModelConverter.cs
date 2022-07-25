using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Base.Converters;


public class CellLineModelConverter
{
    public DataModels.CellLineModel Convert(in string referenceId, in CellLineModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new DataModels.CellLineModel
        {
            ReferenceId = referenceId,
            Species = source.Species,
            Type = source.Type,
            CultureType = source.CultureType,

            Info = Convert(source.Info)
        };
    }


    private static DataModels.CellLineInfoModel Convert(in CellLineInfoModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new DataModels.CellLineInfoModel
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
