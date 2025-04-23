using System;
using System.Collections.Generic;
using System.Linq;

public class OrderService
{
    private List<Order> orders = new List<Order>();

    public void AddOrder(Order order)
    {
        if (orders.Contains(order))
            throw new Exception("订单号已存在");
        orders.Add(order);
    }

    public void RemoveOrder(string orderId)
    {
        var order = GetOrderById(orderId);
        if (order == null)
            throw new Exception("订单不存在");
        orders.Remove(order);
    }

    public void UpdateOrder(Order updatedOrder)
    {
        var order = GetOrderById(updatedOrder.OrderId);
        if (order == null)
            throw new Exception("订单不存在");
        
        int index = orders.IndexOf(order);
        orders[index] = updatedOrder;
    }

    public Order GetOrderById(string orderId)
    {
        return orders.FirstOrDefault(o => o.OrderId == orderId);
    }

    public List<Order> QueryByProductName(string productName)
    {
        return orders.Where(o => o.Details.Any(d => d.Product.Name.Contains(productName)))
                    .OrderByDescending(o => o.TotalAmount)
                    .ToList();
    }

    public List<Order> QueryByCustomer(string customerName)
    {
        return orders.Where(o => o.Customer.Name.Contains(customerName))
                    .OrderByDescending(o => o.TotalAmount)
                    .ToList();
    }

    public List<Order> QueryByAmountRange(decimal minAmount, decimal maxAmount)
    {
        return orders.Where(o => o.TotalAmount >= minAmount && o.TotalAmount <= maxAmount)
                    .OrderByDescending(o => o.TotalAmount)
                    .ToList();
    }

    public void Sort()
    {
        orders.Sort((o1, o2) => o1.OrderId.CompareTo(o2.OrderId));
    }

    public void Sort(Comparison<Order> comparison)
    {
        orders.Sort(comparison);
    }

    public List<Order> GetAllOrders()
    {
        return orders.OrderByDescending(o => o.TotalAmount).ToList();
    }
} 