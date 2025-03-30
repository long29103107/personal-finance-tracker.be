using Identity.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Api.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasIndex(op => new { op.ScopeId, op.OperationId }).IsUnique();

        builder.HasOne(p => p.Operation)
           .WithMany() 
           .HasForeignKey(p => p.OperationId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Scope)
            .WithMany()
            .HasForeignKey(p => p.ScopeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}