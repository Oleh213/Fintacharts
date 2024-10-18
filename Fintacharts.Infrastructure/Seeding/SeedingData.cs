using Fintacharts.Abstractions.Entities;
using Fintacharts.Abstractions.Enums;
using Fintacharts.Abstractions.Models;
using Fintacharts.Abstractions.Services;

namespace Fintacharts.Infrastructure.Seeding;

public class SeedingData
{
    private readonly IFintachartsService _fintachartsService;

    public SeedingData(IFintachartsService fintachartsService)
    {
        _fintachartsService = fintachartsService;
    }
    
    public List<Asset> Assets { get; set; } = new();
    public List<Provider> Providers { get; set; } = new();

    public async Task LoadData()
    { 
        await LoadProviders();
        await LoadAssets();
    }

    private async Task LoadProviders()
    {
        var providersResponse = await _fintachartsService.GetProvidersAsync();

        foreach (var provider in providersResponse.Data)
        {
            Providers.Add(new Provider { Name = provider });
        }
    }

    private async Task LoadAssets()
    {
        var assetsResponse = await _fintachartsService.GetAllAssetsAsync(); 
        
        foreach (var asset in assetsResponse)
        {
            Assets.Add(new Asset
            {
                Id = asset.Id,
                Description = asset.Description,
                Symbol = asset.Symbol,
                Providers = GetProvidersFromAssetResponse(asset),
                AssetKind = GetAssetKindFromAssetResponse(asset) 
            });
        }
    }

    private AssetKind GetAssetKindFromAssetResponse(AssetResponse asset)
    {
        return Enum.TryParse<AssetKind>(asset.AssetKind, true, out var assetKind) ? assetKind : AssetKind.NotSet;
    }

    private List<Provider> GetProvidersFromAssetResponse(AssetResponse assetResponse)
    {
        var providers = new List<Provider>();
        
        assetResponse.Providers.Keys.ToList().ForEach(p=> providers.Add(Providers.Find(provider => provider.Name == p)));
        
        return providers;
    }
}