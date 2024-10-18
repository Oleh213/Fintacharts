using Fintacharts.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fintacharts.DataService;

public static class DataServiceCollectionExtensions
{
    public static void AddDataServiceInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IFintachartsService, FintachartsService>();
        services.AddScoped<IFintachartsWebSocketService, FintachartsWebSocketService>();
    }
}