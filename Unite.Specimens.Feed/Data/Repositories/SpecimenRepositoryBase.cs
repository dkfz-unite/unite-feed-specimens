using Unite.Data.Context;
using Unite.Data.Entities.Specimens;
using Unite.Specimens.Feed.Data.Models;

namespace Unite.Specimens.Feed.Data.Repositories;

internal abstract class SpecimenRepositoryBase<TModel> where TModel : SpecimenModel
{
    protected readonly DomainDbContext _dbContext;

    public SpecimenRepositoryBase(DomainDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public abstract Specimen Find(int donorId, int? parentId, in TModel model);

    public virtual Specimen Create(int donorId, int? parentId, in TModel model)
    {
        var entity = new Specimen
        {
            DonorId = donorId,
            ParentId = parentId,
            ReferenceId = model.ReferenceId
        };

        Map(model, ref entity);

        _dbContext.Add(entity);
        _dbContext.SaveChanges();

        return entity;
    }

    public virtual void Update(ref Specimen entity, in TModel model)
    {
        Map(model, ref entity);

        _dbContext.Update(entity);
        _dbContext.SaveChanges();
    }


    protected virtual void Map(in TModel model, ref Specimen entity)
    {
        entity.CreationDate = model.CreationDate;
        entity.CreationDay = model.CreationDay;

        if (model.MolecularData != null)
        {
            if (entity.MolecularData == null)
            {
                entity.MolecularData = new MolecularData();
            }

            entity.MolecularData.MgmtStatusId = model.MolecularData.MgmtStatus;
            entity.MolecularData.IdhStatusId = model.MolecularData.IdhStatus;
            entity.MolecularData.IdhMutationId = model.MolecularData.IdhMutation;
            entity.MolecularData.GeneExpressionSubtypeId = model.MolecularData.GeneExpressionSubtype;
            entity.MolecularData.MethylationSubtypeId = model.MolecularData.MethylationSubtype;
            entity.MolecularData.GcimpMethylation = model.MolecularData.GcimpMethylation;
        }
    }
}
