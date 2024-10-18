using Fintacharts.Abstractions.Models;
using Fintacharts.Business.Commands.Asset;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fintacharts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("assets")]
    public async Task<IEnumerable<AssetModel>> GetAssets(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetAssetsCommand(), cancellationToken);
    }
    
    [HttpGet("assets/id={id}/provider={provider}")]
    public async Task<AssetModel> GetAssetById(Guid id, string provider,CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetAssetByIdCommand(id, provider), cancellationToken);
    }
}