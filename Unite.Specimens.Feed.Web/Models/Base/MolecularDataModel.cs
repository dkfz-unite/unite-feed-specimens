using System.Text.Json.Serialization;
using Unite.Data.Entities.Specimens.Enums;

namespace Unite.Specimens.Feed.Web.Models.Base;

public class MolecularDataModel
{
    [JsonPropertyName("mgmt_status")]
    public MgmtStatus? MgmtStatus { get; set; }

    [JsonPropertyName("idh_status")]
    public IdhStatus? IdhStatus { get; set; }

    [JsonPropertyName("idh_mutation")]
    public IdhMutation? IdhMutation { get; set; }

    [JsonPropertyName("gene_expression_subtype")]
    public GeneExpressionSubtype? GeneExpressionSubtype { get; set; }

    [JsonPropertyName("methylation_subtype")]
    public MethylationSubtype? MethylationSubtype { get; set; }

    [JsonPropertyName("gcimp_methylation")]
    public bool? GcimpMethylation { get; set; }
}
