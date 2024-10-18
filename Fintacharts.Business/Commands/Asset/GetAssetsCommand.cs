using Fintacharts.Abstractions.Models;
using MediatR;

namespace Fintacharts.Business.Commands.Asset;

public class GetAssetsCommand: IRequest<IEnumerable<AssetModel>>
{
    
}