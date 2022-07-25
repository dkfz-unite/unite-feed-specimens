using FluentValidation;

namespace Unite.Specimens.Feed.Web.Models.Base.Validators;

public class DrugScreeningModelValidator : AbstractValidator<DrugScreeningModel>
{
    public DrugScreeningModelValidator()
    {
        RuleFor(model => model.Drug)
            .NotEmpty().WithMessage("Should not be empty")
            .MaximumLength(255).WithMessage("Maximum length is 255");

        RuleFor(model => model.MinConcentration)
            .Must(value => value > 0).WithMessage("Should be greater than 0");

        RuleFor(model => model.MaxConcentration)
            .Must(value => value > 0).WithMessage("Should be greater than 0");

        RuleFor(model => model.AbsIC25)
            .Must(value => value > 0).WithMessage("Should be greater than 0");

        RuleFor(model => model.AbsIC50)
            .Must(value => value > 0).WithMessage("Should be greater than 0");

        RuleFor(model => model.AbsIC75)
            .Must(value => value > 0).WithMessage("Should be greater than 0");

        RuleFor(model => model.Dss)
            .NotEmpty().WithMessage("Should not be empty");
    }
}
