namespace Unite.Specimens.Feed.Data.Models;

public class SampleModel
{
    public SpecimenModel Specimen;
    public AnalysisModel Analysis;

    public IEnumerable<Drugs.DrugScreeningModel> DrugScreenings;
}
