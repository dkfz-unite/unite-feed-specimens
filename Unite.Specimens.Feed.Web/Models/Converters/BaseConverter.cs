using Unite.Data.Entities.Specimens.Enums;

namespace Unite.Specimens.Feed.Web.Models.Converters;

public abstract class BaseConverter
{
    protected virtual Data.Models.DonorModel GetDonorModel(string id)
    {
        return new Data.Models.DonorModel { ReferenceId = id };
    }

    protected virtual Data.Models.SpecimenModel GetSpecimenModel(string id, SpecimenType type)
    {
        return type switch
        {
            SpecimenType.Material => new Data.Models.MaterialModel { ReferenceId = id },
            SpecimenType.Line => new Data.Models.LineModel { ReferenceId = id },
            SpecimenType.Organoid => new Data.Models.OrganoidModel { ReferenceId = id },
            SpecimenType.Xenograft => new Data.Models.XenograftModel { ReferenceId = id },
            _ => throw new NotImplementedException("Specimen type is not supported yet")
        };
    }
}
