using FluentValidation;
using Unite.Specimens.Feed.Web.Models.Base;
using Unite.Specimens.Feed.Web.Models.Base.Validators;

namespace Unite.Specimens.Feed.Web.Models.Validators;

public class SpecimenDataModelValidator : AbstractValidator<SpecimenDataModel>
{
    private readonly IValidator<MaterialModel> _materialModelValidator = new MaterialModelValidator();
    private readonly IValidator<LineModel> _lineModelValidator = new LineModelValidator();
    private readonly IValidator<OrganoidModel> _organoidModelValidator = new OrganoidModelValidator();
    private readonly IValidator<XenograftModel> _xenograftModelValidator = new XenograftModelValidator();
    private readonly IValidator<MolecularDataModel> _molecularDataModelValidator = new MolecularDataModelValidator();
    private readonly IValidator<InterventionModel> _interventionModelValidator = new InterventionModelValidator();
    private readonly IValidator<DrugScreeningModel> _drugScreeningModelValidator = new DrugScreeningModelValidator();

    public SpecimenDataModelValidator()
    {
        RuleFor(model => model.Id)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.Id)
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


        RuleFor(model => model.DonorId)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.DonorId)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");


        RuleFor(model => model.CreationDate)
            .Empty()
            .When(model => model.CreationDay.HasValue)
            .WithMessage("Either exact 'date' or relative 'day' can be set, not both");


        RuleFor(model => model.CreationDay)
            .Empty()
            .When(model => model.CreationDate.HasValue)
            .WithMessage("Either exact 'date' or relative 'day' can be set, not both");

        RuleFor(model => model.CreationDay)
            .GreaterThanOrEqualTo(1)
            .When(model => model.CreationDay.HasValue)
            .WithMessage("Should be greater than or equal to 1");


        RuleFor(model => model)
            .Must(HaveModelSet)
            .WithMessage("Specific specimen data (Material, Line, Organoid or Xenograft) has to be set");


        RuleFor(model => model.Material)
            .SetValidator(_materialModelValidator);

        RuleFor(model => model.Line)
            .SetValidator(_lineModelValidator);

        RuleFor(model => model.Organoid)
            .SetValidator(_organoidModelValidator);

        RuleFor(model => model.Xenograft)
            .SetValidator(_xenograftModelValidator);

        RuleFor(model => model.MolecularData)
            .SetValidator(_molecularDataModelValidator);

        RuleForEach(model => model.Interventions)
            .SetValidator(_interventionModelValidator);

        RuleForEach(model => model.DrugScreenings)
            .SetValidator(_drugScreeningModelValidator);
    }


    private bool HaveModelSet(SpecimenDataModel model)
    {
        return model.Material != null
            || model.Line != null
            || model.Organoid != null
            || model.Xenograft != null;
    }
}


public class SpecimenModelsValidator : AbstractValidator<SpecimenDataModel[]>
{
    private readonly IValidator<SpecimenDataModel> _specimenModelValidator = new SpecimenDataModelValidator();

    public SpecimenModelsValidator()
    {
        RuleFor(model => model)
            .Must(model => model.Any())
            .WithMessage("Should not be empty");

        RuleForEach(model => model)
            .SetValidator(_specimenModelValidator);
    }
}
