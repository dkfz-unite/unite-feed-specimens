using Microsoft.AspNetCore.Mvc.ModelBinding;
using Unite.Essentials.Tsv;
using Unite.Specimens.Feed.Web.Models.Binders.Converters;

namespace Unite.Specimens.Feed.Web.Models.Binders;

public class DrugScreeningTsvModelsBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
            throw new ArgumentNullException(nameof(bindingContext));

        using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);

        var tsv = await reader.ReadToEndAsync();

        var doubleArrayConverter = new DoubleArrayConverter();

        var map = new ClassMap<DrugScreeningDataFlatModel>()
            .Map(entity => entity.DonorId, "donor_id")
            .Map(entity => entity.SpecimenId, "specimen_id")
            .Map(entity => entity.SpecimenType, "specimen_type")
            .Map(entity => entity.Drug, "drug")
            .Map(entity => entity.Dss, "dss")
            .Map(entity => entity.DssSelective, "dss_selective")
            .Map(entity => entity.Gof, "gof")
            .Map(entity => entity.AbsIC25, "abs_ic_25")
            .Map(entity => entity.AbsIC50, "abs_ic_50")
            .Map(entity => entity.AbsIC75, "abs_ic_75")
            .Map(entity => entity.MinConcentration, "min_concentration")
            .Map(entity => entity.MaxConcentration, "max_concentration")
            .Map(entity => entity.Concentration, "concentration", doubleArrayConverter)
            .Map(entity => entity.Inhibition, "inhibition", doubleArrayConverter);

        var models = TsvReader.Read(tsv, map).ToArray();

        bindingContext.Result = ModelBindingResult.Success(models);
    }
}
