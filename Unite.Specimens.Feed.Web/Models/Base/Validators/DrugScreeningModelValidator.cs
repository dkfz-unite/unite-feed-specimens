using FluentValidation;

namespace Unite.Specimens.Feed.Web.Models.Base.Validators;

public class DrugScreeningModelValidator : AbstractValidator<DrugScreeningModel>
{
    public DrugScreeningModelValidator()
    {
        RuleFor(model => model.Drug)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.Drug)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");


        RuleFor(model => model.Dss)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.Dss)
            .Must(value => value >= 0 && value <= 100)
            .WithMessage("Should be in range [0, 100]");


        RuleFor(model => model.DssSelective)
            .Must(value => value >= -100 && value <= 100)
            .WithMessage("Should be in range [-100, 100]");

        RuleFor(model => model.Gof)
            .Must(value => value >= 0 && value <= 1)
            .WithMessage("Should be in range [0.0, 1.0]");

        RuleFor(model => model.MinConcentration)
            .Must(value => value > 0)
            .WithMessage("Should be greater than 0");

        RuleFor(model => model.MaxConcentration)
            .Must(value => value > 0)
            .WithMessage("Should be greater than 0");

        RuleFor(model => model.AbsIC25)
            .Must(value => value > 0)
            .WithMessage("Should be greater than 0");

        RuleFor(model => model.AbsIC50)
            .Must(value => value > 0)
            .WithMessage("Should be greater than 0");

        RuleFor(model => model.AbsIC75)
            .Must(value => value > 0)
            .WithMessage("Should be greater than 0");


        RuleForEach(model => model.Concentration)
            .NotEmpty()
            .WithMessage("Should not contain empty values");

        RuleForEach(model => model.Concentration)
            .Must(value => value >= 0)
            .WithMessage("Should be greater than 0");


        RuleForEach(model => model.Inhibition)
            .NotEmpty()
            .WithMessage("Should not contain empty values");

        RuleForEach(model => model.Inhibition)
            .Must(value => value >= -150 && value <= 150)
            .WithMessage("Should be in range [-150, 150]");


        RuleForEach(model => model.ConcentrationLine)
            .NotEmpty()
            .WithMessage("Should not contain empty values");

        RuleForEach(model => model.ConcentrationLine)
            .Must(value => value >= 0)
            .WithMessage("Should be greater than 0");


        RuleForEach(model => model.InhibitionLine)
           .NotEmpty()
           .WithMessage("Should not contain empty values");

        RuleForEach(model => model.InhibitionLine)
            .Must(value => value >= -150 && value <= 150)
            .WithMessage("Should be in range [-150, 150]");
    }
}
