﻿using Unite.Data.Entities.Specimens.Materials.Enums;

namespace Unite.Specimens.Feed.Data.Models;

public class MaterialModel : SpecimenModel
{
    public MaterialType Type { get; set; }
    public FixationType? FixationType { get; set; }
    public TumorType? TumorType { get; set; }
    public byte? TumorGrade { get; set; }
    public string Source { get; set; }
}
