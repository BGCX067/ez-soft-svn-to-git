namespace Sales
{
    partial class frmThemKhachHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThemKhachHang));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtNoDauKy = new Sales.CurrencyTextBox(this.components);
            this.cboBaoGia = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTenNguoiLienHe = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSoMayFax = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkNoDauKy = new System.Windows.Forms.CheckBox();
            this.txtChietKhau = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaSoThue = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTenKhachHang = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMaKhachHang = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.95238F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.04762F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(587, 410);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnThoat);
            this.panel2.Controls.Add(this.btnLuu);
            this.panel2.Location = new System.Drawing.Point(3, 355);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(581, 52);
            this.panel2.TabIndex = 1;
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(452, 17);
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
            this.btnLuu.Location = new System.Drawing.Point(371, 17);
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
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(581, 346);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtNoDauKy);
            this.panel3.Controls.Add(this.cboBaoGia);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtTenNguoiLienHe);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.txtSoMayFax);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.chkNoDauKy);
            this.panel3.Controls.Add(this.txtChietKhau);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtDienThoai);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtMaSoThue);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txtDiaChi);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.txtTenKhachHang);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.txtMaKhachHang);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(581, 346);
            this.panel3.TabIndex = 0;
            // 
            // txtNoDauKy
            // 
            this.txtNoDauKy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoDauKy.Location = new System.Drawing.Point(191, 305);
            this.txtNoDauKy.Name = "txtNoDauKy";
            this.txtNoDauKy.Size = new System.Drawing.Size(152, 26);
            this.txtNoDauKy.TabIndex = 76;
            this.txtNoDauKy.Text = "0";
            this.txtNoDauKy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNoDauKy.Visible = false;
            // 
            // cboBaoGia
            // 
            this.cboBaoGia.FormattingEnabled = true;
            this.cboBaoGia.Items.AddRange(new object[] {
            "Giá bán sỉ",
            "Giá bán lẻ"});
            this.cboBaoGia.Location = new System.Drawing.Point(191, 241);
            this.cboBaoGia.Name = "cboBaoGia";
            this.cboBaoGia.Size = new System.Drawing.Size(154, 27);
            this.cboBaoGia.TabIndex = 72;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(53, 245);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 19);
            this.label5.TabIndex = 71;
            this.label5.Text = "Báo Giá";
            // 
            // txtTenNguoiLienHe
            // 
            this.txtTenNguoiLienHe.Location = new System.Drawing.Point(191, 208);
            this.txtTenNguoiLienHe.Name = "txtTenNguoiLienHe";
            this.txtTenNguoiLienHe.Size = new System.Drawing.Size(337, 26);
            this.txtTenNguoiLienHe.TabIndex = 70;
            this.txtTenNguoiLienHe.Validating += new System.ComponentModel.CancelEventHandler(this.txtTenNguoiLienHe_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 19);
            this.label1.TabIndex = 69;
            this.label1.Text = "Tên Người Liên Hệ";
            // 
            // txtSoMayFax
            // 
            this.txtSoMayFax.Location = new System.Drawing.Point(191, 176);
            this.txtSoMayFax.Name = "txtSoMayFax";
            this.txtSoMayFax.Size = new System.Drawing.Size(154, 26);
            this.txtSoMayFax.TabIndex = 68;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 19);
            this.label4.TabIndex = 67;
            this.label4.Text = "Số Máy Fax";
            // 
            // chkNoDauKy
            // 
            this.chkNoDauKy.AutoSize = true;
            this.chkNoDauKy.Location = new System.Drawing.Point(52, 308);
            this.chkNoDauKy.Name = "chkNoDauKy";
            this.chkNoDauKy.Size = new System.Drawing.Size(100, 23);
            this.chkNoDauKy.TabIndex = 66;
            this.chkNoDauKy.Text = "Nợ Đầu Kỳ";
            this.chkNoDauKy.UseVisualStyleBackColor = true;
            this.chkNoDauKy.Visible = false;
            // 
            // txtChietKhau
            // 
            this.txtChietKhau.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChietKhau.Location = new System.Drawing.Point(191, 274);
            this.txtChietKhau.Name = "txtChietKhau";
            this.txtChietKhau.Size = new System.Drawing.Size(154, 26);
            this.txtChietKhau.TabIndex = 65;
            this.txtChietKhau.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtChietKhau.Validating += new System.ComponentModel.CancelEventHandler(this.txtChietKhau_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 278);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 19);
            this.label3.TabIndex = 64;
            this.label3.Text = "Chiết Khấu";
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.Location = new System.Drawing.Point(191, 144);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(154, 26);
            this.txtDienThoai.TabIndex = 63;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 19);
            this.label2.TabIndex = 62;
            this.label2.Text = "Điện Thoại";
            // 
            // txtMaSoThue
            // 
            this.txtMaSoThue.Location = new System.Drawing.Point(191, 112);
            this.txtMaSoThue.Name = "txtMaSoThue";
            this.txtMaSoThue.Size = new System.Drawing.Size(154, 26);
            this.txtMaSoThue.TabIndex = 61;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(53, 116);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 19);
            this.label9.TabIndex = 60;
            this.label9.Text = "Mã Số Thuế";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(191, 80);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(336, 26);
            this.txtDiaChi.TabIndex = 59;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(53, 84);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 19);
            this.label10.TabIndex = 58;
            this.label10.Text = "Địa Chỉ (*)";
            // 
            // txtTenKhachHang
            // 
            this.txtTenKhachHang.Location = new System.Drawing.Point(191, 48);
            this.txtTenKhachHang.Name = "txtTenKhachHang";
            this.txtTenKhachHang.Size = new System.Drawing.Size(337, 26);
            this.txtTenKhachHang.TabIndex = 57;
            this.txtTenKhachHang.Validating += new System.ComponentModel.CancelEventHandler(this.txtTenKhachHang_Validating);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(53, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(134, 19);
            this.label13.TabIndex = 56;
            this.label13.Text = "Tên Khách Hàng (*)";
            // 
            // txtMaKhachHang
            // 
            this.txtMaKhachHang.Location = new System.Drawing.Point(191, 16);
            this.txtMaKhachHang.Name = "txtMaKhachHang";
            this.txtMaKhachHang.ReadOnly = true;
            this.txtMaKhachHang.Size = new System.Drawing.Size(154, 26);
            this.txtMaKhachHang.TabIndex = 55;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(53, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(132, 19);
            this.label14.TabIndex = 54;
            this.label14.Text = "Mã Khách Hàng (*)";
            // 
            // frmThemKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 410);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThemKhachHang";
            this.Text = "Them Khach Hang";
            this.Load += new System.EventHandler(this.frmThemKhachHang_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cboBaoGia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTenNguoiLienHe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSoMayFax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkNoDauKy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaSoThue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTenKhachHang;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMaKhachHang;
        private System.Windows.Forms.Label label14;
        private CurrencyTextBox txtNoDauKy;
        private System.Windows.Forms.TextBox txtChietKhau;
    }
}