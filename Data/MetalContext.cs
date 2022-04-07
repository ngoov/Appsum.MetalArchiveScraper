using System.Reflection;

using Microsoft.EntityFrameworkCore;

namespace Data;

public class MetalContext : DbContext
{
    public MetalContext(DbContextOptions<MetalContext> options) : base(options) { }

    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Band> Bands => Set<Band>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<BandGenre> BandGenres => Set<BandGenre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}