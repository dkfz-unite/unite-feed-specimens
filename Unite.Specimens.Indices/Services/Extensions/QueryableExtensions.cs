using Microsoft.EntityFrameworkCore;
using Unite.Data.Entities.Specimens;

namespace Unite.Specimens.Indices.Services.Extensions;

internal static class QueryableExtensions
{
    internal static IQueryable<Specimen> IncludeParentTissue(this IQueryable<Specimen> query)
    {
        return query
            .Include(specimen => specimen.Parent)
                .ThenInclude(specimen => specimen.Tissue)
                    .ThenInclude(tissue => tissue.Source);
    }

    internal static IQueryable<Specimen> IncludeParentCellLine(this IQueryable<Specimen> query)
    {
        return query
            .Include(specimen => specimen.Parent)
                .ThenInclude(specimen => specimen.CellLine)
                    .ThenInclude(cellLine => cellLine.Info);
    }

    internal static IQueryable<Specimen> IncludeParentOrganoid(this IQueryable<Specimen> query)
    {
        return query
            .Include(specimen => specimen.Parent)
                .ThenInclude(specimen => specimen.Organoid)
                    .ThenInclude(organoid => organoid.Interventions)
                        .ThenInclude(intervention => intervention.Type);
    }

    internal static IQueryable<Specimen> IncludeParentXenograft(this IQueryable<Specimen> query)
    {
        return query
            .Include(specimen => specimen.Parent)
                .ThenInclude(specimen => specimen.Xenograft)
                    .ThenInclude(xenograft => xenograft.Interventions)
                        .ThenInclude(intervention => intervention.Type);
    }

    internal static IQueryable<Specimen> IncludeParentMolecularData(this IQueryable<Specimen> query)
    {
        return query
            .Include(specimen => specimen.Parent)
                .ThenInclude(specimen => specimen.MolecularData);
    }
}
