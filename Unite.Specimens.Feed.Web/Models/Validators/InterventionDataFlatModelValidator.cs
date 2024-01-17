using FluentValidation;
using Unite.Specimens.Feed.Web.Models.Base.Validators;

namespace Unite.Specimens.Feed.Web.Models.Validators;

public class InterventionDataFlatModelValidator : AbstractValidator<InterventionDataFlatModel>
{
    public InterventionDataFlatModelValidator()
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

        RuleFor(model => model.SpecimenType)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model)
            .SetValidator(new InterventionModelValidator());
    }
}

public class InterventionDataFlatModelsValidator : AbstractValidator<IEnumerable<InterventionDataFlatModel>>
{
    public InterventionDataFlatModelsValidator()
    {
        RuleForEach(model => model)
            .SetValidator(new InterventionDataFlatModelValidator());

        RuleFor(model => model)
            .Must(HaveAtLeastOneIntervention);

        RuleFor(model => model)
            .Must(HaveUniqueSpecimenGroups);
    }


    private static bool HaveAtLeastOneIntervention(IEnumerable<InterventionDataFlatModel> models)
    {
        return models.Any();
    }

    private static bool HaveUniqueSpecimenGroups(IEnumerable<InterventionDataFlatModel> models)
    {
        var groups = models.GroupBy(model => new { model.DonorId, model.SpecimenId });

        var hasDonors = groups.All(group => group.Key.DonorId != null);
        var hasSpecimens = groups.All(group => group.Key.SpecimenId != null);

        return hasDonors && hasSpecimens;
    }
}
