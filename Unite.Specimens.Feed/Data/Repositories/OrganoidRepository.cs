using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Organoids;
using Unite.Specimens.Feed.Data.Models;

namespace Unite.Specimens.Feed.Data.Repositories;

internal class OrganoidRepository : SpecimenRepositoryBase<OrganoidModel>
{
    public OrganoidRepository(DomainDbContext dbContext) : base(dbContext)
    {
    }


    protected override IQueryable<Specimen> GetQuery()
    {
        return base.GetQuery()
            .Include(entity => entity.Organoid);
    }

    protected override void Map(OrganoidModel model, Specimen entity)
    {
        base.Map(model, entity);

        if (entity.Organoid == null)
            entity.Organoid = new Organoid();

        entity.Organoid.ImplantedCellsNumber = model.ImplantedCellsNumber;
        entity.Organoid.Tumorigenicity = model.Tumorigenicity;
        entity.Organoid.Medium = model.Medium;
    }
}
