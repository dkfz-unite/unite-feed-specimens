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
            .Include(entity => entity.MolecularData);
    }

    protected virtual void Map(TModel model, Specimen entity)
    {
        entity.CreationDate = model.CreationDate;
        entity.CreationDay = model.CreationDay;

        if (model.MolecularData != null)
        {
            if (entity.MolecularData == null)
                entity.MolecularData = new MolecularData();

            entity.MolecularData.MgmtStatusId = model.MolecularData.MgmtStatus;
            entity.MolecularData.IdhStatusId = model.MolecularData.IdhStatus;
            entity.MolecularData.IdhMutationId = model.MolecularData.IdhMutation;
            entity.MolecularData.GeneExpressionSubtypeId = model.MolecularData.GeneExpressionSubtype;
            entity.MolecularData.MethylationSubtypeId = model.MolecularData.MethylationSubtype;
            entity.MolecularData.GcimpMethylation = model.MolecularData.GcimpMethylation;
        }
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
