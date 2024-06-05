namespace Unite.Specimens.Feed.Web.Models.Base.Validators.Extensions;

internal static class ValidationExtensions
{
    public static bool IsNotEmpty<T>(this T source)
    {
        var props = typeof(T).GetProperties();

        return props.Any(prop => prop.GetValue(source) != null);
    }
}
