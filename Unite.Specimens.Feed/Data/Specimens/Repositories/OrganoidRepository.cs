using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Enums;
using Unite.Data.Entities.Specimens.Organoids;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories;

internal class OrganoidRepository : SpecimenRepositoryBase<OrganoidModel>
{
    private readonly OrganoidInterventionRepository _interventionRepository;


    public OrganoidRepository(DomainDbContext dbContext) : base(dbContext)
    {
        _interventionRepository = new OrganoidInterventionRepository(dbContext);
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


    public override Specimen Create(int donorId, int? parentId, in OrganoidModel model)
    {
        var entity = base.Create(donorId, parentId, model);

        if (model.Interventions != null && model.Interventions.Any())
        {
            _interventionRepository.CreateMissing(entity.Id, model.Interventions);
        }

        return entity;
    }

    public override void Update(ref Specimen entity, in OrganoidModel model)
    {
        base.Update(ref entity, model);

        if (model.Interventions != null && model.Interventions.Any())
        {
            _interventionRepository.CreateOrUpdate(entity.Id, model.Interventions);
        }
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
