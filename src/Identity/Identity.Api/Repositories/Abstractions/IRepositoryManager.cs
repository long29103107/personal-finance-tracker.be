using Identity.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Repository.Abstractions;

namespace Identity.Api.Repositories.Abstractions;

public interface IRepositoryManager : IUnitOfWork<CustomIdentityDbContext>
{
    public IPermissionRepository Permission { get; }
    public IOperationRepository Operation { get; }
    public IAccessRuleRepository AccessRule { get; }
    public IScopeRepository Scope { get; }
    public IRoleRepository Role { get; }
    public IUserRepository User { get; }
    public IUserRoleRepository UserRole { get; }

    DbSet<Permission> Permissions { get; }
    DbSet<Operation> Operations { get; }
    DbSet<AccessRule> AccessRules { get; }
    DbSet<Role> Roles { get; }
    DbSet<Scope> Scopes { get; }
    DbSet<User> Users { get; }
    DbSet<UserRole> UserRoles { get; }
}
