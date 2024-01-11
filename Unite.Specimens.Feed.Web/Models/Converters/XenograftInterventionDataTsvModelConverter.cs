namespace Unite.Specimens.Feed.Web.Models.Converters;

public class XenograftInterventionDataTsvModelConverter
{
    public SpecimenDataModel[] Convert(in XenograftInterventionDataTsvModel[] source)
    {
        return source
            .GroupBy(intervention => new { intervention.DonorId, intervention.SpecimenId})
            .Select(group => new SpecimenDataModel
            {
                Id = group.Key.SpecimenId,
                DonorId = group.Key.DonorId,
                Xenograft = new Base.XenograftModel()
                {
                    Interventions = group.Select(intervention => new Base.XenograftInterventionModel()
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
