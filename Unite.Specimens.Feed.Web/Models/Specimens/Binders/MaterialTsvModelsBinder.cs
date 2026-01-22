using Microsoft.AspNetCore.Mvc.ModelBinding;
using Unite.Essentials.Tsv;
using Unite.Specimens.Feed.Web.Models.Base.Binders.Extensions;
using Unite.Specimens.Feed.Web.Models.Base.Validators.Extensions;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Binders;

public class MaterialTsvModelsBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);

        var tsv = await reader.ReadToEndAsync();

        var map = new ClassMap<MaterialModel>()
            .MapSpecimen(entity => entity)
            .Map(entity => entity.FixationType, "fixation_type")
            .Map(entity => entity.Source, "source")
            .MapTumorClassification(entity => entity.TumorClassification)
            .MapMolecularData(entity => entity.MolecularData);

        var models = TsvReader.Read(tsv, map).ToArray();

        foreach (var model in models)
        {
            if (model.TumorClassification.IsEmpty())
                model.TumorClassification = null;

            if (model.MolecularData.IsEmpty())
                model.MolecularData = null;
        }

        bindingContext.Result = ModelBindingResult.Success(models);
    }
}
