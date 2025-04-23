using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class OrderService
{
    private readonly OrderDbContext _context;

    public OrderService()
    {
        _context = new OrderDbContext();
    }

    public void AddOrder(Order order)
    {
        if (_context.Orders.Any(o => o.OrderId == order.OrderId))
            throw new Exception("订单号已存在");

        // 检查客户是否已存在
        var existingCustomer = _context.Customers.Find(order.Customer.Id);
        if (existingCustomer != null)
        {
            // 如果客户已存在，使用现有客户
            order.Customer = existingCustomer;
            order.CustomerId = existingCustomer.Id;
        }

        // 检查每个订单明细中的商品是否存在
        foreach (var detail in order.Details)
        {
            var existingProduct = _context.Products.Find(detail.Product.Id);
            if (existingProduct != null)
            {
                // 如果商品已存在，使用现有商品
                detail.Product = existingProduct;
                detail.ProductId = existingProduct.Id;
            }
        }

        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public void RemoveOrder(string orderId)
    {
        var order = GetOrderById(orderId);
        if (order == null)
            throw new Exception("订单不存在");

        _context.Orders.Remove(order);
        _context.SaveChanges();
    }

    public void UpdateOrder(Order updatedOrder)
    {
        var order = GetOrderById(updatedOrder.OrderId);
        if (order == null)
            throw new Exception("订单不存在");

        _context.Entry(order).CurrentValues.SetValues(updatedOrder);
        _context.SaveChanges();
    }

    public Order GetOrderById(string orderId)
    {
        return _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Details)
                .ThenInclude(d => d.Product)
            .AsEnumerable()
            .FirstOrDefault(o => o.OrderId == orderId);
    }

    public List<Order> QueryByProductName(string productName)
    {
        return _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Details)
                .ThenInclude(d => d.Product)
            .AsEnumerable()
            .Where(o => o.Details.Any(d => d.Product.Name.Contains(productName)))
            .OrderByDescending(o => o.TotalAmount)
            .ToList();
    }

    public List<Order> QueryByCustomer(string customerName)
    {
        return _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Details)
                .ThenInclude(d => d.Product)
            .AsEnumerable()
            .Where(o => o.Customer.Name.Contains(customerName))
            .OrderByDescending(o => o.TotalAmount)
            .ToList();
    }

    public List<Order> QueryByAmountRange(decimal minAmount, decimal maxAmount)
    {
        return _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Details)
                .ThenInclude(d => d.Product)
            .AsEnumerable()
            .Where(o => o.TotalAmount >= minAmount && o.TotalAmount <= maxAmount)
            .OrderByDescending(o => o.TotalAmount)
            .ToList();
    }

    public void Sort()
    {
        _context.Orders.OrderBy(o => o.OrderId).ToList();
    }

    public void Sort(Comparison<Order> comparison)
    {
        _context.Orders.OrderBy(o => o.OrderId).ToList();
    }

    public List<Order> GetAllOrders()
    {
        return _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Details)
                .ThenInclude(d => d.Product)
            .AsEnumerable()
            .OrderByDescending(o => o.TotalAmount)
            .ToList();
    }

    public List<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }

    public List<Customer> GetAllCustomers()
    {
        return _context.Customers.ToList();
    }
} 