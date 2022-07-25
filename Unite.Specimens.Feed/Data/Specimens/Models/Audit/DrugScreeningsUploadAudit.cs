using System.Text;

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
        var message = new StringBuilder();

        message.Append($"{DrugScreeningsCreated} specimen drug screening entries created");

        return message.ToString();
    }
}
