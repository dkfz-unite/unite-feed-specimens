using System.Linq;
using Microsoft.EntityFrameworkCore;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Cells;
using Unite.Data.Services;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories
{
    internal class CellLineRepository : SpecimenRepositoryBase<CellLineModel>
    {
        public CellLineRepository(UniteDbContext dbContext) : base(dbContext)
        {
        }


        public override Specimen Find(int donorId, int? parentId, in CellLineModel model)
        {
            var referenceId = model.ReferenceId;

            var specimen = _dbContext.Specimens
                .Include(specimen => specimen.CellLine)
                    .ThenInclude(cellLine => cellLine.Info)
                .Include(specimen => specimen.MolecularData)
                .FirstOrDefault(specimen =>
                    specimen.DonorId == donorId &&
                    specimen.CellLine != null &&
                    specimen.CellLine.ReferenceId == referenceId
                );

            return specimen;
        }


        protected override void Map(in CellLineModel model, ref Specimen specimen)
        {
            base.Map(model, ref specimen);

            if (specimen.CellLine == null)
            {
                specimen.CellLine = new CellLine();
            }

            specimen.CellLine.ReferenceId = model.ReferenceId;
            specimen.CellLine.SpeciesId = model.Species;
            specimen.CellLine.TypeId = model.Type;
            specimen.CellLine.CultureTypeId = model.CultureType;

            if (model.Info != null)
            {
                if (specimen.CellLine.Info == null)
                {
                    specimen.CellLine.Info = new CellLineInfo();
                }

                specimen.CellLine.Info.Name = model.Info.Name;
                specimen.CellLine.Info.DepositorName = model.Info.DepositorName;
                specimen.CellLine.Info.DepositorEstablishment = model.Info.DepositorEstablishment;
                specimen.CellLine.Info.EstablishmentDate = model.Info.EstablishmentDate;
                specimen.CellLine.Info.PubMedLink = model.Info.PubMedLink;
                specimen.CellLine.Info.AtccLink = model.Info.AtccLink;
                specimen.CellLine.Info.ExPasyLink = model.Info.ExPasyLink;
            }
        }
    }
}
