using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Analysis;
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

    public Specimen Find(SpecimenModel model)
    {
        if (model is MaterialModel materialModel)
            return _materialRepository.Find(materialModel);
        else if (model is LineModel lineModel)
            return _lineRepository.Find(lineModel);
        else if (model is OrganoidModel organoidModel)
            return _organoidRepository.Find(organoidModel);
        else if (model is XenograftModel xenograftModel)
            return _xenograftRepository.Find(xenograftModel);
        else
            throw new NotImplementedException("Specimen type is not yet supported");
    }

    public Specimen Create(SpecimenModel model)
    {
        if (model is MaterialModel materialModel)
            return _materialRepository.Create(materialModel);
        else if (model is LineModel lineModel)
            return _lineRepository.Create(lineModel);
        else if (model is OrganoidModel organoidModel)
            return _organoidRepository.Create(organoidModel);
        else if (model is XenograftModel xenograftModel)
            return _xenograftRepository.Create(xenograftModel);
        else
            throw new NotSupportedException("Specimen type is not yet supported");
    }

    public Specimen FindOrCreate(SpecimenModel model)
    {
        return Find(model) ?? Create(model);
    }

    public void Update(Specimen entity, SpecimenModel model)
    {
        if (model is MaterialModel materialModel)
            _materialRepository.Update(entity, materialModel);
        else if (model is LineModel lineModel)
            _lineRepository.Update(entity, lineModel);
        else if (model is OrganoidModel organoidModel)
            _organoidRepository.Update(entity, organoidModel);
        else if (model is XenograftModel xenograftModel)
            _xenograftRepository.Update(entity, xenograftModel);
        else
            throw new NotSupportedException("Specimen type is not yet supported");
    }

    public void Delete(Specimen specimen)
    {
        var children = LoadChildren(specimen);

        if (children.IsNotEmpty())
        {
            children.ForEach(Delete);
        }

        var analyses = _dbContext.Set<Sample>()
            .AsNoTracking()
            .Include(entity => entity.Analysis)
            .Where(entity => entity.SpecimenId == specimen.Id)
            .Select(entity => entity.Analysis)
            .Distinct()
            .ToArray();

        _dbContext.RemoveRange(analyses);
        _dbContext.Remove(specimen);
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
