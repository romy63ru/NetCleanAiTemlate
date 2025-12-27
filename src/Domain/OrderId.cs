namespace Domain;

public readonly struct OrderId
{
    public Guid Value { get; }

    private OrderId(Guid value)
    {
        Value = value;
    }

    public static OrderId New() => new(Guid.NewGuid());
    public static OrderId From(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("OrderId cannot be empty", nameof(value));
        return new OrderId(value);
    }

    public override string ToString() => Value.ToString();
}
