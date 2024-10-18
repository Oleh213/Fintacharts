using System.Text.Json.Serialization;

namespace Fintacharts.Abstractions.Models;

public class CountBackResponse
{
    [JsonPropertyName("data")]
    public List<PriceResponse> Prices { get; set; }
}