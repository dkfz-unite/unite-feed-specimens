using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens;

namespace Unite.Specimens.Feed.Data.Repositories;

public class InterventionTypeRepository
{
    private readonly DomainDbContext _dbContext;


    public InterventionTypeRepository(DomainDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public InterventionType Find(string name)
    {
        return _dbContext.Set<InterventionType>().AsNoTracking().FirstOrDefault(entity =>
            entity.Name == name
        );
    }

    public InterventionType FindOrCreate(string name)
    {
        var entity = Find(name);

        if (entity == null)
        {
            entity = new InterventionType()
            {
                Name = name
            };

            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        return entity;
    }

    public IEnumerable<InterventionType> CreateMissing(IEnumerable<string> names)
    {
        var entitiesToAdd = new List<InterventionType>();

        foreach (var name in names)
        {
            var entity = Find(name);

            if (entity == null)
            {
                entity = new InterventionType() { Name = name };

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
}
