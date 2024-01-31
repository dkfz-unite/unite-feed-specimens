using FluentValidation;

namespace Unite.Specimens.Feed.Web.Models.Base.Validators;

public class OrganoidModelValidator : AbstractValidator<OrganoidModel>
{
    public OrganoidModelValidator()
    {
        RuleFor(model => model)
            .Must(HaveAnythingSet)
            .WithMessage("At least one field has to be set");

        RuleFor(model => model.ImplantedCellsNumber)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Should be greater than or equal to 0");
    }


    private bool HaveAnythingSet(OrganoidModel model)
    {
        return model.ImplantedCellsNumber != null
            || model.Tumorigenicity != null
            || !string.IsNullOrWhiteSpace(model.Medium);
    }
}
