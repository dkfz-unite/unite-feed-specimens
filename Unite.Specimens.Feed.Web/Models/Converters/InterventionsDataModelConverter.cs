using Unite.Specimens.Feed.Web.Models.Base;

using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Converters;


public class InterventionsDataModelConverter : BaseConverter
{
    private readonly Base.Converters.InterventionModelConverter _converter = new();
    

    public DataModels.SpecimenModel Convert(in InterventionsDataModel source)
    {
        var target = GetSpecimenModel(source.SpecimenId, source.SpecimenType.Value);

        target.Donor = GetDonorModel(source.DonorId);

        target.Interventions = GetinterventionModels(source.Data);

        return target;
    }

    private DataModels.InterventionModel[] GetinterventionModels(InterventionModel[] sources)
    {
        return _converter.Convert(sources);
    }
}
