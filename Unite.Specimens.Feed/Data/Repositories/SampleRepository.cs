using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens.Analysis;
using Unite.Specimens.Feed.Data.Models;

namespace Unite.Specimens.Feed.Data.Repositories;

internal class SampleRepository
{
    private readonly DomainDbContext _dbContext;
    private readonly DonorRepository _donorRepository;
    private readonly SpecimenRepository _specimenRepository;


    public SampleRepository(DomainDbContext dbContext)
    {
        _dbContext = dbContext;
        _donorRepository = new DonorRepository(dbContext);
        _specimenRepository = new SpecimenRepository(dbContext);
    }


     public Sample FindOrCreate(SampleModel model)
    {
        return Find(model) ?? Create(model);
    }

    public Sample Find(SampleModel model)
    {
        return _dbContext.Set<Sample>().AsNoTracking().FirstOrDefault(entity =>
            entity.Specimen.ReferenceId == model.Specimen.Donor.ReferenceId &&
            entity.Specimen.ReferenceId == model.Specimen.ReferenceId &&
            entity.Analysis.TypeId == model.Analysis.Type
        );
    }

    public Sample Create(SampleModel model)
    {
        var entity = new Sample()
        {
            Specimen = _specimenRepository.FindOrCreate(model.Specimen),
            Analysis = Create(model.Analysis),
        };

        _dbContext.Add(entity);
        _dbContext.SaveChanges();

        return entity;
    }


    private static Analysis Create(AnalysisModel model)
    {
        return new Analysis
        {
            TypeId = model.Type,
            Date = model.Date,
            Day = model.Day,
            Parameters = model.Parameters
        };
    }
}
