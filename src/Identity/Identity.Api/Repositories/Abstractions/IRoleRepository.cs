
using Identity.Api.Entities;
using Shared.Repository.Abstractions;

namespace Identity.Api.Repositories.Abstractions;

public interface IRoleRepository : IRepositoryBase<Role, CustomIdentityDbContext>
{ }