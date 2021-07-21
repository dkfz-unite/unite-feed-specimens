using System.Linq;
using Microsoft.EntityFrameworkCore;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Tissues;
using Unite.Data.Services;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories
{
    internal class TissueRepository : SpecimenRepositoryBase<TissueModel>
    {
        public TissueRepository(UniteDbContext dbContext) : base(dbContext)
        {
        }


        public override Specimen Find(int donorId, int? parentId, in TissueModel model)
        {
            var referenceId = model.ReferenceId;

            var specimen = _dbContext.Specimens
                .Include(specimen => specimen.Tissue)
                    .ThenInclude(tissue => tissue.Source)
                .Include(specimen => specimen.MolecularData)
                .FirstOrDefault(specimen =>
                    specimen.DonorId == donorId &&
                    specimen.Tissue != null &&
                    specimen.Tissue.ReferenceId == referenceId
                );

            return specimen;
        }


        protected override void Map(in TissueModel model, ref Specimen specimen)
        {
            base.Map(model, ref specimen);

            if (specimen.Tissue == null)
            {
                specimen.Tissue = new Tissue();
            }

            specimen.Tissue.ReferenceId = model.ReferenceId;
            specimen.Tissue.TypeId = model.Type;
            specimen.Tissue.TumorTypeId = model.TumorType;
            specimen.Tissue.ExtractionDay = model.ExtractionDay;
            specimen.Tissue.Source = GetTissueSource(model.Source);
        }

        private TissueSource GetTissueSource(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var tissueSource = _dbContext.TissueSources.FirstOrDefault(tissueSource =>
                tissueSource.Value == value
            );

            if (tissueSource == null)
            {
                tissueSource = new TissueSource() { Value = value };

                _dbContext.TissueSources.Add(tissueSource);
                _dbContext.SaveChanges();
            }

            return tissueSource;
        }
    }
}
