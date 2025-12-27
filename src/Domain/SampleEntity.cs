namespace Domain;

public sealed class SampleEntity
{
    public Guid Id { get; }
    public string Name { get; }
    public string? Description { get; }

    private SampleEntity(Guid id, string name, string? description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public static SampleEntity Create(Guid id, string name, string? description)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id must be non-empty", nameof(id));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required", nameof(name));
        return new SampleEntity(id, name.Trim(), string.IsNullOrWhiteSpace(description) ? null : description!.Trim());
    }
}
