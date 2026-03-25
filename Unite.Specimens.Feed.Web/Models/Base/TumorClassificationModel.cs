using System.Text.Json.Serialization;

namespace Unite.Specimens.Feed.Web.Models.Base;

public class TumorClassificationModel
{
    private string _superfamily;
    private double? _superfamilyScore;
    private string _family;
    private double? _familyScore;
    private string _class;
    private double? _classScore;
    private string _subclass;
    private double? _subclassScore;


    [JsonPropertyName("superfamily")]
    public string Superfamily { get => _superfamily?.Trim(); set => _superfamily = value; }

    [JsonPropertyName("superfamily_score")]
    public double? SuperfamilyScore { get => _superfamilyScore; set => _superfamilyScore = value; }

    [JsonPropertyName("family")]
    public string Family { get => _family?.Trim(); set => _family = value; }

    [JsonPropertyName("family_score")]
    public double? FamilyScore { get => _familyScore; set => _familyScore = value; }

    [JsonPropertyName("class")]
    public string Class { get => _class?.Trim(); set => _class = value; }

    [JsonPropertyName("class_score")]
    public double? ClassScore { get => _classScore; set => _classScore = value; }

    [JsonPropertyName("subclass")]
    public string Subclass { get => _subclass?.Trim(); set => _subclass = value; }

    [JsonPropertyName("subclass_score")]
    public double? SubclassScore { get => _subclassScore; set => _subclassScore = value; }
}
