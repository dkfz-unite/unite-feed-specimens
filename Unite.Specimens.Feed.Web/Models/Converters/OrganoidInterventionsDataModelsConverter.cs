using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Converters;

public class OrganoidInterventionsDataModelsConverter
{
    private readonly Base.Converters.OrganoidModelConverter _converter = new();

    public DataModels.SpecimenModel[] Convert(in OrganoidInterventionsDataModel[] source)
    {
        return source
            .Select(model => new DataModels.OrganoidModel
            {
                ReferenceId = model.SpecimenId,

                Donor = new DataModels.DonorModel
                {
                    ReferenceId = model.DonorId
                },

                Interventions = _converter.Convert(model.Data)
            })
            .ToArray();
    }
}
