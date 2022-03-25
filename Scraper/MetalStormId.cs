using CSharpFunctionalExtensions;

namespace Scraper;

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