using Unite.Specimens.Feed.Web.Models.Base;

using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Converters;


public class DrugScreeningDataFlatModelsConverter : BaseConverter
{
    private readonly Base.Converters.DrugScreeningModelConverter _converter = new();


    public DataModels.SpecimenModel[] Convert(in DrugScreeningDataFlatModel[] source)
    {
        return source
            .GroupBy(drugScreening => new { drugScreening.DonorId, drugScreening.SpecimenId })
            .Select(group =>
            {
                var target = GetSpecimenModel(group.Key.SpecimenId, group.First().SpecimenType.Value);

                target.Donor = GetDonorModel(group.Key.DonorId);

                target.DrugScreenings = GetDrugScreeningModels(group.ToArray());

                return target;
            })
            .ToArray();
    }

    private DataModels.DrugScreeningModel[] GetDrugScreeningModels(DrugScreeningModel[] sources)
    {
        return _converter.Convert(sources);
    }
}
