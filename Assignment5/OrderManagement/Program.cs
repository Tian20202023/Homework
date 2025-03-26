using System;
using System.Collections.Generic;
using System.Linq;
using OrderManagement.Models;

namespace OrderManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var orderService = new OrderService();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n订单管理系统");
                Console.WriteLine("1. 添加订单");
                Console.WriteLine("2. 删除订单");
                Console.WriteLine("3. 修改订单");
                Console.WriteLine("4. 查询订单");
                Console.WriteLine("5. 显示所有订单");
                Console.WriteLine("6. 退出");
                Console.Write("请选择操作 (1-6): ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddOrder(orderService);
                            break;
                        case "2":
                            RemoveOrder(orderService);
                            break;
                        case "3":
                            UpdateOrder(orderService);
                            break;
                        case "4":
                            QueryOrders(orderService);
                            break;
                        case "5":
                            DisplayAllOrders(orderService);
                            break;
                        case "6":
                            running = false;
                            break;
                        default:
                            Console.WriteLine("无效的选择，请重试");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"错误: {ex.Message}");
                }
            }
        }

        static void AddOrder(OrderService orderService)
        {
            Console.Write("请输入订单号: ");
            string orderId = Console.ReadLine();

            Console.Write("请输入客户ID: ");
            string customerId = Console.ReadLine();
            Console.Write("请输入客户姓名: ");
            string customerName = Console.ReadLine();
            Console.Write("请输入客户地址: ");
            string customerAddress = Console.ReadLine();

            var customer = new Customer(customerId, customerName, customerAddress);
            var order = new Order(orderId, customer);

            bool addingDetails = true;
            while (addingDetails)
            {
                Console.Write("请输入商品ID: ");
                string productId = Console.ReadLine();
                Console.Write("请输入商品名称: ");
                string productName = Console.ReadLine();
                Console.Write("请输入商品单价: ");
                decimal unitPrice = decimal.Parse(Console.ReadLine());
                Console.Write("请输入购买数量: ");
                int quantity = int.Parse(Console.ReadLine());

                var product = new Product(productId, productName, unitPrice);
                var detail = new OrderDetails(product, quantity, unitPrice);
                order.AddOrderDetail(detail);

                Console.Write("是否继续添加商品？(y/n): ");
                addingDetails = Console.ReadLine().ToLower() == "y";
            }

            orderService.AddOrder(order);
            Console.WriteLine("订单添加成功！");
        }

        static void RemoveOrder(OrderService orderService)
        {
            Console.Write("请输入要删除的订单号: ");
            string orderId = Console.ReadLine();
            orderService.RemoveOrder(orderId);
            Console.WriteLine("订单删除成功！");
        }

        static void UpdateOrder(OrderService orderService)
        {
            Console.Write("请输入要修改的订单号: ");
            string orderId = Console.ReadLine();
            var existingOrder = orderService.GetOrderById(orderId);
            if (existingOrder == null)
            {
                throw new InvalidOperationException("订单不存在");
            }

            Console.Write("请输入新的客户ID: ");
            string customerId = Console.ReadLine();
            Console.Write("请输入新的客户姓名: ");
            string customerName = Console.ReadLine();
            Console.Write("请输入新的客户地址: ");
            string customerAddress = Console.ReadLine();

            var customer = new Customer(customerId, customerName, customerAddress);
            var updatedOrder = new Order(orderId, customer);

            bool addingDetails = true;
            while (addingDetails)
            {
                Console.Write("请输入商品ID: ");
                string productId = Console.ReadLine();
                Console.Write("请输入商品名称: ");
                string productName = Console.ReadLine();
                Console.Write("请输入商品单价: ");
                decimal unitPrice = decimal.Parse(Console.ReadLine());
                Console.Write("请输入购买数量: ");
                int quantity = int.Parse(Console.ReadLine());

                var product = new Product(productId, productName, unitPrice);
                var detail = new OrderDetails(product, quantity, unitPrice);
                updatedOrder.AddOrderDetail(detail);

                Console.Write("是否继续添加商品？(y/n): ");
                addingDetails = Console.ReadLine().ToLower() == "y";
            }

            orderService.UpdateOrder(updatedOrder);
            Console.WriteLine("订单修改成功！");
        }

        static void QueryOrders(OrderService orderService)
        {
            Console.WriteLine("查询方式：");
            Console.WriteLine("1. 按订单号查询");
            Console.WriteLine("2. 按客户名查询");
            Console.WriteLine("3. 按商品名查询");
            Console.WriteLine("4. 按金额范围查询");
            Console.Write("请选择查询方式 (1-4): ");

            string choice = Console.ReadLine();
            IEnumerable<Order> orders = null;

            switch (choice)
            {
                case "1":
                    Console.Write("请输入订单号: ");
                    string orderId = Console.ReadLine();
                    var order = orderService.GetOrderById(orderId);
                    if (order != null)
                    {
                        Console.WriteLine(order);
                    }
                    else
                    {
                        Console.WriteLine("未找到订单");
                    }
                    return;

                case "2":
                    Console.Write("请输入客户名: ");
                    string customerName = Console.ReadLine();
                    orders = orderService.GetOrdersByCustomerName(customerName);
                    break;

                case "3":
                    Console.Write("请输入商品名: ");
                    string productName = Console.ReadLine();
                    orders = orderService.GetOrdersByProductName(productName);
                    break;

                case "4":
                    Console.Write("请输入最小金额: ");
                    decimal minAmount = decimal.Parse(Console.ReadLine());
                    Console.Write("请输入最大金额: ");
                    decimal maxAmount = decimal.Parse(Console.ReadLine());
                    orders = orderService.GetOrdersByAmountRange(minAmount, maxAmount);
                    break;

                default:
                    Console.WriteLine("无效的选择");
                    return;
            }

            if (orders != null)
            {
                foreach (var order in orders)
                {
                    Console.WriteLine(order);
                    Console.WriteLine("------------------------");
                }
            }
        }

        static void DisplayAllOrders(OrderService orderService)
        {
            var orders = orderService.GetAllOrders();
            foreach (var order in orders)
            {
                Console.WriteLine(order);
                Console.WriteLine("------------------------");
            }
        }
    }
}
