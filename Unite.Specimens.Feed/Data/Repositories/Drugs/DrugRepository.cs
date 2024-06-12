using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens.Analysis.Drugs;

namespace Unite.Specimens.Feed.Data.Repositories.Drugs;

public class DrugRepository
{
    private readonly DomainDbContext _dbContext;


    public DrugRepository(DomainDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Drug Find(string name)
    {
        return _dbContext.Set<Drug>().AsNoTracking().FirstOrDefault(entity => 
            entity.Name == name
        );
    }

    public Drug Create(string name)
    {
        var entity = new Drug() { Name = name };

        _dbContext.Add(entity);
        _dbContext.SaveChanges();

        return entity;
    }

    public Drug FindOrCreate(string name)
    {
        return Find(name) ?? Create(name);
    }

    public IEnumerable<Drug> CreateMissing(IEnumerable<string> names)
    {
        var entitiesToAdd = new List<Drug>();

        foreach (var name in names)
        {
            var entity = Find(name);

            if (entity == null)
            {
                entity = new Drug() { Name = name }; 

                entitiesToAdd.Add(entity);
            }
        }

        if (entitiesToAdd.Any())
        {
            _dbContext.AddRange(entitiesToAdd);
            _dbContext.SaveChanges();
        }

        return entitiesToAdd.ToArray();
    }
    
}
