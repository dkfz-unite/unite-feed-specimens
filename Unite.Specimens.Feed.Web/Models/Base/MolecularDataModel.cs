using System.Text.Json.Serialization;
using Unite.Data.Entities.Specimens.Enums;

namespace Unite.Specimens.Feed.Web.Models.Base;

public class MolecularDataModel
{
    [JsonPropertyName("mgmt_status")]
    public bool? MgmtStatus { get; set; }

    [JsonPropertyName("idh_status")]
    public bool? IdhStatus { get; set; }

    [JsonPropertyName("idh_mutation")]
    public IdhMutation? IdhMutation { get; set; }

    [JsonPropertyName("tert_status")]
    public bool? TertStatus { get; set; }

    [JsonPropertyName("tert_mutation")]
    public TertMutation? TertMutation { get; set; }

    [JsonPropertyName("expression_subtype")]
    public ExpressionSubtype? ExpressionSubtype { get; set; }

    [JsonPropertyName("methylation_subtype")]
    public MethylationSubtype? MethylationSubtype { get; set; }

    [JsonPropertyName("gcimp_methylation")]
    public bool? GcimpMethylation { get; set; }

    [JsonPropertyName("gene_knockouts")]
    public string[] GeneKnockouts { get; set; }
}
