using Flurl;

using NodaTime;

namespace Data;

public class Band : Entity
{
    public Band(Guid id, MetalStormId metalStormId, string name, Url url) : base(id)
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
    
    private readonly List<Album> _albums = new();
    public IReadOnlyCollection<Album> Albums => _albums.ToList();

    public void AddGenre(Genre genre, Instant from, Instant? to)
    {
        if (_bandGenres.All(x => x.Genre.Id != genre.Id))
        {
            _bandGenres.Add(new BandGenre(Guid.NewGuid(), this, genre, from, to));
        }
    }
    public void AddAlbum(Album album)
    {
        _albums.Add(album);
    }
}