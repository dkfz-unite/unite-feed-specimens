namespace Unite.Specimens.Feed.Web.Models.Base.Validators.Extensions;

internal static class ValidationExtensions
{
    public static bool IsEmpty<T>(this T source)
    {
        if (source == null)
            return true;

        var props = typeof(T).GetProperties();

        return props.All(prop => prop.GetValue(source) == null);
    }

    public static bool IsNotEmpty<T>(this T source)
    {
        if (source == null)
            return false;
            
        var props = typeof(T).GetProperties();

        return props.Any(prop => prop.GetValue(source) != null);
    }
}
