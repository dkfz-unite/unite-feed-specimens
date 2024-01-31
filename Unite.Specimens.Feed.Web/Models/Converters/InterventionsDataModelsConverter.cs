using Unite.Specimens.Feed.Web.Models.Base;

namespace Unite.Specimens.Feed.Web.Models.Converters;

public class InterventionsDataModelsConverter : BaseConverter
{
    private readonly Base.Converters.InterventionModelsConverter _converter = new();
    

    public Data.Models.SpecimenModel Convert(in InterventionsDataModel source)
    {
        if (source == null)
            return null;

        var target = GetSpecimenModel(source.SpecimenId, source.SpecimenType.Value);
        target.Donor = GetDonorModel(source.DonorId);
        target.Interventions = GetinterventionModels(source.Data);

        return target;
    }

    public Data.Models.SpecimenModel[] Convert(in InterventionsDataModel[] models)
    {
        if (models == null)
            return null;

        return models.Select(model => Convert(model)).ToArray();
    }


    private Data.Models.InterventionModel[] GetinterventionModels(InterventionModel[] sources)
    {
        return _converter.Convert(sources);
    }
}
