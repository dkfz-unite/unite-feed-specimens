namespace Unite.Specimens.Feed.Web.Models.Converters;

public class XenograftInterventionsDataModelConverter
{
    public SpecimenDataModel[] Convert(in XenograftInterventionsDataModel[] source)
    {
        return source
            .Select(model => new SpecimenDataModel
            {
                Id = model.SpecimenId,
                DonorId = model.DonorId,
                Xenograft = new Base.XenograftModel()
                {
                    Interventions = model.Data.Select(intervention => new Base.XenograftInterventionModel()
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
