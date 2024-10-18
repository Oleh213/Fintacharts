using Fintacharts.Abstractions.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintacharts.Infrastructure.EntityTypeConfigs;

public class AssetEntityTypeConfiguration: EntityTypeConfiguration<Asset>
{
    public override void Configure(EntityTypeBuilder<Asset> builder)
    {
        AddEntityProperties(builder);

        builder
            .HasMany(x => x.Providers)
            .WithMany();
    }
}

