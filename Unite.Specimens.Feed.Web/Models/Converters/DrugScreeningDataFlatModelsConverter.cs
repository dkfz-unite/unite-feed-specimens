using Unite.Specimens.Feed.Web.Models.Base;

namespace Unite.Specimens.Feed.Web.Models.Converters;

public class DrugScreeningDataFlatModelsConverter : BaseConverter
{
    private readonly Base.Converters.DrugScreeningModelsConverter _converter = new();


    public Data.Models.SpecimenModel[] Convert(in DrugScreeningDataFlatModel[] models)
    {
        if (models == null)
            return null;

        return models
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

    private Data.Models.DrugScreeningModel[] GetDrugScreeningModels(DrugScreeningModel[] sources)
    {
        return _converter.Convert(sources);
    }
}
