namespace Sales
{
    partial class frmThemNhaCungCap
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThemNhaCungCap));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtNoDauKy = new Sales.CurrencyTextBox(this.components);
            this.txtTenNguoiLienHe = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSoMayFax = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkNoDauKy = new System.Windows.Forms.CheckBox();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaSoThue = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTenNhaCungCap = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMaNhaCungCap = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.57226F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.42775F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(596, 346);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnThoat);
            this.panel2.Controls.Add(this.btnLuu);
            this.panel2.Location = new System.Drawing.Point(3, 305);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(590, 38);
            this.panel2.TabIndex = 1;
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(492, 6);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 28);
            this.btnThoat.TabIndex = 17;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoi_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(411, 6);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 28);
            this.btnLuu.TabIndex = 16;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txtNoDauKy);
            this.panel1.Controls.Add(this.txtTenNguoiLienHe);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtSoMayFax);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.chkNoDauKy);
            this.panel1.Controls.Add(this.txtDienThoai);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtMaSoThue);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtDiaChi);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtTenNhaCungCap);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtMaNhaCungCap);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 296);
            this.panel1.TabIndex = 0;
            // 
            // txtNoDauKy
            // 
            this.txtNoDauKy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoDauKy.Location = new System.Drawing.Point(201, 264);
            this.txtNoDauKy.Name = "txtNoDauKy";
            this.txtNoDauKy.Size = new System.Drawing.Size(152, 26);
            this.txtNoDauKy.TabIndex = 75;
            this.txtNoDauKy.Text = "0";
            this.txtNoDauKy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNoDauKy.Visible = false;
            // 
            // txtTenNguoiLienHe
            // 
            this.txtTenNguoiLienHe.Location = new System.Drawing.Point(201, 232);
            this.txtTenNguoiLienHe.Name = "txtTenNguoiLienHe";
            this.txtTenNguoiLienHe.Size = new System.Drawing.Size(337, 26);
            this.txtTenNguoiLienHe.TabIndex = 50;
            this.txtTenNguoiLienHe.Validating += new System.ComponentModel.CancelEventHandler(this.txtTenNguoiLienHe_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 19);
            this.label1.TabIndex = 49;
            this.label1.Text = "Tên Người Liên Hệ";
            // 
            // txtSoMayFax
            // 
            this.txtSoMayFax.Location = new System.Drawing.Point(201, 200);
            this.txtSoMayFax.Name = "txtSoMayFax";
            this.txtSoMayFax.Size = new System.Drawing.Size(154, 26);
            this.txtSoMayFax.TabIndex = 48;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 19);
            this.label4.TabIndex = 47;
            this.label4.Text = "Số Máy Fax";
            // 
            // chkNoDauKy
            // 
            this.chkNoDauKy.AutoSize = true;
            this.chkNoDauKy.Location = new System.Drawing.Point(51, 267);
            this.chkNoDauKy.Name = "chkNoDauKy";
            this.chkNoDauKy.Size = new System.Drawing.Size(100, 23);
            this.chkNoDauKy.TabIndex = 39;
            this.chkNoDauKy.Text = "Nợ Đầu Kỳ";
            this.chkNoDauKy.UseVisualStyleBackColor = true;
            this.chkNoDauKy.Visible = false;
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.Location = new System.Drawing.Point(201, 168);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(154, 26);
            this.txtDienThoai.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 19);
            this.label2.TabIndex = 31;
            this.label2.Text = "Điện Thoại";
            // 
            // txtMaSoThue
            // 
            this.txtMaSoThue.Location = new System.Drawing.Point(201, 136);
            this.txtMaSoThue.Name = "txtMaSoThue";
            this.txtMaSoThue.Size = new System.Drawing.Size(154, 26);
            this.txtMaSoThue.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(52, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 19);
            this.label9.TabIndex = 24;
            this.label9.Text = "Mã Số Thuế";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(201, 79);
            this.txtDiaChi.Multiline = true;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(337, 51);
            this.txtDiaChi.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(52, 83);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 19);
            this.label10.TabIndex = 22;
            this.label10.Text = "Địa Chỉ (*)";
            // 
            // txtTenNhaCungCap
            // 
            this.txtTenNhaCungCap.Location = new System.Drawing.Point(201, 47);
            this.txtTenNhaCungCap.Name = "txtTenNhaCungCap";
            this.txtTenNhaCungCap.Size = new System.Drawing.Size(337, 26);
            this.txtTenNhaCungCap.TabIndex = 17;
            this.txtTenNhaCungCap.Validating += new System.ComponentModel.CancelEventHandler(this.txtTenNhaCungCap_Validating);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(52, 51);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(150, 19);
            this.label13.TabIndex = 16;
            this.label13.Text = "Tên Nhà Cung Cấp (*)";
            // 
            // txtMaNhaCungCap
            // 
            this.txtMaNhaCungCap.Enabled = false;
            this.txtMaNhaCungCap.Location = new System.Drawing.Point(201, 15);
            this.txtMaNhaCungCap.Name = "txtMaNhaCungCap";
            this.txtMaNhaCungCap.Size = new System.Drawing.Size(154, 26);
            this.txtMaNhaCungCap.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(52, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(148, 19);
            this.label14.TabIndex = 14;
            this.label14.Text = "Mã Nhà Cung Cấp (*)";
            // 
            // frmThemNhaCungCap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 346);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThemNhaCungCap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them Nha Cung Cap";
            this.Load += new System.EventHandler(this.frmThemNhaCungCap_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtTenNguoiLienHe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSoMayFax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkNoDauKy;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaSoThue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTenNhaCungCap;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMaNhaCungCap;
        private System.Windows.Forms.Label label14;
        private CurrencyTextBox txtNoDauKy;
    }
}