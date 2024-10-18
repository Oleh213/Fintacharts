using Fintacharts.Business.Mappers.EntityToModel;
using Microsoft.Extensions.DependencyInjection;

namespace Fintacharts.Business;

public static class BusinessServiceCollectionExtensions
{
    public static void AddBusiness(this IServiceCollection services)
    {
        var profileAssembly = typeof(AssetToAssetModelMappingProfile).Assembly;
        services.AddAutoMapper(profileAssembly);
    }
}
