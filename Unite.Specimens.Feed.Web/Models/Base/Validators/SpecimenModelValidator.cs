using FluentValidation;

namespace Unite.Specimens.Feed.Web.Models.Base.Validators;

public abstract class SpecimenModelValidator<TModel> : AbstractValidator<TModel>
    where TModel : SpecimenModel
{
    public SpecimenModelValidator()
    {
        RuleFor(model => model.Id)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.Id)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");

        RuleFor(model => model.DonorId)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.DonorId)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");

        RuleFor(model => model.ParentId)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");

        RuleFor(model => model.ParentType)
            .Empty()
            .When(model => string.IsNullOrEmpty(model.ParentId))
            .WithMessage("Should be empty if parent specimen Id is not set");

        RuleFor(model => model.ParentType)
            .NotEmpty()
            .When(model => !string.IsNullOrEmpty(model.ParentId))
            .WithMessage("Should not be empty if parent specimen Id is set");

        RuleFor(model => model.CreationDate)
            .Empty()
            .When(model => model.CreationDay.HasValue)
            .WithMessage("Either exact 'date' or relative 'day' should be set, not both");

        RuleFor(model => model.CreationDay)
            .Empty()
            .When(model => model.CreationDate.HasValue)
            .WithMessage("Either exact 'date' or relative 'day' should be set, not both");

        RuleFor(model => model.CreationDay)
            .GreaterThanOrEqualTo(1)
            .When(model => model.CreationDay.HasValue)
            .WithMessage("Should be greater than or equal to 1");
    }
}
