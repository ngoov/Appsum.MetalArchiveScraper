using NodaTime;

namespace Data;

public class BandGenre : Entity
{
    private BandGenre(Guid id) : base(id)
    {
        
    }
    public BandGenre(Guid id, Band band, Genre genre, Instant from, Instant? to) : this(id)
    {
        Band = band;
        Genre = genre;
        From = from;
        To = to;
    }

    public Band Band { get; } = null!;
    public Genre Genre { get; } = null!;
    public Instant From { get; }
    public Instant? To { get; }
}