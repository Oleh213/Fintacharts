namespace Fintacharts.Abstractions.Models;

public class FintachartsApiEndpoints
{
    public const string GetAnAccessToken = "/identity/realms/fintatech/protocol/openid-connect/token";
    public const string GetAssets = "/api/instruments/v1/instruments";
    public const string GetProviders = "/api/instruments/v1/providers";
    public const string GetPriceOfAsset = "/api/bars/v1/bars/count-back";
    public const string WSPath = "/api/streaming/ws/v1/realtime/";
}