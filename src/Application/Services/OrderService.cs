using Application.Abstractions;
using Application.Common;
using Domain;

namespace Application.Services;

public sealed class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;

    public OrderService(IOrderRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<Order>> CreateAsync(IEnumerable<OrderItemInput> items, bool confirm, CancellationToken ct)
    {
        try
        {
            if (items is null)
                return Result<Order>.Failure("Items are required", code: "validation");

            var list = items.ToList();
            if (list.Count == 0)
                return Result<Order>.Failure("Order must contain at least one item", code: "validation");

            var order = Order.Create(OrderId.New());

            foreach (var i in list)
            {
                order.AddItem(i.ProductId, i.Quantity, i.Price);
            }

            if (confirm)
            {
                order.Confirm();
            }

            await _repo.AddAsync(order, ct);
            return Result<Order>.Success(order);
        }
        catch (ArgumentException ex)
        {
            return Result<Order>.Failure(ex.Message, code: "validation");
        }
        catch (InvalidOperationException ex)
        {
            // Domain lifecycle violation (e.g., confirm without items)
            return Result<Order>.Failure(ex.Message, code: "validation");
        }
        catch (Exception ex)
        {
            return Result<Order>.Failure("Unexpected error: " + ex.Message, code: "error");
        }
    }
}
