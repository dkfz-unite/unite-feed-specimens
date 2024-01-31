namespace Unite.Specimens.Feed.Data;

public class SpecimensDataUploadAudit
{
    public int DonorsCreated;
    public int MaterialsCreated;
    public int MaterialsUpdated;
    public int LinesCreated;
    public int LinesUpdated;
    public int OrganoidsCreated;
    public int OrganoidsUpdate;
    public int XenograftsCreated;
    public int XenograftsUpdated;
    public int InterventionsCreated;
    public int InterventionsUpdated;
    public int DrugScreeningsCreated;
    public int DrugScreeningsUpdated;

    public HashSet<int> Specimens = [];

    public override string ToString()
    {
        return string.Join(Environment.NewLine,
        [
            $"{DonorsCreated} donors created",
            $"{MaterialsCreated} materials created",
            $"{MaterialsUpdated} materials updated",
            $"{LinesCreated} lines created",
            $"{LinesUpdated} lines updated",
            $"{OrganoidsCreated} organoids created",
            $"{OrganoidsUpdate} organoids updated",
            $"{XenograftsCreated} xenografts created",
            $"{XenograftsUpdated} xenografts updated",
            $"{InterventionsCreated} interventions created",
            $"{InterventionsUpdated} interventions updated",
            $"{DrugScreeningsCreated} specimen drug screening entries created",
            $"{DrugScreeningsUpdated} specimen drug screening entries updated"
        ]);
    }
}
