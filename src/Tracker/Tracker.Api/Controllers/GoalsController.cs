using Microsoft.AspNetCore.Mvc;
using Shared.Presentation;
using Tracker.Api.Services.Abstractions;
using static Shared.Dtos.Tracker.GoalDtos;

namespace Tracker.Api.Controllers;

public class GoalsController(IGoalService _service) : CustomControllerBase
{
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

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] GoalCreateRequest request)
    {
       return GetResponse(await _service.CreateAsync(request));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync([FromRoute]int id, [FromBody] GoalUpdateRequest request)
    {
        return GetResponse(await _service.UpdateAsync(id, request));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        return GetResponse(await _service.DeleteAsync(id));
    }
}