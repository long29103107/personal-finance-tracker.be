using Microsoft.AspNetCore.Mvc;
using Tracker.Api.Dtos;
using Tracker.Api.Services.Abstractions;

namespace Tracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDto dto)
    {
        var created = await _service.CreateCategoryAsync(dto);
        return CreatedAtAction(nameof(GetCategoryById), new { id = created.Id }, created);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _service.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _service.GetCategoryByIdAsync(id);
        if (category == null) return NotFound();
        return Ok(category);
    }

    [HttpGet("{id}/subcategories")]
    public async Task<IActionResult> GetSubCategories(int id)
    {
        var subCategories = await _service.GetSubCategoriesAsync(id);
        return Ok(subCategories);
    }
}