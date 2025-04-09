using Microsoft.AspNetCore.Mvc;
using Shared.Presentation;
using Tracker.Api.Dtos.Category;
using Tracker.Api.Services.Abstractions;

namespace Tracker.Api.Controllers;

public class CategoryController : CustomControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        return GetResponse(await _service.GetListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return GetResponse(await _service.GetByIdAsync(id));
    }

    [HttpGet("{id}/subcategories")]
    public async Task<IActionResult> GetSubCategories(int id)
    {
        return GetResponse(await _service.GetSubCategoriesAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CategoryCreateRequest request)
    {
       return GetResponse(await _service.CreateAsync(request));
    }
}