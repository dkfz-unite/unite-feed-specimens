using System.Linq;
using Microsoft.EntityFrameworkCore;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Xenografts;
using Unite.Data.Services;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories
{
    internal class XenograftRepository : SpecimenRepositoryBase<XenograftModel>
    {
        private readonly XenograftInterventionRepository _interventionRepository;


        public XenograftRepository(DomainDbContext dbContext) : base(dbContext)
        {
            _interventionRepository = new XenograftInterventionRepository(dbContext);
        }


        public override Specimen Find(int donorId, int? parentId, in XenograftModel model)
        {
            var referenceId = model.ReferenceId;

            var specimen = _dbContext.Specimens
                .Include(specimen => specimen.Xenograft)
                .Include(specimen => specimen.MolecularData)
                .FirstOrDefault(specimen =>
                    specimen.DonorId == donorId &&
                    specimen.Xenograft != null &&
                    specimen.Xenograft.ReferenceId == referenceId
                );

            return specimen;
        }


        public override Specimen Create(int donorId, int? parentId, in XenograftModel model)
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

        public override void Update(ref Specimen specimen, in XenograftModel model)
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


        protected override void Map(in XenograftModel model, ref Specimen specimen)
        {
            base.Map(model, ref specimen);

            if (specimen.Xenograft == null)
            {
                specimen.Xenograft = new Xenograft();
            }

            specimen.Xenograft.ReferenceId = model.ReferenceId;
            specimen.Xenograft.MouseStrain = model.MouseStrain;
            specimen.Xenograft.GroupSize = model.GroupSize;
            specimen.Xenograft.ImplantTypeId = model.ImplantType;
            specimen.Xenograft.TissueLocationId = model.TissueLocation;
            specimen.Xenograft.ImplantedCellsNumber = model.ImplantedCellsNumber;
            specimen.Xenograft.Tumorigenicity = model.Tumorigenicity;
            specimen.Xenograft.TumorGrowthFormId = model.TumorGrowthForm;
            specimen.Xenograft.SurvivalDaysFrom = model.SurvivalDaysFrom;
            specimen.Xenograft.SurvivalDaysTo = model.SurvivalDaysTo;
        }
    }
}
