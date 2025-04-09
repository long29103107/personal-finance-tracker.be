using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;

namespace Tracker.Api.Validations;

public class CategoryValidator : AbstractValidator<Category>
{
    private readonly ICategoryRepository _cateRepo;
    public CategoryValidator(ICategoryRepository cateRepo)
    {
        _cateRepo = cateRepo;
        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must be less than 100 characters.");

        RuleFor(p => p.Type)
            .NotEmpty().WithMessage("Type is required.")
            .Must(type => type == "Income" || type == "Expense")
            .WithMessage("Type must be either 'Income' or 'Expense'.");

        RuleFor(p => p.ParentCategoryId)
            .GreaterThan(0).When(p => p.ParentCategoryId.HasValue)
            .WithMessage("ParentCategoryId must be greater than 0.");

        RuleFor(p => p)
          .CustomAsync(CheckExistCategoryAsync);
    }

    private async Task CheckExistCategoryAsync(Category category, ValidationContext<Category> context, CancellationToken cancellationToken)
    {
        IQueryable<Category> existCategoryQuery = _cateRepo.FindByCondition(x => x.Email == category.Email
                    && x.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase));

        if (category.ParentCategoryId.GetValueOrDefault(0) != 0)
        {
            existCategoryQuery = existCategoryQuery.Where(x => x.ParentCategoryId == category.ParentCategoryId);
        }

        if (existCategoryQuery.FirstOrDefaultAsync(cancellationToken) is not null)
        {
            context.AddFailure("ExitCategory", "Category already exists in the app");
        }

        return;
    }
}