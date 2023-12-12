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
            .Map(entity => entity.DonorId, "donor_id")
            .Map(entity => entity.ParentId, "parent_id")
            .Map(entity => entity.ParentType, "parent_type")
            .Map(entity => entity.CreationDate, "creation_date")
            .Map(entity => entity.CreationDay, "creation_day")
            .Map(entity => entity.Tissue.Type, "type")
            .Map(entity => entity.Tissue.TumorType, "tumor_type")
            .Map(entity => entity.Tissue.Source, "source")
            .Map(entity => entity.MolecularData.MgmtStatus, "mgmt_status")
            .Map(entity => entity.MolecularData.IdhStatus, "idh_status")
            .Map(entity => entity.MolecularData.IdhMutation, "idh_mutation")
            .Map(entity => entity.MolecularData.GeneExpressionSubtype, "gene_expression_subtype")
            .Map(entity => entity.MolecularData.MethylationSubtype, "methylation_subtype")
            .Map(entity => entity.MolecularData.GcimpMethylation, "gcimp_methylation");

        var model = TsvReader.Read(tsv, map).ToArray();

        bindingContext.Result = ModelBindingResult.Success(model);
    }
}