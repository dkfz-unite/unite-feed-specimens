using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Unite.Data.Entities.Specimens.Enums;
using Unite.Essentials.Tsv;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Binders;

public class InterventionsTsvModelsBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);

        var tsv = await reader.ReadToEndAsync();

        var map = new ClassMap<InterventionModel>()
            .Map(entity => entity.DonorId, "donor_id")
            .Map(entity => entity.SpecimenId, "specimen_id")
            .Map(entity => entity.SpecimenType, "specimen_type")
            .Map(entity => entity.Type, "type")
            .Map(entity => entity.Details, "details")
            .Map(entity => entity.StartDate, "start_date")
            .Map(entity => entity.StartDay, "start_day")
            .Map(entity => entity.EndDate, "end_date")
            .Map(entity => entity.DurationDays, "duration_days")
            .Map(entity => entity.Results, "results");

        var interventionModels = TsvReader.Read(tsv, map).ToArray();

        var interventionsModels = interventionModels
            .GroupBy(model => new { model.DonorId, model.SpecimenId, model.SpecimenType })
            .Select(group => new InterventionsModel
            {
                DonorId = group.Key.DonorId,
                SpecimenId = group.Key.SpecimenId,
                SpecimenType = group.Key.SpecimenType,
                Entries = group.ToArray()
            })
            .ToArray();

        bindingContext.Result = ModelBindingResult.Success(interventionsModels);
    }
}

public record InterventionModel : Base.InterventionModel
{
    private string _donorId;
    private string _specimenId;
    private SpecimenType? _specimenType;

    [JsonPropertyName("donor_id")]
    public string DonorId { get => _donorId?.Trim(); set => _donorId = value; }

    [JsonPropertyName("specimen_id")]
    public string SpecimenId { get => _specimenId?.Trim(); set => _specimenId = value; }

    [JsonPropertyName("specimen_type")]
    public SpecimenType? SpecimenType { get => _specimenType; set => _specimenType = value; }
}
