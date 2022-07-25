using FluentValidation;

namespace Unite.Specimens.Feed.Web.Models.Base.Validators;

public class CellLineModelValidator : AbstractValidator<CellLineModel>
{
    private readonly IValidator<CellLineInfoModel> _cellLineInfoModelValidator;
    private readonly IValidator<DrugScreeningModel> _drugScreeningModelValidator;


    public CellLineModelValidator() : base()
    {
        _cellLineInfoModelValidator = new CellLineInfoModelValidator();
        _drugScreeningModelValidator = new DrugScreeningModelValidator();

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
