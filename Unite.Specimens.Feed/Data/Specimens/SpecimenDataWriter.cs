using Unite.Data.Entities.Donors;
using Unite.Data.Entities.Specimens;
using Unite.Data.Services;
using Unite.Specimens.Feed.Data.Exceptions;
using Unite.Specimens.Feed.Data.Specimens.Models;
using Unite.Specimens.Feed.Data.Specimens.Models.Audit;
using Unite.Specimens.Feed.Data.Specimens.Repositories;

namespace Unite.Specimens.Feed.Data.Specimens
{
    public class SpecimenDataWriter : DataWriter<SpecimenModel, SpecimensUploadAudit>
    {
        private readonly DonorRepository _donorRepository;
        private readonly SpecimenRepository _specimenRepository;


        public SpecimenDataWriter(UniteDbContext dbContext) : base(dbContext)
        {
            _donorRepository = new DonorRepository(dbContext);
            _specimenRepository = new SpecimenRepository(dbContext);
        }


        protected override void ProcessModel(SpecimenModel model, ref SpecimensUploadAudit audit)
        {
            var donor = FindOrCreateDonor(model.Donor, ref audit);

            var parentSpecimen = FindSpecimen(donor.Id, null, model.Parent, true);

            var specimen = FindSpecimen(donor.Id, parentSpecimen?.Id, model);

            if (specimen == null)
            {
                CreateSpecimen(donor.Id, parentSpecimen?.Id, model, ref audit);
            }
            else
            {
                UpdateSpecimen(specimen, model, ref audit);
            }
        }


        private Donor FindOrCreateDonor(DonorModel donorModel, ref SpecimensUploadAudit audit)
        {
            var donor = _donorRepository.Find(donorModel.ReferenceId);

            if (donor == null)
            {
                donor = _donorRepository.Create(donorModel);

                audit.DonorsCreated++;
            }

            return donor;
        }

        private Specimen FindSpecimen(int donorId, int? parentId, SpecimenModel specimenModel, bool throwNotFound = false)
        {
            if (specimenModel == null)
            {
                return null;
            }

            var specimen = _specimenRepository.Find(donorId, parentId, specimenModel);

            if (specimen == null && throwNotFound)
            {
                throw new NotFoundException($"Parent specimen with id '{specimenModel.ReferenceId}' was not found");
            }
            else
            {
                return specimen;
            }
        }

        private Specimen CreateSpecimen(int donorId, int? parentId, SpecimenModel specimenModel, ref SpecimensUploadAudit audit)
        {
            var specimen = _specimenRepository.Create(donorId, parentId, specimenModel);

            if (specimen.Tissue != null)
            {
                audit.TissuesCreated++;
            }
            else if (specimen.CellLine != null)
            {
                audit.CellLinesCreated++;
            }

            return specimen;
        }

        private Specimen UpdateSpecimen(Specimen specimen, SpecimenModel specimenModel, ref SpecimensUploadAudit audit)
        {
            _specimenRepository.Update(ref specimen, specimenModel);

            if (specimen.Tissue != null)
            {
                audit.TissuesUpdated++;
            }
            else if (specimen.CellLine != null)
            {
                audit.CellLinesUpdated++;
            }

            return specimen;
        }
    }
}
