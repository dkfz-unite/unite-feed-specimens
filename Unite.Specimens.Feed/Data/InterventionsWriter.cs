using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Specimens.Feed.Data.Exceptions;
using Unite.Specimens.Feed.Data.Models;
using Unite.Specimens.Feed.Data.Repositories;

namespace Unite.Specimens.Feed.Data;

public class InterventionsWriter : SpecimenWriterBase<SpecimenModel, InterventionsWriteAudit>
{
    private SpecimenRepository _specimenRepository;


    public InterventionsWriter(IDbContextFactory<DomainDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }


    protected override void Initialize(DomainDbContext dbContext)
    {
        _specimenRepository = new SpecimenRepository(dbContext);
        _interventionTypeRepository = new InterventionTypeRepository(dbContext);
        _interventionRepository = new InterventionRepository(dbContext);
    }

    protected override void ProcessModel(SpecimenModel model, ref InterventionsWriteAudit audit)
    {
        var specimen = _specimenRepository.Find(model)
            ?? throw new NotFoundException($"Specimen with referenceId '{model.ReferenceId}' was not found");

        WriteInterventions(specimen.Id, model.Interventions, ref audit);
    }
}
