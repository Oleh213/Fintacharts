using Fintacharts.Abstractions.Database;
using Fintacharts.Abstractions.Entities;
using Fintacharts.Infrastructure.EntityTypeConfigs;
using Microsoft.EntityFrameworkCore;

namespace Fintacharts.Infrastructure.Database;

public class FintachartsDbContext : DbContext, IFintachartsDbContext
{
    public FintachartsDbContext(DbContextOptions<FintachartsDbContext> options) : base(options)
    { }
    
    public DbSet<Provider> Providers { get; set; } = default!;
    
    public DbSet<Asset> Assets { get; set; } = default!;
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AssetEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProviderEntityTypeConfiguration());
    }
}