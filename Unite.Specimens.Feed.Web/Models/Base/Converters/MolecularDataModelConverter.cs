namespace Unite.Specimens.Feed.Web.Models.Base.Converters;

public class MolecularDataModelConverter
{
    public Data.Models.MolecularDataModel Convert(MolecularDataModel source)
    {
        if (source == null)
            return null;

        return new Data.Models.MolecularDataModel
        {
            MgmtStatus = source.MgmtStatus,
            IdhStatus = source.IdhStatus,
            IdhMutation = source.IdhMutation,
            GeneExpressionSubtype = source.GeneExpressionSubtype,
            MethylationSubtype = source.MethylationSubtype,
            GcimpMethylation = source.GcimpMethylation
        };
    }
}
