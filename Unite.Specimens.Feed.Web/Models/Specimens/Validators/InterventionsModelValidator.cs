using FluentValidation;
using Unite.Specimens.Feed.Web.Models.Base.Validators;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Validators;

public class InterventionsModelValidator : AbstractValidator<InterventionsModel>
{
    private readonly InterventionModelValidator _interventionModelValidator = new();

    public InterventionsModelValidator()
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

        RuleFor(model => model.Entries)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleForEach(model => model.Entries)
            .SetValidator(_interventionModelValidator);
    }
}

public class InterventionsModelsValidator : AbstractValidator<InterventionsModel[]>
{
    private readonly InterventionsModelValidator _validator = new();

    public InterventionsModelsValidator()
    {
        RuleForEach(model => model)
            .SetValidator(_validator);
    }
}
