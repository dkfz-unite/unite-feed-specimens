using Microsoft.AspNetCore.Mvc.ModelBinding;
using Unite.Essentials.Tsv;
using Unite.Specimens.Feed.Web.Models.Binders.Extensions;

namespace Unite.Specimens.Feed.Web.Models.Binders;

public class LineTsvModelsBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);

        var tsv = await reader.ReadToEndAsync();

        var map = new ClassMap<SpecimenDataModel>()
            .MapSpecimen(entity => entity)
            .Map(entity => entity.Line.CellsSpecies, "cells_species")
            .Map(entity => entity.Line.CellsType, "cells_type")
            .Map(entity => entity.Line.CellsCultureType, "cells_culture_type")
            .Map(entity => entity.Line.Info.Name, "name")
            .Map(entity => entity.Line.Info.DepositorName, "depositor_name")
            .Map(entity => entity.Line.Info.DepositorEstablishment, "depositor_establishment")
            .Map(entity => entity.Line.Info.EstablishmentDate, "establishment_date")
            .Map(entity => entity.Line.Info.PubMedLink, "pubmed_link")
            .Map(entity => entity.Line.Info.AtccLink, "atcc_link")
            .Map(entity => entity.Line.Info.ExPasyLink, "expasy_link")
            .Map(entity => entity.Line.Info.ExPasyLink, "expasy_link")
            .MapMolecularData(entity => entity.MolecularData);

        var model = TsvReader.Read(tsv, map).ToArray();

        bindingContext.Result = ModelBindingResult.Success(model);
    }
}
