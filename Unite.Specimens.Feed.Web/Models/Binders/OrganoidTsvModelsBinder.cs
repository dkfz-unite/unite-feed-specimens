using Microsoft.AspNetCore.Mvc.ModelBinding;
using Unite.Essentials.Tsv;
using Unite.Specimens.Feed.Web.Models.Binders.Extensions;

namespace Unite.Specimens.Feed.Web.Models.Binders;

public class OrganoidTsvModelsBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);

        var tsv = await reader.ReadToEndAsync();

        var map = new ClassMap<SpecimenDataModel>()
            .MapSpecimen(entity => entity)
            .Map(entity => entity.Organoid.Medium, "medium")
            .Map(entity => entity.Organoid.ImplantedCellsNumber, "implanted_cells_number")
            .Map(entity => entity.Organoid.Tumorigenicity, "tumorigenicity")
            .MapMolecularData(entity => entity.MolecularData);

        var models = TsvReader.Read(tsv, map).ToArray();

        bindingContext.Result = ModelBindingResult.Success(models);
    }
}
