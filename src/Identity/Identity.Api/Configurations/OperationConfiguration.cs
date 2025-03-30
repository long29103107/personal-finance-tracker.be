using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Entities;

namespace Identity.Api.Configurations;

public class OperationConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.HasIndex(c => c.Code)
            .IsUnique();      
    }
}