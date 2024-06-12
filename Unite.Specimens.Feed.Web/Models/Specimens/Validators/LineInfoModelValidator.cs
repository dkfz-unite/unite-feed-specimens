using FluentValidation;
using Unite.Specimens.Feed.Web.Models.Base.Validators.Extensions;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Validators;

public class LineInfoModelValidator : AbstractValidator<LineInfoModel>
{
    public LineInfoModelValidator()
    {
        RuleFor(model => model)
            .Must(model => model.IsNotEmpty())
            .WithMessage("At least one field has to be set");
    }
}
