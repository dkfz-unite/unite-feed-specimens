using FluentValidation;
using Unite.Specimens.Feed.Web.Models.Base;
using Unite.Specimens.Feed.Web.Models.Base.Validators;

namespace Unite.Specimens.Feed.Web.Models.Validators;

public class SpecimenDataModelValidator : AbstractValidator<SpecimenDataModel>
{
    private readonly IValidator<TissueModel> _tissueModelValidator;
    private readonly IValidator<CellLineModel> _cellLineModelValidator;
    private readonly IValidator<OrganoidModel> _organoidModelValidator;
    private readonly IValidator<XenograftModel> _xenograftModelValidator;
    private readonly IValidator<MolecularDataModel> _molecularDataModelValidator;
    private readonly IValidator<DrugScreeningModel> _drugScreeningModelValidator;


    public SpecimenDataModelValidator()
    {
        _tissueModelValidator = new TissueModelValidator();
        _cellLineModelValidator = new CellLineModelValidator();
        _organoidModelValidator = new OrganoidModelValidator();
        _xenograftModelValidator = new XenograftModelValidator();
        _molecularDataModelValidator = new MolecularDataModelValidator();
        _drugScreeningModelValidator = new DrugScreeningModelValidator();


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
            .WithMessage("Either 'CreationDate' or 'CreationDay' can be set, not both");


        RuleFor(model => model.CreationDay)
            .Empty()
            .When(model => model.CreationDate.HasValue)
            .WithMessage("Either 'CreationDate' or 'CreationDay' can be set, not both");


        RuleFor(model => model)
            .Must(HaveModelSet)
            .WithMessage("Specific specimen data (Tissue, CellLine or Xenograft) has to be set");


        RuleFor(model => model.Tissue)
            .SetValidator(_tissueModelValidator);

        RuleFor(model => model.CellLine)
            .SetValidator(_cellLineModelValidator);

        RuleFor(model => model.Organoid)
            .SetValidator(_organoidModelValidator);

        RuleFor(model => model.Xenograft)
            .SetValidator(_xenograftModelValidator);

        RuleFor(model => model.MolecularData)
            .SetValidator(_molecularDataModelValidator);

        RuleForEach(model => model.DrugsScreeningData)
            .SetValidator(_drugScreeningModelValidator);
    }


    private bool HaveModelSet(SpecimenDataModel model)
    {
        return model.Tissue != null
            || model.CellLine != null
            || model.Organoid != null
            || model.Xenograft != null;
    }
}


public class SpecimenModelsValidator : AbstractValidator<SpecimenDataModel[]>
{
    private readonly IValidator<SpecimenDataModel> _specimenModelValidator;


    public SpecimenModelsValidator()
    {
        _specimenModelValidator = new SpecimenDataModelValidator();


        RuleForEach(model => model)
            .SetValidator(_specimenModelValidator);
    }
}
