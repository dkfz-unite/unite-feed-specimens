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
    public int DrugScreeningsCreated;
    public int DrugScreeningsUpdated;

    public HashSet<int> Specimens;


    public SpecimensUploadAudit()
    {
        Specimens = new HashSet<int>();
    }


    public override string ToString()
    {
        var messages = new List<string>();

        messages.Add($"{DonorsCreated} donors created");
        messages.Add($"{TissuesCreated} tissues created");
        messages.Add($"{TissuesUpdated} tissues updated");
        messages.Add($"{CellLinesCreated} cell lines created");
        messages.Add($"{CellLinesUpdated} cell lines updated");
        messages.Add($"{OrganoidsCreated} organoids created");
        messages.Add($"{OrganoidsUpdate} organoids updated");
        messages.Add($"{XenograftsCreated} xenografts created");
        messages.Add($"{XenograftsUpdated} xenografts updated");
        messages.Add($"{DrugScreeningsCreated} specimen drug screening entries created");
        messages.Add($"{DrugScreeningsUpdated} specimen drug screening entries updated");

        return string.Join(Environment.NewLine, messages);
    }
}
