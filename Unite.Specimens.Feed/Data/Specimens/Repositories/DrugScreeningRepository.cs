using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories;

internal class DrugScreeningRepository
{
    private readonly DomainDbContext _dbContext;


    public DrugScreeningRepository(DomainDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public DrugScreening Find(int specimenId, DrugScreeningModel model)
    {
        return _dbContext.Set<DrugScreening>()
            .Include(entity => entity.Drug)
            .FirstOrDefault(entity =>
                entity.SpecimenId == specimenId &&
                entity.Drug.Name == model.Drug
            );
    }

    public IEnumerable<DrugScreening> CreateOrUpdate(int specimenId, IEnumerable<DrugScreeningModel> models)
    {
        RemoveRedundant(specimenId, models);

        var created = CreateMissing(specimenId, models);

        var updated = UpdateExisting(specimenId, models);

        return Enumerable.Concat(created, updated);
    }

    public IEnumerable<DrugScreening> CreateMissing(int specimenId, IEnumerable<DrugScreeningModel> models)
    {
        var entitiesToAdd = new List<DrugScreening>();

        foreach (var model in models)
        {
            var entity = Find(specimenId, model);

            if (entity == null)
            {
                var drugId = GetDrug(model.Drug).Id;

                entity = new DrugScreening()
                {
                    SpecimenId = specimenId,
                    DrugId = drugId
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

    public IEnumerable<DrugScreening> UpdateExisting(int specimenId, IEnumerable<DrugScreeningModel> models)
    {
        var entitiesToUpdate = new List<DrugScreening>();

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

    public void RemoveRedundant(int specimenId, IEnumerable<DrugScreeningModel> models)
    {
        var drugNames = models.Select(model => model.Drug);

        var entitiesToRemove = _dbContext.Set<DrugScreening>()
            .Include(entity => entity.Drug)
            .Where(entity => entity.SpecimenId == specimenId && !drugNames.Contains(entity.Drug.Name))
            .ToArray();

        if (entitiesToRemove.Any())
        {
            _dbContext.RemoveRange(entitiesToRemove);
            _dbContext.SaveChanges();
        }
    }


    private Drug GetDrug(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        var entity = _dbContext.Set<Drug>()
            .FirstOrDefault(entity =>
                entity.Name == name
            );

        if (entity == null)
        {
            entity = new Drug { Name = name };

            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        return entity;
    }


    private static void Map(in DrugScreeningModel model, ref DrugScreening entity)
    {
        entity.Dss = model.Dss;
        entity.DssSelective = model.DssSelective;
        entity.Gof = model.Gof;
        entity.AbsIC25 = model.AbsIC25;
        entity.AbsIC50 = model.AbsIC50;
        entity.AbsIC75 = model.AbsIC75;
        entity.MinConcentration = model.MinConcentration;
        entity.MaxConcentration = model.MaxConcentration;
        entity.Concentration = model.Concentration;
        entity.Inhibition = model.Inhibition;
        entity.ConcentrationLine = model.ConcentrationLine;
        entity.InhibitionLine = model.IngibitionLine;
    }
}
