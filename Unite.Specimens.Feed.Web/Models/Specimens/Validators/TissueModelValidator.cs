using FluentValidation;
using Unite.Data.Entities.Specimens.Tissues.Enums;

namespace Unite.Specimens.Feed.Web.Services.Specimens.Validators;

public class TissueModelValidator : AbstractValidator<TissueModel>
{
    public TissueModelValidator()
    {
        RuleFor(model => model.Type)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.TumorType)
            .Empty()
            .When(model => model.Type != TissueType.Tumor)
            .WithMessage("Tumor type can be set only for tumor tissues");

        RuleFor(model => model.Source)
            .MaximumLength(100)
            .WithMessage("Maximum length is 100");
    }
}
