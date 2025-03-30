using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Entities;
using Identity.Api.Contants;

namespace Identity.Api.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(name: IdentitySchemaConstants.Table.Users);
        builder.HasQueryFilter(p => p.IsActive);
    }
}


