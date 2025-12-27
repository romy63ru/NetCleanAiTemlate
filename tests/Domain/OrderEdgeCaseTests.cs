using Domain;

namespace Domain.Tests;

public class OrderEdgeCaseTests
{
    [Fact]
    public void Confirm_AfterCancel_Throws()
    {
        var order = Order.Create(OrderId.New());
        order.Cancel("Customer requested");
        Assert.Throws<InvalidOperationException>(() => order.Confirm());
    }

    [Fact]
    public void Cancel_AfterConfirm_Throws()
    {
        var order = Order.Create(OrderId.New());
        order.AddItem(Guid.NewGuid(), 1, 1m);
        order.Confirm();
        Assert.Throws<InvalidOperationException>(() => order.Cancel("Late cancel"));
    }

    [Fact]
    public void AddItem_InvalidQuantity_Throws()
    {
        var order = Order.Create(OrderId.New());
        Assert.Throws<ArgumentException>(() => order.AddItem(Guid.NewGuid(), 0, 10m));
    }

    [Fact]
    public void AddItem_NegativePrice_Throws()
    {
        var order = Order.Create(OrderId.New());
        Assert.Throws<ArgumentException>(() => order.AddItem(Guid.NewGuid(), 1, -1m));
    }
}
