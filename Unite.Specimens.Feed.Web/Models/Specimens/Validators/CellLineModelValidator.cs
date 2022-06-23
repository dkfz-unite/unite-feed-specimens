using FluentValidation;

namespace Unite.Specimens.Feed.Web.Services.Specimens.Validators;

public class CellLineModelValidator : AbstractValidator<CellLineModel>
{
    private readonly IValidator<CellLineInfoModel> _cellLineInfoModelValidator;


    public CellLineModelValidator() : base()
    {
        _cellLineInfoModelValidator = new CellLineInfoModelValidator();

        RuleFor(model => model.Species)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.Type)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.CultureType)
            .NotEmpty()
            .WithMessage("Should not be empty");


        RuleFor(model => model.Info)
            .SetValidator(_cellLineInfoModelValidator);
    }
}
