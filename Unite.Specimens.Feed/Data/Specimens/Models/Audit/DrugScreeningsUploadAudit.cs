namespace Unite.Specimens.Feed.Data.Specimens.Models.Audit;

public class DrugScreeningsUploadAudit
{
    public int DrugScreeningsCreated;

    public HashSet<int> Specimens = [];


    public override string ToString()
    {
        return string.Join(Environment.NewLine,
        [
            $"{DrugScreeningsCreated} specimen drug screening entries created"
        ]);
    }
}
