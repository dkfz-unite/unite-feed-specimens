using FluentValidation;

namespace Unite.Specimens.Feed.Web.Models.Base.Validators;

public class LineModelValidator : AbstractValidator<LineModel>
{
    private readonly IValidator<LineInfoModel> _infoModelValidator;


    public LineModelValidator() : base()
    {
        _infoModelValidator = new LineInfoModelValidator();

        RuleFor(model => model.CellsSpecies)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.CellsType)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.CellsCultureType)
            .NotEmpty()
            .WithMessage("Should not be empty");


        RuleFor(model => model.Info)
            .SetValidator(_infoModelValidator);
    }
}
