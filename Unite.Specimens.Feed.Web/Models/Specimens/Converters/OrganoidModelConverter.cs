namespace Unite.Specimens.Feed.Web.Models.Specimens.Converters;

public class OrganoidModelConverter : Base.Converters.SpecimenModelConverter<OrganoidModel, Data.Models.OrganoidModel>
{
    protected override void Map(in OrganoidModel source, ref Data.Models.OrganoidModel target)
    {
        base.Map(source, ref target);

        target.ImplantedCellsNumber = source.ImplantedCellsNumber;
        target.Tumorigenicity = source.Tumorigenicity;
        target.Medium = source.Medium;
    }
}
