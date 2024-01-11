using System.Linq.Expressions;
using Unite.Essentials.Extensions;
using Unite.Essentials.Tsv;
using Unite.Specimens.Feed.Web.Models.Base;

namespace Unite.Specimens.Feed.Web.Models.Binders.Extensions;

internal static class MolecularDataBindingExtensions
{
    public static ClassMap<T> MapMolecularData<T>(this ClassMap<T> map, Expression<Func<T, MolecularDataModel>> path)
        where T : class
    {
        return map
            .Map(path.Join(entity => entity.MgmtStatus), "mgmt_status")
            .Map(path.Join(entity => entity.IdhStatus), "idh_status")
            .Map(path.Join(entity => entity.IdhMutation), "idh_mutation")
            .Map(path.Join(entity => entity.GeneExpressionSubtype), "gene_expression_subtype")
            .Map(path.Join(entity => entity.MethylationSubtype), "methylation_subtype")
            .Map(path.Join(entity => entity.GcimpMethylation), "gcimp_methylation");
    }
}
