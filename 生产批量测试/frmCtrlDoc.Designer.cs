namespace 生产批量测试
{
    partial class frmCtrlDoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCtrlDoc));
            this.btAdd = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.txtAmmeterAdr = new System.Windows.Forms.TextBox();
            this.lbAmmeterAdr = new System.Windows.Forms.Label();
            this.txtCollectorAdr = new System.Windows.Forms.TextBox();
            this.lbCollectorAdr = new System.Windows.Forms.Label();
            this.chkOption = new System.Windows.Forms.CheckBox();
            this.lbAmmeterNum = new System.Windows.Forms.Label();
            this.txtAmmeterNum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(40, 129);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(72, 26);
            this.btAdd.TabIndex = 2;
            this.btAdd.Text = "添加";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(137, 129);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(72, 26);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // txtAmmeterAdr
            // 
            this.txtAmmeterAdr.Location = new System.Drawing.Point(123, 73);
            this.txtAmmeterAdr.Name = "txtAmmeterAdr";
            this.txtAmmeterAdr.Size = new System.Drawing.Size(86, 21);
            this.txtAmmeterAdr.TabIndex = 0;
            this.txtAmmeterAdr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAmmeterAdr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmmeterAdr_KeyPress);
            // 
            // lbAmmeterAdr
            // 
            this.lbAmmeterAdr.AutoSize = true;
            this.lbAmmeterAdr.Location = new System.Drawing.Point(14, 76);
            this.lbAmmeterAdr.Name = "lbAmmeterAdr";
            this.lbAmmeterAdr.Size = new System.Drawing.Size(89, 12);
            this.lbAmmeterAdr.TabIndex = 10;
            this.lbAmmeterAdr.Text = "电能表起始地址";
            // 
            // txtCollectorAdr
            // 
            this.txtCollectorAdr.Location = new System.Drawing.Point(123, 41);
            this.txtCollectorAdr.Name = "txtCollectorAdr";
            this.txtCollectorAdr.Size = new System.Drawing.Size(86, 21);
            this.txtCollectorAdr.TabIndex = 5;
            this.txtCollectorAdr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCollectorAdr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCollectorAdr_KeyPress);
            // 
            // lbCollectorAdr
            // 
            this.lbCollectorAdr.AutoSize = true;
            this.lbCollectorAdr.Location = new System.Drawing.Point(38, 47);
            this.lbCollectorAdr.Name = "lbCollectorAdr";
            this.lbCollectorAdr.Size = new System.Drawing.Size(65, 12);
            this.lbCollectorAdr.TabIndex = 7;
            this.lbCollectorAdr.Text = "采集器地址";
            // 
            // chkOption
            // 
            this.chkOption.AutoSize = true;
            this.chkOption.Location = new System.Drawing.Point(31, 20);
            this.chkOption.Name = "chkOption";
            this.chkOption.Size = new System.Drawing.Size(72, 16);
            this.chkOption.TabIndex = 4;
            this.chkOption.Text = "有采集器";
            this.chkOption.UseVisualStyleBackColor = true;
            this.chkOption.CheckedChanged += new System.EventHandler(this.chkOption_CheckedChanged);
            // 
            // lbAmmeterNum
            // 
            this.lbAmmeterNum.AutoSize = true;
            this.lbAmmeterNum.Location = new System.Drawing.Point(38, 102);
            this.lbAmmeterNum.Name = "lbAmmeterNum";
            this.lbAmmeterNum.Size = new System.Drawing.Size(65, 12);
            this.lbAmmeterNum.TabIndex = 10;
            this.lbAmmeterNum.Text = "电能表个数";
            this.lbAmmeterNum.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtAmmeterNum
            // 
            this.txtAmmeterNum.Location = new System.Drawing.Point(123, 99);
            this.txtAmmeterNum.Name = "txtAmmeterNum";
            this.txtAmmeterNum.Size = new System.Drawing.Size(86, 21);
            this.txtAmmeterNum.TabIndex = 1;
            this.txtAmmeterNum.Text = "1";
            this.txtAmmeterNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAmmeterNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmmeterNum_KeyPress);
            // 
            // frmCtrlDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 171);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.txtAmmeterNum);
            this.Controls.Add(this.txtAmmeterAdr);
            this.Controls.Add(this.lbAmmeterNum);
            this.Controls.Add(this.lbAmmeterAdr);
            this.Controls.Add(this.txtCollectorAdr);
            this.Controls.Add(this.lbCollectorAdr);
            this.Controls.Add(this.chkOption);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCtrlDoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加档案";
            this.Load += new System.EventHandler(this.frmCtrlDoc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.TextBox txtAmmeterAdr;
        private System.Windows.Forms.Label lbAmmeterAdr;
        private System.Windows.Forms.TextBox txtCollectorAdr;
        private System.Windows.Forms.Label lbCollectorAdr;
        private System.Windows.Forms.CheckBox chkOption;
        private System.Windows.Forms.Label lbAmmeterNum;
        private System.Windows.Forms.TextBox txtAmmeterNum;
    }
}