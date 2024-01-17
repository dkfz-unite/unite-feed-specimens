using Unite.Data.Context;
using Unite.Data.Entities.Specimens;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories;

internal class SpecimenRepository
{
    private readonly SpecimenRepositoryBase<MaterialModel> _materialRepository;
    private readonly SpecimenRepositoryBase<LineModel> _lineRepository;
    private readonly SpecimenRepositoryBase<OrganoidModel> _organoidRepository;
    private readonly SpecimenRepositoryBase<XenograftModel> _xenograftRepository;


    public SpecimenRepository(DomainDbContext dbContext)
    {
        _materialRepository = new MaterialRepository(dbContext);
        _lineRepository = new LineRepository(dbContext);
        _organoidRepository = new OrganoidRepository(dbContext);
        _xenograftRepository = new XenograftRepository(dbContext);
    }


    public Specimen Find(int donorId, int? parentId, SpecimenModel model)
    {
        if (model is MaterialModel materialModel)
        {
            return _materialRepository.Find(donorId, parentId, materialModel);
        }
        else if (model is LineModel lineModel)
        {
            return _lineRepository.Find(donorId, parentId, lineModel);
        }
        else if (model is OrganoidModel organoidModel)
        {
            return _organoidRepository.Find(donorId, parentId, organoidModel);
        }
        else if (model is XenograftModel xenograftModel)
        {
            return _xenograftRepository.Find(donorId, parentId, xenograftModel);
        }
        else
        {
            throw new NotImplementedException("Specimen type is not yet supported");
        }
    }

    public Specimen Create(int donorId, int? parentId, SpecimenModel model)
    {
        if (model is MaterialModel materialModel)
        {
            return _materialRepository.Create(donorId, parentId, materialModel);
        }
        else if (model is LineModel lineModel)
        {
            return _lineRepository.Create(donorId, parentId, lineModel);
        }
        else if (model is OrganoidModel organoidModel)
        {
            return _organoidRepository.Create(donorId, parentId, organoidModel);
        }
        else if (model is XenograftModel xenograftModel)
        {
            return _xenograftRepository.Create(donorId, parentId, xenograftModel);
        }
        else
        {
            throw new NotSupportedException("Specimen type is not yet supported");
        }
    }

    public void Update(ref Specimen entity, in SpecimenModel model)
    {
        if (model is MaterialModel materialModel)
        {
            _materialRepository.Update(ref entity, materialModel);
        }
        else if (model is LineModel lineModel)
        {
            _lineRepository.Update(ref entity, lineModel);
        }
        else if (model is OrganoidModel organoidModel)
        {
            _organoidRepository.Update(ref entity, organoidModel);
        }
        else if (model is XenograftModel xenograftModel)
        {
            _xenograftRepository.Update(ref entity, xenograftModel);
        }
        else
        {
            throw new NotSupportedException("Specimen type is not yet supported");
        }
    }
}
