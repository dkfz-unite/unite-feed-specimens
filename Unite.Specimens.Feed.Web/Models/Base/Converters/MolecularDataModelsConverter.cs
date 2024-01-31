namespace Unite.Specimens.Feed.Web.Models.Base.Converters;

public class MolecularDataModelsConverter
{
    public Data.Models.MolecularDataModel Convert(in MolecularDataModel source)
    {
        if (source == null)
        {
            return null;
        }

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
