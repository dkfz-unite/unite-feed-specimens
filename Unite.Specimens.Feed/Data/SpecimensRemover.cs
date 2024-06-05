using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Context.Services;
using Unite.Data.Entities.Specimens;
using Unite.Specimens.Feed.Data.Repositories;

namespace Unite.Specimens.Feed.Data;

public class SpecimensRemover : DataWriter<Specimen>
{
    private SpecimenRepository _specimenRepository;

    public SpecimensRemover(IDbContextFactory<DomainDbContext> dbContextFactory) : base(dbContextFactory)
    {
        var dbContext = dbContextFactory.CreateDbContext();

        Initialize(dbContext);
    }
    

    public Specimen Find(int id)
    {
        return _specimenRepository.Find(id);
    }

    protected override void Initialize(DomainDbContext dbContext)
    {
        _specimenRepository = new SpecimenRepository(dbContext);
    }

    protected override void ProcessModel(Specimen specimen)
    {
        _specimenRepository.Delete(specimen);
    }
}
