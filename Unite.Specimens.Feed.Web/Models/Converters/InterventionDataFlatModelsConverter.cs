using Unite.Specimens.Feed.Web.Models.Base;

using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Converters;


public class InterventionDataFlatModelsConverter : BaseConverter
{
    private readonly Base.Converters.InterventionModelConverter _converter = new();


    public DataModels.SpecimenModel[] Convert(in InterventionDataFlatModel[] source)
    {
        return source
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

    private DataModels.InterventionModel[] GetinterventionModels(InterventionModel[] sources)
    {
        return _converter.Convert(sources);
    }
}
