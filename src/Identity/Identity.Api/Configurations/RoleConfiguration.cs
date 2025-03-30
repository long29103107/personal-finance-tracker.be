using Identity.Api.Contants;
using Identity.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Api.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(name: IdentitySchemaConstants.Table.Roles);

        builder.HasIndex(c => c.Code)
           .IsUnique();
        builder.HasIndex(c => c.Weight)
            .IsUnique();
        builder.HasQueryFilter(p => p.IsActive);
    }
}

