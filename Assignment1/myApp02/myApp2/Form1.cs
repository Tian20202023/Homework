using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void equal_Click(object sender, EventArgs e)
        {
            double num1, num2, result = 0;

            double.TryParse(textBox1.Text,out num1);
            double.TryParse(textBox2.Text, out num2);          
            if (add.Checked)
            {
                result = num1 + num2;
            }
            else if (sub.Checked)
            {
                result = num1 - num2;
            }
            else if (mul.Checked)
            {
                result = num1 * num2;
            }
            else if (div.Checked)
            {
                result = num1 / num2;
            }
            res.Text = "结果: " + result.ToString();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
