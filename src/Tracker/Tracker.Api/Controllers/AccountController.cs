using Microsoft.AspNetCore.Mvc;
using Tracker.Api.Entities;
using Tracker.Api.Services.Abstractions;

namespace Tracker.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _service;
    public AccountController(IAccountService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var acc = await _service.GetByIdAsync(id);
        return acc is null ? NotFound() : Ok(acc);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Account acc)
    {
        var created = await _service.CreateAsync(acc);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Account acc)
    {
        var updated = await _service.UpdateAsync(id, acc);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}