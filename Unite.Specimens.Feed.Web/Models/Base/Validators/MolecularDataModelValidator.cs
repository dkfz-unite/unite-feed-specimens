using FluentValidation;
using Unite.Specimens.Feed.Web.Models.Base.Validators.Extensions;

namespace Unite.Specimens.Feed.Web.Models.Base.Validators;

public class MolecularDataModelValidator : AbstractValidator<MolecularDataModel>
{
    public MolecularDataModelValidator()
    {
        RuleFor(model => model)
            .Must(model => model.IsNotEmpty())
            .WithMessage("At least one field has to be set");

        RuleFor(model => model.IdhMutation)
            .Empty()
            .When(model => model.IdhStatus != true)
            .WithMessage("IDH mutation can be set only if IDH status is 'Mutant'");

        RuleFor(model => model.TertMutation)
            .Empty()
            .When(model => model.TertStatus != true)
            .WithMessage("TERT mutation can be set only if TERT status is 'Mutant'");

        RuleFor(model => model.ExpressionSubtype)
            .Empty()
            .When(model => model.IdhStatus != false)
            .WithMessage("Gene expression subtype can be set only if IDH status is 'Wild Type'");

        RuleFor(model => model.MethylationSubtype)
            .Empty()
            .When(model => model.IdhStatus != false)
            .WithMessage("Methylation subtype can be set only if IDH status is 'Wild Type'");

        RuleForEach(model => model.GeneKnockouts)
            .NotEmpty()
            .WithMessage("Should not have empty values");

        RuleForEach(model => model.GeneKnockouts)
            .MaximumLength(100)
            .WithMessage("Maximum length is 100");
    }
}
