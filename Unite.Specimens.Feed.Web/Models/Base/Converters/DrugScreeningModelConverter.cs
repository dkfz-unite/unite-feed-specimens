using Unite.Essentials.Extensions;

using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Base.Converters;


public class DrugScreeningModelConverter
{
    public DataModels.DrugScreeningModel Convert(in DrugScreeningModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new DataModels.DrugScreeningModel
        {
            Drug = source.Drug,
            Dss = source.Dss,
            DssSelective = source.DssSelective,
            Gof = source.Gof,
            MinConcentration = source.MinConcentration,
            MaxConcentration = source.MaxConcentration,
            AbsIC25 = source.AbsIC25,
            AbsIC50 = source.AbsIC50,
            AbsIC75 = source.AbsIC75,
            Concentration = source.Concentration,
            Inhibition = source.Inhibition,
            ConcentrationLine = source.ConcentrationLine,
            IngibitionLine = source.InhibitionLine
        };
    }

    public DataModels.DrugScreeningModel[] Convert(in DrugScreeningModel[] sources)
    {
        return sources.IsNotEmpty() ? sources.Select(source => Convert(source)).ToArray() : null;
    }
}
