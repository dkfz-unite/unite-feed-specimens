namespace Unite.Specimens.Feed.Data;

public class InterventionsDataUploadAudit
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
