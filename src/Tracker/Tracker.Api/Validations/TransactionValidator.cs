using FluentValidation;
using Tracker.Api.Entities;

namespace Tracker.Api.Validations;

public class TransactionValidator : AbstractValidator<Transaction>
{
    public TransactionValidator()
    {
        RuleFor(p => p.UserId).NotEmpty();
    }
}