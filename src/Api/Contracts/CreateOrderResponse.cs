namespace Api.Contracts;

public sealed class CreateOrderResponse
{
    public Guid OrderId { get; set; }
    public int Status { get; set; }
    public int ItemsCount { get; set; }
    public decimal TotalPrice { get; set; }
    public string? CancellationReason { get; set; }
}
