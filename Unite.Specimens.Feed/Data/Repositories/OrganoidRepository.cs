using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Enums;
using Unite.Data.Entities.Specimens.Organoids;
using Unite.Specimens.Feed.Data.Models;

namespace Unite.Specimens.Feed.Data.Repositories;

internal class OrganoidRepository : SpecimenRepositoryBase<OrganoidModel>
{
    public OrganoidRepository(DomainDbContext dbContext) : base(dbContext)
    {
    }


    public override Specimen Find(int donorId, int? parentId, in OrganoidModel model)
    {
        var referenceId = model.ReferenceId;

        var entity = _dbContext.Specimens
            .Include(entity => entity.Organoid)
            .Include(entity => entity.MolecularData)
            .FirstOrDefault(entity =>
                entity.DonorId == donorId &&
                entity.Organoid != null &&
                entity.Organoid.ReferenceId == referenceId
            );

        return entity;
    }


    protected override void Map(in OrganoidModel model, ref Specimen entity)
    {
        base.Map(model, ref entity);

        entity.TypeId = SpecimenType.Organoid;

        if (entity.Organoid == null)
        {
            entity.Organoid = new Organoid();
        }

        entity.Organoid.ReferenceId = model.ReferenceId;
        entity.Organoid.ImplantedCellsNumber = model.ImplantedCellsNumber;
        entity.Organoid.Tumorigenicity = model.Tumorigenicity;
        entity.Organoid.Medium = model.Medium;
    }
}
