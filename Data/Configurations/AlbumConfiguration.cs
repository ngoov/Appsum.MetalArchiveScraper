using Flurl;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.MetalStormId).HasConversion(x => x.Value, x => new MetalStormId(x));
        builder.Property(x => x.Title).HasConversion(x => x.Value, x => new AlbumTitle(x));
        builder.Property(x => x.Url).HasConversion(x => x.ToString(), x => Url.Parse(x));
        builder.Property(x => x.ReleaseDate);
    }
}