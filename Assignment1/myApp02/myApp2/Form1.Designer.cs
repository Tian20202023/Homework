namespace myApp2
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.add = new System.Windows.Forms.RadioButton();
            this.mul = new System.Windows.Forms.RadioButton();
            this.sub = new System.Windows.Forms.RadioButton();
            this.div = new System.Windows.Forms.RadioButton();
            this.equal = new System.Windows.Forms.Button();
            this.res = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(67, 183);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(106, 28);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(364, 182);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 28);
            this.textBox2.TabIndex = 1;
            // 
            // add
            // 
            this.add.AutoSize = true;
            this.add.Location = new System.Drawing.Point(236, 99);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(60, 22);
            this.add.TabIndex = 2;
            this.add.TabStop = true;
            this.add.Text = "add";
            this.add.UseVisualStyleBackColor = true;
            // 
            // mul
            // 
            this.mul.AutoSize = true;
            this.mul.Location = new System.Drawing.Point(236, 225);
            this.mul.Name = "mul";
            this.mul.Size = new System.Drawing.Size(60, 22);
            this.mul.TabIndex = 3;
            this.mul.TabStop = true;
            this.mul.Text = "mul";
            this.mul.UseVisualStyleBackColor = true;
            // 
            // sub
            // 
            this.sub.AutoSize = true;
            this.sub.Location = new System.Drawing.Point(236, 162);
            this.sub.Name = "sub";
            this.sub.Size = new System.Drawing.Size(60, 22);
            this.sub.TabIndex = 4;
            this.sub.TabStop = true;
            this.sub.Text = "sub";
            this.sub.UseVisualStyleBackColor = true;
            // 
            // div
            // 
            this.div.AutoSize = true;
            this.div.Location = new System.Drawing.Point(236, 292);
            this.div.Name = "div";
            this.div.Size = new System.Drawing.Size(60, 22);
            this.div.TabIndex = 5;
            this.div.TabStop = true;
            this.div.Text = "div";
            this.div.UseVisualStyleBackColor = true;
            // 
            // equal
            // 
            this.equal.Location = new System.Drawing.Point(515, 164);
            this.equal.Name = "equal";
            this.equal.Size = new System.Drawing.Size(108, 75);
            this.equal.TabIndex = 6;
            this.equal.Text = "equal";
            this.equal.UseVisualStyleBackColor = true;
            this.equal.Click += new System.EventHandler(this.equal_Click);
            // 
            // res
            // 
            this.res.AutoSize = true;
            this.res.Location = new System.Drawing.Point(687, 193);
            this.res.Name = "res";
            this.res.Size = new System.Drawing.Size(35, 18);
            this.res.TabIndex = 7;
            this.res.Text = "res";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.res);
            this.Controls.Add(this.equal);
            this.Controls.Add(this.div);
            this.Controls.Add(this.sub);
            this.Controls.Add(this.mul);
            this.Controls.Add(this.add);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.RadioButton add;
        private System.Windows.Forms.RadioButton mul;
        private System.Windows.Forms.RadioButton sub;
        private System.Windows.Forms.RadioButton div;
        private System.Windows.Forms.Button equal;
        private System.Windows.Forms.Label res;
    }
}

