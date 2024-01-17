using FluentValidation;
using Unite.Specimens.Feed.Web.Models.Base.Validators;

namespace Unite.Specimens.Feed.Web.Models.Validators;

public class DrugScreeningDataFlatModelValidator : AbstractValidator<DrugScreeningDataFlatModel>
{
    public DrugScreeningDataFlatModelValidator()
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

        RuleFor(model => model)
            .SetValidator(new DrugScreeningModelValidator());
    }
}

public class DrugScreeningDataFlatModelsValidator : AbstractValidator<IEnumerable<DrugScreeningDataFlatModel>>
{
    public DrugScreeningDataFlatModelsValidator()
    {
        RuleForEach(model => model)
            .SetValidator(new DrugScreeningDataFlatModelValidator());

        RuleFor(model => model)
            .Must(HaveAtLeastOneDrugScreening);

        RuleFor(model => model)
            .Must(HaveUniqueSpecimenGroups);
    }


    private static bool HaveAtLeastOneDrugScreening(IEnumerable<DrugScreeningDataFlatModel> models)
    {
        return models.Any();
    }

    private static bool HaveUniqueSpecimenGroups(IEnumerable<DrugScreeningDataFlatModel> models)
    {
        var groups = models.GroupBy(model => new { model.DonorId, model.SpecimenId });

        var hasDonors = groups.All(group => group.Key.DonorId != null);
        var hasSpecimens = groups.All(group => group.Key.SpecimenId != null);

        return hasDonors && hasSpecimens;
    }
}
