using Microsoft.EntityFrameworkCore;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Cells;
using Unite.Data.Services;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories;

internal class CellLineRepository : SpecimenRepositoryBase<CellLineModel>
{
    public CellLineRepository(DomainDbContext dbContext) : base(dbContext)
    {
    }


    public override Specimen Find(int donorId, int? parentId, in CellLineModel model)
    {
        var referenceId = model.ReferenceId;

        var entity = _dbContext.Set<Specimen>()
            .Include(entity => entity.CellLine)
                .ThenInclude(cellLine => cellLine.Info)
            .Include(entity => entity.MolecularData)
            .FirstOrDefault(entity =>
                entity.DonorId == donorId &&
                entity.CellLine != null &&
                entity.CellLine.ReferenceId == referenceId
            );

        return entity;
    }


    protected override void Map(in CellLineModel model, ref Specimen entity)
    {
        base.Map(model, ref entity);

        if (entity.CellLine == null)
        {
            entity.CellLine = new CellLine();
        }

        entity.CellLine.ReferenceId = model.ReferenceId;
        entity.CellLine.SpeciesId = model.Species;
        entity.CellLine.TypeId = model.Type;
        entity.CellLine.CultureTypeId = model.CultureType;

        if (model.Info != null)
        {
            if (entity.CellLine.Info == null)
            {
                entity.CellLine.Info = new CellLineInfo();
            }

            entity.CellLine.Info.Name = model.Info.Name;
            entity.CellLine.Info.DepositorName = model.Info.DepositorName;
            entity.CellLine.Info.DepositorEstablishment = model.Info.DepositorEstablishment;
            entity.CellLine.Info.EstablishmentDate = model.Info.EstablishmentDate;
            entity.CellLine.Info.PubMedLink = model.Info.PubMedLink;
            entity.CellLine.Info.AtccLink = model.Info.AtccLink;
            entity.CellLine.Info.ExPasyLink = model.Info.ExPasyLink;
        }
    }
}
