using Unite.Data.Entities.Specimens.Enums;

namespace Unite.Specimens.Feed.Data.Models;

public class MolecularDataModel
{
    public bool? MgmtStatus { get; set; }
    public bool? IdhStatus { get; set; }
    public IdhMutation? IdhMutation { get; set; }
    public bool? TertStatus { get; set; }
    public TertMutation? TertMutation { get; set; }
    public ExpressionSubtype? ExpressionSubtype { get; set; }
    public MethylationSubtype? MethylationSubtype { get; set; }
    public bool? GcimpMethylation { get; set; }
    public string[] GeneKnockouts { get; set; }
}
