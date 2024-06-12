namespace Unite.Specimens.Feed.Data;

public abstract class SpecimenWriteAuditBase
{
    public int InterventionTypesCreated;
    public int InterventionsCreated;

    public HashSet<int> Specimens = [];

    public override string ToString()
    {
        return string.Join(Environment.NewLine,
        [
            $"{InterventionTypesCreated} intervention types created",
            $"{InterventionsCreated} interventions associated"
        ]);
    }
}
