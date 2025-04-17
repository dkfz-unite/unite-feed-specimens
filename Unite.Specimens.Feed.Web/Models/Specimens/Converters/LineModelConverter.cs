namespace Unite.Specimens.Feed.Web.Models.Specimens.Converters;

public class LineModelConverter : Base.Converters.SpecimenModelConverter<LineModel, Data.Models.LineModel>
{
    protected override void Map(in LineModel source, ref Data.Models.LineModel target)
    {
        base.Map(source, ref target);

        target.CellsType = source.CellsType;
        target.CellsCultureType = source.CellsCultureType;
        target.CellsSpecies = source.CellsSpecies;
        target.Info = Convert(source.Info);
    }


    private static Data.Models.LineInfoModel Convert(in LineInfoModel source)
    {
        if (source == null)
            return null;

        return new Data.Models.LineInfoModel
        {
            Name = source.Name,
            DepositorName = source.DepositorName,
            DepositorEstablishment = source.DepositorEstablishment,
            EstablishmentDate = source.EstablishmentDate,
            PubmedLink = source.PubMedLink,
            AtccLink = source.AtccLink,
            ExpasyLink = source.ExPasyLink
        };
    }
}
