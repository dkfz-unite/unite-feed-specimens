using Unite.Specimens.Feed.Web.Models.Base;
using Unite.Specimens.Feed.Web.Models.Base.Converters;

using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Converters;


public class DrugScreeningsDataModelConverter : BaseConverter
{
    private readonly DrugScreeningModelConverter _converter = new();


    public DataModels.SpecimenModel Convert(in DrugScreeningsDataModel source)
    {
        var target = GetSpecimenModel(source.SpecimenId, source.SpecimenType.Value);

        target.Donor = GetDonorModel(source.DonorId);

        target.DrugScreenings = GetDrugScreeningModels(source.Data);

        return target;
    }


    private DataModels.DrugScreeningModel[] GetDrugScreeningModels(DrugScreeningModel[] sources)
    {
        return _converter.Convert(sources);
    }
}
