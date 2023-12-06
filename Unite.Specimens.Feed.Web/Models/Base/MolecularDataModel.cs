using System.Text.Json.Serialization;
using Unite.Data.Entities.Specimens.Enums;

namespace Unite.Specimens.Feed.Web.Models.Base;

public class MolecularDataModel
{
    [JsonPropertyName("mgmtStatus")]
    public MgmtStatus? MgmtStatus { get; set; }
    [JsonPropertyName("idhStatus")]
    public IdhStatus? IdhStatus { get; set; }
    [JsonPropertyName("idhMutation")]
    public IdhMutation? IdhMutation { get; set; }
    [JsonPropertyName("geneExpressionSubtype")]
    public GeneExpressionSubtype? GeneExpressionSubtype { get; set; }
    [JsonPropertyName("methylationSubtype")]
    public MethylationSubtype? MethylationSubtype { get; set; }
    [JsonPropertyName("gcimpMethylation")]
    public bool? GcimpMethylation { get; set; }
}
