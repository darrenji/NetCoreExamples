# Domain events 

领域事件让系统中的组件可以相互通知。当一个组件的状态发生改变通知其它组件，这样的系统叫做"反应式系统(reactive system)"。

领域事件的持久化，可以追踪每次状态的变化，这就是"事件溯源Event Sourcing"。

# 一致性Consistency

比如处理订单，通常用工作者单元来做。

```
[Route("/api/order/pay/credit/{orderId}")]
public async Task TakeOnCustomerCredit(int orderId)
{
    using(var context = new CommerceContext())
    using(context.Database.BeginTransaction())
    {
        var order = await context.Orders.Where(x => x.Id == orderId).FirstAsync();
        var amount = order.UnpaidAmount;

        var customer = order.Customer;
        if(customer.Credit < amount)
        {
            throw new InvalidOperationException("Not enough credit");
        }
        customer.Credit -= amount;
        
        order.PlaidAmount += amount;
        order.UnpaidAmount -= amount;

        customer.TotalSpend += amount;

        if(customer.TotalSpend > CommerceConstants.PreferredLimit)
        {
            customer.Preferred = true;
        }

        order.IsPaid = order.UnpaidAmount = 0;

        await context.SaveChangesAsync();
    }
}
```

通常这样设计类
```
public class Customer
{
    public int Id{get;set;}
    public string Name{get;set;}
    public string Address1{get;set;}
    public string Address2{get;set;}
    public string City{get;set;}
    public string Country{get;set;}
    public string Credit{get;set;}
    public string Preferred{get;set;}
}

public class Order
{
    public int Id{get;set;}
    public int CustomerId{get;set;}
    public decimal TotalAmount{get;set;}
    public decimal ProductAmount{get;set;}
    public decimal UnpaidAmount{get;set;}
    public bool IsPaid{get;set;}
    public short DeliveryStatus{get;set;}
}

public class Product
{
    public int Id{get;set;}
    public string Description{get;set;}
    public decimal ListPrice{get;set;}
}

public class OrderLine
{
    public int Id{get;set;}
    public int OrderId{get;set;}
    public int ProductId{get;set;}
    public string ProductName{get;set;}
    public decimal ProductPrice{get;set;}
    public int Quantity{get;set;}
    public decimal LineTotal{get;set;}
    public bool IsShipped{get;set;}
}
```

工作者单元具有4个特性：

- Atomicity:原子性
- Consistency:一致性
- Isolation:隔离性
- Durability:持久性

所谓原子性是指要么完成，要么不完成，没有其它可能。

再来一个工作者单元。

```
public async Task ShipOrderLine(int orderLineId)
{
    using(var context = new CommerceContext())
    using(context.Database.BeginTransaction())
    {
        var oderLine = await context.OrderLines
            .Where(x => x.Id == orderLineId)
            .FirstAsync();

        orderLine.IsShipped = true;

        orderLine.Order.DeliveryStatus = orderLine.Order.OrderLines.All(x => x.IsShipped) ? DeliveryStatus.Shipped : DeliveryStatus.PartiallyShipped;
        await context.SaveChangesAsync();
    }
}
```

以上发现，工作者单元的边界完全是由代码来决定的。当两个工作者单元同时发生的时候，很有可能一个成功，一个不成功，这就造成了不一致性了。

可以把某种边界定义在领域中。领域模型中关系数据库的强关系越少越好。比如Order和OrderLine是聚合的关系。

```
public class Order
{
    public int Id{get;set;}

    public List<OrderLine> OrderLines{get;set;}
    public DeliveryStatus DeliveryStatus{get;set;}

    public void ShipOrderLine(int orderLineId)
    {
        var orderLine = OrderLines.First(x => x.Id==orderLineId);
        orderLine.IsShipped = true;

        DeliveryStatus = OrderLines.All(x => x.IsHhipped) ? DeliveryStatus.Shipped : DeliveryStatus.PartiallyShipped;
        
    }
}

public class OrderLine
{
    public int Id{get;set;}
}
```

如果在Order这个领域中需要Customer的某些信息呢？--可以通过接口。

```
public interface ICustomerCreditService
{
    Task<bool> EnsureEnoughCredit(int customerId, decimal amount);
}
```

