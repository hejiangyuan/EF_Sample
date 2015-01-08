namespace WindowsFormsApplication9
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dg = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnStateTest = new System.Windows.Forms.Button();
            this.btnHunhe = new System.Windows.Forms.Button();
            this.btnSpeedTest = new System.Windows.Forms.Button();
            this.btnTransactionTimeoutTest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(70, 66);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 21);
            this.txtName.TabIndex = 0;
            this.txtName.Text = "ttt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "名称：";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(70, 33);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(100, 21);
            this.txtCode.TabIndex = 0;
            this.txtCode.Text = "tttt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "编码：";
            // 
            // dg
            // 
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Location = new System.Drawing.Point(25, 117);
            this.dg.Name = "dg";
            this.dg.RowTemplate.Height = 23;
            this.dg.Size = new System.Drawing.Size(590, 271);
            this.dg.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(197, 66);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnStateTest
            // 
            this.btnStateTest.Location = new System.Drawing.Point(307, 66);
            this.btnStateTest.Name = "btnStateTest";
            this.btnStateTest.Size = new System.Drawing.Size(75, 23);
            this.btnStateTest.TabIndex = 4;
            this.btnStateTest.Text = "StateTest";
            this.btnStateTest.UseVisualStyleBackColor = true;
            this.btnStateTest.Click += new System.EventHandler(this.btnStateTest_Click);
            // 
            // btnHunhe
            // 
            this.btnHunhe.Location = new System.Drawing.Point(417, 65);
            this.btnHunhe.Name = "btnHunhe";
            this.btnHunhe.Size = new System.Drawing.Size(75, 23);
            this.btnHunhe.TabIndex = 5;
            this.btnHunhe.Text = "混合事务";
            this.btnHunhe.UseVisualStyleBackColor = true;
            this.btnHunhe.Click += new System.EventHandler(this.btnHunhe_Click);
            // 
            // btnSpeedTest
            // 
            this.btnSpeedTest.Location = new System.Drawing.Point(526, 65);
            this.btnSpeedTest.Name = "btnSpeedTest";
            this.btnSpeedTest.Size = new System.Drawing.Size(75, 23);
            this.btnSpeedTest.TabIndex = 6;
            this.btnSpeedTest.Text = "速度比较";
            this.btnSpeedTest.UseVisualStyleBackColor = true;
            this.btnSpeedTest.Click += new System.EventHandler(this.btnSpeedTest_Click);
            // 
            // btnTransactionTimeoutTest
            // 
            this.btnTransactionTimeoutTest.Location = new System.Drawing.Point(631, 63);
            this.btnTransactionTimeoutTest.Name = "btnTransactionTimeoutTest";
            this.btnTransactionTimeoutTest.Size = new System.Drawing.Size(87, 23);
            this.btnTransactionTimeoutTest.TabIndex = 7;
            this.btnTransactionTimeoutTest.Text = "事务超时测试";
            this.btnTransactionTimeoutTest.UseVisualStyleBackColor = true;
            this.btnTransactionTimeoutTest.Click += new System.EventHandler(this.btnTransactionTimeoutTest_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 589);
            this.Controls.Add(this.btnTransactionTimeoutTest);
            this.Controls.Add(this.btnSpeedTest);
            this.Controls.Add(this.btnHunhe);
            this.Controls.Add(this.btnStateTest);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtName);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnStateTest;
        private System.Windows.Forms.Button btnHunhe;
        private System.Windows.Forms.Button btnSpeedTest;
        private System.Windows.Forms.Button btnTransactionTimeoutTest;

    }
}