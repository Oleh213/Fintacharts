using System.Text.Json.Serialization;

namespace Fintacharts.Abstractions.Models;

public class InstrumentsResponse
{
    [JsonPropertyName("data")]
    public List<AssetResponse> Assets { get; set; }
}