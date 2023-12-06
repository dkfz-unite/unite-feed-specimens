using Microsoft.AspNetCore.Mvc.ModelBinding;
using Unite.Essentials.Tsv;

namespace Unite.Specimens.Feed.Web.Models.Binders;

public class TissuesTsvModelBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
            throw new ArgumentNullException(nameof(bindingContext));

        using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);

        var tsv = await reader.ReadToEndAsync();

        var map = new ClassMap<SpecimenDataModel>()
            .Map(entity => entity.Id, "id")
            .Map(entity => entity.DonorId, "donorId")
            .Map(entity => entity.ParentId, "parentId")
            .Map(entity => entity.ParentType, "parentType")
            .Map(entity => entity.CreationDate, "creationDate")
            .Map(entity => entity.CreationDay, "creationDay")
            .Map(entity => entity.Tissue.Type, "type")
            .Map(entity => entity.Tissue.TumorType, "tumorType")
            .Map(entity => entity.Tissue.Source, "source")
            .Map(entity => entity.MolecularData.MgmtStatus, "mgmtStatus")
            .Map(entity => entity.MolecularData.IdhStatus, "idhStatus")
            .Map(entity => entity.MolecularData.IdhMutation, "idhMutation")
            .Map(entity => entity.MolecularData.GeneExpressionSubtype, "geneExpressionSubtype")
            .Map(entity => entity.MolecularData.MethylationSubtype, "methylationSubtype")
            .Map(entity => entity.MolecularData.GcimpMethylation, "gcimpMethylation");

        var model = TsvReader.Read(tsv, map).ToArray();

        bindingContext.Result = ModelBindingResult.Success(model);
    }
}