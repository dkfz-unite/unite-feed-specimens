using System.Linq.Expressions;
using Unite.Essentials.Extensions;
using Unite.Essentials.Tsv;

namespace Unite.Specimens.Feed.Web.Models.Binders.Extensions;

internal static class SpecimenBindingExtensions
{
    public static ClassMap<T> MapSpecimen<T>(this ClassMap<T> map, Expression<Func<T, SpecimenDataModel>> path)
        where T : class
    {
        return map
            .Map(path.Join(entity => entity.Id), "id")
            .Map(path.Join(entity => entity.DonorId), "donor_id")
            .Map(path.Join(entity => entity.ParentId), "parent_id")
            .Map(path.Join(entity => entity.ParentType), "parent_type")
            .Map(path.Join(entity => entity.CreationDate), "creation_date")
            .Map(path.Join(entity => entity.CreationDay), "creation_day");
    }
}
