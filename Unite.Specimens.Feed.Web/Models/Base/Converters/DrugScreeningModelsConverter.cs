using Unite.Essentials.Extensions;

namespace Unite.Specimens.Feed.Web.Models.Base.Converters;

public class DrugScreeningModelsConverter
{
    public Data.Models.DrugScreeningModel Convert(in DrugScreeningModel source)
    {
        if (source == null)
        {
            return null;
        }

        return new Data.Models.DrugScreeningModel
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

    public Data.Models.DrugScreeningModel[] Convert(in DrugScreeningModel[] sources)
    {
        return sources.IsNotEmpty() ? sources.Select(source => Convert(source)).ToArray() : null;
    }
}
