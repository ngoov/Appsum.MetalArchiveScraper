using Flurl;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class BandConfiguration : IEntityTypeConfiguration<Band>
{
    public void Configure(EntityTypeBuilder<Band> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.MetalStormId).HasConversion(x => x.Value, x => new MetalStormId(x));
        builder.Property(x => x.Name);
        builder.Property(x => x.Url).HasConversion(x => x.ToString(), x => Url.Parse(x));
        builder.HasMany(x => x.Albums).WithOne();
    }
}