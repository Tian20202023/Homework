using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class OrderServiceTests
{
    private OrderService orderService;
    private Customer customer;
    private Product product1;
    private Product product2;

    [TestInitialize]
    public void Setup()
    {
        orderService = new OrderService();
        customer = new Customer("C001", "张三", "13800138000");
        product1 = new Product("P001", "笔记本电脑", 5999m);
        product2 = new Product("P002", "手机", 2999m);
    }

    [TestMethod]
    public void TestAddOrder()
    {
        Order order = new Order("O001", customer);
        order.AddDetail(new OrderDetails(product1, 1, product1.Price));
        orderService.AddOrder(order);

        var result = orderService.GetOrderById("O001");
        Assert.IsNotNull(result);
        Assert.AreEqual(order.OrderId, result.OrderId);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void TestAddDuplicateOrder()
    {
        Order order1 = new Order("O001", customer);
        Order order2 = new Order("O001", customer);
        
        orderService.AddOrder(order1);
        orderService.AddOrder(order2); // 应该抛出异常
    }

    [TestMethod]
    public void TestRemoveOrder()
    {
        Order order = new Order("O001", customer);
        orderService.AddOrder(order);
        orderService.RemoveOrder("O001");

        var result = orderService.GetOrderById("O001");
        Assert.IsNull(result);
    }

    [TestMethod]
    public void TestUpdateOrder()
    {
        Order order = new Order("O001", customer);
        order.AddDetail(new OrderDetails(product1, 1, product1.Price));
        orderService.AddOrder(order);

        order.AddDetail(new OrderDetails(product2, 2, product2.Price));
        orderService.UpdateOrder(order);

        var result = orderService.GetOrderById("O001");
        Assert.AreEqual(2, result.Details.Count);
        Assert.AreEqual(11997m, result.TotalAmount); // 5999 + 2999 * 2
    }

    [TestMethod]
    public void TestQueryByProductName()
    {
        CreateSampleOrders();
        var results = orderService.QueryByProductName("笔记本");
        Assert.AreEqual(1, results.Count);
        Assert.IsTrue(results[0].Details.Any(d => d.Product.Name.Contains("笔记本")));
    }

    [TestMethod]
    public void TestQueryByCustomer()
    {
        CreateSampleOrders();
        var results = orderService.QueryByCustomer("张三");
        Assert.AreEqual(2, results.Count);
        Assert.IsTrue(results.All(o => o.Customer.Name == "张三"));
    }

    [TestMethod]
    public void TestQueryByAmountRange()
    {
        CreateSampleOrders();
        var results = orderService.QueryByAmountRange(5000m, 7000m);
        Assert.AreEqual(1, results.Count);
        Assert.IsTrue(results.All(o => o.TotalAmount >= 5000m && o.TotalAmount <= 7000m));
    }

    private void CreateSampleOrders()
    {
        Order order1 = new Order("O001", customer);
        order1.AddDetail(new OrderDetails(product1, 1, product1.Price));
        orderService.AddOrder(order1);

        Order order2 = new Order("O002", customer);
        order2.AddDetail(new OrderDetails(product2, 1, product2.Price));
        orderService.AddOrder(order2);
    }
} 