using Fintacharts.Abstractions.Models;

namespace Fintacharts.Abstractions.Services;

public interface IFintachartsService
{
    Task<ProviderResponse> GetProvidersAsync();
    Task<List<AssetResponse>> GetAllAssetsAsync();
    Task<List<PriceResponse>> GetPriceHistoryByAssetIdAsync(Guid assetId, string provider);
    Task<string> GetAnAccessTokenAsync();
}