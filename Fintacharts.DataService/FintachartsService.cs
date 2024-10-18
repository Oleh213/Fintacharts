using System.Net.Http.Headers;
using System.Text.Json;
using Fintacharts.Abstractions.AppSettings;
using Fintacharts.Abstractions.Models;
using Fintacharts.Abstractions.Services;

namespace Fintacharts.DataService;

public class FintachartsService : IFintachartsService
{
    private readonly AppSettings _appSettings;

    public FintachartsService(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public async Task<List<AssetResponse>> GetAllAssetsAsync()
    {
        var client = new HttpClient();
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAnAccessTokenAsync());
        
        var response = await client.GetAsync($"{_appSettings.Fintacharts.ApiUrl}{FintachartsApiEndpoints.GetAssets}?size=660");
        var content = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<InstrumentsResponse>(content).Assets;
    }

    public async Task<ProviderResponse> GetProvidersAsync()
    {
        var client = new HttpClient();
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAnAccessTokenAsync());
        
        var response = await client.GetAsync($"{_appSettings.Fintacharts.ApiUrl}{FintachartsApiEndpoints.GetProviders}");
        var content = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<ProviderResponse>(content);
    }

    public async Task<List<PriceResponse>> GetPriceHistoryByAssetIdAsync(Guid assetId, string provider)
    {
        var client = new HttpClient();
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAnAccessTokenAsync());
        
        var response = await client.GetAsync($"{_appSettings.Fintacharts.ApiUrl}{FintachartsApiEndpoints.GetPriceOfAsset}?instrumentId={assetId}&provider={provider}&interval=1&periodicity=minute&barsCount=10");
        var content = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<CountBackResponse>(content).Prices;
    }
    
    public async Task<string> GetAnAccessTokenAsync()
    {
        var client = new HttpClient();
        
        var values = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", _appSettings.Fintacharts.ClientId },
            { "username", _appSettings.Fintacharts.Username },
            { "password", _appSettings.Fintacharts.Password },
        };

        var content = new FormUrlEncodedContent(values);
        var response = await client.PostAsync($"{_appSettings.Fintacharts.ApiUrl}{FintachartsApiEndpoints.GetAnAccessToken}", content);
        var json = await response.Content.ReadAsStringAsync();
        
        var accessToken = JsonSerializer.Deserialize<TokenResponse>(json).AccessToken;
        
        return accessToken;
    }
}