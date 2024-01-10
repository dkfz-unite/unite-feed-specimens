using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens.Organoids;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories;

internal class OrganoidInterventionRepository
{
    private readonly DomainDbContext _dbContext;


    public OrganoidInterventionRepository(DomainDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Intervention Find(int specimenId, OrganoidInterventionModel model)
    {
        var entity = _dbContext.Set<Intervention>()
            .Include(entity => entity.Type)
            .FirstOrDefault(entity =>
                entity.SpecimenId == specimenId &&
                entity.Type.Name == model.Type &&
                entity.StartDate == model.StartDate &&
                entity.StartDay == model.StartDay
            );

        return entity;
    }

    public IEnumerable<Intervention> CreateOrUpdate(int specimenId, IEnumerable<OrganoidInterventionModel> models)
    {
        RemoveRedundant(specimenId, models);

        var created = CreateMissing(specimenId, models);

        var updated = UpdateExisting(specimenId, models);

        return Enumerable.Concat(created, updated);
    }

    public IEnumerable<Intervention> CreateMissing(int specimenId, IEnumerable<OrganoidInterventionModel> models)
    {
        var entitiesToAdd = new List<Intervention>();

        foreach (var model in models)
        {
            var entity = Find(specimenId, model);

            if (entity == null)
            {
                var typeId = GetInterventionType(model.Type).Id;

                entity = new Intervention()
                {
                    SpecimenId = specimenId,
                    TypeId = typeId
                };

                Map(model, ref entity);

                entitiesToAdd.Add(entity);
            }
        }

        if (entitiesToAdd.Any())
        {
            _dbContext.AddRange(entitiesToAdd);
            _dbContext.SaveChanges();
        }

        return entitiesToAdd;
    }

    public IEnumerable<Intervention> UpdateExisting(int specimenId, IEnumerable<OrganoidInterventionModel> models)
    {
        var entitiesToUpdate = new List<Intervention>();

        foreach (var model in models)
        {
            var entity = Find(specimenId, model);

            if (entity != null)
            {
                Map(model, ref entity);

                entitiesToUpdate.Add(entity);
            }
        }

        if (entitiesToUpdate.Any())
        {
            _dbContext.UpdateRange(entitiesToUpdate);
            _dbContext.SaveChanges();
        }

        return entitiesToUpdate;
    }

    public void RemoveRedundant(int specimenId, IEnumerable<OrganoidInterventionModel> models)
    {
        var typeNames = models.Select(model => model.Type);

        var entitiesToRemove = _dbContext.Set<Intervention>()
            .Include(entity => entity.Type)
            .Where(entity => entity.SpecimenId == specimenId && !typeNames.Contains(entity.Type.Name))
            .ToArray();

        if (entitiesToRemove.Any())
        {
            _dbContext.RemoveRange(entitiesToRemove);
            _dbContext.SaveChanges();
        }
    }


    private InterventionType GetInterventionType(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        var entity = _dbContext.Set<InterventionType>()
            .FirstOrDefault(entity =>
                entity.Name == name
            );

        if (entity == null)
        {
            entity = new InterventionType { Name = name };

            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        return entity;
    }


    private static void Map(in OrganoidInterventionModel model, ref Intervention entity)
    {
        entity.Details = model.Details;
        entity.StartDate = model.StartDate;
        entity.StartDay = model.StartDay;
        entity.EndDate = model.EndDate;
        entity.DurationDays = model.DurationDays;
        entity.Results = model.Results;
    }
}
