namespace Sales
{
    partial class frmMatHangBan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMatHangBan));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radioTheoTen = new System.Windows.Forms.RadioButton();
            this.txtMatHang = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboNhomHang = new System.Windows.Forms.ComboBox();
            this.radioTheoMa = new System.Windows.Forms.RadioButton();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grdvDSMatHang = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XuatXu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiaBanSi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiaBanLe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DienGiai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgungBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnTaoMoi = new System.Windows.Forms.Button();
            this.btnInRa = new System.Windows.Forms.Button();
            this.chkInGiaLe = new System.Windows.Forms.CheckBox();
            this.chkInGiaSi = new System.Windows.Forms.CheckBox();
            this.btnSuaLai = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.imglstMenu = new System.Windows.Forms.ImageList(this.components);
            this.pnlTitle.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdvDSMatHang)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.Margin = new System.Windows.Forms.Padding(2);
            this.pnlTitle.Size = new System.Drawing.Size(797, 18);
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(2, 2);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Size = new System.Drawing.Size(87, 16);
            this.lblTitle.Text = "MẶT HÀNG";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.04517F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.30303F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.86869F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.4899F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.9899F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.10101F));
            this.tableLayoutPanel1.Controls.Add(this.radioTheoTen, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtMatHang, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboNhomHang, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.radioTheoMa, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnTimKiem, 5, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 20);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 30);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // radioTheoTen
            // 
            this.radioTheoTen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radioTheoTen.AutoSize = true;
            this.radioTheoTen.Checked = true;
            this.radioTheoTen.Location = new System.Drawing.Point(337, 2);
            this.radioTheoTen.Margin = new System.Windows.Forms.Padding(2);
            this.radioTheoTen.Name = "radioTheoTen";
            this.radioTheoTen.Size = new System.Drawing.Size(90, 26);
            this.radioTheoTen.TabIndex = 2;
            this.radioTheoTen.TabStop = true;
            this.radioTheoTen.Text = "Theo Tên";
            this.radioTheoTen.UseVisualStyleBackColor = true;
            // 
            // txtMatHang
            // 
            this.txtMatHang.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMatHang.Location = new System.Drawing.Point(97, 2);
            this.txtMatHang.Margin = new System.Windows.Forms.Padding(2);
            this.txtMatHang.Name = "txtMatHang";
            this.txtMatHang.Size = new System.Drawing.Size(236, 20);
            this.txtMatHang.TabIndex = 4;
            this.txtMatHang.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMatHang_KeyUp);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 30);
            this.label1.TabIndex = 8;
            this.label1.Text = "Mặt Hàng";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboNhomHang
            // 
            this.cboNhomHang.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboNhomHang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboNhomHang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboNhomHang.FormattingEnabled = true;
            this.cboNhomHang.Location = new System.Drawing.Point(522, 2);
            this.cboNhomHang.Margin = new System.Windows.Forms.Padding(2);
            this.cboNhomHang.Name = "cboNhomHang";
            this.cboNhomHang.Size = new System.Drawing.Size(186, 22);
            this.cboNhomHang.TabIndex = 1;
            // 
            // radioTheoMa
            // 
            this.radioTheoMa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.radioTheoMa.AutoSize = true;
            this.radioTheoMa.Location = new System.Drawing.Point(431, 2);
            this.radioTheoMa.Margin = new System.Windows.Forms.Padding(2);
            this.radioTheoMa.Name = "radioTheoMa";
            this.radioTheoMa.Size = new System.Drawing.Size(87, 26);
            this.radioTheoMa.TabIndex = 3;
            this.radioTheoMa.Text = "Theo Mã";
            this.radioTheoMa.UseVisualStyleBackColor = true;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimKiem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Location = new System.Drawing.Point(712, 2);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(2);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(78, 26);
            this.btnTimKiem.TabIndex = 5;
            this.btnTimKiem.Text = "Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.grdvDSMatHang, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 52);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(792, 446);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // grdvDSMatHang
            // 
            this.grdvDSMatHang.AllowUserToAddRows = false;
            this.grdvDSMatHang.AllowUserToDeleteRows = false;
            this.grdvDSMatHang.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdvDSMatHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdvDSMatHang.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grdvDSMatHang.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.grdvDSMatHang.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdvDSMatHang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdvDSMatHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdvDSMatHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.MaHang,
            this.TenHang,
            this.XuatXu,
            this.DonViTinh,
            this.GiaBanSi,
            this.GiaBanLe,
            this.VAT,
            this.DienGiai,
            this.NgungBan});
            this.grdvDSMatHang.Location = new System.Drawing.Point(2, 2);
            this.grdvDSMatHang.Margin = new System.Windows.Forms.Padding(2);
            this.grdvDSMatHang.Name = "grdvDSMatHang";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdvDSMatHang.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.grdvDSMatHang.RowHeadersVisible = false;
            this.grdvDSMatHang.RowHeadersWidth = 20;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdvDSMatHang.RowsDefaultCellStyle = dataGridViewCellStyle11;
            this.grdvDSMatHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdvDSMatHang.Size = new System.Drawing.Size(788, 442);
            this.grdvDSMatHang.TabIndex = 6;
            this.grdvDSMatHang.TabStop = false;
            // 
            // STT
            // 
            this.STT.FillWeight = 40F;
            this.STT.HeaderText = "STT";
            this.STT.Name = "STT";
            this.STT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.STT.Width = 51;
            // 
            // MaHang
            // 
            this.MaHang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MaHang.FillWeight = 40.48997F;
            this.MaHang.HeaderText = "Mã Hàng";
            this.MaHang.Name = "MaHang";
            // 
            // TenHang
            // 
            this.TenHang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.TenHang.DefaultCellStyle = dataGridViewCellStyle2;
            this.TenHang.FillWeight = 80.97993F;
            this.TenHang.HeaderText = "Tên Hàng";
            this.TenHang.Name = "TenHang";
            // 
            // XuatXu
            // 
            this.XuatXu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.XuatXu.DefaultCellStyle = dataGridViewCellStyle3;
            this.XuatXu.FillWeight = 40.48997F;
            this.XuatXu.HeaderText = "Xuất Xứ";
            this.XuatXu.Name = "XuatXu";
            // 
            // DonViTinh
            // 
            this.DonViTinh.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.DonViTinh.DefaultCellStyle = dataGridViewCellStyle4;
            this.DonViTinh.FillWeight = 40.48997F;
            this.DonViTinh.HeaderText = "Đơn Vị Tính";
            this.DonViTinh.Name = "DonViTinh";
            // 
            // GiaBanSi
            // 
            this.GiaBanSi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C0";
            dataGridViewCellStyle5.NullValue = null;
            this.GiaBanSi.DefaultCellStyle = dataGridViewCellStyle5;
            this.GiaBanSi.FillWeight = 40.48997F;
            this.GiaBanSi.HeaderText = "Giá Bán Sỉ";
            this.GiaBanSi.Name = "GiaBanSi";
            // 
            // GiaBanLe
            // 
            this.GiaBanLe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "C0";
            dataGridViewCellStyle6.NullValue = null;
            this.GiaBanLe.DefaultCellStyle = dataGridViewCellStyle6;
            this.GiaBanLe.FillWeight = 40.48997F;
            this.GiaBanLe.HeaderText = "Giá Bán Lẻ";
            this.GiaBanLe.Name = "GiaBanLe";
            // 
            // VAT
            // 
            this.VAT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.VAT.DefaultCellStyle = dataGridViewCellStyle7;
            this.VAT.FillWeight = 16.19599F;
            this.VAT.HeaderText = "VAT";
            this.VAT.Name = "VAT";
            // 
            // DienGiai
            // 
            this.DienGiai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.DienGiai.DefaultCellStyle = dataGridViewCellStyle8;
            this.DienGiai.FillWeight = 40.48997F;
            this.DienGiai.HeaderText = "Diễn Giải";
            this.DienGiai.Name = "DienGiai";
            // 
            // NgungBan
            // 
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NgungBan.DefaultCellStyle = dataGridViewCellStyle9;
            this.NgungBan.HeaderText = "Trạng Thái";
            this.NgungBan.Name = "NgungBan";
            this.NgungBan.Width = 83;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 8;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 91F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel3.Controls.Add(this.btnThoat, 7, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnTaoMoi, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnInRa, 6, 1);
            this.tableLayoutPanel3.Controls.Add(this.chkInGiaLe, 6, 0);
            this.tableLayoutPanel3.Controls.Add(this.chkInGiaSi, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnSuaLai, 5, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnXoa, 4, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 500);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(792, 64);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // btnThoat
            // 
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoat.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(707, 24);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(83, 38);
            this.btnThoat.TabIndex = 13;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnTaoMoi
            // 
            this.btnTaoMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTaoMoi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaoMoi.Location = new System.Drawing.Point(2, 24);
            this.btnTaoMoi.Margin = new System.Windows.Forms.Padding(2);
            this.btnTaoMoi.Name = "btnTaoMoi";
            this.btnTaoMoi.Size = new System.Drawing.Size(81, 38);
            this.btnTaoMoi.TabIndex = 7;
            this.btnTaoMoi.Text = "Tạo Mới";
            this.btnTaoMoi.UseVisualStyleBackColor = true;
            this.btnTaoMoi.Visible = false;
            this.btnTaoMoi.Click += new System.EventHandler(this.btnTaoMoi_Click);
            // 
            // btnInRa
            // 
            this.btnInRa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInRa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInRa.Location = new System.Drawing.Point(620, 24);
            this.btnInRa.Margin = new System.Windows.Forms.Padding(2);
            this.btnInRa.Name = "btnInRa";
            this.btnInRa.Size = new System.Drawing.Size(83, 38);
            this.btnInRa.TabIndex = 12;
            this.btnInRa.Text = "In Ra";
            this.btnInRa.UseVisualStyleBackColor = true;
            this.btnInRa.Click += new System.EventHandler(this.btnInRa_Click);
            // 
            // chkInGiaLe
            // 
            this.chkInGiaLe.AutoSize = true;
            this.chkInGiaLe.Checked = true;
            this.chkInGiaLe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInGiaLe.Location = new System.Drawing.Point(621, 3);
            this.chkInGiaLe.Name = "chkInGiaLe";
            this.chkInGiaLe.Size = new System.Drawing.Size(68, 16);
            this.chkInGiaLe.TabIndex = 10;
            this.chkInGiaLe.Text = "In Giá Lẻ";
            this.chkInGiaLe.UseVisualStyleBackColor = true;
            // 
            // chkInGiaSi
            // 
            this.chkInGiaSi.AutoSize = true;
            this.chkInGiaSi.Location = new System.Drawing.Point(541, 3);
            this.chkInGiaSi.Name = "chkInGiaSi";
            this.chkInGiaSi.Size = new System.Drawing.Size(65, 16);
            this.chkInGiaSi.TabIndex = 11;
            this.chkInGiaSi.Text = "In Giá Sỉ";
            this.chkInGiaSi.UseVisualStyleBackColor = true;
            // 
            // btnSuaLai
            // 
            this.btnSuaLai.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSuaLai.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuaLai.Location = new System.Drawing.Point(540, 24);
            this.btnSuaLai.Margin = new System.Windows.Forms.Padding(2);
            this.btnSuaLai.Name = "btnSuaLai";
            this.btnSuaLai.Size = new System.Drawing.Size(76, 38);
            this.btnSuaLai.TabIndex = 9;
            this.btnSuaLai.Text = "Sửa Lại";
            this.btnSuaLai.UseVisualStyleBackColor = true;
            this.btnSuaLai.Visible = false;
            this.btnSuaLai.Click += new System.EventHandler(this.btnSuaLai_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoa.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(449, 24);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(87, 38);
            this.btnXoa.TabIndex = 8;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Visible = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // imglstMenu
            // 
            this.imglstMenu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstMenu.ImageStream")));
            this.imglstMenu.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstMenu.Images.SetKeyName(0, "import.png");
            this.imglstMenu.Images.SetKeyName(1, "alarm.png");
            this.imglstMenu.Images.SetKeyName(2, "animation.png");
            this.imglstMenu.Images.SetKeyName(3, "archive_add.png");
            // 
            // frmMatHangBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "frmMatHangBan";
            this.Text = "MAT HANG";
            this.Load += new System.EventHandler(this.frmMatHang_Load);
            this.Controls.SetChildIndex(this.tableLayoutPanel2, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel3, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdvDSMatHang)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMatHang;
        private System.Windows.Forms.RadioButton radioTheoTen;
        private System.Windows.Forms.RadioButton radioTheoMa;
        private System.Windows.Forms.ComboBox cboNhomHang;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView grdvDSMatHang;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnTaoMoi;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnInRa;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSuaLai;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.CheckBox chkInGiaSi;
        private System.Windows.Forms.CheckBox chkInGiaLe;
        private System.Windows.Forms.ImageList imglstMenu;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn XuatXu;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiaBanSi;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiaBanLe;
        private System.Windows.Forms.DataGridViewTextBoxColumn VAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DienGiai;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgungBan;

    }
}