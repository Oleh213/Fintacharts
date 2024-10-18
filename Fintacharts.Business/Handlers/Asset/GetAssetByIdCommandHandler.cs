using AutoMapper;
using Fintacharts.Abstractions.Models;
using Fintacharts.Abstractions.Services;
using Fintacharts.Business.Commands.Asset;
using Fintacharts.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fintacharts.Business.Handlers.Asset;

public class GetAssetByIdCommandHandler: IRequestHandler<GetAssetByIdCommand, AssetModel>
{
    private FintachartsDbContext _fintachartsDbContext { get; set; }
    private IFintachartsService _fintachartsService { get; set; }
    private IMapper _mapper { get; set; }
    private IFintachartsWebSocketService _fintachartsWebSocketService { get; set; }

    public GetAssetByIdCommandHandler(
        IMapper mapper, 
        FintachartsDbContext fintachartsDbContext, 
        IFintachartsService fintachartsService, 
        IFintachartsWebSocketService fintachartsWebSocketService)
    {
        _mapper = mapper;
        _fintachartsDbContext = fintachartsDbContext;
        _fintachartsService = fintachartsService;
        _fintachartsWebSocketService = fintachartsWebSocketService;
    }
    
    public async Task<AssetModel> Handle(GetAssetByIdCommand request, CancellationToken cancellationToken)
    {
        var asset = await _fintachartsDbContext.Assets
            .Include(a=> a.Providers)
            .SingleAsync(a => a.Id == request.Id, cancellationToken);
        
        var assetModel = _mapper.Map<AssetModel>(asset);
        
        assetModel.PriceHistory = _mapper.Map<List<PriceModel>>(await _fintachartsService.GetPriceHistoryByAssetIdAsync(request.Id, request.Provider));
        
        assetModel.Price = await _fintachartsWebSocketService.GetPriceByAssetIdAsync(request.Id);
        
        return assetModel;
    }
}