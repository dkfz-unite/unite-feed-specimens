using FluentValidation;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Validators
{
    public class CellLineInfoModelValidator : AbstractValidator<CellLineInfoModel>
    {
        public CellLineInfoModelValidator()
        {
            RuleFor(model => model)
                .Must(HaveAnythingSet)
                .WithMessage("At least one field has to be set");
        }


        private bool HaveAnythingSet(CellLineInfoModel model)
        {
            return !string.IsNullOrWhiteSpace(model.Name)
                || !string.IsNullOrWhiteSpace(model.DepositorName)
                || !string.IsNullOrWhiteSpace(model.DepositorEstablishment)
                || model.EstablishmentDate != null
                || !string.IsNullOrWhiteSpace(model.PubMedLink)
                || !string.IsNullOrWhiteSpace(model.AtccLink)
                || !string.IsNullOrWhiteSpace(model.ExPasyLink);
        }
    }
}
