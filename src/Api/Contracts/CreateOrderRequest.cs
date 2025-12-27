namespace Api.Contracts;

public sealed class CreateOrderRequest
{
    public List<OrderItemDto>? Items { get; set; }
    public bool Confirm { get; set; }
}
