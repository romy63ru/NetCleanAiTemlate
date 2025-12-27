using Domain;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests;

public class OrderEfRepositoryTests
{
    [Fact]
    public async Task AddAsync_PersistsOrderWithItems_AndCanLoad()
    {
        var dbName = $"OrdersDb_{Guid.NewGuid()}";
        var options = new DbContextOptionsBuilder<OrderDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        await using var db = new OrderDbContext(options);
        var repo = new OrderEfRepository(db);

        var order = Order.Create(OrderId.New());
        order.AddItem(Guid.NewGuid(), 2, 10m);
        order.AddItem(Guid.NewGuid(), 1, 5m);
        order.Confirm();

        await repo.AddAsync(order, CancellationToken.None);

        var loaded = await db.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id.Value == order.Id.Value);

        Assert.NotNull(loaded);
        Assert.Equal(OrderStatus.Confirmed, loaded!.Status);
        Assert.Equal(2, loaded.Items.Count);
        Assert.Equal(25m, loaded.TotalPrice);
    }
}
