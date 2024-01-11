namespace Unite.Specimens.Feed.Web.Models.Converters;

public class OrganoidInterventionDataTsvModelConverter
{
    public SpecimenDataModel[] Convert(in OrganoidInterventionDataTsvModel[] source)
    {
        return source
            .GroupBy(intervention => new { intervention.DonorId, intervention.SpecimenId})
            .Select(group => new SpecimenDataModel
            {
                Id = group.Key.SpecimenId,
                DonorId = group.Key.DonorId,
                Organoid = new Base.OrganoidModel()
                {
                    Interventions = group.Select(intervention => new Base.OrganoidInterventionModel()
                    {
                        Type = intervention.Type,
                        Details = intervention.Details,
                        StartDate = intervention.StartDate,
                        StartDay = intervention.StartDay,
                        EndDate = intervention.EndDate,
                        DurationDays = intervention.DurationDays,
                        Results = intervention.Results

                    }).ToArray()
                }
            })
            .ToArray();
    }
}
