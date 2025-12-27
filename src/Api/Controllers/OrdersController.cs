using Api.Contracts;
using Application.Common;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request, CancellationToken ct)
    {
        if (request is null)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest,
                title: "Validation Failed",
                detail: "Request body is required");
        }

        if (request.Items is null || request.Items.Count == 0)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest,
                title: "Validation Failed",
                detail: "Order must contain at least one item");
        }

        if (request.Items.Any(i => i.Quantity <= 0))
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest,
                title: "Validation Failed",
                detail: "Item quantity must be greater than zero");
        }

        if (request.Items.Any(i => i.Price < 0))
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest,
                title: "Validation Failed",
                detail: "Item price cannot be negative");
        }

        var inputs = request.Items.Select(i => new OrderItemInput(i.ProductId, i.Quantity, i.Price));

        var result = await _service.CreateAsync(inputs, request.Confirm, ct);

        if (result.IsSuccess)
        {
            var order = result.Value!;
            var response = new CreateOrderResponse
            {
                OrderId = order.Id.Value,
                Status = (int)order.Status,
                ItemsCount = order.Items.Count,
                TotalPrice = order.TotalPrice,
                CancellationReason = order.CancellationReason
            };

            // Return 201 Created with a canonical location
            return Created($"/api/orders/{response.OrderId}", response);
        }

        var code = result.Code ?? "error";
        if (string.Equals(code, "validation", StringComparison.OrdinalIgnoreCase))
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest,
                title: "Validation Failed",
                detail: result.Error);
        }

        return Problem(statusCode: StatusCodes.Status500InternalServerError,
            title: "Unexpected Error",
            detail: result.Error);
    }
}
