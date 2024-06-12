using FluentValidation;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Validators;

public class XenograftModelValidator : Base.Validators.SpecimenModelValidator<XenograftModel>
{
    public XenograftModelValidator() : base()
    {
        RuleFor(model => model.GroupSize)
            .GreaterThan(0)
            .WithMessage("Should be greater than 0");

        RuleFor(model => model.ImplantedCellsNumber)
            .GreaterThan(0)
            .WithMessage("Should be greater than 0");

        RuleFor(model => model.SurvivalDays)
            .Must(BeNumberOrRange)
            .WithMessage("Should be positive integer number '1234567890' or range '1234567890-1234567890'");
    }

    private bool BeNumberOrRange(string duration)
    {
        if (string.IsNullOrWhiteSpace(duration))
        {
            return true;
        }

        if (duration.Contains('-'))
        {
            var blocks = duration.Split('-');

            if (blocks.Length != 2)
            {
                return false;
            }
            else
            {
                var startIsNumber = int.TryParse(blocks[0].Trim(), out var start);
                var endIsNumber = int.TryParse(blocks[1].Trim(), out var end);

                return startIsNumber
                    && start >= 0
                    && endIsNumber
                    && end >= 0;
            }
        }
        else
        {
            var isNumber = int.TryParse(duration.Trim(), out var number);

            return isNumber && number >= 0;
        }
    }
}

public class XenograftModelsValidator : AbstractValidator<XenograftModel[]>
{
    private readonly XenograftModelValidator _validator = new();

    public XenograftModelsValidator()
    {
        RuleForEach(models => models)
            .SetValidator(_validator);
    }
}
