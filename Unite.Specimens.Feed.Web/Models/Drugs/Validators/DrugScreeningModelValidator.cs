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


        RuleFor(model => model.DoseMin)
            .GreaterThanOrEqualTo(0)
            .When(model => model.DoseMin != null)
            .WithMessage("Should be greater than 0");

        RuleFor(model => model.DoseMin)
            .LessThan(model => model.DoseMax)
            .When(model => model.DoseMin != null && model.DoseMax != null)
            .WithMessage("Should be less than 'DoseMax'");


        RuleFor(model => model.DoseMax)
            .GreaterThanOrEqualTo(0)
            .When(model => model.DoseMax != null)
            .WithMessage("Should be greater than 0");

        RuleFor(model => model.DoseMax)
            .GreaterThan(model => model.DoseMin)
            .When(model => model.DoseMax != null && model.DoseMin != null)
            .WithMessage("Should be greater than 'DoseMin'");


        RuleFor(model => model.Dose25)
            .GreaterThanOrEqualTo(0)
            .When(model => model.Dose25 != null)
            .WithMessage("Should be greater than or euqal to 0");

        RuleFor(model => model.Dose25)
            .LessThanOrEqualTo(model => model.Dose50)
            .When(model => model.Dose25 != null && model.Dose50 != null)
            .WithMessage("Should be less than or equal to 'Dose50'");

        RuleFor(model => model.Dose25)
            .GreaterThanOrEqualTo(model => model.DoseMin)
            .LessThanOrEqualTo(model => model.DoseMax)
            .When(model => model.Dose25 != null && model.DoseMin != null && model.DoseMax != null)
            .WithMessage("Should be in range ['DoseMax', 'DoseMax']");


        RuleFor(model => model.Dose50)
            .GreaterThanOrEqualTo(0)
            .When(model => model.Dose50 != null)
            .WithMessage("Should be greater than or euqal to 0");

        RuleFor(model => model.Dose50)
            .LessThanOrEqualTo(model => model.Dose75)
            .When(model => model.Dose50 != null && model.Dose75 != null)
            .WithMessage("Should be less than or equal to Dose75");

        RuleFor(model => model.Dose50)
            .GreaterThanOrEqualTo(model => model.DoseMin)
            .LessThanOrEqualTo(model => model.DoseMax)
            .When(model => model.Dose50 != null && model.DoseMin != null && model.DoseMax != null)
            .WithMessage("Should be in range ['DoseMin', 'DoseMax']");


        RuleFor(model => model.Dose75)
            .GreaterThanOrEqualTo(0)
            .When(model => model.Dose75 != null)
            .WithMessage("Should be greater than or euqal to 0");
        
        RuleFor(model => model.Dose75)
            .GreaterThanOrEqualTo(model => model.DoseMin)
            .LessThanOrEqualTo(model => model.DoseMax)
            .When(model => model.Dose75 != null && model.DoseMin != null && model.DoseMax != null)
            .WithMessage("Should be in range ['DoseMin', 'DoseMax']");


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
