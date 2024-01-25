namespace Unite.Specimens.Feed.Data;

public class DrugScreeningsDataUploadAudit
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
