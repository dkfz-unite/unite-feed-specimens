using System.Text;

namespace Unite.Specimens.Feed.Data.Specimens.Models.Audit;

public class SpecimensUploadAudit
{
    public int DonorsCreated;
    public int TissuesCreated;
    public int TissuesUpdated;
    public int CellLinesCreated;
    public int CellLinesUpdated;
    public int OrganoidsCreated;
    public int OrganoidsUpdate;
    public int XenograftsCreated;
    public int XenograftsUpdated;
    public int MolecularDataCreated;

    public HashSet<int> Specimens;


    public SpecimensUploadAudit()
    {
        Specimens = new HashSet<int>();
    }


    public override string ToString()
    {
        var message = new StringBuilder();

        message.AppendLine($"{DonorsCreated} donors created");
        message.AppendLine($"{TissuesCreated} tissues created");
        message.AppendLine($"{TissuesUpdated} tissues updated");
        message.AppendLine($"{CellLinesCreated} cell lines created");
        message.AppendLine($"{CellLinesUpdated} cell lines updated");
        message.AppendLine($"{OrganoidsCreated} organoids created");
        message.AppendLine($"{OrganoidsUpdate} organoids updated");
        message.AppendLine($"{XenograftsCreated} xenografts created");
        message.AppendLine($"{XenograftsUpdated} xenografts updated");
        message.Append($"{MolecularDataCreated} specimen molecular data entries created");

        return message.ToString();
    }
}
