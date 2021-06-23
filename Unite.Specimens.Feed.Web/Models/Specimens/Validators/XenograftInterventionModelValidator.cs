using FluentValidation;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Validators
{
    public class XenograftInterventionModelValidator : AbstractValidator<XenograftInterventionModel>
    {
        public XenograftInterventionModelValidator()
        {
            RuleFor(model => model.Type)
                .NotEmpty()
                .WithMessage("Should not be empty");

            RuleFor(model => model.Type)
                .MaximumLength(100)
                .WithMessage("Maximum length is 100");


            RuleFor(model => model.StartDay)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Should be greater than or equal to 0");

            RuleFor(model => model.DurationDays)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Should be greater than or equal to 0");
        }
    }
}
