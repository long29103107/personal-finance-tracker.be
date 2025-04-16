using Identity.Api.Repositories.Abstractions;
using Shared.Service.Abstractions;
using static Shared.Dtos.Identity.SeedDtos;

namespace Identity.Api.Services.Abstractions;

public interface ISeedService : IBaseService<IRepositoryManager>
{
    Task SeedDataAsync(SeedDataRequest request);
}
