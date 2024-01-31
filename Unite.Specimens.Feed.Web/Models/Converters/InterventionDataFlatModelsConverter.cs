using Unite.Specimens.Feed.Web.Models.Base;

namespace Unite.Specimens.Feed.Web.Models.Converters;

public class InterventionDataFlatModelsConverter : BaseConverter
{
    private readonly Base.Converters.InterventionModelsConverter _converter = new();


    public Data.Models.SpecimenModel[] Convert(in InterventionDataFlatModel[] models)
    {
        if (models == null)
            return null;
            
        return models
            .GroupBy(intervention => new { intervention.DonorId, intervention.SpecimenId})
            .Select(group =>
            {
                var target = GetSpecimenModel(group.Key.SpecimenId, group.First().SpecimenType.Value);
                target.Donor = GetDonorModel(group.Key.DonorId);
                target.Interventions = GetinterventionModels(group.ToArray());

                return target;
            })
            .ToArray();
    }

    private Data.Models.InterventionModel[] GetinterventionModels(InterventionModel[] sources)
    {
        return _converter.Convert(sources);
    }
}
