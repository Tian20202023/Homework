using System;
using System.Windows.Forms;
using System.Linq;

public class DetailForm : Form
{
    private OrderDetails detail;
    private ComboBox cmbProduct;
    private NumericUpDown numQuantity;
    private Button btnOK;
    private Button btnCancel;

    public OrderDetails Detail => detail;

    public DetailForm(List<Product> products)
    {
        InitializeComponents(products);
    }

    private void InitializeComponents(List<Product> products)
    {
        this.Text = "添加商品";
        this.Size = new System.Drawing.Size(300, 200);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.CenterParent;
        this.MaximizeBox = false;
        this.MinimizeBox = false;

        // 创建商品选择区域
        Panel productPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 100
        };

        Label lblProduct = new Label
        {
            Text = "商品:",
            Location = new System.Drawing.Point(10, 15),
            AutoSize = true
        };

        cmbProduct = new ComboBox
        {
            Location = new System.Drawing.Point(80, 12),
            Width = 180,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cmbProduct.DataSource = products;
        cmbProduct.DisplayMember = "Name";

        Label lblQuantity = new Label
        {
            Text = "数量:",
            Location = new System.Drawing.Point(10, 45),
            AutoSize = true
        };

        numQuantity = new NumericUpDown
        {
            Location = new System.Drawing.Point(80, 42),
            Width = 100,
            Minimum = 1,
            Maximum = 999,
            Value = 1
        };

        productPanel.Controls.AddRange(new Control[] { lblProduct, cmbProduct, lblQuantity, numQuantity });

        // 创建按钮区域
        Panel buttonPanel = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 50
        };

        btnOK = new Button
        {
            Text = "确定",
            Location = new System.Drawing.Point(10, 10),
            Width = 80
        };
        btnOK.Click += BtnOK_Click;

        btnCancel = new Button
        {
            Text = "取消",
            Location = new System.Drawing.Point(100, 10),
            Width = 80
        };
        btnCancel.Click += BtnCancel_Click;

        buttonPanel.Controls.AddRange(new Control[] { btnOK, btnCancel });

        // 添加控件到窗体
        this.Controls.AddRange(new Control[] { productPanel, buttonPanel });
    }

    private void BtnOK_Click(object sender, EventArgs e)
    {
        Product selectedProduct = (Product)cmbProduct.SelectedItem;
        detail = new OrderDetails(selectedProduct, (int)numQuantity.Value, selectedProduct.Price);
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
} 