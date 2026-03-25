using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Enums;
using Unite.Specimens.Feed.Data.Exceptions;
using Unite.Specimens.Feed.Data.Models;

namespace Unite.Specimens.Feed.Data.Repositories;

internal abstract class SpecimenRepositoryBase<TModel> where TModel : SpecimenModel
{
    protected readonly DomainDbContext _dbContext;
    protected readonly DonorRepository _donorRepository;

    public SpecimenRepositoryBase(DomainDbContext dbContext)
    {
        _dbContext = dbContext;
        _donorRepository = new DonorRepository(dbContext);
    }


    public virtual Specimen Find(SpecimenModel model)
    {
        var type = GetSpecimenType(model);
        var donor = _donorRepository.Find(model.Donor);

        if (donor == null)
            return null;

        return GetQuery().FirstOrDefault(entity =>
            entity.DonorId == donor.Id &&
            entity.ReferenceId == model.ReferenceId &&
            entity.TypeId == type
        );
    }

    public virtual Specimen FindParent(SpecimenModel model)
    {
        var specimen = Find(model);

        if (specimen == null)
            throw new NotFoundException($"Parent specimen with referenceId '{model.ReferenceId}' was not found");

        return specimen;
    }

    public virtual Specimen Create(TModel model)
    {
        var type = GetSpecimenType(model);
        var donor = _donorRepository.FindOrCreate(model.Donor);

        var entity = new Specimen
        {
            DonorId = donor.Id,
            ReferenceId = model.ReferenceId,
            TypeId = type
        };

        if (model.Parent != null)
            entity.ParentId = FindParent(model.Parent).Id;

        Map(model, entity);

        _dbContext.Add(entity);
        _dbContext.SaveChanges();

        return entity;
    }

    public virtual void Update(Specimen entity, TModel model)
    {
        if (model.Parent != null)
            entity.ParentId = FindParent(model.Parent).Id;

        Map(model, entity);

        _dbContext.Update(entity);
        _dbContext.SaveChanges();
    }


    protected virtual IQueryable<Specimen> GetQuery()
    {
        return _dbContext.Set<Specimen>()
            .Include(entity => entity.TumorClassification)
            .Include(entity => entity.MolecularData);
    }

    protected virtual void Map(TModel model, Specimen entity)
    {
        entity.CreationDate = model.CreationDate;
        entity.CreationDay = model.CreationDay;
        entity.CategoryId = model.Category;
        entity.TumorTypeId = model.TumorType;
        entity.TumorGrade = model.TumorGrade;

        if (model.TumorClassification != null)
        {
            if (entity.TumorClassification == null)
                entity.TumorClassification = new TumorClassification();

            entity.TumorClassification.Superfamily = GetTumorSuperfamily(model.TumorClassification.Superfamily);
            entity.TumorClassification.SuperfamilyScore = model.TumorClassification.SuperfamilyScore;
            entity.TumorClassification.Family = GetTumorFamily(model.TumorClassification.Family);
            entity.TumorClassification.FamilyScore = model.TumorClassification.FamilyScore;
            entity.TumorClassification.Class = GetTumorClass(model.TumorClassification.Class);
            entity.TumorClassification.ClassScore = model.TumorClassification.ClassScore;
            entity.TumorClassification.Subclass = GetTumorSubclass(model.TumorClassification.Subclass);
            entity.TumorClassification.SubclassScore = model.TumorClassification.SubclassScore;
        }

        if (model.MolecularData != null)
        {
            if (entity.MolecularData == null)
                entity.MolecularData = new MolecularData();

            entity.MolecularData.MgmtStatus = model.MolecularData.MgmtStatus;
            entity.MolecularData.IdhStatus = model.MolecularData.IdhStatus;
            entity.MolecularData.IdhMutationId = model.MolecularData.IdhMutation;
            entity.MolecularData.TertStatus = model.MolecularData.TertStatus;
            entity.MolecularData.TertMutationId = model.MolecularData.TertMutation;
            entity.MolecularData.ExpressionSubtypeId = model.MolecularData.ExpressionSubtype;
            entity.MolecularData.MethylationSubtypeId = model.MolecularData.MethylationSubtype;
            entity.MolecularData.GcimpMethylation = model.MolecularData.GcimpMethylation;
            entity.MolecularData.GeneKnockouts = model.MolecularData.GeneKnockouts;
        }
    }


    private TumorSuperfamily GetTumorSuperfamily(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        var entity = _dbContext.Set<TumorSuperfamily>()
            .FirstOrDefault(entity =>
                entity.Name == value
            );

        if (entity == null)
        {
            entity = new TumorSuperfamily() { Name = value };

            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        return entity;
    }

    private TumorFamily GetTumorFamily(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        var entity = _dbContext.Set<TumorFamily>()
            .FirstOrDefault(entity =>
                entity.Name == value
            );

        if (entity == null)
        {
            entity = new TumorFamily() { Name = value };

            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        return entity;
    }

    private TumorClass GetTumorClass(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        var entity = _dbContext.Set<TumorClass>()
            .FirstOrDefault(entity =>
                entity.Name == value
            );

        if (entity == null)
        {
            entity = new TumorClass() { Name = value };

            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        return entity;
    }

    private TumorSubclass GetTumorSubclass(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        var entity = _dbContext.Set<TumorSubclass>()
            .FirstOrDefault(entity =>
                entity.Name == value
            );

        if (entity == null)
        {
            entity = new TumorSubclass() { Name = value };

            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        return entity;
    }


    private static SpecimenType GetSpecimenType(SpecimenModel model)
    {
        if (model is MaterialModel)
            return SpecimenType.Material;
        else if (model is LineModel)
            return SpecimenType.Line;
        else if (model is OrganoidModel)
            return SpecimenType.Organoid;
        else if (model is XenograftModel)
            return SpecimenType.Xenograft;
        else
            throw new NotSupportedException("Specimen type is not supported");
    }
}
