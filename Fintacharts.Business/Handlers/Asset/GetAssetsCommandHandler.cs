using AutoMapper;
using Fintacharts.Abstractions.Models;
using Fintacharts.Business.Commands.Asset;
using Fintacharts.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fintacharts.Business.Handlers.Asset;

public class GetAssetsCommandHandler: IRequestHandler<GetAssetsCommand, IEnumerable<AssetModel>>
{
    private FintachartsDbContext _fintachartsDbContext { get; set; }
    private IMapper _mapper { get; set; }

    public GetAssetsCommandHandler(
        IMapper mapper, 
        FintachartsDbContext fintachartsDbContext)
    {
        _mapper = mapper;
        _fintachartsDbContext = fintachartsDbContext;
    }
    
    public async Task<IEnumerable<AssetModel>> Handle(GetAssetsCommand request, CancellationToken cancellationToken)
    {
        var assets = await _fintachartsDbContext.Assets
            .Include(a=> a.Providers)
            .ToListAsync(cancellationToken);
        
        return _mapper.Map<IEnumerable<AssetModel>>(assets);
    }
}