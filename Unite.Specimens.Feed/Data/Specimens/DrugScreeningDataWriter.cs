﻿using Unite.Data.Services;
using Unite.Specimens.Feed.Data.Specimens.Exceptions;
using Unite.Specimens.Feed.Data.Specimens.Models;
using Unite.Specimens.Feed.Data.Specimens.Models.Audit;
using Unite.Specimens.Feed.Data.Specimens.Repositories;

namespace Unite.Specimens.Feed.Data.Specimens;

public class DrugScreeningDataWriter : DataWriter<SpecimenModel, DrugScreeningsUploadAudit>
{
    private readonly DonorRepository _donorRepository;
    private readonly SpecimenRepository _specimenRepository;
    private readonly DrugScreeningRepository _drugScreeningRepository;


    public DrugScreeningDataWriter(DomainDbContext dbContext) : base(dbContext)
    {
        _donorRepository = new DonorRepository(dbContext);
        _specimenRepository = new SpecimenRepository(dbContext);
        _drugScreeningRepository = new DrugScreeningRepository(dbContext);
    }


    protected override void ProcessModel(SpecimenModel model, ref DrugScreeningsUploadAudit audit)
    {
        var donor = _donorRepository.Find(model.Donor.ReferenceId);

        if (donor == null)
        {
            throw new NotFoundException($"Donor with id '{model.Donor.ReferenceId}' was not found");
        }

        var specimen = _specimenRepository.Find(donor.Id, null, model);

        if (specimen == null)
        {
            throw new NotFoundException($"Specimen with id '{model.ReferenceId}' was not found");
        }

        var drugScreenings = _drugScreeningRepository.CreateOrUpdate(specimen.Id, model.DrugsScreeningData);

        audit.DrugScreeningsCreated += drugScreenings.Count();

        if (audit.DrugScreeningsCreated > 0)
        {
            audit.Specimens.Add(specimen.Id);
        }
    }
}
