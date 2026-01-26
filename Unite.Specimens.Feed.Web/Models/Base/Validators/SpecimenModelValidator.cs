using FluentValidation;
using Unite.Data.Entities.Specimens.Enums;

namespace Unite.Specimens.Feed.Web.Models.Base.Validators;

public abstract class SpecimenModelValidator<TModel> : AbstractValidator<TModel>
    where TModel : SpecimenModel
{
    private readonly TumorClassificationModelValidator _tumorClassificationModelValidator = new();
    private readonly MolecularDataModelValidator _molecularDataModelValidator = new();

    public SpecimenModelValidator()
    {
        RuleFor(model => model.Id)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.Id)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");

        RuleFor(model => model.DonorId)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.DonorId)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");

        RuleFor(model => model.ParentId)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");

        RuleFor(model => model.ParentType)
            .Empty()
            .When(model => string.IsNullOrEmpty(model.ParentId))
            .WithMessage("Should be empty if parent specimen Id is not set");

        RuleFor(model => model.ParentType)
            .NotEmpty()
            .When(model => !string.IsNullOrEmpty(model.ParentId))
            .WithMessage("Should not be empty if parent specimen Id is set");

        RuleFor(model => model.CreationDate)
            .Empty()
            .When(model => model.CreationDay.HasValue)
            .WithMessage("Either exact 'date' or relative 'day' should be set, not both");

        RuleFor(model => model.CreationDay)
            .Empty()
            .When(model => model.CreationDate.HasValue)
            .WithMessage("Either exact 'date' or relative 'day' should be set, not both");

        RuleFor(model => model.CreationDay)
            .GreaterThanOrEqualTo(1)
            .When(model => model.CreationDay.HasValue)
            .WithMessage("Should be greater than or equal to 1");


        RuleFor(model => model.TumorType)
            .Empty()
            .When(model => model.Category != Category.Tumor)
            .WithMessage("Tumor type can be set only when category is 'Tumor'");

        RuleFor(model => model.TumorGrade)
            .GreaterThan((byte)0)
            .WithMessage("Should be greater than 0");

        RuleFor(model => model.TumorGrade)
            .Empty()
            .When(model => model.Category != Category.Tumor)
            .WithMessage("Tumor grade can be set only when category is 'Tumor'");


        RuleFor(model => model.TumorClassification)
            .SetValidator(_tumorClassificationModelValidator)
            .When(model => model.TumorClassification != null);

        RuleFor(model => model.TumorClassification)
            .Empty()
            .When(model => model.Category != Category.Tumor)
            .WithMessage("Tumor classification can be set only when category is 'Tumor'");


        RuleFor(model => model.MolecularData)
            .SetValidator(_molecularDataModelValidator)
            .When(model => model.MolecularData != null);
    }
}
