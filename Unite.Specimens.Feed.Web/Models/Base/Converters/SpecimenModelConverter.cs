using Unite.Essentials.Extensions;

namespace Unite.Specimens.Feed.Web.Models.Base.Converters;

public abstract class SpecimenModelConverter<TSource, TTarget> : ConverterBase
    where TSource : SpecimenModel
    where TTarget : Data.Models.SpecimenModel, new()
{
    private readonly MolecularDataModelConverter _molecularDataModelConverter = new();
    private readonly InterventionModelConverter _interventionModelConverter = new();

    public TTarget Convert(TSource source)
    {
        var target = new TTarget();

        Map(source, ref target);

        return target;
    }


    protected virtual void Map(in TSource source, ref TTarget target)
    {
        target.ReferenceId = source.Id;
        target.CreationDate = source.CreationDate;
        target.CreationDay = source.CreationDay;
        target.Parent = GetSpecimen(source.DonorId, source.ParentId, source.ParentType);
        target.Donor = GetDonor(source.DonorId);
        target.MolecularData = _molecularDataModelConverter.Convert(source.MolecularData);
        target.Interventions = source.Interventions?.Select(_interventionModelConverter.Convert).ToArrayOrNull();
    }
}
