using System;

namespace Unite.Specimens.Feed.Data.Specimens.Models
{
    public class SpecimenModel
    {
        public string ReferenceId { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? CreationDay { get; set; }

        public virtual SpecimenModel Parent { get; set; }

        public virtual DonorModel Donor { get; set; }

        public virtual MolecularDataModel MolecularData { get; set; }
    }
}
