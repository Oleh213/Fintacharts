using Fintacharts.Abstractions.Enums;

namespace Fintacharts.Abstractions.Models;

public class AssetModel
{
    public Guid Id { get; set; }
    public string Symbol { get; set; }
    public string Description { get; set; }
    public AssetKind AssetKind { get; set; }
    public PriceModel Price { get; set; }
    public List<PriceModel> PriceHistory { get; set; }
    public List<ProviderModel> Providers { get; set; }
}