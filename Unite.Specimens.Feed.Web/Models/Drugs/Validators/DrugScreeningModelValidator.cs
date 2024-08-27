using FluentValidation;

namespace Unite.Specimens.Feed.Web.Models.Drugs.Validators;

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


        RuleFor(model => model.Gof)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(1)
            .When(model => model.Gof != null)
            .WithMessage("Should be in range [0, 1]");


        RuleFor(model => model.Dss)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(1)
            .When(model => model.Dss != null)
            .WithMessage("Should be in range [0, 1]");


        RuleFor(model => model.DssS)
            .GreaterThanOrEqualTo(-1)
            .LessThanOrEqualTo(1)
            .When(model => model.DssS != null)
            .WithMessage("Should be in range [-1, 1]");


        RuleFor(model => model.MinDose)
            .GreaterThanOrEqualTo(0)
            .When(model => model.MinDose != null)
            .WithMessage("Should be greater than 0");

        RuleFor(model => model.MinDose)
            .LessThan(model => model.MaxDose)
            .When(model => model.MinDose != null && model.MaxDose != null)
            .WithMessage("Should be less than 'MaxDose'");


        RuleFor(model => model.MaxDose)
            .GreaterThanOrEqualTo(0)
            .When(model => model.MaxDose != null)
            .WithMessage("Should be greater than 0");

        RuleFor(model => model.MaxDose)
            .GreaterThan(model => model.MinDose)
            .When(model => model.MaxDose != null && model.MinDose != null)
            .WithMessage("Should be greater than 'MinDose'");


        RuleFor(model => model.Dose25)
            .GreaterThanOrEqualTo(0)
            .When(model => model.Dose25 != null)
            .WithMessage("Should be greater than or euqal to 0");

        RuleFor(model => model.Dose25)
            .LessThanOrEqualTo(model => model.Dose50)
            .When(model => model.Dose25 != null && model.Dose50 != null)
            .WithMessage("Should be less than or equal to 'Dose50'");

        RuleFor(model => model.Dose25)
            .GreaterThanOrEqualTo(model => model.MinDose)
            .LessThanOrEqualTo(model => model.MaxDose)
            .When(model => model.Dose25 != null && model.MinDose != null && model.MaxDose != null)
            .WithMessage("Should be in range ['MinDose', 'MaxDose']");


        RuleFor(model => model.Dose50)
            .GreaterThanOrEqualTo(0)
            .When(model => model.Dose50 != null)
            .WithMessage("Should be greater than or euqal to 0");

        RuleFor(model => model.Dose50)
            .LessThanOrEqualTo(model => model.Dose75)
            .When(model => model.Dose50 != null && model.Dose75 != null)
            .WithMessage("Should be less than or equal to Dose75");

        RuleFor(model => model.Dose50)
            .GreaterThanOrEqualTo(model => model.MinDose)
            .LessThanOrEqualTo(model => model.MaxDose)
            .When(model => model.Dose50 != null && model.MinDose != null && model.MaxDose != null)
            .WithMessage("Should be in range ['MinDose', 'MaxDose']");


        RuleFor(model => model.Dose75)
            .GreaterThanOrEqualTo(0)
            .When(model => model.Dose75 != null)
            .WithMessage("Should be greater than or euqal to 0");
        
        RuleFor(model => model.Dose75)
            .GreaterThanOrEqualTo(model => model.MinDose)
            .LessThanOrEqualTo(model => model.MaxDose)
            .When(model => model.Dose75 != null && model.MinDose != null && model.MaxDose != null)
            .WithMessage("Should be in range ['MinDose', 'MaxDose']");


        RuleForEach(model => model.Doses)
            .GreaterThan(0)
            .When(model => model.Doses != null)
            .WithMessage("Should be greater than 0");


        RuleForEach(model => model.Responses)
            .GreaterThanOrEqualTo(0)
            .When(model => model.Responses != null)
            .WithMessage("Should be greater than or euqal to 0");
    }
}
