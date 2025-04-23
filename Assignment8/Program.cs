using System;
using System.Windows.Forms;
using System.Linq;

static class Program
{
    [STAThread]
    static void Main()
    {
        try
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 初始化数据库
            InitializeDatabase();

            Application.Run(new MainForm());
        }
        catch (Exception ex)
        {
            MessageBox.Show($"程序启动错误：{ex.Message}\n\n详细信息：{ex.StackTrace}", 
                          "错误", 
                          MessageBoxButtons.OK, 
                          MessageBoxIcon.Error);
        }
    }

    private static void InitializeDatabase()
    {
        try
        {
            using (var context = new OrderDbContext())
            {
                // 确保数据库已创建
                context.Database.EnsureCreated();

                // 检查连接
                if (!context.Database.CanConnect())
                {
                    throw new Exception("无法连接到数据库，请检查数据库连接配置。");
                }

                // 如果没有商品数据，添加示例商品
                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                        new Product("P001", "笔记本电脑", 5999m),
                        new Product("P002", "手机", 2999m),
                        new Product("P003", "平板电脑", 3999m)
                    );
                }

                // 如果没有客户数据，添加示例客户
                if (!context.Customers.Any())
                {
                    context.Customers.AddRange(
                        new Customer("C001", "张三", "13800138000"),
                        new Customer("C002", "李四", "13900139000")
                    );
                }

                context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("数据库初始化失败：" + ex.Message, ex);
        }
    }
} 