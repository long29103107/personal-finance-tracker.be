using Identity.Api.Contants;
using Identity.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api;

public class CustomIdentityDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> options) : base(options)
    {

    }

    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<Operation> Operations { get; set; }
    public virtual DbSet<AccessRule> AccessRules { get; set; }
    public virtual DbSet<Scope> Scopes { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(IdentityApiReference.Assembly);

        builder.Entity<IdentityRoleClaim<int>>().ToTable(name: IdentitySchemaConstants.Table.RoleClaims);
        builder.Entity<IdentityUserClaim<int>>().ToTable(name: IdentitySchemaConstants.Table.UserClaims);
        builder.Entity<IdentityUserLogin<int>>().ToTable(name: IdentitySchemaConstants.Table.UserLogins);
        builder.Entity<IdentityUserToken<int>>().ToTable(name: IdentitySchemaConstants.Table.UserTokens);
    }
}
