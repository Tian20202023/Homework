using System;

public class OrderDetails
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public OrderDetails(Product product, int quantity, decimal unitPrice)
    {
        Product = product;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public decimal TotalAmount => Quantity * UnitPrice;

    public override string ToString()
    {
        return $"商品: {Product.Name}, 数量: {Quantity}, 单价: {UnitPrice:C}, 小计: {TotalAmount:C}";
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        OrderDetails other = (OrderDetails)obj;
        return Product.Equals(other.Product);
    }

    public override int GetHashCode()
    {
        return Product.GetHashCode();
    }
} 