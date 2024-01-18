using FluentValidation;
using Unite.Specimens.Feed.Web.Models.Base;
using Unite.Specimens.Feed.Web.Models.Base.Validators;

namespace Unite.Specimens.Feed.Web.Models.Validators;

public class InterventionsDataModelValidator : AbstractValidator<InterventionsDataModel>
{
    private readonly IValidator<InterventionModel> _interventionModelValidator = new InterventionModelValidator();

    public InterventionsDataModelValidator()
    {
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

        RuleFor(model => model.Data)
            .Must(data => data.Any())
            .WithMessage("Should not be empty");


        RuleForEach(model => model.Data)
            .SetValidator(_interventionModelValidator);
    }
}

public class InterventionsDataModelsValidator : AbstractValidator<IEnumerable<InterventionsDataModel>>
{
    private readonly InterventionsDataModelValidator _validator = new();

    public InterventionsDataModelsValidator()
    {
        RuleFor(model => model)
            .Must(model => model.Any())
            .WithMessage("Should not be empty");

        RuleForEach(model => model)
            .SetValidator(_validator);
    }
}
