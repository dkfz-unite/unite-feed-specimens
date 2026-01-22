using System.Linq.Expressions;
using Unite.Essentials.Extensions;
using Unite.Essentials.Tsv;
using Unite.Essentials.Tsv.Converters;

namespace Unite.Specimens.Feed.Web.Models.Base.Binders.Extensions;

internal static class MolecularDataBindingExtensions
{
    public static ClassMap<T> MapMolecularData<T>(this ClassMap<T> map, Expression<Func<T, MolecularDataModel>> path)
        where T : class
    {
        var arrayConverter = new ArrayConverter();

        return map
            .Map(path.Join(entity => entity.MgmtStatus), "mgmt_status")
            .Map(path.Join(entity => entity.IdhStatus), "idh_status")
            .Map(path.Join(entity => entity.IdhMutation), "idh_mutation")
            .Map(path.Join(entity => entity.TertStatus), "tert_status")
            .Map(path.Join(entity => entity.TertMutation), "tert_mutation")
            .Map(path.Join(entity => entity.ExpressionSubtype), "expression_subtype")
            .Map(path.Join(entity => entity.MethylationSubtype), "methylation_subtype")
            .Map(path.Join(entity => entity.GcimpMethylation), "gcimp_methylation")
            .Map(path.Join(entity => entity.GeneKnockouts), "gene_knockouts", arrayConverter);
    }
}

internal class ArrayConverter : IConverter<string[]>
{
    public object Convert(string value, string row)
    {
        if (string.IsNullOrEmpty(value))
            return null;
        
        return value.Split(',', StringSplitOptions.RemoveEmptyEntries);
    }

    public string Convert(object value, object row)
    {
        throw new NotImplementedException();
    }
}