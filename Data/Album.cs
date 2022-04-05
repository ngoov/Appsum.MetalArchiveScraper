using Flurl;

using NodaTime;

namespace Data;

public sealed class Album : Entity
{
    public AlbumTitle Title { get; }
    public MetalStormId MetalStormId { get; }
    public Url Url { get; }
    public Instant ReleaseDate { get; }

    public Album(Guid id, MetalStormId metalStormId, AlbumTitle title, Url url, Instant releaseDate) : base(id)
    {
        Title = title;
        Url = url;
        MetalStormId = metalStormId;
        ReleaseDate = releaseDate;
    }   
}