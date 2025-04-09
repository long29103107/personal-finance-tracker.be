using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;
using Tracker.Api.Repositories.Abstractions.Views;

namespace Tracker.Api.Validations;

public class CategoryValidator : AbstractValidator<Category>
{
    private readonly ICategoryRepository _cateRepo;
    private readonly IIdentityUsersVRepository _identityUsersRepo;
    public CategoryValidator(ICategoryRepository cateRepo, IIdentityUsersVRepository identityUsersRepo)
    {
        _cateRepo = cateRepo;
        _identityUsersRepo = identityUsersRepo;

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

        RuleFor(p => p).CustomAsync(CheckExistCategoryAsync);
        //RuleFor(p => p).CustomAsync(CheckEmailExistInIdentityAsync);
        RuleFor(p => p).CustomAsync(CheckExistingParentCategoryAsync);
    }

    private async Task CheckExistCategoryAsync(Category category, ValidationContext<Category> context, CancellationToken cancellationToken)
    {
        IQueryable<Category> existCategoryQuery = _cateRepo.FindByCondition(x => 
            x.UserId == category.UserId
            && x.Name.Equals(category.Name));

        if (category.ParentCategoryId.GetValueOrDefault(0) != 0)
        {
            existCategoryQuery = existCategoryQuery.Where(x => x.ParentCategoryId == category.ParentCategoryId);
        }
        else
        {
            existCategoryQuery = existCategoryQuery.Where(x => x.ParentCategoryId == null);
        }

        if ((await existCategoryQuery.FirstOrDefaultAsync(cancellationToken)) is not null)
        {
            context.AddFailure("ExitCategory", "Category already exists in the app");
            return;
        }
    }

    //TODO: Open rule
    private async Task CheckEmailExistInIdentityAsync(Category category, ValidationContext<Category> context, CancellationToken cancellationToken)
    {
        var existingEmail = await _identityUsersRepo.AnyAsync(x => x.Email.Equals(category.UserId));

        if (!existingEmail)
        {
            context.AddFailure("NotExistEmail", "The email must be in the app");
            return;
        }
    }

    private async Task CheckExistingParentCategoryAsync(Category category, ValidationContext<Category> context, CancellationToken cancellationToken)
    {
        if (category.ParentCategoryId == null)
            return;

        var existingCategory = await _cateRepo.AnyAsync(x => x.Id == category.ParentCategoryId);

        if (!existingCategory)
        {
            context.AddFailure("NotExistingParentCategory", "The parent category must be in the app");
            return;
        }
    }
}