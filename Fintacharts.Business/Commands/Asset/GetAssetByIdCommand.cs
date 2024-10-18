using Fintacharts.Abstractions.Models;
using MediatR;

namespace Fintacharts.Business.Commands.Asset;

public class GetAssetByIdCommand: IRequest<AssetModel>
{
    public GetAssetByIdCommand(Guid id, string provider)
    {
        Id = id;
        Provider = provider;
    }

    public Guid Id { get; set; }
    public string Provider { get; set; }
}