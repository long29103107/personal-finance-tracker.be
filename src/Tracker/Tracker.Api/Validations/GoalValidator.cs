using FluentValidation;
using Tracker.Api.Entities;

namespace Tracker.Api.Validations;

public class GoalValidator : AbstractValidator<Goal>
{
    public GoalValidator()
    {
        RuleFor(p => p.UserId).NotEmpty();
    }
}