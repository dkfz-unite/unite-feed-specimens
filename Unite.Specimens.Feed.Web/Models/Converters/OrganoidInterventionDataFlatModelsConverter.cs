using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Converters;

public class OrganoidInterventionDataFlatModelsConverter
{
    private readonly Base.Converters.OrganoidModelConverter _converter = new();

    public DataModels.SpecimenModel[] Convert(in OrganoidInterventionDataFlatModel[] source)
    {
        return source
            .GroupBy(intervention => new { intervention.DonorId, intervention.SpecimenId})
            .Select(group => new DataModels.OrganoidModel
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
