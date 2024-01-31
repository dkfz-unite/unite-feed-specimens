using Microsoft.EntityFrameworkCore;
using Unite.Data.Entities.Specimens;

namespace Unite.Specimens.Indices.Services.Extensions;

internal static class QueryableExtensions
{
    internal static IQueryable<Specimen> IncludeParentMaterial(this IQueryable<Specimen> query)
    {
        return query
            .Include(specimen => specimen.Parent)
                .ThenInclude(specimen => specimen.Material)
                    .ThenInclude(material => material.Source);
    }

    internal static IQueryable<Specimen> IncludeParentLine(this IQueryable<Specimen> query)
    {
        return query
            .Include(specimen => specimen.Parent)
                .ThenInclude(specimen => specimen.Line);
    }

    internal static IQueryable<Specimen> IncludeParentOrganoid(this IQueryable<Specimen> query)
    {
        return query
            .Include(specimen => specimen.Parent)
                .ThenInclude(specimen => specimen.Organoid);
    }

    internal static IQueryable<Specimen> IncludeParentXenograft(this IQueryable<Specimen> query)
    {
        return query
            .Include(specimen => specimen.Parent)
                .ThenInclude(specimen => specimen.Xenograft);
    }
}
