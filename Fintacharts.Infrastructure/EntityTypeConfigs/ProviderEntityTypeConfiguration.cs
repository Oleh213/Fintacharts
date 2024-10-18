using Fintacharts.Abstractions.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintacharts.Infrastructure.EntityTypeConfigs;

public class ProviderEntityTypeConfiguration: EntityTypeConfiguration<Provider>
{
    public override void Configure(EntityTypeBuilder<Provider> builder)
    {
        AddEntityProperties(builder);
    }
}
