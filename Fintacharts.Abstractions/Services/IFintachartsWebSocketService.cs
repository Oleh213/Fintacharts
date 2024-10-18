using Fintacharts.Abstractions.Models;

namespace Fintacharts.Abstractions.Services;

public interface IFintachartsWebSocketService
{
    Task<PriceModel> GetPriceByAssetIdAsync(Guid assetId);
}