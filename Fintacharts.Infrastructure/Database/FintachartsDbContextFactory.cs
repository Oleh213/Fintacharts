using Fintacharts.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class FintachartsDbContextFactory : IDesignTimeDbContextFactory<FintachartsDbContext>
{
    public FintachartsDbContext CreateDbContext(string[] args = null)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FintachartsDbContext>();
        optionsBuilder.UseSqlite("Data Source=fintacharts.db");

        return new FintachartsDbContext(optionsBuilder.Options);
    }
}