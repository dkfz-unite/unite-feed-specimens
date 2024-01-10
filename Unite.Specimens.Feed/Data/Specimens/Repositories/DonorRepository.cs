using Unite.Data.Context;
using Unite.Data.Entities.Donors;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories;

internal class DonorRepository
{
    private readonly DomainDbContext _dbContext;


    public DonorRepository(DomainDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Donor Find(string referenceId)
    {
        var entity = _dbContext.Set<Donor>()
            .FirstOrDefault(entity =>
                entity.ReferenceId == referenceId
            );

        return entity;
    }

    public Donor Create(DonorModel model)
    {
        var entity = new Donor{ ReferenceId = model.ReferenceId };

        _dbContext.Add(entity);
        _dbContext.SaveChanges();

        return entity;
    }
}
