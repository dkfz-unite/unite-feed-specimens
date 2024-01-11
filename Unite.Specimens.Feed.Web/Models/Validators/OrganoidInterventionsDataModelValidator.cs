using FluentValidation;
using Unite.Specimens.Feed.Web.Models.Base.Validators;

namespace Unite.Specimens.Feed.Web.Models.Validators;

public class OrganoidInterventionsDataModelValidator : AbstractValidator<OrganoidInterventionsDataModel>
{
    private readonly OrganoidInterventionModelValidator _interventionModelValidator = new();

    public OrganoidInterventionsDataModelValidator()
    {
        RuleFor(model => model.SpecimenId)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.SpecimenId)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");

        RuleFor(model => model.DonorId)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.DonorId)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");
        
        RuleFor(model => model.Data)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.Data)
            .Must(interventions => interventions.Length > 0)
            .WithMessage("Should have at least one model");

        RuleForEach(model => model.Data)
            .SetValidator(_interventionModelValidator);
    }
}

public class OrganoidInterventionsDataModelsValidator : AbstractValidator<IEnumerable<OrganoidInterventionsDataModel>>
{
    private readonly OrganoidInterventionsDataModelValidator _validator = new();

    public OrganoidInterventionsDataModelsValidator()
    {
        RuleForEach(model => model)
            .SetValidator(_validator);

        RuleFor(model => model)
            .Must(BeUniquePerDonor)
            .WithMessage("Each donor should have unique specimens");
    }

    private bool BeUniquePerDonor(IEnumerable<OrganoidInterventionsDataModel> models)
    {
        return models
            .GroupBy(model => model.DonorId)
            .All(group => group.Count() == group.DistinctBy(model => model.SpecimenId).Count());
    }
}
