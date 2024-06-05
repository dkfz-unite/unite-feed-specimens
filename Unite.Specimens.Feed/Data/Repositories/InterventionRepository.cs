using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens;
using Unite.Specimens.Feed.Data.Models;

namespace Unite.Specimens.Feed.Data.Repositories;

public class InterventionRepository
{
    private readonly DomainDbContext _dbContext;
    private readonly InterventionTypeRepository _interventionTypeRepository;


    public InterventionRepository(DomainDbContext dbContext)
    {
        _dbContext = dbContext;
        _interventionTypeRepository = new InterventionTypeRepository(dbContext);
    }


    public Intervention Find(int specimenId, InterventionModel model)
    {
        var type = _interventionTypeRepository.Find(model.Type);

        if (type == null)
            return null;

        return _dbContext.Set<Intervention>().AsNoTracking().FirstOrDefault(entity =>
            entity.SpecimenId == specimenId &&
            entity.TypeId == type.Id &&
            entity.StartDate == model.StartDate &&
            entity.StartDay == model.StartDay
        );
    }

    public IEnumerable<Intervention> CreateAll(int specimenId, IEnumerable<InterventionModel> models)
    {
        var entities = new List<Intervention>();

        foreach (var model in models)
        {
            var typeId = _interventionTypeRepository.FindOrCreate(model.Type).Id;

            var entity = new Intervention()
            {
                SpecimenId = specimenId,
                TypeId = typeId
            };

            Map(model, ref entity);

            entities.Add(entity);
        }

        if (entities.Any())
        {
            _dbContext.AddRange(entities);
            _dbContext.SaveChanges();
        }

        return entities;
    }

    public IEnumerable<Intervention> RecreateAll(int specimenId, IEnumerable<InterventionModel> models)
    {
        RemoveAll(specimenId);

        return CreateAll(specimenId, models);
    }


    private void RemoveAll(int specimenId)
    {
        var entities = _dbContext.Set<Intervention>()
            .Where(entity => entity.SpecimenId == specimenId)
            .ToList();

        if (entities.Any())
        {
            _dbContext.RemoveRange(entities);
            _dbContext.SaveChanges();
        }
    }
    
    private static void Map(in InterventionModel model, ref Intervention entity)
    {
        entity.Details = model.Details;
        entity.StartDate = model.StartDate;
        entity.StartDay = model.StartDay;
        entity.EndDate = model.EndDate;
        entity.DurationDays = model.DurationDays;
        entity.Results = model.Results;
    }
}
