using System;
using System.Collections.Generic;
using System.Linq;

public class Order
{
    public string OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public Customer Customer { get; set; }
    public List<OrderDetails> Details { get; set; }

    public Order(string orderId, Customer customer)
    {
        OrderId = orderId;
        Customer = customer;
        OrderDate = DateTime.Now;
        Details = new List<OrderDetails>();
    }

    public decimal TotalAmount => Details.Sum(d => d.TotalAmount);

    public void AddDetail(OrderDetails detail)
    {
        if (Details.Contains(detail))
            throw new Exception("该商品已存在于订单中");
        Details.Add(detail);
    }

    public void RemoveDetail(OrderDetails detail)
    {
        Details.Remove(detail);
    }

    public override string ToString()
    {
        var detailsStr = string.Join("\n", Details.Select(d => "  " + d.ToString()));
        return $"订单号: {OrderId}\n下单时间: {OrderDate}\n客户信息: {Customer}\n订单明细:\n{detailsStr}\n总金额: {TotalAmount:C}";
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Order other = (Order)obj;
        return OrderId == other.OrderId;
    }

    public override int GetHashCode()
    {
        return OrderId.GetHashCode();
    }
} 