using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Xenografts;
using Unite.Specimens.Feed.Data.Models;

namespace Unite.Specimens.Feed.Data.Repositories;

internal class XenograftRepository : SpecimenRepositoryBase<XenograftModel>
{
    public XenograftRepository(DomainDbContext dbContext) : base(dbContext)
    {
    }


    protected override IQueryable<Specimen> GetQuery()
    {
        return base.GetQuery()
            .Include(entity => entity.Xenograft);
    }

    protected override void Map(XenograftModel model, Specimen entity)
    {
        base.Map(model, entity);

        if (entity.Xenograft == null)
            entity.Xenograft = new Xenograft();
        
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
