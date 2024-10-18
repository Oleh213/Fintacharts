using System.Text.Json.Serialization;

namespace Fintacharts.Abstractions.Models;

public class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
}