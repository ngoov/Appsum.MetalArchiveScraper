using NodaTime;

namespace WebApi;

public sealed class AlbumDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}

public sealed class BandDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<AlbumDto> Albums { get; set; }
    public ICollection<GenreDto> Genres { get; set; }
}

public sealed class GenreDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Instant FromDate { get; set; }
    public Instant? ToDate { get; set; }
} 