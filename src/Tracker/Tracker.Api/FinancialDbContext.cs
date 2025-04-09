using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using Shared.Domain.Abstractions;
using System.Reflection;
using Tracker.Api.Entities;
using Tracker.Api.Entities.Views;

namespace Tracker.Api;

public class FinancialDbContext : DbContext
{
    //private readonly IScopedCache _scopedCache;

    public FinancialDbContext(DbContextOptions<FinancialDbContext> options)//, IScopedCache scopedCache) 
        : base(options)
    {
        //_scopedCache = scopedCache;
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Goal> Goals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureRelation(modelBuilder);
        ConfigurView(modelBuilder);

        //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //{
        //    if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
        //    {
        //        var method = typeof(FinancialDbContext).GetMethod(nameof(SetGlobalQuery), BindingFlags.NonPublic | BindingFlags.Instance)
        //            ?.MakeGenericMethod(entityType.ClrType);

        //        method?.Invoke(this, new object[] { modelBuilder });
        //    }
        //}
    }

    private void ConfigureRelation(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasMany(a => a.Transactions)
            .WithOne(t => t.Account)
            .HasForeignKey(t => t.AccountId);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Transactions)
            .WithOne(t => t.Category)
            .HasForeignKey(t => t.CategoryId);
    }

    private void ConfigurView(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUsersV>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("IdentityUsersV");
        });
    }

    //private void SetGlobalQuery<T>(ModelBuilder builder) where T : BaseEntity
    //{
    //    var userId = _scopedCache.ScopedContext.UserId;

    //    builder.Entity<T>().HasQueryFilter(e => e.UserId == userId);
    //}
}