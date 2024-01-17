using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Context.Services;
using Unite.Data.Entities.Donors;
using Unite.Data.Entities.Specimens;
using Unite.Essentials.Extensions;
using Unite.Specimens.Feed.Data.Specimens.Exceptions;
using Unite.Specimens.Feed.Data.Specimens.Models;
using Unite.Specimens.Feed.Data.Specimens.Models.Audit;
using Unite.Specimens.Feed.Data.Specimens.Repositories;

namespace Unite.Specimens.Feed.Data.Specimens;

public class SpecimensDataWriter : DataWriter<SpecimenModel, SpecimensUploadAudit>
{
    private readonly DonorRepository _donorRepository;
    private readonly SpecimenRepository _specimenRepository;
    private readonly InterventionRepository _interventionRepository;
    private readonly DrugScreeningRepository _drugScreeningRepository;


    public SpecimensDataWriter(IDbContextFactory<DomainDbContext> dbContextFactory) : base(dbContextFactory)
    {
        var dbContext = dbContextFactory.CreateDbContext();
        
        _donorRepository = new DonorRepository(dbContext);
        _specimenRepository = new SpecimenRepository(dbContext);
        _interventionRepository = new InterventionRepository(dbContext);
        _drugScreeningRepository = new DrugScreeningRepository(dbContext);
    }


    protected override void ProcessModel(SpecimenModel model, ref SpecimensUploadAudit audit)
    {
        var donor = FindOrCreateDonor(model.Donor, ref audit);

        var parentSpecimen = FindSpecimen(donor.Id, null, model.Parent, true);

        var specimen = FindSpecimen(donor.Id, parentSpecimen?.Id, model);

        if (specimen == null)
        {
            specimen = CreateSpecimen(donor.Id, parentSpecimen?.Id, model, ref audit);

            if (model.Interventions.IsNotEmpty())
            {
                var interventions = _interventionRepository.CreateMissing(specimen.Id, model.Interventions);

                audit.InterventionsCreated += interventions.Count();
            }

            if (model.DrugScreenings.IsNotEmpty())
            {
                var drugScreenings = _drugScreeningRepository.CreateMissing(specimen.Id, model.DrugScreenings);

                audit.DrugScreeningsCreated += drugScreenings.Count();
            }

            audit.Specimens.Add(specimen.Id);
        }
        else
        {
            UpdateSpecimen(specimen, model, ref audit);

            if (model.Interventions.IsNotEmpty())
            {
                var interventions = _interventionRepository.CreateOrUpdate(specimen.Id, model.Interventions);

                audit.InterventionsUpdated += interventions.Count();
            }

            if (model.DrugScreenings.IsNotEmpty())
            {
                var drugScreenings = _drugScreeningRepository.CreateOrUpdate(specimen.Id, model.DrugScreenings);

                audit.DrugScreeningsUpdated += drugScreenings.Count();
            }

            audit.Specimens.Add(specimen.Id);
        }
    }


    private Donor FindOrCreateDonor(DonorModel model, ref SpecimensUploadAudit audit)
    {
        var entity = _donorRepository.Find(model.ReferenceId);

        if (entity == null)
        {
            entity = _donorRepository.Create(model);

            audit.DonorsCreated++;
        }

        return entity;
    }

    private Specimen FindSpecimen(int donorId, int? parentId, SpecimenModel model, bool throwNotFound = false)
    {
        if (model == null)
        {
            return null;
        }

        var entity = _specimenRepository.Find(donorId, parentId, model);

        if (entity == null && throwNotFound)
        {
            throw new NotFoundException($"Parent specimen with id '{model.ReferenceId}' was not found");
        }
        else
        {
            return entity;
        }
    }

    private Specimen CreateSpecimen(int donorId, int? parentId, SpecimenModel model, ref SpecimensUploadAudit audit)
    {
        var entity = _specimenRepository.Create(donorId, parentId, model);

        if (entity.Material != null)
        {
            audit.MaterialsCreated++;
        }
        else if (entity.Line != null)
        {
            audit.LinesCreated++;
        }
        else if (entity.Organoid != null)
        {
            audit.OrganoidsCreated++;
        }
        else if (entity.Xenograft != null)
        {
            audit.XenograftsCreated++;
        }

        return entity;
    }

    private Specimen UpdateSpecimen(Specimen entity, SpecimenModel model, ref SpecimensUploadAudit audit)
    {
        _specimenRepository.Update(ref entity, model);

        if (entity.Material != null)
        {
            audit.MaterialsUpdated++;
        }
        else if (entity.Line != null)
        {
            audit.LinesUpdated++;
        }
        else if (entity.Organoid != null)
        {
            audit.OrganoidsUpdate++;
        }
        else if (entity.Xenograft != null)
        {
            audit.XenograftsUpdated++;
        }

        return entity;
    }
}
