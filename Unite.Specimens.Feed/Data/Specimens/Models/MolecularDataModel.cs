using Unite.Data.Entities.Molecular.Enums;

namespace Unite.Specimens.Feed.Data.Specimens.Models
{
    public class MolecularDataModel
    {
        public GeneExpressionSubtype? GeneExpressionSubtype { get; set; }
        public IDHStatus? IdhStatus { get; set; }
        public IDHMutation? IdhMutation { get; set; }
        public MethylationStatus? MethylationStatus { get; set; }
        public MethylationType? MethylationType { get; set; }
        public bool? GcimpMethylation { get; set; }
    }
}
