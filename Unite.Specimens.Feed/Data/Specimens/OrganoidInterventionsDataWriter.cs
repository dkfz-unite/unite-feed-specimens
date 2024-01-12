using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Context.Services;
using Unite.Specimens.Feed.Data.Specimens.Exceptions;
using Unite.Specimens.Feed.Data.Specimens.Models;
using Unite.Specimens.Feed.Data.Specimens.Models.Audit;
using Unite.Specimens.Feed.Data.Specimens.Repositories;

namespace Unite.Specimens.Feed.Data.Specimens;

public class OrganoidInterventionsDataWriter : DataWriter<SpecimenModel, InterventionsUploadAudit>
{
    private readonly DonorRepository _donorRepository;
    private readonly SpecimenRepository _specimenRepository;
    private readonly OrganoidInterventionRepository _interventionRepository;


    public OrganoidInterventionsDataWriter(IDbContextFactory<DomainDbContext> dbContextFactory) : base(dbContextFactory)
    {
        var dbContext = dbContextFactory.CreateDbContext();

        _donorRepository = new DonorRepository(dbContext);
        _specimenRepository = new SpecimenRepository(dbContext);
        _interventionRepository = new OrganoidInterventionRepository(dbContext);
    }


    protected override void ProcessModel(SpecimenModel model, ref InterventionsUploadAudit audit)
    {
        var donor = _donorRepository.Find(model.Donor.ReferenceId) 
            ?? throw new NotFoundException($"Donor with id '{model.Donor.ReferenceId}' was not found");

        var specimen = _specimenRepository.Find(donor.Id, null, model)
            ?? throw new NotFoundException($"Specimen with id '{model.ReferenceId}' was not found");

        var interventions = _interventionRepository.CreateOrUpdate(specimen.Id, ((OrganoidModel)model).Interventions);

        audit.InterventionsCreated += interventions.Count();

        if (audit.InterventionsCreated > 0)
        {
            audit.Specimens.Add(specimen.Id);
        }
    }
}
