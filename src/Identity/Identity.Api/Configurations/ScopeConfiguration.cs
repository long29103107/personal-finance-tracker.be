using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Entities;

namespace Identity.Api.Configurations;

public class ScopeConfiguration : IEntityTypeConfiguration<Scope>
{
    public void Configure(EntityTypeBuilder<Scope> builder)
    {
        builder.HasIndex(c => c.Code)
            .IsUnique();
    }
}