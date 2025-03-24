using Unite.Data.Entities.Specimens.Analysis.Enums;

namespace Unite.Specimens.Feed.Data.Models;

public class AnalysisModel
{
    public AnalysisType Type;
    public DateOnly? Date;
    public int? Day;
    public Dictionary<string, string> Parameters;
}
