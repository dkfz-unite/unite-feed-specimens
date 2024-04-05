using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Context.Services;
using Unite.Specimens.Feed.Data.Exceptions;
using Unite.Specimens.Feed.Data.Models;
using Unite.Specimens.Feed.Data.Repositories;

namespace Unite.Specimens.Feed.Data;

public class DrugScreeningsDataWriter : DataWriter<SpecimenModel, DrugScreeningsDataUploadAudit>
{
    private DonorRepository _donorRepository;
    private SpecimenRepository _specimenRepository;
    private DrugScreeningRepository _drugScreeningRepository;


    public DrugScreeningsDataWriter(IDbContextFactory<DomainDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }


    protected override void Initialize(DomainDbContext dbContext)
    {
        _donorRepository = new DonorRepository(dbContext);
        _specimenRepository = new SpecimenRepository(dbContext);
        _drugScreeningRepository = new DrugScreeningRepository(dbContext);
    }

    protected override void ProcessModel(SpecimenModel model, ref DrugScreeningsDataUploadAudit audit)
    {
        var donor = _donorRepository.Find(model.Donor.ReferenceId) 
            ?? throw new NotFoundException($"Donor with id '{model.Donor.ReferenceId}' was not found");

        var specimen = _specimenRepository.Find(donor.Id, null, model)
            ?? throw new NotFoundException($"Specimen with id '{model.ReferenceId}' was not found");
            
        var drugScreenings = _drugScreeningRepository.CreateOrUpdate(specimen.Id, model.DrugScreenings);

        audit.DrugScreeningsCreated += drugScreenings.Count();

        if (audit.DrugScreeningsCreated > 0)
        {
            audit.Specimens.Add(specimen.Id);
        }
    }
}
