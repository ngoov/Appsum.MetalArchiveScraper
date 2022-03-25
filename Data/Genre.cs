namespace Data;

public sealed class Genre : Entity
{
    public Genre(Guid id, string name) : base(id)
    {
        Name = name;
    }
    public string Name { get; }

    public override string ToString() => Name;
}