using System.Text.Json.Serialization;

namespace Fintacharts.Abstractions.Models;

public class PriceResponse
{
    [JsonPropertyName("t")]
    public DateTimeOffset Time { get; set; }
    
    [JsonPropertyName("o")]
    public decimal Price { get; set; }
}