using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Base.Converters;


public class MolecularDataModelConverter
{
    public DataModels.MolecularDataModel Convert(in MolecularDataModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new DataModels.MolecularDataModel
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
