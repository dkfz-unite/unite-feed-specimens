using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Converters;

public class XenograftInterventionDataFlatModelsConverter
{
    private readonly Base.Converters.XenograftModelConverter _converter = new();

    public DataModels.SpecimenModel[] Convert(in XenograftInterventionDataFlatModel[] source)
    {
        return source
            .GroupBy(intervention => new { intervention.DonorId, intervention.SpecimenId})
            .Select(group => new DataModels.XenograftModel
            {
                ReferenceId = group.Key.SpecimenId,

                Donor = new DataModels.DonorModel()
                {
                    ReferenceId = group.Key.DonorId
                },

                Interventions = _converter.Convert(group.ToArray())
            })
            .ToArray();
    }
}
