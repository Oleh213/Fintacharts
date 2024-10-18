using AutoMapper;
using Fintacharts.Abstractions.Entities;
using Fintacharts.Abstractions.Models;

namespace Fintacharts.Business.Mappers.EntityToModel;

public class AssetToAssetModelMappingProfile: Profile
{
    public AssetToAssetModelMappingProfile()
    {
        CreateMap<Asset, AssetModel>();
    }
}