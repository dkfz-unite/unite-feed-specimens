using FluentValidation;
using Unite.Specimens.Feed.Web.Models.Base.Validators;

namespace Unite.Specimens.Feed.Web.Models.Validators;

public class XenograftInterventionDataTsvModelValidator : AbstractValidator<XenograftInterventionDataTsvModel>
{
    public XenograftInterventionDataTsvModelValidator()
    {
        RuleFor(model => model.DonorId)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.DonorId)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");
        
        RuleFor(model => model.SpecimenId)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.SpecimenId)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");

        RuleFor(model => model)
            .SetValidator(new XenograftInterventionModelValidator());
    }
}

public class XenograftInterventionDataTsvModelsValidator : AbstractValidator<IEnumerable<XenograftInterventionDataTsvModel>>
{
    private readonly XenograftInterventionDataTsvModelValidator _validator = new();

    public XenograftInterventionDataTsvModelsValidator()
    {
        RuleForEach(model => model)
            .SetValidator(_validator);

        RuleFor(model => model)
            .Must(BeUniquePerDonor)
            .WithMessage("Each donor should have unique specimens");
    }

    private bool BeUniquePerDonor(IEnumerable<XenograftInterventionDataTsvModel> interventions)
    {
        return interventions
            .GroupBy(model => model.DonorId)
            .All(group => group.Count() == group.DistinctBy(model => model.SpecimenId).Count());
    }
}
