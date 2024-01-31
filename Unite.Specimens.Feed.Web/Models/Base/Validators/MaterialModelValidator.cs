using FluentValidation;
using Unite.Data.Entities.Specimens.Materials.Enums;

namespace Unite.Specimens.Feed.Web.Models.Base.Validators;

public class MaterialModelValidator : AbstractValidator<MaterialModel>
{
    public MaterialModelValidator()
    {
        RuleFor(model => model.Type)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.TumorType)
            .Empty()
            .When(model => model.Type != MaterialType.Tumor)
            .WithMessage("Tumor type can be set only for tumor tissues");

        RuleFor(model => model.Source)
            .MaximumLength(100)
            .WithMessage("Maximum length is 100");
    }
}
