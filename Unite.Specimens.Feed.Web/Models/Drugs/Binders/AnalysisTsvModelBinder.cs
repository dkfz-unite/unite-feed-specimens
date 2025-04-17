using Unite.Essentials.Tsv;
using Unite.Specimens.Feed.Web.Models.Drugs.Binders.Converters;

namespace Unite.Specimens.Feed.Web.Models.Drugs.Binders;

public class AnalysisTsvModelBinder : Base.Binders.AnalysisTsvModelBinder<DrugScreeningModel>
{
    protected override ClassMap<DrugScreeningModel> CreateMap()
    {
        var doubleArrayConverter = new DoubleArrayConverter();
        
        return new ClassMap<DrugScreeningModel>()
            .Map(entity => entity.Drug, "drug")
            .Map(entity => entity.Gof, "gof")
            .Map(entity => entity.Dss, "dss")
            .Map(entity => entity.DssS, "dsss")
            .Map(entity => entity.DoseMin, "dose_min")
            .Map(entity => entity.DoseMax, "dose_max")
            .Map(entity => entity.Dose25, "dose_25")
            .Map(entity => entity.Dose50, "dose_50")
            .Map(entity => entity.Dose75, "dose_75")
            .Map(entity => entity.Doses, "doses", doubleArrayConverter)
            .Map(entity => entity.Responses, "responses", doubleArrayConverter);
    }
}
