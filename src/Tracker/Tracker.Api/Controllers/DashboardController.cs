using Microsoft.AspNetCore.Mvc;
using Shared.Presentation;
using Tracker.Api.Services.Abstractions;

namespace Tracker.Api.Controllers;

public class DashboardController(IDashboardService _service) : CustomControllerBase
{
    [HttpGet("total-balance")]
    public async Task<IActionResult> GetTotalBalanceAsync()
    {
        return GetResponse(await _service.GetTotalBalanceAsync());
    }
}