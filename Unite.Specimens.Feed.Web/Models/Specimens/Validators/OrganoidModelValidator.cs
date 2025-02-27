using FluentValidation;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Validators;

public class OrganoidModelValidator : Base.Validators.SpecimenModelValidator<OrganoidModel>
{
    public OrganoidModelValidator() : base()
    {
        RuleFor(model => model.Medium)
            .MaximumLength(100)
            .WithMessage("Maximum length is 100");
        
        RuleFor(model => model.ImplantedCellsNumber)
            .GreaterThan(0)
            .WithMessage("Should be greater than 0");
    }
}

public class OrganoidModelsValidator : AbstractValidator<OrganoidModel[]>
{
    private readonly OrganoidModelValidator _validator = new();

    public OrganoidModelsValidator()
    {
        RuleForEach(model => model)
            .SetValidator(_validator);
    }
}
