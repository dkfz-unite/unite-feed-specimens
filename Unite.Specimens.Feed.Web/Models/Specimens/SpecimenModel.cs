using Unite.Specimens.Feed.Web.Models.Specimens.Enums;

namespace Unite.Specimens.Feed.Web.Models.Specimens
{
    public class SpecimenModel
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public SpecimenType? ParentType { get; set; }
        public string DonorId { get; set; }

        public TissueModel Tissue { get; set; }
        public CellLineModel CellLine { get; set; }

        public MolecularDataModel MolecularData { get; set; }


        public virtual void Sanitise()
        {
            Id = Id?.Trim();
            ParentId = ParentId?.Trim();
            DonorId = DonorId?.Trim();

            MolecularData?.Sanitise();
        }
    }
}
