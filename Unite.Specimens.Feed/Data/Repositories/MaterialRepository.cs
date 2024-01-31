using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Enums;
using Unite.Data.Entities.Specimens.Materials;
using Unite.Specimens.Feed.Data.Models;

namespace Unite.Specimens.Feed.Data.Repositories;

internal class MaterialRepository : SpecimenRepositoryBase<MaterialModel>
{
    public MaterialRepository(DomainDbContext dbContext) : base(dbContext)
    {
    }


    public override Specimen Find(int donorId, int? parentId, in MaterialModel model)
    {
        var referenceId = model.ReferenceId;

        var entity = _dbContext.Set<Specimen>()
            .Include(entity => entity.Material)
                .ThenInclude(tissue => tissue.Source)
            .Include(entity => entity.MolecularData)
            .FirstOrDefault(entity =>
                entity.DonorId == donorId &&
                entity.Material != null &&
                entity.Material.ReferenceId == referenceId
            );

        return entity;
    }


    protected override void Map(in MaterialModel model, ref Specimen entity)
    {
        base.Map(model, ref entity);

        entity.TypeId = SpecimenType.Material;

        if (entity.Material == null)
        {
            entity.Material = new Material();
        }

        entity.Material.ReferenceId = model.ReferenceId;
        entity.Material.TypeId = model.Type;
        entity.Material.TumorTypeId = model.TumorType;
        entity.Material.Source = GetTissueSource(model.Source);
    }

    private MaterialSource GetTissueSource(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var entity = _dbContext.Set<MaterialSource>()
                .FirstOrDefault(entity =>
                entity.Value == value
            );

        if (entity == null)
        {
            entity = new MaterialSource() { Value = value };

            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }

        return entity;
    }
}
