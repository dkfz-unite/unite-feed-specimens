using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Specimens.Feed.Data.Models;
using Unite.Specimens.Feed.Data.Repositories;

namespace Unite.Specimens.Feed.Data;

public abstract class SpecimenWriterBase<TModel, TAudit> : Unite.Data.Context.Services.DataWriter<TModel, TAudit>
    where TModel : class
    where TAudit : SpecimenWriteAuditBase, new()
{
    protected InterventionTypeRepository _interventionTypeRepository;
    protected InterventionRepository _interventionRepository;


    public SpecimenWriterBase(IDbContextFactory<DomainDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }


    protected void WriteInterventions(int specimenId, IEnumerable<InterventionModel> models, ref TAudit audit)
    {
        var types = models.Select(model => model.Type).Distinct();
        var entities = _interventionTypeRepository.CreateMissing(types);
        audit.InterventionTypesCreated += entities.Count();

        var entries = _interventionRepository.RecreateAll(specimenId, models);
        audit.InterventionTypesCreated += entries.Count();

        if (audit.InterventionTypesCreated > 0)
            audit.Specimens.Add(specimenId);
    }
}
