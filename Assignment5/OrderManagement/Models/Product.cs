using System;

namespace OrderManagement.Models
{
    public class Product
    {
        public string ProductId { get; }
        public string Name { get; }
        public decimal Price { get; }

        public Product(string productId, string name, decimal price)
        {
            ProductId = productId;
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"商品ID: {ProductId}, 名称: {Name}, 价格: {Price:C}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Product other)
            {
                return ProductId == other.ProductId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ProductId.GetHashCode();
        }
    }
}