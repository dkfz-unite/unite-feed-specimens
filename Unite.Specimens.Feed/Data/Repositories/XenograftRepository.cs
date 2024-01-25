using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Enums;
using Unite.Data.Entities.Specimens.Xenografts;
using Unite.Specimens.Feed.Data.Models;

namespace Unite.Specimens.Feed.Data.Repositories;

internal class XenograftRepository : SpecimenRepositoryBase<XenograftModel>
{
    public XenograftRepository(DomainDbContext dbContext) : base(dbContext)
    {
    }


    public override Specimen Find(int donorId, int? parentId, in XenograftModel model)
    {
        var referenceId = model.ReferenceId;

        var entity = _dbContext.Set<Specimen>()
            .Include(entity => entity.Xenograft)
            .Include(entity => entity.MolecularData)
            .FirstOrDefault(entity =>
                entity.DonorId == donorId &&
                entity.Xenograft != null &&
                entity.Xenograft.ReferenceId == referenceId
            );

        return entity;
    }


    protected override void Map(in XenograftModel model, ref Specimen entity)
    {
        base.Map(model, ref entity);

        entity.TypeId = SpecimenType.Xenograft;

        if (entity.Xenograft == null)
        {
            entity.Xenograft = new Xenograft();
        }

        entity.Xenograft.ReferenceId = model.ReferenceId;
        entity.Xenograft.MouseStrain = model.MouseStrain;
        entity.Xenograft.GroupSize = model.GroupSize;
        entity.Xenograft.ImplantTypeId = model.ImplantType;
        entity.Xenograft.ImplantLocationId = model.ImplantLocation;
        entity.Xenograft.ImplantedCellsNumber = model.ImplantedCellsNumber;
        entity.Xenograft.Tumorigenicity = model.Tumorigenicity;
        entity.Xenograft.TumorGrowthFormId = model.TumorGrowthForm;
        entity.Xenograft.SurvivalDaysFrom = model.SurvivalDaysFrom;
        entity.Xenograft.SurvivalDaysTo = model.SurvivalDaysTo;
    }
}
