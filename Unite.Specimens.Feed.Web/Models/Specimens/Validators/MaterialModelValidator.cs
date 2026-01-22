using FluentValidation;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Validators;

public class MaterialModelValidator : Base.Validators.SpecimenModelValidator<MaterialModel>
{
    public MaterialModelValidator() : base()
    {
        RuleFor(model => model.Source)
            .MaximumLength(100)
            .WithMessage("Maximum length is 100");
    }
}

public class MaterialModelsValidator : AbstractValidator<MaterialModel[]>
{
    private readonly MaterialModelValidator _validator = new();

    public MaterialModelsValidator()
    {
        RuleForEach(model => model)
            .SetValidator(_validator);
    }
}
