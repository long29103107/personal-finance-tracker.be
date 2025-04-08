using Microsoft.EntityFrameworkCore;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;

namespace Tracker.Api.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly FinancialDbContext _context;

    public CategoryRepository(FinancialDbContext context)
    {
        _context = context;
    }

    public async Task<Category> CreateAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories
            .Include(c => c.SubCategories)
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories
            .Include(c => c.SubCategories)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Category>> GetByParentIdAsync(int parentId)
    {
        return await _context.Categories
            .Where(c => c.ParentCategoryId == parentId)
            .ToListAsync();
    }
}