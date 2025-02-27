using FluentValidation;
using Unite.Data.Entities.Specimens.Materials.Enums;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Validators;

public class MaterialModelValidator : Base.Validators.SpecimenModelValidator<MaterialModel>
{
    public MaterialModelValidator() : base()
    {
        RuleFor(model => model.TumorType)
            .Empty()
            .When(model => model.Type != MaterialType.Tumor)
            .WithMessage("Tumor type can be set only 'Type' is 'Tumor'");

        RuleFor(model => model.TumorGrade)
            .GreaterThan((byte)0)
            .WithMessage("Should be greater than 0");


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
