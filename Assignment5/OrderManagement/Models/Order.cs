using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagement.Models
{
    public class Order
    {
        public string OrderId { get; }
        public Customer Customer { get; }
        public DateTime OrderDate { get; }
        public List<OrderDetails> OrderDetails { get; }

        public Order(string orderId, Customer customer)
        {
            OrderId = orderId;
            Customer = customer;
            OrderDate = DateTime.Now;
            OrderDetails = new List<OrderDetails>();
        }

        public decimal TotalAmount => OrderDetails.Sum(detail => detail.TotalAmount);

        public void AddOrderDetail(OrderDetails detail)
        {
            if (OrderDetails.Contains(detail))
            {
                throw new InvalidOperationException("订单明细已存在");
            }
            OrderDetails.Add(detail);
        }

        public override string ToString()
        {
            return $"订单号: {OrderId}\n客户信息: {Customer}\n下单时间: {OrderDate}\n订单明细:\n{string.Join("\n", OrderDetails)}\n总金额: {TotalAmount:C}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Order other)
            {
                return OrderId == other.OrderId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return OrderId.GetHashCode();
        }
    }
}