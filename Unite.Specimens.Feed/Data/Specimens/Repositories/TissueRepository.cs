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
        public TissueRepository(DomainDbContext dbContext) : base(dbContext)
        {
        }


        public override Specimen Find(int donorId, int? parentId, in TissueModel model)
        {
            var referenceId = model.ReferenceId;

            var entity = _dbContext.Set<Specimen>()
                .Include(entity => entity.Tissue)
                    .ThenInclude(tissue => tissue.Source)
                .Include(entity => entity.MolecularData)
                .FirstOrDefault(entity =>
                    entity.DonorId == donorId &&
                    entity.Tissue != null &&
                    entity.Tissue.ReferenceId == referenceId
                );

            return entity;
        }


        protected override void Map(in TissueModel model, ref Specimen entity)
        {
            base.Map(model, ref entity);

            if (entity.Tissue == null)
            {
                entity.Tissue = new Tissue();
            }

            entity.Tissue.ReferenceId = model.ReferenceId;
            entity.Tissue.TypeId = model.Type;
            entity.Tissue.TumorTypeId = model.TumorType;
            entity.Tissue.Source = GetTissueSource(model.Source);
        }

        private TissueSource GetTissueSource(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var entity = _dbContext.Set<TissueSource>()
                    .FirstOrDefault(entity =>
                    entity.Value == value
                );

            if (entity == null)
            {
                entity = new TissueSource() { Value = value };

                _dbContext.Add(entity);
                _dbContext.SaveChanges();
            }

            return entity;
        }
    }
}
