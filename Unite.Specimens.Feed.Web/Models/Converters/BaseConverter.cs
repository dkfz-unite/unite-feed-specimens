using Unite.Data.Entities.Specimens.Enums;

using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Converters;


public abstract class BaseConverter
{
    protected virtual DataModels.DonorModel GetDonorModel(string id)
    {
        return new DataModels.DonorModel { ReferenceId = id };
    }

    protected virtual DataModels.SpecimenModel GetSpecimenModel(string id, SpecimenType type)
    {
        return type switch
        {
            SpecimenType.Material => new DataModels.MaterialModel { ReferenceId = id },
            SpecimenType.Line => new DataModels.LineModel { ReferenceId = id },
            SpecimenType.Organoid => new DataModels.OrganoidModel { ReferenceId = id },
            SpecimenType.Xenograft => new DataModels.XenograftModel { ReferenceId = id },
            _ => throw new NotImplementedException("Specimen type is not supported yet")
        };
    }
}
