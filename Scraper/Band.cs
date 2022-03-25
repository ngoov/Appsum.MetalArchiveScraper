using CSharpFunctionalExtensions;

using Flurl;

using NodaTime;

namespace Scraper;

public class Band
{
    public Band(MetalStormId metalStormId, string name, Url url)
    {
        MetalStormId = metalStormId;
        Name = name;
        Url = url;
    }
    public MetalStormId MetalStormId { get; }
    public string Name { get; }
    public Url Url { get; }
    private readonly List<BandGenre> _bandGenres = new();
    public IReadOnlyCollection<BandGenre> BandGenres => _bandGenres.ToList();

    public void AddGenre(Genre genre, Instant from, Instant? to)
    {
        _bandGenres.Add(new BandGenre(this, genre, from, to));
    }
}

public class BandGenre
{
    public BandGenre(Band band, Genre genre, Instant from, Instant? to)
    {
        Band = band;
        Genre = genre;
        From = from;
        To = to;
    }
    public Band Band { get; }
    public Genre Genre { get; }
    public Instant From { get; }
    public Instant? To { get; }
}

public class Genre : ValueObject
{
    public Genre(string value)
    {
        Value = value;
    }

    public string Value { get; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static implicit operator string(Genre albumTitle) => albumTitle.Value;
    public static explicit operator Genre(string value) => new (value);

    public override string ToString() => Value;
}