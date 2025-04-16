using Identity.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Shared.Presentation;
using static Shared.Dtos.Identity.SeedDtos;

namespace MyBlog.Identity.Api.Controllers;

public class SeedController : CustomControllerBase
{
    private readonly ISeedService _seedService;

    public SeedController(ISeedService seedService)
    {
        _seedService = seedService;
    }

    [HttpPost]
    public async Task<IActionResult> Seed([FromBody] SeedDataRequest request)
    {
        await _seedService.SeedDataAsync(request);
        return GetResponse();
    }
}
