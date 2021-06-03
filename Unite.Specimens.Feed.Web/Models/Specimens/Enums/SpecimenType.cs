using System.Runtime.Serialization;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Enums
{
    public enum SpecimenType
    {
        [EnumMember(Value = "Tissue")]
        Tissue = 1,

        [EnumMember(Value = "CellLine")]
        CellLine = 2,

        [EnumMember(Value = "Xenograft")]
        Xenograft = 3
    }
}
