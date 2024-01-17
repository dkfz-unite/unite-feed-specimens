using FluentValidation;
using Unite.Specimens.Feed.Web.Models.Base;
using Unite.Specimens.Feed.Web.Models.Base.Validators;

namespace Unite.Specimens.Feed.Web.Models.Validators;

public class DrugScreeningsDataModelValidator : AbstractValidator<DrugScreeningsDataModel>
{
    private readonly IValidator<DrugScreeningModel> _drugScreeningModelValidator;


    public DrugScreeningsDataModelValidator()
    {
        _drugScreeningModelValidator = new DrugScreeningModelValidator();


        RuleFor(model => model.DonorId)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.DonorId)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");


        RuleFor(model => model.SpecimenId)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.SpecimenId)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");


        RuleFor(model => model.SpecimenType)
            .NotEmpty()
            .WithMessage("Should not be empty");


        RuleFor(model => model.Data)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleForEach(model => model.Data)
            .SetValidator(_drugScreeningModelValidator);
    }
}
