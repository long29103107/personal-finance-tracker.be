using Microsoft.AspNetCore.Mvc;
using Shared.Presentation;
using Tracker.Api.Services.Abstractions;
using static Shared.Dtos.Tracker.AccountDtos;

namespace Tracker.Api.Controllers;

public class AccountsController(IAccountService _service) : CustomControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return GetResponse(await _service.GetListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        return GetResponse(await _service.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(AccountCreateRequest request)
    {
        return GetResponse(await _service.CreateAsync(request));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, AccountUpdateRequest request)
    {
        return GetResponse(await _service.UpdateAsync(id, request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        return GetResponse(await _service.DeleteAsync(id));
    }

    [HttpGet("balances")]
    public async Task<IActionResult> GetAccountBalances(AccountSummaryRequest request)
    {
        var result = await _service.GetAccountBalancesAsync(request);
        return Ok(result);
    }

}