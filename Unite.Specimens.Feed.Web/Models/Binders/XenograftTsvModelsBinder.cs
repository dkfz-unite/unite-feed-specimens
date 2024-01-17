using Microsoft.AspNetCore.Mvc.ModelBinding;
using Unite.Essentials.Tsv;
using Unite.Specimens.Feed.Web.Models.Binders.Extensions;

namespace Unite.Specimens.Feed.Web.Models.Binders;

public class XenograftTsvModelsBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);

        var tsv = await reader.ReadToEndAsync();

        var map = new ClassMap<SpecimenDataModel>()
            .MapSpecimen(entity => entity)
            .Map(entity => entity.Xenograft.MouseStrain, "mouse_strain")
            .Map(entity => entity.Xenograft.GroupSize, "group_size")
            .Map(entity => entity.Xenograft.ImplantType, "implant_type")
            .Map(entity => entity.Xenograft.ImplantLocation, "tissue_location")
            .Map(entity => entity.Xenograft.ImplantedCellsNumber, "implanted_cells_number")
            .Map(entity => entity.Xenograft.Tumorigenicity, "tumorigenicity")
            .Map(entity => entity.Xenograft.TumorGrowthForm, "tumor_growth_form")
            .Map(entity => entity.Xenograft.SurvivalDays, "survival_days")
            .MapMolecularData(entity => entity.MolecularData);

        var model = TsvReader.Read(tsv, map).ToArray();

        bindingContext.Result = ModelBindingResult.Success(model);
    }
}
