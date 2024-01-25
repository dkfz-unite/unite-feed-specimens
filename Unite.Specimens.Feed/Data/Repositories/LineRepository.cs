using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Lines;
using Unite.Data.Entities.Specimens.Enums;
using Unite.Specimens.Feed.Data.Models;

namespace Unite.Specimens.Feed.Data.Repositories;

internal class LineRepository : SpecimenRepositoryBase<LineModel>
{
    public LineRepository(DomainDbContext dbContext) : base(dbContext)
    {
    }


    public override Specimen Find(int donorId, int? parentId, in LineModel model)
    {
        var referenceId = model.ReferenceId;

        var entity = _dbContext.Set<Specimen>()
            .Include(entity => entity.Line)
                .ThenInclude(line => line.Info)
            .Include(entity => entity.MolecularData)
            .FirstOrDefault(entity =>
                entity.DonorId == donorId &&
                entity.Line != null &&
                entity.Line.ReferenceId == referenceId
            );

        return entity;
    }


    protected override void Map(in LineModel model, ref Specimen entity)
    {
        base.Map(model, ref entity);

        entity.TypeId = SpecimenType.Line;

        if (entity.Line == null)
        {
            entity.Line = new Line();
        }

        entity.Line.ReferenceId = model.ReferenceId;
        entity.Line.CellsSpeciesId = model.CellsSpecies;
        entity.Line.CellsTypeId = model.CellsType;
        entity.Line.CellsCultureTypeId = model.CellsCultureType;

        if (model.Info != null)
        {
            if (entity.Line.Info == null)
            {
                entity.Line.Info = new LineInfo();
            }

            entity.Line.Info.Name = model.Info.Name;
            entity.Line.Info.DepositorName = model.Info.DepositorName;
            entity.Line.Info.DepositorEstablishment = model.Info.DepositorEstablishment;
            entity.Line.Info.EstablishmentDate = model.Info.EstablishmentDate;
            entity.Line.Info.PubMedLink = model.Info.PubMedLink;
            entity.Line.Info.AtccLink = model.Info.AtccLink;
            entity.Line.Info.ExPasyLink = model.Info.ExPasyLink;
        }
    }
}
