using System.Text.Json.Serialization;

namespace Fintacharts.Abstractions.Models;

public class ProviderResponse
{
    [JsonPropertyName("data")]
    public List<string> Data { get; set; }
}