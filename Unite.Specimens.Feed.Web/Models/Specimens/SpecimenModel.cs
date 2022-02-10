using System;
using Unite.Specimens.Feed.Web.Services.Specimens.Enums;

namespace Unite.Specimens.Feed.Web.Services.Specimens
{
    public class SpecimenModel
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public SpecimenType? ParentType { get; set; }
        public string DonorId { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? CreationDay { get; set; }

        public TissueModel Tissue { get; set; }
        public CellLineModel CellLine { get; set; }
        public OrganoidModel Organoid { get; set; }
        public XenograftModel Xenograft { get; set; }

        public MolecularDataModel MolecularData { get; set; }


        public virtual void Sanitise()
        {
            Id = Id?.Trim();
            ParentId = ParentId?.Trim();
            DonorId = DonorId?.Trim();

            Tissue?.Sanitise();
            CellLine?.Sanitise();
            Organoid?.Sanitise();
            Xenograft?.Sanitise();

            MolecularData?.Sanitise();
        }
    }
}
