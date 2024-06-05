using FluentValidation;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Validators;

public class LineModelValidator : Base.Validators.SpecimenModelValidator<LineModel>
{
    private readonly LineInfoModelValidator _lineInfoModelValidator = new();

    public LineModelValidator() : base()
    {
        RuleFor(model => model.Info)
            .SetValidator(_lineInfoModelValidator);
    }
}

public class LineModelsValidator : AbstractValidator<LineModel[]>
{
    private readonly LineModelValidator _validator = new();

    public LineModelsValidator()
    {
        RuleForEach(model => model)
            .SetValidator(_validator);
    }
}
