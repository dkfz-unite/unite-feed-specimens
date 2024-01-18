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
    private readonly IValidator<InterventionDataFlatModel> _validator = new InterventionDataFlatModelValidator();

    public InterventionDataFlatModelsValidator()
    {
        RuleFor(model => model)
            .Must(model => model.Any())
            .WithMessage("Should not be empty");

        RuleForEach(model => model)
            .SetValidator(_validator);
    }
}
