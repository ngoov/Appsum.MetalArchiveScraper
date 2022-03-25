using Flurl;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NodaTime;

namespace Data.Configurations;

public class BandGenreConfiguration : IEntityTypeConfiguration<BandGenre>
{
    public void Configure(EntityTypeBuilder<BandGenre> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Band).WithMany(x => x.BandGenres);
        builder.HasOne(x => x.Genre).WithMany();
        builder.Property(x => x.From).HasConversion(x => x.ToUnixTimeMilliseconds(), x => Instant.FromUnixTimeMilliseconds(x));
        builder.Property(x => x.To).HasConversion(x => x == null ? (long?)null : ((Instant)x).ToUnixTimeMilliseconds(), x => x == null ? null : Instant.FromUnixTimeMilliseconds((long)x));
    }
}