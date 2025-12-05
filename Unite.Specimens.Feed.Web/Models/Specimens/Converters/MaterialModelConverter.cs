namespace Unite.Specimens.Feed.Web.Models.Specimens.Converters;

public class MaterialModelConverter : Base.Converters.SpecimenModelConverter<MaterialModel, Data.Models.MaterialModel>
{
    protected override void Map(in MaterialModel source, ref Data.Models.MaterialModel target)
    {
        base.Map(source, ref target);

<<<<<<< Updated upstream
        target.Type = source.Type.Value;
=======
>>>>>>> Stashed changes
        target.FixationType = source.FixationType;
        target.Source = source.Source;
    }
}
