using System.Linq.Expressions;
using Unite.Essentials.Extensions;
using Unite.Essentials.Tsv;

namespace Unite.Specimens.Feed.Web.Models.Base.Binders.Extensions;

internal static class TumorClassificationBindingExtensions
{
    public static ClassMap<T> MapTumorClassification<T>(this ClassMap<T> map, Expression<Func<T, TumorClassificationModel>> path)
        where T : class
    {
        var arrayConverter = new ArrayConverter();

        return map
            .Map(path.Join(entity => entity.Superfamily), "tumor_superfamily")
            .Map(path.Join(entity => entity.Family), "tumor_family")
            .Map(path.Join(entity => entity.Class), "tumor_class")
            .Map(path.Join(entity => entity.Subclass), "tumor_subclass");
    }
}
