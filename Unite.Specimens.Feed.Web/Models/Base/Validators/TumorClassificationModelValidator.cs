using FluentValidation;
using Unite.Specimens.Feed.Web.Models.Base.Validators.Extensions;

namespace Unite.Specimens.Feed.Web.Models.Base.Validators;

public class TumorClassificationModelValidator : AbstractValidator<TumorClassificationModel>
{
    public TumorClassificationModelValidator()
    {
        RuleFor(model => model)
            .Must(model => model.IsNotEmpty())
            .WithMessage("At least one field has to be set");

        RuleFor(model => model.Superfamily)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");

        RuleFor(model => model.SuperfamilyScore)
            .InclusiveBetween(0, 1)
            .WithMessage("Score has to be between 0 and 1");

        RuleFor(model => model.Family)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");

        RuleFor(model => model.FamilyScore)
            .InclusiveBetween(0, 1)
            .WithMessage("Score has to be between 0 and 1");

        RuleFor(model => model.Class)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");

        RuleFor(model => model.ClassScore)
            .InclusiveBetween(0, 1)
            .WithMessage("Score has to be between 0 and 1");

        RuleFor(model => model.Subclass)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");

        RuleFor(model => model.SubclassScore)
            .InclusiveBetween(0, 1)
            .WithMessage("Score has to be between 0 and 1");
    }
}
