namespace Unite.Specimens.Feed.Web.Models.Converters;

public class OrganoidInterventionsDataModelConverter
{
    public SpecimenDataModel[] Convert(in OrganoidInterventionsDataModel[] source)
    {
        return source
            .Select(model => new SpecimenDataModel
            {
                Id = model.SpecimenId,
                DonorId = model.DonorId,
                Organoid = new Base.OrganoidModel()
                {
                    Interventions = model.Data.Select(intervention => new Base.OrganoidInterventionModel()
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
