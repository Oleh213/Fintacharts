using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Fintacharts.Infrastructure.Mediatr;

public static class MediatRServiceCollectionExtensions
{
    public static void AddMediatRInfrastructure(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        }
    }
}
