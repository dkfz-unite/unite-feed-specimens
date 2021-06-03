using FluentValidation;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Validators
{
    public class MolecularDataModelValidator : AbstractValidator<MolecularDataModel>
    {
        public MolecularDataModelValidator()
        {
            RuleFor(model => model)
                .Must(HaveAnythingSet)
                .WithMessage("At least one field has to be set");
        }


        private bool HaveAnythingSet(MolecularDataModel model)
        {
            return model.GeneExpressionSubtype != null
                || model.IdhStatus != null
                || model.IdhMutation != null
                || model.MethylationStatus != null
                || model.MethylationType != null
                || model.GcimpMethylation != null;
        }
    }
}
