using FluentValidation;
using Unite.Specimens.Feed.Web.Models.Base.Validators;

namespace Unite.Specimens.Feed.Web.Models.Validators;

public class OrganoidInterventionDataTsvModelValidator : AbstractValidator<OrganoidInterventionDataTsvModel>
{
    public OrganoidInterventionDataTsvModelValidator()
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
            .SetValidator(new OrganoidInterventionModelValidator());
    }
}

public class OrganoidInterventionDataTsvModelsValidator : AbstractValidator<IEnumerable<OrganoidInterventionDataTsvModel>>
{
    public OrganoidInterventionDataTsvModelsValidator()
    {
        RuleForEach(model => model)
            .SetValidator(new OrganoidInterventionDataTsvModelValidator());

        RuleFor(model => model)
            .Must(HaveAtLeastOneIntervention);

        RuleFor(model => model)
            .Must(HaveUniqueSpecimenGroups);
    }


    private static bool HaveAtLeastOneIntervention(IEnumerable<OrganoidInterventionDataTsvModel> models)
    {
        return models.Any();
    }

    private static bool HaveUniqueSpecimenGroups(IEnumerable<OrganoidInterventionDataTsvModel> models)
    {
        var groups = models.GroupBy(model => new { model.DonorId, model.SpecimenId });

        var hasDonors = groups.All(group => group.Key.DonorId != null);
        var hasSpecimens = groups.All(group => group.Key.SpecimenId != null);

        return hasDonors && hasSpecimens;
    }
}
