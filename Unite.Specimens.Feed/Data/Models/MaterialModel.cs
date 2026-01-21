using Unite.Data.Entities.Specimens.Materials.Enums;

namespace Unite.Specimens.Feed.Data.Models;

public class MaterialModel : SpecimenModel
{
    public FixationType? FixationType { get; set; }
    public string Source { get; set; }
}
