using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens.Analysis.Drugs;
using Unite.Specimens.Feed.Data.Models.Drugs;
using Unite.Specimens.Feed.Data.Repositories.Drugs;

namespace Unite.Specimens.Feed.Data.Repositories;

internal class DrugScreeningRepository
{
    private readonly DomainDbContext _dbContext;
    private readonly DrugRepository _drugRepository;


    public DrugScreeningRepository(DomainDbContext dbContext)
    {
        _dbContext = dbContext;
        _drugRepository = new DrugRepository(dbContext);
    }


    public DrugScreening Find(int sampleId, DrugScreeningModel model)
    {
        var drug = _drugRepository.Find(model.Drug);

        if (drug == null)
            return null;

        return _dbContext.Set<DrugScreening>().AsNoTracking().FirstOrDefault(entity =>
            entity.SampleId == sampleId &&
            entity.EntityId == drug.Id
        );
    }

    public IEnumerable<DrugScreening> RecreateAll(int sampleId, IEnumerable<DrugScreeningModel> models)
    {
        RemoveAll(sampleId);

        var entities = new List<DrugScreening>();

        foreach (var model in models)
        {
            var entity = Find(sampleId, model);

            if (entity == null)
            {
                var drugId = _drugRepository.FindOrCreate(model.Drug).Id;

                entity = new DrugScreening()
                {
                    SampleId = sampleId,
                    EntityId = drugId
                };

                Map(model, ref entity);

                entities.Add(entity);
            }
        }

        if (entities.Any())
        {
            _dbContext.AddRange(entities);
            _dbContext.SaveChanges();
        }

        return entities;
    }


    private void RemoveAll(int sampleId)
    {
        var entitiesToRemove = _dbContext.Set<DrugScreening>()
            .Where(entity => entity.SampleId == sampleId)
            .ToArray();

        if (entitiesToRemove.Any())
        {
            _dbContext.RemoveRange(entitiesToRemove);
            _dbContext.SaveChanges();
        }
    }

    private static void Map(in DrugScreeningModel model, ref DrugScreening entity)
    {
        entity.Gof = model.Gof;
        entity.Dss = model.Dss;
        entity.DssS = model.DssS;
        entity.MinDose = model.MinDose;
        entity.MaxDose = model.MaxDose;
        entity.Dose25 = model.Dose25;
        entity.Dose50 = model.Dose50;
        entity.Dose75 = model.Dose75;
        entity.Doses = model.Doses;
        entity.Responses = model.Responses;
    }
}
