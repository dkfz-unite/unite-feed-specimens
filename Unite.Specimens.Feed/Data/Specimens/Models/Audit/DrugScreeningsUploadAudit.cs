namespace Unite.Specimens.Feed.Data.Specimens.Models.Audit;

public class DrugScreeningsUploadAudit
{
    public int DrugScreeningsCreated;

    public HashSet<int> Specimens;


    public DrugScreeningsUploadAudit()
    {
        Specimens = new HashSet<int>();
    }


    public override string ToString()
    {
        var messages = new List<string>();

        messages.Add($"{DrugScreeningsCreated} specimen drug screening entries created");

        return string.Join(Environment.NewLine, messages);
    }
}
