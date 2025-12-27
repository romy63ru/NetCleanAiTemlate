namespace Domain;

public sealed class OrderItem
{
    public Guid ProductId { get; }
    public int Quantity { get; }
    public decimal Price { get; }

    private OrderItem(Guid productId, int quantity, decimal price)
    {
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public static OrderItem Create(Guid productId, int quantity, decimal price)
    {
        if (productId == Guid.Empty) throw new ArgumentException("ProductId cannot be empty", nameof(productId));
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
        if (price < 0) throw new ArgumentException("Price cannot be negative", nameof(price));
        return new OrderItem(productId, quantity, price);
    }

    public decimal LineTotal => Price * Quantity;
}
