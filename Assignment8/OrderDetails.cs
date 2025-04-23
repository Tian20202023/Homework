using System;

public class OrderDetails
{
    public string OrderId { get; set; }
    public string ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount => Quantity * UnitPrice;

    public OrderDetails(Product product, int quantity, decimal unitPrice)
    {
        Product = product;
        ProductId = product.Id;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public OrderDetails() { }

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