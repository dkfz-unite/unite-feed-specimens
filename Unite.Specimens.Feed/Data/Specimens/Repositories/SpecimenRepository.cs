using System;
using Unite.Data.Entities.Specimens;
using Unite.Data.Services;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories
{
    internal class SpecimenRepository
    {
        private readonly SpecimenRepositoryBase<TissueModel> _tissueRepository;
        private readonly SpecimenRepositoryBase<CellLineModel> _cellLineRepository;
        private readonly SpecimenRepositoryBase<OrganoidModel> _organoidRepository;
        private readonly SpecimenRepositoryBase<XenograftModel> _xenograftRepository;


        public SpecimenRepository(DomainDbContext dbContext)
        {
            _tissueRepository = new TissueRepository(dbContext);
            _cellLineRepository = new CellLineRepository(dbContext);
            _organoidRepository = new OrganoidRepository(dbContext);
            _xenograftRepository = new XenograftRepository(dbContext);
        }


        public Specimen Find(int donorId, int? parentId, SpecimenModel specimenModel)
        {
            if (specimenModel is TissueModel tissueModel)
            {
                return _tissueRepository.Find(donorId, parentId, tissueModel);
            }
            else if (specimenModel is CellLineModel cellLineModel)
            {
                return _cellLineRepository.Find(donorId, parentId, cellLineModel);
            }
            else if (specimenModel is OrganoidModel organoidModel)
            {
                return _organoidRepository.Find(donorId, parentId, organoidModel);
            }
            else if (specimenModel is XenograftModel xenograftModel)
            {
                return _xenograftRepository.Find(donorId, parentId, xenograftModel);
            }
            else
            {
                throw new NotImplementedException("Specimen type is not yet supported");
            }
        }

        public Specimen Create(int donorId, int? parentId, SpecimenModel specimenModel)
        {
            if (specimenModel is TissueModel tissueModel)
            {
                return _tissueRepository.Create(donorId, parentId, tissueModel);
            }
            else if (specimenModel is CellLineModel cellLineModel)
            {
                return _cellLineRepository.Create(donorId, parentId, cellLineModel);
            }
            else if (specimenModel is OrganoidModel organoidModel)
            {
                return _organoidRepository.Create(donorId, parentId, organoidModel);
            }
            else if (specimenModel is XenograftModel xenograftModel)
            {
                return _xenograftRepository.Create(donorId, parentId, xenograftModel);
            }
            else
            {
                throw new NotSupportedException("Specimen type is not yet supported");
            }
        }

        public void Update(ref Specimen specimen, in SpecimenModel specimenModel)
        {
            if (specimenModel is TissueModel tissueModel)
            {
                _tissueRepository.Update(ref specimen, tissueModel);
            }
            else if (specimenModel is CellLineModel cellLineModel)
            {
                _cellLineRepository.Update(ref specimen, cellLineModel);
            }
            else if (specimenModel is OrganoidModel organoidModel)
            {
                _organoidRepository.Update(ref specimen, organoidModel);
            }
            else if (specimenModel is XenograftModel xenograftModel)
            {
                _xenograftRepository.Update(ref specimen, xenograftModel);
            }
            else
            {
                throw new NotSupportedException("Specimen type is not yet supported");
            }
        }
    }
}
