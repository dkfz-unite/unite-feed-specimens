using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Genome.Analysis;
using Unite.Data.Entities.Specimens;
using Unite.Essentials.Extensions;
using Unite.Specimens.Feed.Data.Models;

namespace Unite.Specimens.Feed.Data.Repositories;

internal class SpecimenRepository
{
    private readonly DomainDbContext _dbContext;
    private readonly SpecimenRepositoryBase<MaterialModel> _materialRepository;
    private readonly SpecimenRepositoryBase<LineModel> _lineRepository;
    private readonly SpecimenRepositoryBase<OrganoidModel> _organoidRepository;
    private readonly SpecimenRepositoryBase<XenograftModel> _xenograftRepository;


    public SpecimenRepository(DomainDbContext dbContext)
    {
        _dbContext = dbContext;
        _materialRepository = new MaterialRepository(dbContext);
        _lineRepository = new LineRepository(dbContext);
        _organoidRepository = new OrganoidRepository(dbContext);
        _xenograftRepository = new XenograftRepository(dbContext);
    }


    public Specimen Find(int id)
    {
        return _dbContext.Set<Specimen>()
            .AsNoTracking()
            .FirstOrDefault(entity => entity.Id == id);
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

    public void Delete(Specimen specimen)
    {
        var children = LoadChildren(specimen);

        if (children.IsNotEmpty())
        {
            children.ForEach(Delete);
        }

        var analyses = _dbContext.Set<AnalysedSample>()
            .AsNoTracking()
            .Include(entity => entity.Analysis)
            .Where(entity => entity.TargetSampleId == specimen.Id)
            .Select(entity => entity.Analysis)
            .Distinct()
            .ToArray();

        _dbContext.Remove(specimen);
        _dbContext.RemoveRange(analyses);
        _dbContext.SaveChanges();
    }

    private Specimen[] LoadChildren(Specimen specimen)
    {
        return _dbContext.Set<Specimen>()
            .AsNoTracking()
            .Where(entity => entity.ParentId == specimen.Id)
            .ToArray();
    }
}
