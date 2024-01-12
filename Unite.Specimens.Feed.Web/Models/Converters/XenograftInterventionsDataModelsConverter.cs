using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Converters;

public class XenograftInterventionsDataModelsConverter
{
    private readonly Base.Converters.XenograftModelConverter _converter = new();

    public DataModels.SpecimenModel[] Convert(in XenograftInterventionsDataModel[] source)
    {
        return source
            .Select(model => new DataModels.XenograftModel
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
