using Fintacharts.Abstractions.Services;
using Fintacharts.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Fintacharts.Infrastructure.Seeding;

public class Seeding
{
    private readonly IFintachartsService _fintachartsService;

    public Seeding(IFintachartsService fintachartsService)
    {
        _fintachartsService = fintachartsService;
    }

    public async Task Seed(FintachartsDbContext dbContext)
    {
        if (!dbContext.Providers.Any())
        {
            var seedingData = new SeedingData(_fintachartsService);
            await seedingData.LoadData();
            
            foreach (var provider in seedingData.Providers)
            {
                dbContext.Providers.Add(provider);
            }
            
            foreach (var asset in seedingData.Assets)
            {
                dbContext.Assets.Add(asset);
            }

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                Console.WriteLine(dbUpdateException);
            }
        }
    }
}