using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Fintacharts.Abstractions.AppSettings;
using Fintacharts.Abstractions.Models;
using Fintacharts.Abstractions.Services;
using Microsoft.Extensions.Logging;

namespace Fintacharts.DataService;

public class FintachartsWebSocketService : IFintachartsWebSocketService
{
    private readonly ClientWebSocket _client;
    private readonly ILogger<FintachartsWebSocketService> _logger;
    private readonly AppSettings _appSettings;
    private readonly IFintachartsService _fintachartsService;

    public FintachartsWebSocketService(
        AppSettings appSettings, 
        IFintachartsService fintachartsService, 
        ILogger<FintachartsWebSocketService> logger)
    {
        _appSettings = appSettings;
        _fintachartsService = fintachartsService;
        _logger = logger;
        _client = new ClientWebSocket();
    }

    private async Task<PriceModel> StartAsync(Guid assetId)
    {
        try
        {
            await ConnectAsync();
            await SendSubscriptionMessageAsync(assetId);
            return await ListenForLastPriceAsync();
        }
        finally
        {
            await CloseWebSocketAsync(assetId);
        }
    }

    public async Task<PriceModel> GetPriceByAssetIdAsync(Guid assetId)
    {
        return await StartAsync(assetId);
    }

    private async Task ConnectAsync()
    {
        var webSocketUri = new Uri($"{_appSettings.Fintacharts.WSUrl}{FintachartsApiEndpoints.WSPath}?token={await _fintachartsService.GetAnAccessTokenAsync()}");
        
        try
        {
            await _client.ConnectAsync(webSocketUri, CancellationToken.None);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to connect: {ex.Message}");
            throw;
        }
    }

    private async Task SendSubscriptionMessageAsync(Guid assetId)
    {
        var subscriptionMessage = new
        {
            type = "l1-subscription",
            id = "1",
            instrumentId = assetId,
            provider = "simulation",
            subscribe = true,
            kinds = new[] {"last" }
        };

        string messageJson = JsonSerializer.Serialize(subscriptionMessage);
        var messageBytes = Encoding.UTF8.GetBytes(messageJson);

        await _client.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    private async Task<PriceModel> ListenForLastPriceAsync()
    {
        var buffer = new byte[1024];
        while (_client.State == WebSocketState.Open)
        {
            var receiveResult = await _client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (receiveResult.MessageType == WebSocketMessageType.Close)
            {
                break;
            }

            var receivedMessage = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
            using var document = JsonDocument.Parse(receivedMessage);
            var root = document.RootElement;

            if (root.TryGetProperty("type", out var typeElement) && typeElement.GetString() == "l1-update" &&
                root.TryGetProperty("last", out var lastElement))
            {
                var lastPrice = lastElement.GetProperty("price").GetDecimal();
                var timestamp = lastElement.GetProperty("timestamp").GetDateTimeOffset();
                
                return new PriceModel { Timestamp = timestamp, Price = lastPrice };
            }
        }
        return new PriceModel();
    }
    
    private async Task SendUnsubscribeMessageAsync(Guid assetId)
    {
        var unsubscribeMessage = new
        {
            type = "l1-unsubscription",
            id = "1",
            instrumentId = assetId,
            provider = "simulation",
            subscribe = false
        };

        string messageJson = JsonSerializer.Serialize(unsubscribeMessage);
        var messageBytes = Encoding.UTF8.GetBytes(messageJson);

        await _client.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    private async Task CloseWebSocketAsync(Guid assetId)
    {
        if (_client.State == WebSocketState.Open || _client.State == WebSocketState.CloseReceived)
        {
            await SendUnsubscribeMessageAsync(assetId);

            await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing connection", CancellationToken.None);
        }
        
        _client.Dispose();
    }
}
