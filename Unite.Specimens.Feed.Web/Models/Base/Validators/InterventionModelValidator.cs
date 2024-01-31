﻿using FluentValidation;

namespace Unite.Specimens.Feed.Web.Models.Base.Validators;

public class InterventionModelValidator : AbstractValidator<InterventionModel>
{
    public InterventionModelValidator()
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
            .WithMessage("Either exact 'date' or relative 'days' can be set, not both");

        RuleFor(model => model.StartDate)
            .LessThanOrEqualTo(model => model.EndDate)
            .When(model => model.EndDate.HasValue)
            .WithMessage("Should be less than or equal to 'EndDate'");

        RuleFor(model => model.StartDay)
            .Empty()
            .When(model => model.StartDate.HasValue)
            .WithMessage("Either exact 'date' or relative 'day' can be set, not both");

        RuleFor(model => model.StartDay)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Should be greater than or equal to 1");

        RuleFor(model => model.EndDate)
            .Empty()
            .When(model => model.DurationDays.HasValue)
            .WithMessage("Either exact 'date' or relative 'day' can be set, not both");

        RuleFor(model => model.EndDate)
            .Empty()
            .When(model => model.StartDay.HasValue)
            .WithMessage("'EndDate' can not be set when 'StartDay' is set");

        RuleFor(model => model.EndDate)
            .GreaterThanOrEqualTo(model => model.StartDate)
            .When(model => model.StartDate.HasValue)
            .WithMessage("Should be greater than or equal to 'StartDate'");

        RuleFor(model => model.DurationDays)
            .Empty()
            .When(model => model.EndDate.HasValue)
            .WithMessage("Either exact 'date' or relative 'day' can be set, not both");

        RuleFor(model => model.DurationDays)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Should be greater than or equal to 1");
    }
}
