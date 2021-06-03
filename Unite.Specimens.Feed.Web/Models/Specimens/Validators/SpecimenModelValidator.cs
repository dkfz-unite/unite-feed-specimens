using System.Collections.Generic;
using FluentValidation;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Validators
{
    public class SpecimenModelValidator : AbstractValidator<SpecimenModel>
    {
        private IValidator<TissueModel> _tissueModelValidator;
        private IValidator<CellLineModel> _cellLineModelValidator;
        protected IValidator<MolecularDataModel> _molecularDataModelValidator;


        public SpecimenModelValidator()
        {
            _tissueModelValidator = new TissueModelValidator();
            _cellLineModelValidator = new CellLineModelValidator();
            _molecularDataModelValidator = new MolecularDataModelValidator();


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


            RuleFor(model => model)
                .Must(HaveModelSet)
                .WithMessage("Specific specimen data (Tissue, CellLine or Xenograft) has to be set");


            RuleFor(model => model.Tissue)
                .SetValidator(_tissueModelValidator);

            RuleFor(model => model.CellLine)
                .SetValidator(_cellLineModelValidator);

            RuleFor(model => model.MolecularData)
                .SetValidator(_molecularDataModelValidator);
        }


        private bool HaveModelSet(SpecimenModel model)
        {
            return model.Tissue != null
                || model.CellLine != null;
        }
    }


    public class SpecimenModelsValidator : AbstractValidator<IEnumerable<SpecimenModel>>
    {
        private readonly IValidator<SpecimenModel> _specimenModelValidator;


        public SpecimenModelsValidator()
        {
            _specimenModelValidator = new SpecimenModelValidator();


            RuleForEach(model => model)
                .SetValidator(_specimenModelValidator);
        }
    }
}
