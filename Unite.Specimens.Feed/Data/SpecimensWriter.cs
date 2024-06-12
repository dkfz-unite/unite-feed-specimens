using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Enums;
using Unite.Essentials.Extensions;
using Unite.Specimens.Feed.Data.Models;
using Unite.Specimens.Feed.Data.Repositories;

namespace Unite.Specimens.Feed.Data;

public class SpecimensWriter : SpecimenWriterBase<SpecimenModel, SpecimensWriteAudit>
{
    private SpecimenRepository _specimenRepository;


    public SpecimensWriter(IDbContextFactory<DomainDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }


    protected override void Initialize(DomainDbContext dbContext)
    {
        _specimenRepository = new SpecimenRepository(dbContext);
        _interventionTypeRepository = new InterventionTypeRepository(dbContext);
        _interventionRepository = new InterventionRepository(dbContext);
    }

    protected override void ProcessModel(SpecimenModel model, ref SpecimensWriteAudit audit)
    {
        var specimen = FindSpecimen(model);

        if (specimen == null)
            specimen = CreateSpecimen(model, ref audit);
        else
            UpdateSpecimen(specimen, model, ref audit);

        audit.Specimens.Add(specimen.Id);

        if (model.Interventions.IsNotEmpty())
            WriteInterventions(specimen.Id, model.Interventions, ref audit);
    }


    private Specimen FindSpecimen(SpecimenModel model)
    {
        return _specimenRepository.Find(model);
    }

    private Specimen CreateSpecimen(SpecimenModel model, ref SpecimensWriteAudit audit)
    {
        var entity = _specimenRepository.Create(model);

        if (entity.TypeId == SpecimenType.Material)
            audit.MaterialsCreated++;
        else if (entity.TypeId == SpecimenType.Line)
            audit.LinesCreated++;
        else if (entity.TypeId == SpecimenType.Organoid)
            audit.OrganoidsCreated++;
        else if (entity.TypeId == SpecimenType.Xenograft)
            audit.XenograftsCreated++;

        return entity;
    }

    private Specimen UpdateSpecimen(Specimen entity, SpecimenModel model, ref SpecimensWriteAudit audit)
    {
        _specimenRepository.Update(entity, model);

        if (entity.TypeId == SpecimenType.Material)
            audit.MaterialsUpdated++;
        else if (entity.TypeId == SpecimenType.Line)
            audit.LinesUpdated++;
        else if (entity.TypeId == SpecimenType.Organoid)
            audit.OrganoidsUpdate++;
        else if (entity.TypeId == SpecimenType.Xenograft)
            audit.XenograftsUpdated++;

        return entity;
    }
}
