using AutoMapper;
using Fintacharts.Abstractions.Entities;
using Fintacharts.Abstractions.Models;

namespace Fintacharts.Business.Mappers.EntityToModel;

public class ProviderToProviderModelMappingProfile: Profile
{
    public ProviderToProviderModelMappingProfile()
    {
        CreateMap<Provider, ProviderModel>();
    }
}