using CSharpFunctionalExtensions;

namespace Scraper;

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