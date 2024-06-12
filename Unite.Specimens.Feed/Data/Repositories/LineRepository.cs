using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Lines;
using Unite.Specimens.Feed.Data.Models;

namespace Unite.Specimens.Feed.Data.Repositories;

internal class LineRepository : SpecimenRepositoryBase<LineModel>
{
    public LineRepository(DomainDbContext dbContext) : base(dbContext)
    {
    }


    protected override IQueryable<Specimen> GetQuery()
    {
        return base.GetQuery()
            .Include(entity => entity.Line.Info);
    }

    protected override void Map(LineModel model, Specimen entity)
    {
        base.Map(model, entity);

        if (entity.Line == null)
            entity.Line = new Line();

        entity.Line.CellsCultureTypeId = model.CellsCultureType;
        entity.Line.CellsSpeciesId = model.CellsSpecies;
        entity.Line.CellsTypeId = model.CellsType;
        entity.Line.Info = GetLineInfo(model.Info);
    }

    private LineInfo GetLineInfo(LineInfoModel model)
    {
        if (model == null)
            return null;

        return new LineInfo {
            Name = model.Name,
            DepositorName = model.DepositorName,
            DepositorEstablishment = model.DepositorEstablishment,
            EstablishmentDate = model.EstablishmentDate,
            PubMedLink = model.PubMedLink,
            AtccLink = model.AtccLink,
            ExPasyLink = model.ExPasyLink
        };
    }
}
