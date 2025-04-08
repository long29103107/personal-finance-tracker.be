using FluentValidation;
using Tracker.Api.Entities;

namespace Tracker.Api.Validations;

public class AccountValidator : AbstractValidator<Account>
{
    public AccountValidator()
    {
        RuleFor(p => p.Email).NotEmpty();
    }
}