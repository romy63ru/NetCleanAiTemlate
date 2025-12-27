using Domain;

namespace Domain.Tests;

public class OrderTests
{
    [Fact]
    public void Confirm_WithItems_SetsStatusConfirmed()
    {
        var id = OrderId.New();
        var order = Order.Create(id);
        order.AddItem(Guid.NewGuid(), 2, 10m);
        order.Confirm();
        Assert.Equal(OrderStatus.Confirmed, order.Status);
    }

    [Fact]
    public void Confirm_WithoutItems_Throws()
    {
        var order = Order.Create(OrderId.New());
        Assert.Throws<InvalidOperationException>(() => order.Confirm());
    }

    [Fact]
    public void AddItem_AfterConfirm_Throws()
    {
        var order = Order.Create(OrderId.New());
        order.AddItem(Guid.NewGuid(), 1, 5m);
        order.Confirm();
        Assert.Throws<InvalidOperationException>(() => order.AddItem(Guid.NewGuid(), 1, 2m));
    }

    [Fact]
    public void Cancel_InCreated_SetsStatusCancelled_AndReason()
    {
        var order = Order.Create(OrderId.New());
        order.Cancel("Customer requested");
        Assert.Equal(OrderStatus.Cancelled, order.Status);
        Assert.Equal("Customer requested", order.CancellationReason);
    }

    [Fact]
    public void Cancel_WithoutReason_Throws()
    {
        var order = Order.Create(OrderId.New());
        Assert.Throws<ArgumentException>(() => order.Cancel(" "));
    }

    [Fact]
    public void TotalPrice_SumsLineTotals()
    {
        var order = Order.Create(OrderId.New());
        order.AddItem(Guid.NewGuid(), 2, 10m); // 20
        order.AddItem(Guid.NewGuid(), 3, 5m);  // 15
        Assert.Equal(35m, order.TotalPrice);
    }
}
