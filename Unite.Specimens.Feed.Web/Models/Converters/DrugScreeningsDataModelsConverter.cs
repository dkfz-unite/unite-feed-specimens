using Unite.Specimens.Feed.Web.Models.Base;
using Unite.Specimens.Feed.Web.Models.Base.Converters;

namespace Unite.Specimens.Feed.Web.Models.Converters;

public class DrugScreeningsDataModelsConverter : BaseConverter
{
    private readonly DrugScreeningModelsConverter _converter = new();


    public Data.Models.SpecimenModel Convert(in DrugScreeningsDataModel model)
    {
        if (model == null)
            return null;

        var target = GetSpecimenModel(model.SpecimenId, model.SpecimenType.Value);
        target.Donor = GetDonorModel(model.DonorId);
        target.DrugScreenings = GetDrugScreeningModels(model.Data);

        return target;
    }

    public Data.Models.SpecimenModel[] Convert(in DrugScreeningsDataModel[] models)
    {
        if (models == null)
            return null;

        return models.Select(model => Convert(model)).ToArray();
    }


    private Data.Models.DrugScreeningModel[] GetDrugScreeningModels(DrugScreeningModel[] models)
    {
        return _converter.Convert(models);
    }
}
