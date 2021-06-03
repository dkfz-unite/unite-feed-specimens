using FluentValidation;
using Unite.Data.Entities.Specimens.Tissues.Enums;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Validators
{
    public class TissueModelValidator : AbstractValidator<TissueModel>
    {
        public TissueModelValidator()
        {
            RuleFor(model => model.Type)
                .NotEmpty()
                .WithMessage("Should not be empty");

            RuleFor(model => model.TumourType)
                .Empty()
                .When(model => model.Type != TissueType.Tumour)
                .WithMessage("Tumour type can be set only for tumour tissues");

            RuleFor(model => model.Source)
                .MaximumLength(100)
                .WithMessage("Maximum length is 100");
        }
    }
}
