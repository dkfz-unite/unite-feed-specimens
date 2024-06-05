using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Donors;
using Unite.Specimens.Feed.Data.Models;

namespace Unite.Specimens.Feed.Data.Repositories;

internal class DonorRepository
{
    private readonly DomainDbContext _dbContext;


    public DonorRepository(DomainDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Donor Find(DonorModel model)
    {
        return _dbContext.Set<Donor>().AsNoTracking().FirstOrDefault(entity =>
            entity.ReferenceId == model.ReferenceId
        );
    }

    public Donor Create(DonorModel model)
    {
        var entity = new Donor{ ReferenceId = model.ReferenceId };

        _dbContext.Add(entity);
        _dbContext.SaveChanges();

        return entity;
    }

    public Donor FindOrCreate(DonorModel model)
    {
        return Find(model) ?? Create(model);
    }
}
