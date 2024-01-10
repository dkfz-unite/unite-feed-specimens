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

    public HashSet<int> Specimens = [];

    public override string ToString()
    {
        return string.Join(Environment.NewLine,
        [
            $"{DonorsCreated} donors created",
            $"{TissuesCreated} tissues created",
            $"{TissuesUpdated} tissues updated",
            $"{CellLinesCreated} cell lines created",
            $"{CellLinesUpdated} cell lines updated",
            $"{OrganoidsCreated} organoids created",
            $"{OrganoidsUpdate} organoids updated",
            $"{XenograftsCreated} xenografts created",
            $"{XenograftsUpdated} xenografts updated",
            $"{DrugScreeningsCreated} specimen drug screening entries created",
            $"{DrugScreeningsUpdated} specimen drug screening entries updated"
        ]);
    }
}
