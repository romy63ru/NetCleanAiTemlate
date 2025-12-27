using Application.Common;
using Domain;

namespace Application.Services;

public interface IOrderService
{
    Task<Result<Order>> CreateAsync(IEnumerable<OrderItemInput> items, bool confirm, CancellationToken ct);
}

public readonly record struct OrderItemInput(Guid ProductId, int Quantity, decimal Price);
