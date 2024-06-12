namespace Unite.Specimens.Feed.Data;

public class AnalysisWriteAudit
{
    public int DrugsCreated;
    public int DrugScreeningsCreated;

    public HashSet<int> Samples = [];


    public override string ToString()
    {
        return string.Join(Environment.NewLine,
        [
            $"{DrugsCreated} drugs created",
            $"{DrugScreeningsCreated} drug screenings associated"
        ]);
    }
}
