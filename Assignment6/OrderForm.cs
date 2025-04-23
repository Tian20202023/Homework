using System;
using System.Windows.Forms;
using System.ComponentModel;

public class OrderForm : Form
{
    private Order order;
    private BindingSource detailBindingSource = new BindingSource();
    private DataGridView detailGridView;
    private Button btnAddDetail;
    private Button btnRemoveDetail;
    private Button btnSave;
    private Button btnCancel;
    private TextBox txtOrderId;
    private ComboBox cmbCustomer;
    private Dictionary<string, Product> products = new Dictionary<string, Product>();
    private Dictionary<string, Customer> customers = new Dictionary<string, Customer>();

    public Order Order => order;

    public OrderForm()
    {
        InitializeData();
        InitializeComponents();
        order = new Order("", customers.Values.First());
        SetupDataBinding();
    }

    public OrderForm(Order existingOrder)
    {
        InitializeData();
        InitializeComponents();
        order = existingOrder;
        SetupDataBinding();
    }

    private void InitializeData()
    {
        // 初始化示例商品
        products.Add("P001", new Product("P001", "笔记本电脑", 5999m));
        products.Add("P002", new Product("P002", "手机", 2999m));
        products.Add("P003", new Product("P003", "平板电脑", 3999m));

        // 初始化示例客户
        customers.Add("C001", new Customer("C001", "张三", "13800138000"));
        customers.Add("C002", new Customer("C002", "李四", "13900139000"));
    }

    private void InitializeComponents()
    {
        this.Text = "订单编辑";
        this.Size = new System.Drawing.Size(600, 500);
        this.MinimumSize = new System.Drawing.Size(500, 400);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.CenterParent;
        this.MaximizeBox = false;
        this.MinimizeBox = false;

        // 创建订单基本信息区域
        Panel infoPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 100
        };

        Label lblOrderId = new Label
        {
            Text = "订单号:",
            Location = new System.Drawing.Point(10, 15),
            AutoSize = true
        };

        txtOrderId = new TextBox
        {
            Location = new System.Drawing.Point(80, 12),
            Width = 150
        };

        Label lblCustomer = new Label
        {
            Text = "客户:",
            Location = new System.Drawing.Point(10, 45),
            AutoSize = true
        };

        cmbCustomer = new ComboBox
        {
            Location = new System.Drawing.Point(80, 42),
            Width = 150,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cmbCustomer.DataSource = customers.Values.ToList();
        cmbCustomer.DisplayMember = "Name";

        infoPanel.Controls.AddRange(new Control[] { lblOrderId, txtOrderId, lblCustomer, cmbCustomer });

        // 创建订单明细表格
        detailGridView = new DataGridView
        {
            Dock = DockStyle.Top,
            Height = 200,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect
        };

        // 创建按钮区域
        Panel buttonPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 50
        };

        btnAddDetail = new Button
        {
            Text = "添加商品",
            Location = new System.Drawing.Point(10, 10),
            Width = 80
        };
        btnAddDetail.Click += BtnAddDetail_Click;

        btnRemoveDetail = new Button
        {
            Text = "删除商品",
            Location = new System.Drawing.Point(100, 10),
            Width = 80
        };
        btnRemoveDetail.Click += BtnRemoveDetail_Click;

        buttonPanel.Controls.AddRange(new Control[] { btnAddDetail, btnRemoveDetail });

        // 创建保存/取消按钮
        Panel bottomPanel = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 50
        };

        btnSave = new Button
        {
            Text = "保存",
            Location = new System.Drawing.Point(10, 10),
            Width = 80
        };
        btnSave.Click += BtnSave_Click;

        btnCancel = new Button
        {
            Text = "取消",
            Location = new System.Drawing.Point(100, 10),
            Width = 80
        };
        btnCancel.Click += BtnCancel_Click;

        bottomPanel.Controls.AddRange(new Control[] { btnSave, btnCancel });

        // 添加控件到窗体
        this.Controls.AddRange(new Control[] { infoPanel, detailGridView, buttonPanel, bottomPanel });
    }

    private void SetupDataBinding()
    {
        // 绑定订单基本信息
        txtOrderId.DataBindings.Add("Text", order, "OrderId");
        cmbCustomer.DataBindings.Add("SelectedItem", order, "Customer");

        // 绑定订单明细
        detailBindingSource.DataSource = order.Details;
        detailGridView.DataSource = detailBindingSource;

        // 配置订单明细表格列
        detailGridView.Columns.Add("ProductName", "商品名称");
        detailGridView.Columns.Add("Quantity", "数量");
        detailGridView.Columns.Add("UnitPrice", "单价");

        // 设置金额列的格式
        detailGridView.Columns["UnitPrice"].DefaultCellStyle.Format = "C2";
    }

    private void BtnAddDetail_Click(object sender, EventArgs e)
    {
        using (var form = new DetailForm(products.Values.ToList()))
        {
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    order.AddDetail(form.Detail);
                    detailBindingSource.ResetBindings(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }

    private void BtnRemoveDetail_Click(object sender, EventArgs e)
    {
        if (detailGridView.SelectedRows.Count > 0)
        {
            OrderDetails detail = (OrderDetails)detailGridView.SelectedRows[0].DataBoundItem;
            order.RemoveDetail(detail);
            detailBindingSource.ResetBindings(false);
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(order.OrderId))
        {
            MessageBox.Show("请输入订单号");
            return;
        }

        if (order.Details.Count == 0)
        {
            MessageBox.Show("订单必须包含至少一个商品");
            return;
        }

        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
} 