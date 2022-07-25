using FluentValidation;
using Unite.Data.Entities.Specimens.Enums;

namespace Unite.Specimens.Feed.Web.Models.Base.Validators;

public class MolecularDataModelValidator : AbstractValidator<MolecularDataModel>
{
    public MolecularDataModelValidator()
    {
        RuleFor(model => model)
            .Must(HaveAnythingSet)
            .WithMessage("At least one field has to be set");

        RuleFor(model => model.IdhMutation)
            .Empty()
            .When(model => model.IdhStatus != IdhStatus.Mutant)
            .WithMessage("IDH mutation can be set only if IDH status is 'Mutant'");

        RuleFor(model => model.GeneExpressionSubtype)
            .Empty()
            .When(model => model.IdhStatus != IdhStatus.WildType)
            .WithMessage("Gene expression subtype can be set only if IDH status is 'Wild Type'");

        RuleFor(model => model.MethylationSubtype)
            .Empty()
            .When(model => model.IdhStatus != IdhStatus.WildType)
            .WithMessage("Methylation subtype can be set only if IDH status is 'Wild Type'");
    }


    private bool HaveAnythingSet(MolecularDataModel model)
    {
        return model.MgmtStatus != null
            || model.IdhStatus != null
            || model.IdhMutation != null
            || model.GeneExpressionSubtype != null
            || model.MethylationSubtype != null
            || model.GcimpMethylation != null;
    }
}
