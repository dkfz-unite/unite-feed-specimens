using Microsoft.AspNetCore.Mvc.ModelBinding;
using Unite.Essentials.Tsv;
using Unite.Specimens.Feed.Web.Models.Binders.Extensions;

namespace Unite.Specimens.Feed.Web.Models.Binders;

public class MaterialTsvModelsBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);

        var tsv = await reader.ReadToEndAsync();

        var map = new ClassMap<SpecimenDataModel>()
            .MapSpecimen(entity => entity)
            .Map(entity => entity.Material.Type, "type")
            .Map(entity => entity.Material.TumorType, "tumor_type")
            .Map(entity => entity.Material.Source, "source")
            .MapMolecularData(entity => entity.MolecularData);

        var model = TsvReader.Read(tsv, map).ToArray();

        bindingContext.Result = ModelBindingResult.Success(model);
    }
}
