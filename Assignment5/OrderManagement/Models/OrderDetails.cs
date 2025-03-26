using System;

namespace OrderManagement.Models
{
    public class OrderDetails
    {
        public Product Product { get; }
        public int Quantity { get; }
        public decimal UnitPrice { get; }

        public OrderDetails(Product product, int quantity, decimal unitPrice)
        {
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public decimal TotalAmount => Quantity * UnitPrice;

        public override string ToString()
        {
            return $"{Product}, 数量: {Quantity}, 单价: {UnitPrice:C}, 总金额: {TotalAmount:C}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is OrderDetails other)
            {
                return Product.Equals(other.Product) &&
                       Quantity == other.Quantity &&
                       UnitPrice == other.UnitPrice;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Product, Quantity, UnitPrice);
        }
    }
}