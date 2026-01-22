using System.Text.Json.Serialization;

namespace Unite.Specimens.Feed.Web.Models.Base;

public class TumorClassificationModel
{
    private string _superfamily;
    private string _family;
    private string _class;
    private string _subclass;


    [JsonPropertyName("superfamily")]
    public string Superfamily { get => _superfamily?.Trim(); set => _superfamily = value; }

    [JsonPropertyName("family")]
    public string Family { get => _family?.Trim(); set => _family = value; }

    [JsonPropertyName("class")]
    public string Class { get => _class?.Trim(); set => _class = value; }

    [JsonPropertyName("subclass")]
    public string Subclass { get => _subclass?.Trim(); set => _subclass = value; }
}
