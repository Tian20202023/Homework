using System;
using System.Windows.Forms;
using System.ComponentModel;

public class MainForm : Form
{
    private OrderService orderService = new OrderService();
    private BindingSource orderBindingSource = new BindingSource();
    private BindingSource detailBindingSource = new BindingSource();
    private DataGridView orderGridView;
    private DataGridView detailGridView;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnDelete;
    private Button btnQuery;
    private TextBox txtQuery;
    private ComboBox cmbQueryType;

    public MainForm()
    {
        InitializeComponents();
        SetupDataBinding();
        LoadData();
    }

    private void InitializeComponents()
    {
        this.Text = "订单管理系统";
        this.Size = new System.Drawing.Size(1000, 600);
        this.MinimumSize = new System.Drawing.Size(800, 500);

        // 创建查询区域
        Panel queryPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 50
        };

        cmbQueryType = new ComboBox
        {
            Location = new System.Drawing.Point(10, 15),
            Width = 100,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cmbQueryType.Items.AddRange(new string[] { "订单号", "商品名称", "客户名称", "金额范围" });
        cmbQueryType.SelectedIndex = 0;

        txtQuery = new TextBox
        {
            Location = new System.Drawing.Point(120, 15),
            Width = 200
        };

        btnQuery = new Button
        {
            Text = "查询",
            Location = new System.Drawing.Point(330, 15),
            Width = 80
        };
        btnQuery.Click += BtnQuery_Click;

        queryPanel.Controls.AddRange(new Control[] { cmbQueryType, txtQuery, btnQuery });

        // 创建订单表格
        orderGridView = new DataGridView
        {
            Dock = DockStyle.Top,
            Height = 200,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false
        };
        orderGridView.SelectionChanged += OrderGridView_SelectionChanged;

        // 创建订单明细表格
        detailGridView = new DataGridView
        {
            Dock = DockStyle.Top,
            Height = 150,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true
        };

        // 创建按钮区域
        Panel buttonPanel = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 50
        };

        btnAdd = new Button
        {
            Text = "添加订单",
            Location = new System.Drawing.Point(10, 10),
            Width = 80
        };
        btnAdd.Click += BtnAdd_Click;

        btnEdit = new Button
        {
            Text = "修改订单",
            Location = new System.Drawing.Point(100, 10),
            Width = 80
        };
        btnEdit.Click += BtnEdit_Click;

        btnDelete = new Button
        {
            Text = "删除订单",
            Location = new System.Drawing.Point(190, 10),
            Width = 80
        };
        btnDelete.Click += BtnDelete_Click;

        buttonPanel.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete });

        // 添加控件到窗体
        this.Controls.AddRange(new Control[] { queryPanel, orderGridView, detailGridView, buttonPanel });
    }

    private void SetupDataBinding()
    {
        // 设置订单数据源
        orderBindingSource.DataSource = typeof(Order);
        orderGridView.DataSource = orderBindingSource;

        // 设置订单明细数据源
        detailBindingSource.DataSource = orderBindingSource;
        detailBindingSource.DataMember = "Details";
        detailGridView.DataSource = detailBindingSource;

        // 配置订单表格列
        orderGridView.Columns.Add("OrderId", "订单号");

        // 配置订单明细表格列
        detailGridView.Columns.Add("ProductName", "商品名称");
        detailGridView.Columns.Add("Quantity", "数量");
        detailGridView.Columns.Add("UnitPrice", "单价");

        // 设置金额列的格式
        detailGridView.Columns["UnitPrice"].DefaultCellStyle.Format = "C2";
    }

    private void LoadData()
    {
        orderBindingSource.DataSource = orderService.GetAllOrders();
    }

    private void OrderGridView_SelectionChanged(object sender, EventArgs e)
    {
        if (orderGridView.SelectedRows.Count > 0)
        {
            Order selectedOrder = (Order)orderGridView.SelectedRows[0].DataBoundItem;
            detailBindingSource.DataSource = selectedOrder.Details;
        }
    }

    private void BtnQuery_Click(object sender, EventArgs e)
    {
        string queryText = txtQuery.Text.Trim();
        List<Order> results = null;

        switch (cmbQueryType.SelectedIndex)
        {
            case 0: // 订单号
                var order = orderService.GetOrderById(queryText);
                if (order != null)
                    results = new List<Order> { order };
                break;
            case 1: // 商品名称
                results = orderService.QueryByProductName(queryText);
                break;
            case 2: // 客户名称
                results = orderService.QueryByCustomer(queryText);
                break;
            case 3: // 金额范围
                if (decimal.TryParse(queryText, out decimal amount))
                {
                    results = orderService.QueryByAmountRange(amount - 1000, amount + 1000);
                }
                break;
        }

        orderBindingSource.DataSource = results ?? new List<Order>();
    }

    private void BtnAdd_Click(object sender, EventArgs e)
    {
        using (var form = new OrderForm())
        {
            if (form.ShowDialog() == DialogResult.OK)
            {
                orderService.AddOrder(form.Order);
                LoadData();
            }
        }
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        if (orderGridView.SelectedRows.Count == 0)
        {
            MessageBox.Show("请选择要修改的订单");
            return;
        }

        Order selectedOrder = (Order)orderGridView.SelectedRows[0].DataBoundItem;
        using (var form = new OrderForm(selectedOrder))
        {
            if (form.ShowDialog() == DialogResult.OK)
            {
                orderService.UpdateOrder(form.Order);
                LoadData();
            }
        }
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        if (orderGridView.SelectedRows.Count == 0)
        {
            MessageBox.Show("请选择要删除的订单");
            return;
        }

        Order selectedOrder = (Order)orderGridView.SelectedRows[0].DataBoundItem;
        if (MessageBox.Show("确定要删除此订单吗？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            try
            {
                orderService.RemoveOrder(selectedOrder.OrderId);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除失败: {ex.Message}");
            }
        }
    }
} 