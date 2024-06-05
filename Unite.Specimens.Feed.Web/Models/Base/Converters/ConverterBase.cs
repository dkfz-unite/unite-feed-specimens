using Unite.Data.Entities.Specimens.Enums;

namespace Unite.Specimens.Feed.Web.Models.Base.Converters;

public abstract class ConverterBase
{
    protected virtual Data.Models.SpecimenModel GetSpecimen(string donorId, string specimenId, SpecimenType? specimenType)
    {
        if (specimenId == null || specimenType == null)
            return null;

        Data.Models.SpecimenModel specimen;

        if (specimenType == SpecimenType.Material)
            specimen = new Data.Models.MaterialModel { ReferenceId = specimenId };
        else if (specimenType == SpecimenType.Line)
            specimen = new Data.Models.LineModel { ReferenceId = specimenId };
        else if (specimenType == SpecimenType.Organoid)
            specimen = new Data.Models.OrganoidModel { ReferenceId = specimenId };
        else if (specimenType == SpecimenType.Xenograft)
            specimen = new Data.Models.XenograftModel { ReferenceId = specimenId };
        else
            throw new NotSupportedException($"Specimen type '{specimenType}' is not supported");

        specimen.Donor = GetDonor(donorId);

        return specimen;
    }

    protected virtual Data.Models.DonorModel GetDonor(string donorId)
    {
        return new Data.Models.DonorModel { ReferenceId = donorId };
    }
}
