using Fintacharts.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fintacharts.Abstractions.Database;

public interface IFintachartsDbContext : IDisposable
{
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Provider> Providers { get; set; }
}