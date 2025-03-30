using Identity.Api.Dtos.User;
using Identity.Api.Entities;

namespace Identity.Api.Services.Abstractions;

public interface IUserService
{
    public Task<User> CreateOrFindUserAsync(CreateOrFindRequest request);
}
