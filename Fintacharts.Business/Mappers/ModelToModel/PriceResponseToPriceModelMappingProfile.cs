using AutoMapper;
using Fintacharts.Abstractions.Models;

namespace Fintacharts.Business.Mappers.ModelToModel;

public class PriceResponseToPriceModelMappingProfile: Profile
{
    public PriceResponseToPriceModelMappingProfile()
    {
        CreateMap<PriceResponse, PriceModel>();
    }
}