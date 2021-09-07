using System.Linq;
using Microsoft.EntityFrameworkCore;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Organoids;
using Unite.Data.Services;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories
{
    internal class OrganoidRepository : SpecimenRepositoryBase<OrganoidModel>
    {
        private readonly OrganoidInterventionRepository _interventionRepository;


        public OrganoidRepository(DomainDbContext dbContext) : base(dbContext)
        {
            _interventionRepository = new OrganoidInterventionRepository(dbContext);
        }


        public override Specimen Find(int donorId, int? parentId, in OrganoidModel model)
        {
            var referenceId = model.ReferenceId;

            var specimen = _dbContext.Specimens
                .Include(specimen => specimen.Organoid)
                .Include(specimen => specimen.MolecularData)
                .FirstOrDefault(specimen =>
                    specimen.DonorId == donorId &&
                    specimen.Organoid != null &&
                    specimen.Organoid.ReferenceId == referenceId
                );

            return specimen;
        }


        public override Specimen Create(int donorId, int? parentId, in OrganoidModel model)
        {
            var specimen = base.Create(donorId, parentId, model);

            if (model.Interventions != null && model.Interventions.Any())
            {
                foreach (var interventionModel in model.Interventions)
                {
                    _interventionRepository.Create(specimen.Id, interventionModel);
                }
            }

            return specimen;
        }

        public override void Update(ref Specimen specimen, in OrganoidModel model)
        {
            base.Update(ref specimen, model);

            if (model.Interventions != null && model.Interventions.Any())
            {
                foreach (var interventionModel in model.Interventions)
                {
                    var intervention = _interventionRepository.Find(specimen.Id, interventionModel);

                    if (intervention == null)
                    {
                        _interventionRepository.Create(specimen.Id, interventionModel);
                    }
                    else
                    {
                        _interventionRepository.Update(intervention, interventionModel);
                    }
                }
            }
        }


        protected override void Map(in OrganoidModel model, ref Specimen specimen)
        {
            base.Map(model, ref specimen);

            if (specimen.Organoid == null)
            {
                specimen.Organoid = new Organoid();
            }

            specimen.Organoid.ReferenceId = model.ReferenceId;
            specimen.Organoid.ImplantedCellsNumber = model.ImplantedCellsNumber;
            specimen.Organoid.Tumorigenicity = model.Tumorigenicity;
            specimen.Organoid.Medium = model.Medium;
        }
    }
}
