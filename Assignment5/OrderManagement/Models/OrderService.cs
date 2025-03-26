using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagement.Models
{
    public class OrderService
    {
        private readonly List<Order> _orders = new();

        public void AddOrder(Order order)
        {
            if (_orders.Contains(order))
            {
                throw new InvalidOperationException("订单已存在");
            }
            _orders.Add(order);
        }

        public void RemoveOrder(string orderId)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new InvalidOperationException("订单不存在");
            }
            _orders.Remove(order);
        }

        public void UpdateOrder(Order order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.OrderId == order.OrderId);
            if (existingOrder == null)
            {
                throw new InvalidOperationException("订单不存在");
            }
            var index = _orders.IndexOf(existingOrder);
            _orders[index] = order;
        }

        public Order? GetOrderById(string orderId)
        {
            return _orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public IEnumerable<Order> GetOrdersByCustomerName(string customerName)
        {
            return _orders.Where(o => o.Customer.Name.Contains(customerName))
                         .OrderByDescending(o => o.TotalAmount);
        }

        public IEnumerable<Order> GetOrdersByProductName(string productName)
        {
            return _orders.Where(o => o.OrderDetails.Any(d => d.Product.Name.Contains(productName)))
                         .OrderByDescending(o => o.TotalAmount);
        }

        public IEnumerable<Order> GetOrdersByAmountRange(decimal minAmount, decimal maxAmount)
        {
            return _orders.Where(o => o.TotalAmount >= minAmount && o.TotalAmount <= maxAmount)
                         .OrderByDescending(o => o.TotalAmount);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orders.OrderBy(o => o.OrderId);
        }
    }
}