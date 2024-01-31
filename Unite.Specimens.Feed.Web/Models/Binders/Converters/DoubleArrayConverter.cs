using Unite.Essentials.Tsv.Converters;

namespace Unite.Specimens.Feed.Web.Models.Binders.Converters;

public class DoubleArrayConverter : IConverter<double[]>
{
    public object Convert(string value, string row)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        var separator = 
            value.Contains(';') ? ';' :
            value.Contains(',') ? ',' :
            throw new ArgumentException("Not supported separator");

        var rawValues = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);

        return Array.ConvertAll(rawValues, double.Parse);
    }

    public string Convert(object value, object row)
    {
        throw new NotImplementedException();
    }
}
