using Flurl;

namespace Data;

public class Album : Entity
{
    public AlbumTitle Title { get; }
    public MetalStormId MetalStormId { get; }
    public Url Url { get; }

    public Album(Guid id, MetalStormId metalStormId, AlbumTitle title, Url url) : base(id)
    {
        Title = title;
        Url = url;
        MetalStormId = metalStormId;
    }   
}