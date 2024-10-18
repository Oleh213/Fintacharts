using System.Text.Json.Serialization;

namespace Fintacharts.Abstractions.Models;

public class AssetResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("kind")]
    public string AssetKind { get; set; }
    
    [JsonPropertyName("mappings")]
    public Dictionary<string, object> Providers { get; set; }
}