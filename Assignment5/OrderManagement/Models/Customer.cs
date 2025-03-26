using System;

namespace OrderManagement.Models
{
    public class Customer
    {
        public string CustomerId { get; }
        public string Name { get; }
        public string Address { get; }

        public Customer(string customerId, string name, string address)
        {
            CustomerId = customerId;
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return $"客户ID: {CustomerId}, 姓名: {Name}, 地址: {Address}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Customer other)
            {
                return CustomerId == other.CustomerId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return CustomerId.GetHashCode();
        }
    }
}