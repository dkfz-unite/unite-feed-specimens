using Microsoft.AspNetCore.Mvc.ModelBinding;
using Unite.Essentials.Tsv;
using Unite.Specimens.Feed.Web.Models.Base.Binders.Extensions;

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
            .Map(entity => entity.Type, "type")
            .Map(entity => entity.FixationType, "fixation_type")
            .Map(entity => entity.TumorType, "tumor_type")
            .Map(entity => entity.TumorGrade, "tumor_grade")
            .Map(entity => entity.Source, "source")
            .MapMolecularData(entity => entity.MolecularData);

        var models = TsvReader.Read(tsv, map).ToArray();

        bindingContext.Result = ModelBindingResult.Success(models);
    }
}
