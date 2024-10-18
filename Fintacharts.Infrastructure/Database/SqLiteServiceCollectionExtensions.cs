using Fintacharts.Abstractions.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fintacharts.Infrastructure.Database;

public static class SqLiteServiceCollectionExtensions
{
    public static void AddSqLiteInfrastructure(this IServiceCollection services)
    {
        
        services.AddDbContext<FintachartsDbContext>(options =>
        {
            options.UseSqlite("Data Source=fintacharts.db");
        });
        
        services.AddScoped<IFintachartsDbContext>(provider => provider.GetService<FintachartsDbContext>());

    }
}