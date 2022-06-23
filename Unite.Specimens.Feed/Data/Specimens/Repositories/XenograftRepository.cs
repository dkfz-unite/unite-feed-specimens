using Microsoft.EntityFrameworkCore;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Xenografts;
using Unite.Data.Services;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories;

internal class XenograftRepository : SpecimenRepositoryBase<XenograftModel>
{
    private readonly XenograftInterventionRepository _interventionRepository;


    public XenograftRepository(DomainDbContext dbContext) : base(dbContext)
    {
        _interventionRepository = new XenograftInterventionRepository(dbContext);
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


    public override Specimen Create(int donorId, int? parentId, in XenograftModel model)
    {
        var entity = base.Create(donorId, parentId, model);

        if (model.Interventions != null && model.Interventions.Any())
        {
            foreach (var interventionModel in model.Interventions)
            {
                _interventionRepository.Create(entity.Id, interventionModel);
            }
        }

        return entity;
    }

    public override void Update(ref Specimen entity, in XenograftModel model)
    {
        base.Update(ref entity, model);

        if (model.Interventions != null && model.Interventions.Any())
        {
            foreach (var interventionModel in model.Interventions)
            {
                var intervention = _interventionRepository.Find(entity.Id, interventionModel);

                if (intervention == null)
                {
                    _interventionRepository.Create(entity.Id, interventionModel);
                }
                else
                {
                    _interventionRepository.Update(intervention, interventionModel);
                }
            }
        }
    }


    protected override void Map(in XenograftModel model, ref Specimen entity)
    {
        base.Map(model, ref entity);

        if (entity.Xenograft == null)
        {
            entity.Xenograft = new Xenograft();
        }

        entity.Xenograft.ReferenceId = model.ReferenceId;
        entity.Xenograft.MouseStrain = model.MouseStrain;
        entity.Xenograft.GroupSize = model.GroupSize;
        entity.Xenograft.ImplantTypeId = model.ImplantType;
        entity.Xenograft.TissueLocationId = model.TissueLocation;
        entity.Xenograft.ImplantedCellsNumber = model.ImplantedCellsNumber;
        entity.Xenograft.Tumorigenicity = model.Tumorigenicity;
        entity.Xenograft.TumorGrowthFormId = model.TumorGrowthForm;
        entity.Xenograft.SurvivalDaysFrom = model.SurvivalDaysFrom;
        entity.Xenograft.SurvivalDaysTo = model.SurvivalDaysTo;
    }
}
