using Unite.Specimens.Feed.Web.Models.Base;

namespace Unite.Specimens.Feed.Web.Models.Drugs.Converters;

public class AnalysisModelConverter : Base.Converters.AnalysisModelConverter<DrugScreeningModel>
{
    protected override void MapEntries(AnalysisModel<DrugScreeningModel> source, Data.Models.SampleModel target)
    {
        target.DrugScreenings = source.Entries.Distinct().Select(entry =>
        {
            return new Data.Models.Drugs.DrugScreeningModel
            {
                Drug = entry.Drug,
                Gof = entry.Gof,
                Dss = entry.Dss,
                DssS = entry.DssS,
                MinDose = entry.MinDose,
                MaxDose = entry.MaxDose,
                Dose25 = entry.Dose25,
                Dose50 = entry.Dose50,
                Dose75 = entry.Dose75,
                Doses = entry.Doses,
                Responses = entry.Responses
            };

        }).ToArray();
    }
}
