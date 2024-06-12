using Microsoft.AspNetCore.Mvc.ModelBinding;
using Unite.Essentials.Tsv;
using Unite.Specimens.Feed.Web.Models.Base.Binders.Extensions;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Binders;

public class LineTsvModelsBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);

        var tsv = await reader.ReadToEndAsync();

        var map = new ClassMap<LineModel>()
            .MapSpecimen(entity => entity)
            .Map(entity => entity.CellsSpecies, "cells_species")
            .Map(entity => entity.CellsType, "cells_type")
            .Map(entity => entity.CellsCultureType, "cells_culture_type")
            .Map(entity => entity.Info.Name, "name")
            .Map(entity => entity.Info.DepositorName, "depositor_name")
            .Map(entity => entity.Info.DepositorEstablishment, "depositor_establishment")
            .Map(entity => entity.Info.EstablishmentDate, "establishment_date")
            .Map(entity => entity.Info.PubMedLink, "pubmed_link")
            .Map(entity => entity.Info.AtccLink, "atcc_link")
            .Map(entity => entity.Info.ExPasyLink, "expasy_link")
            .Map(entity => entity.Info.ExPasyLink, "expasy_link")
            .MapMolecularData(entity => entity.MolecularData);

        var models = TsvReader.Read(tsv, map).ToArray();

        bindingContext.Result = ModelBindingResult.Success(models);
    }
}
