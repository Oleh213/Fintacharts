using Fintacharts.Abstractions.Enums;

namespace Fintacharts.Abstractions.Entities;

public class Asset : Entity
{
    public string Symbol { get; set; }
    public string Description { get; set; }
    public AssetKind AssetKind { get; set; }
    public List<Provider> Providers { get; set; }
}