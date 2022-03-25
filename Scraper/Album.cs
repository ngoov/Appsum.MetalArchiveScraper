using Flurl;

namespace Scraper;

public class Album
{
    public AlbumTitle Title { get; }
    public Band Band { get; }
    public MetalStormId MetalStormId { get; }
    public Url Url { get; }

    public Album(MetalStormId metalStormId, AlbumTitle title, Band band, Url url)
    {
        Title = title;
        Band = band;
        Url = url;
        MetalStormId = metalStormId;
    }   
}