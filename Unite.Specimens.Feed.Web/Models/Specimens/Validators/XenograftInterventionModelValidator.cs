﻿using FluentValidation;

namespace Unite.Specimens.Feed.Web.Services.Specimens.Validators;

public class XenograftInterventionModelValidator : AbstractValidator<XenograftInterventionModel>
{
    public XenograftInterventionModelValidator()
    {
        RuleFor(model => model.Type)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.Type)
            .MaximumLength(100)
            .WithMessage("Maximum length is 100");


        RuleFor(model => model.StartDate)
            .Empty()
            .When(model => model.StartDay.HasValue)
            .WithMessage("Either 'StartDate' or 'StartDay' can be set, not both");

        RuleFor(model => model.StartDate)
            .LessThanOrEqualTo(model => model.EndDate)
            .When(model => model.EndDate.HasValue)
            .WithMessage("Should be less than or equal to 'EndDate'");


        RuleFor(model => model.StartDay)
            .Empty()
            .When(model => model.StartDate.HasValue)
            .WithMessage("Either 'StartDate' or 'StartDay' can be set, not both");

        RuleFor(model => model.StartDay)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Should be greater than or equal to 0");


        RuleFor(model => model.EndDate)
            .Empty()
            .When(model => model.DurationDays.HasValue)
            .WithMessage("Either 'EndDate' or 'DurationDays' can be set, not both");

        RuleFor(model => model.EndDate)
            .GreaterThanOrEqualTo(model => model.StartDate)
            .When(model => model.StartDate.HasValue)
            .WithMessage("Should be greater than or equal to 'StartDate'");


        RuleFor(model => model.DurationDays)
            .Empty()
            .When(model => model.EndDate.HasValue)
            .WithMessage("Either 'EndDate' or 'DurationDays' can be set, not both");

        RuleFor(model => model.DurationDays)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Should be greater than or equal to 0");
    }
}
