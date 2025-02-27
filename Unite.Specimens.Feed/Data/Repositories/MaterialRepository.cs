using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Materials;
using Unite.Specimens.Feed.Data.Models;

namespace Unite.Specimens.Feed.Data.Repositories;

internal class MaterialRepository : SpecimenRepositoryBase<MaterialModel>
{
    public MaterialRepository(DomainDbContext dbContext) : base(dbContext)
    {
    }


    protected override IQueryable<Specimen> GetQuery()
    {
        return base.GetQuery()
            .Include(entity => entity.Material);
    }

    protected override void Map(MaterialModel model, Specimen entity)
    {
        base.Map(model, entity);

        if (entity.Material == null)
            entity.Material = new Material();

        entity.Material.TypeId = model.Type;
        entity.Material.FixationTypeId = model.FixationType;
        entity.Material.TumorTypeId = model.TumorType;
        entity.Material.TumorGrade = model.TumorGrade;
        entity.Material.Source = GetMaterialSource(model.Source);
    }

    private MaterialSource GetMaterialSource(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

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
