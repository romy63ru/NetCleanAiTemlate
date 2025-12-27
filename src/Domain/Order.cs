namespace Domain;

public sealed class Order
{
    private readonly List<OrderItem> _items = new();

    public OrderId Id { get; }
    public OrderStatus Status { get; private set; }
    public IReadOnlyList<OrderItem> Items => _items;
    public decimal TotalPrice => _items.Sum(i => i.LineTotal);
    public string? CancellationReason { get; private set; }

    private Order(OrderId id)
    {
        Id = id;
        Status = OrderStatus.Created;
    }

    public static Order Create(OrderId id) => new(id);

    public void AddItem(Guid productId, int quantity, decimal price)
    {
        if (Status != OrderStatus.Created) throw new InvalidOperationException("Order cannot be modified after confirmation");
        var item = OrderItem.Create(productId, quantity, price);
        _items.Add(item);
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Created) throw new InvalidOperationException("Order is not in a confirmable state");
        if (_items.Count == 0) throw new InvalidOperationException("Order must have at least one item to confirm");
        Status = OrderStatus.Confirmed;
    }

    public void Cancel(string reason)
    {
        if (Status != OrderStatus.Created) throw new InvalidOperationException("Only orders in Created state can be cancelled");
        if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("Cancellation reason is required", nameof(reason));
        Status = OrderStatus.Cancelled;
        CancellationReason = reason.Trim();
    }
}
