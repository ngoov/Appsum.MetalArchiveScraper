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
        Value = value;
    }

    public int Value { get; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator int(MetalStormId metalStormId) => metalStormId.Value;
    public static explicit operator MetalStormId(string value) => new (int.Parse(value));
    public override string ToString() => Value.ToString();
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
        Value = value;
    }

    public string Value { get; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public static implicit operator string(AlbumTitle albumTitle) => albumTitle.Value;
    public static explicit operator AlbumTitle(string value) => new (value);

    public override string ToString() => Value;
}