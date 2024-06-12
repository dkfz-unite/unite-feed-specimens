using Unite.Specimens.Feed.Web.Models.Base.Converters;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Converters;

public class InterventionsModelConverter : ConverterBase
{
    private readonly InterventionModelConverter _interventionModelConverter = new();

    public Data.Models.SpecimenModel Convert(InterventionsModel source)
    {
        var specimen = GetSpecimen(source.DonorId, source.SpecimenId, source.SpecimenType);
        
        specimen.Interventions = source.Entries.Select(_interventionModelConverter.Convert).ToArray();

        return specimen;
    }
}
