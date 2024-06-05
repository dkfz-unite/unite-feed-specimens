namespace Unite.Specimens.Feed.Data;

public class SpecimensWriteAudit : SpecimenWriteAuditBase
{
    public int MaterialsCreated;
    public int MaterialsUpdated;
    public int LinesCreated;
    public int LinesUpdated;
    public int OrganoidsCreated;
    public int OrganoidsUpdate;
    public int XenograftsCreated;
    public int XenograftsUpdated;

    public override string ToString()
    {
        return string.Join(Environment.NewLine,
        [
            $"{MaterialsCreated} materials created",
            $"{MaterialsUpdated} materials updated",
            $"{LinesCreated} lines created",
            $"{LinesUpdated} lines updated",
            $"{OrganoidsCreated} organoids created",
            $"{OrganoidsUpdate} organoids updated",
            $"{XenograftsCreated} xenografts created",
            $"{XenograftsUpdated} xenografts updated",
            base.ToString()
        ]);
    }
}
