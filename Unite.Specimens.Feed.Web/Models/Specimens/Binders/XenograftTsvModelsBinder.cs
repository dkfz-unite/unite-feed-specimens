using Microsoft.AspNetCore.Mvc.ModelBinding;
using Unite.Essentials.Tsv;
using Unite.Specimens.Feed.Web.Models.Base.Binders.Extensions;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Binders;

public class XenograftTsvModelsBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);

        var tsv = await reader.ReadToEndAsync();

        var map = new ClassMap<XenograftModel>()
            .MapSpecimen(entity => entity)
            .Map(entity => entity.MouseStrain, "mouse_strain")
            .Map(entity => entity.GroupSize, "group_size")
            .Map(entity => entity.ImplantType, "implant_type")
            .Map(entity => entity.ImplantLocation, "tissue_location")
            .Map(entity => entity.ImplantedCellsNumber, "implanted_cells_number")
            .Map(entity => entity.Tumorigenicity, "tumorigenicity")
            .Map(entity => entity.TumorGrowthForm, "tumor_growth_form")
            .Map(entity => entity.SurvivalDays, "survival_days")
            .MapMolecularData(entity => entity.MolecularData);

        var models = TsvReader.Read(tsv, map).ToArray();

        bindingContext.Result = ModelBindingResult.Success(models);
    }
}
