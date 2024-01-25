using Unite.Data.Entities.Specimens.Lines.Enums;

namespace Unite.Specimens.Feed.Data.Models;

public class LineModel : SpecimenModel
{
    public CellsSpecies? CellsSpecies { get; set; }
    public CellsType? CellsType { get; set; }
    public CellsCultureType? CellsCultureType { get; set; }

    public LineInfoModel Info { get; set; }
}
