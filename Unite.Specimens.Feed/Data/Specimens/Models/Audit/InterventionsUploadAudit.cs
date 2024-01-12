namespace Unite.Specimens.Feed.Data.Specimens.Models.Audit;

public class InterventionsUploadAudit
{
    public int InterventionsCreated;

    public HashSet<int> Specimens = [];

    public override string ToString()
    {
        return string.Join(Environment.NewLine,
        [
            $"{InterventionsCreated} specimen interventions created"
        ]);
    }
}
