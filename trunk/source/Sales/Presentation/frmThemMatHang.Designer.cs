namespace Sales
{
    partial class frmThemMatHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThemMatHang));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSoLuongTon = new Sales.NumberTextBox(this.components);
            this.txtGia = new Sales.CurrencyTextBox(this.components);
            this.txtGiaBanLe = new Sales.CurrencyTextBox(this.components);
            this.txtGiaBanSi = new Sales.CurrencyTextBox(this.components);
            this.txtGiaMua = new Sales.CurrencyTextBox(this.components);
            this.chkHangNgungBan = new System.Windows.Forms.CheckBox();
            this.cboVAT = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnNhomHang = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDatLe = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDatSi = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMaVach = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDonViTinh = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboNhomHang = new System.Windows.Forms.ComboBox();
            this.cboXuatXu = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDienGiai = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTenHang = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMaHang = new System.Windows.Forms.TextBox();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.15888F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.84112F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(572, 535);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnThoat);
            this.panel2.Controls.Add(this.btnLuu);
            this.panel2.Location = new System.Drawing.Point(3, 480);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(566, 52);
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
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.txtSoLuongTon);
            this.panel1.Controls.Add(this.txtGia);
            this.panel1.Controls.Add(this.txtGiaBanLe);
            this.panel1.Controls.Add(this.txtGiaBanSi);
            this.panel1.Controls.Add(this.txtGiaMua);
            this.panel1.Controls.Add(this.chkHangNgungBan);
            this.panel1.Controls.Add(this.cboVAT);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnNhomHang);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtDatLe);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtDatSi);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtMaVach);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboDonViTinh);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cboNhomHang);
            this.panel1.Controls.Add(this.cboXuatXu);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtDienGiai);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtTenHang);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtMaHang);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(566, 471);
            this.panel1.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(52, 307);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(98, 19);
            this.label15.TabIndex = 45;
            this.label15.Text = "Số Lượng Tồn";
            // 
            // txtSoLuongTon
            // 
            this.txtSoLuongTon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSoLuongTon.Location = new System.Drawing.Point(165, 305);
            this.txtSoLuongTon.Name = "txtSoLuongTon";
            this.txtSoLuongTon.Size = new System.Drawing.Size(154, 26);
            this.txtSoLuongTon.TabIndex = 12;
            this.txtSoLuongTon.Text = "0";
            this.txtSoLuongTon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGia
            // 
            this.txtGia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGia.Location = new System.Drawing.Point(356, 304);
            this.txtGia.Name = "txtGia";
            this.txtGia.Size = new System.Drawing.Size(147, 26);
            this.txtGia.TabIndex = 12;
            this.txtGia.TabStop = false;
            this.txtGia.Text = "0";
            this.txtGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGia.Visible = false;
            // 
            // txtGiaBanLe
            // 
            this.txtGiaBanLe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGiaBanLe.Location = new System.Drawing.Point(166, 242);
            this.txtGiaBanLe.Name = "txtGiaBanLe";
            this.txtGiaBanLe.Size = new System.Drawing.Size(154, 26);
            this.txtGiaBanLe.TabIndex = 9;
            this.txtGiaBanLe.Text = "0";
            this.txtGiaBanLe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGiaBanLe.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtGiaBanLe_KeyDown);
            // 
            // txtGiaBanSi
            // 
            this.txtGiaBanSi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGiaBanSi.Location = new System.Drawing.Point(165, 209);
            this.txtGiaBanSi.Name = "txtGiaBanSi";
            this.txtGiaBanSi.Size = new System.Drawing.Size(154, 26);
            this.txtGiaBanSi.TabIndex = 7;
            this.txtGiaBanSi.Text = "0";
            this.txtGiaBanSi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGiaBanSi.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtGiaBanSi_KeyDown);
            // 
            // txtGiaMua
            // 
            this.txtGiaMua.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGiaMua.Location = new System.Drawing.Point(166, 177);
            this.txtGiaMua.Name = "txtGiaMua";
            this.txtGiaMua.Size = new System.Drawing.Size(154, 26);
            this.txtGiaMua.TabIndex = 6;
            this.txtGiaMua.Text = "0";
            this.txtGiaMua.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGiaMua.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtGiaMua_KeyDown);
            // 
            // chkHangNgungBan
            // 
            this.chkHangNgungBan.AutoSize = true;
            this.chkHangNgungBan.Location = new System.Drawing.Point(369, 341);
            this.chkHangNgungBan.Name = "chkHangNgungBan";
            this.chkHangNgungBan.Size = new System.Drawing.Size(134, 23);
            this.chkHangNgungBan.TabIndex = 14;
            this.chkHangNgungBan.Text = "Hàng Ngưng Bán";
            this.chkHangNgungBan.UseVisualStyleBackColor = true;
            // 
            // cboVAT
            // 
            this.cboVAT.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboVAT.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboVAT.FormattingEnabled = true;
            this.cboVAT.Items.AddRange(new object[] {
            "0%",
            "5%",
            "10%",
            "15%",
            "20%",
            "25%",
            "30%"});
            this.cboVAT.Location = new System.Drawing.Point(166, 339);
            this.cboVAT.Name = "cboVAT";
            this.cboVAT.Size = new System.Drawing.Size(154, 27);
            this.cboVAT.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(52, 343);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 19);
            this.label7.TabIndex = 44;
            this.label7.Text = "VAT (%)";
            // 
            // btnNhomHang
            // 
            this.btnNhomHang.Location = new System.Drawing.Point(438, 79);
            this.btnNhomHang.Name = "btnNhomHang";
            this.btnNhomHang.Size = new System.Drawing.Size(31, 27);
            this.btnNhomHang.TabIndex = 18;
            this.btnNhomHang.TabStop = false;
            this.btnNhomHang.Text = "+";
            this.btnNhomHang.UseVisualStyleBackColor = true;
            this.btnNhomHang.Click += new System.EventHandler(this.btnNhomHang_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(323, 307);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 19);
            this.label6.TabIndex = 41;
            this.label6.Text = "Giá";
            this.label6.Visible = false;
            // 
            // txtDatLe
            // 
            this.txtDatLe.Location = new System.Drawing.Point(356, 241);
            this.txtDatLe.Name = "txtDatLe";
            this.txtDatLe.Size = new System.Drawing.Size(57, 26);
            this.txtDatLe.TabIndex = 10;
            this.txtDatLe.TabStop = false;
            this.txtDatLe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDatLe.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDatLe_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(323, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 19);
            this.label5.TabIndex = 37;
            this.label5.Text = "Đạt";
            // 
            // txtDatSi
            // 
            this.txtDatSi.Location = new System.Drawing.Point(356, 210);
            this.txtDatSi.Name = "txtDatSi";
            this.txtDatSi.Size = new System.Drawing.Size(57, 26);
            this.txtDatSi.TabIndex = 8;
            this.txtDatSi.TabStop = false;
            this.txtDatSi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDatSi.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDatSi_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(323, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 19);
            this.label4.TabIndex = 35;
            this.label4.Text = "Đạt";
            // 
            // txtMaVach
            // 
            this.txtMaVach.Location = new System.Drawing.Point(166, 273);
            this.txtMaVach.Name = "txtMaVach";
            this.txtMaVach.Size = new System.Drawing.Size(154, 26);
            this.txtMaVach.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 277);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 19);
            this.label3.TabIndex = 33;
            this.label3.Text = "Mã Vạch";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 19);
            this.label2.TabIndex = 31;
            this.label2.Text = "Giá Bán Lẻ (*)";
            // 
            // cboDonViTinh
            // 
            this.cboDonViTinh.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDonViTinh.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDonViTinh.FormattingEnabled = true;
            this.cboDonViTinh.Location = new System.Drawing.Point(166, 144);
            this.cboDonViTinh.Name = "cboDonViTinh";
            this.cboDonViTinh.Size = new System.Drawing.Size(154, 27);
            this.cboDonViTinh.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 19);
            this.label1.TabIndex = 29;
            this.label1.Text = "ĐVT";
            // 
            // cboNhomHang
            // 
            this.cboNhomHang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboNhomHang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboNhomHang.FormattingEnabled = true;
            this.cboNhomHang.Location = new System.Drawing.Point(166, 78);
            this.cboNhomHang.Name = "cboNhomHang";
            this.cboNhomHang.Size = new System.Drawing.Size(266, 27);
            this.cboNhomHang.TabIndex = 3;
            // 
            // cboXuatXu
            // 
            this.cboXuatXu.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboXuatXu.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboXuatXu.FormattingEnabled = true;
            this.cboXuatXu.Location = new System.Drawing.Point(166, 111);
            this.cboXuatXu.Name = "cboXuatXu";
            this.cboXuatXu.Size = new System.Drawing.Size(154, 27);
            this.cboXuatXu.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(52, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 19);
            this.label8.TabIndex = 26;
            this.label8.Text = "Xuất Xứ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(52, 213);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 19);
            this.label9.TabIndex = 24;
            this.label9.Text = "Giá Bán Sỉ (*)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(52, 181);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 19);
            this.label10.TabIndex = 22;
            this.label10.Text = "Giá Mua (*)";
            // 
            // txtDienGiai
            // 
            this.txtDienGiai.Location = new System.Drawing.Point(166, 374);
            this.txtDienGiai.Multiline = true;
            this.txtDienGiai.Name = "txtDienGiai";
            this.txtDienGiai.Size = new System.Drawing.Size(337, 73);
            this.txtDienGiai.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(52, 378);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 19);
            this.label11.TabIndex = 20;
            this.label11.Text = "Diễn Giải";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(52, 83);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 19);
            this.label12.TabIndex = 18;
            this.label12.Text = "Nhóm Hàng";
            // 
            // txtTenHang
            // 
            this.txtTenHang.Location = new System.Drawing.Point(166, 47);
            this.txtTenHang.Name = "txtTenHang";
            this.txtTenHang.Size = new System.Drawing.Size(337, 26);
            this.txtTenHang.TabIndex = 2;
            this.txtTenHang.Validating += new System.ComponentModel.CancelEventHandler(this.txtTenHang_Validating);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(52, 51);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 19);
            this.label13.TabIndex = 16;
            this.label13.Text = "Tên Hàng (*)";
            // 
            // txtMaHang
            // 
            this.txtMaHang.Enabled = false;
            this.txtMaHang.Location = new System.Drawing.Point(166, 15);
            this.txtMaHang.Name = "txtMaHang";
            this.txtMaHang.Size = new System.Drawing.Size(154, 26);
            this.txtMaHang.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(52, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 19);
            this.label14.TabIndex = 14;
            this.label14.Text = "Mã Hàng (*)";
            // 
            // frmThemMatHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 535);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThemMatHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thong Tin Mat Hang";
            this.Load += new System.EventHandler(this.frmThemMatHang_Load);
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
        private System.Windows.Forms.ComboBox cboXuatXu;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDienGiai;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTenHang;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMaHang;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboNhomHang;
        private System.Windows.Forms.ComboBox cboDonViTinh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDatLe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDatSi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMaVach;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNhomHang;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkHangNgungBan;
        private System.Windows.Forms.ComboBox cboVAT;
        private System.Windows.Forms.Label label7;
        private CurrencyTextBox txtGiaMua;
        private CurrencyTextBox txtGiaBanLe;
        private CurrencyTextBox txtGiaBanSi;
        private CurrencyTextBox txtGia;
        private NumberTextBox txtSoLuongTon;
        private System.Windows.Forms.Label label15;
    }
}