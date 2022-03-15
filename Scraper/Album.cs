using CSharpFunctionalExtensions;

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

public class MetalStormId : ValueObject
{
    public MetalStormId(int value)
    {
        _value = value;
    }

    private readonly int _value;
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
}

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
    
}

public sealed class AlbumTitle : ValueObject
{
    public AlbumTitle(string value)
    {
        _value = value;
    }

    private readonly string _value;
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
}