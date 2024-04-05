using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Context.Services;
using Unite.Specimens.Feed.Data.Repositories;

namespace Unite.Specimens.Feed.Data;

public class SpecimensDataRemover : DataWriter<int>
{
    private SpecimenRepository _specimenRepository;

    public SpecimensDataRemover(IDbContextFactory<DomainDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }
    

    protected override void Initialize(DomainDbContext dbContext)
    {
        _specimenRepository = new SpecimenRepository(dbContext);
    }

    protected override void ProcessModel(int model)
    {
        _specimenRepository.Delete(model);
    }
}
