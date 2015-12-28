using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sales.Reports;

namespace Sales
{
    public partial class frmChiTietHangNhap : frmTemplete
    {

        #region Thuộc tính
        private clsPhieuNhapBUS PhieuNhapBus = new clsPhieuNhapBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        #endregion

        public frmChiTietHangNhap()
        {
            InitializeComponent();
        }

        private void frmChiTietHangNhap_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboNhaCungCap();
                KhoiTaoComboMatHang();
                dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpDenNgay.Value = DateTime.Now;
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Phím tắt
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Thoát 
            if (keyData == Keys.Escape)
            {
                DongCuaSo();
            }

            //thông tin 
            if (keyData == (Keys.Control | Keys.I))
            {
                In();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void DongCuaSo()
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Nhà cung cấp
        //Lấy danh sách nhà cung cấp
        private void KhoiTaoComboNhaCungCap()
        {
            //Load combo nhom hang
            DataTable BangNhaCungCap = new clsNhaCungCapBUS().LayBang();
            if (BangNhaCungCap.Rows.Count > 0)
            {
                //Đưa dòng thứ 0 vào vị trí cuối cùng và sau đó gán lại dòng thứ ko là tất cả
                DataRow DongTam = BangNhaCungCap.NewRow();
                DongTam["MaNhaCungCap"] = BangNhaCungCap.Rows[0]["MaNhaCungCap"];
                DongTam["TenNhaCungCap"] = BangNhaCungCap.Rows[0]["TenNhaCungCap"];
                DongTam["DiaChi"] = BangNhaCungCap.Rows[0]["DiaChi"];
                DongTam["DienThoai"] = BangNhaCungCap.Rows[0]["DienThoai"];
                DongTam["Fax"] = BangNhaCungCap.Rows[0]["Fax"];
                DongTam["MaSoThue"] = BangNhaCungCap.Rows[0]["MaSoThue"];
                DongTam["NoDauKy"] = BangNhaCungCap.Rows[0]["NoDauKy"];
                DongTam["TenNguoiLienHe"] = BangNhaCungCap.Rows[0]["TenNguoiLienHe"];
                BangNhaCungCap.Rows.Add(DongTam);
                BangNhaCungCap.Rows[0]["MaNhaCungCap"] = "TatCa";
                BangNhaCungCap.Rows[0]["TenNhaCungCap"] = "<Tất cả>";
                BangNhaCungCap.Rows[0]["DiaChi"] = "";
                BangNhaCungCap.Rows[0]["DienThoai"] = "";
                BangNhaCungCap.Rows[0]["Fax"] = "";
                BangNhaCungCap.Rows[0]["MaSoThue"] = "";
                BangNhaCungCap.Rows[0]["NoDauKy"] = 0;
            }
            else
            {
                //Đưa dòng thứ 0 vào vị trí cuối cùng và sau đó gán lại dòng thứ ko là tất cả
                DataRow DongTam = BangNhaCungCap.NewRow();
                DongTam["MaNhaCungCap"] = "TatCa";
                DongTam["TenNhaCungCap"] = "<Tất cả>";
                DongTam["DiaChi"] = "";
                DongTam["DienThoai"] = "";
                DongTam["Fax"] = "";
                DongTam["MaSoThue"] = "";
                DongTam["NoDauKy"] = 0;
                BangNhaCungCap.Rows.Add(DongTam);
            }
            cboNhaCungCap.DataSource = BangNhaCungCap;
            cboNhaCungCap.DisplayMember = "TenNhaCungCap";
            cboNhaCungCap.ValueMember = "MaNhaCungCap";
        }


        private void cboNhaCungCap_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (cboNhaCungCap.SelectedIndex != -1)
                    {
                        clsNhaCungCapDTO NhaCungCap = new clsNhaCungCapDTO();
                        NhaCungCap.MaNhaCungCap = ((DataRowView)cboNhaCungCap.SelectedItem).Row["MaNhaCungCap"].ToString().Trim();
                        NhaCungCap.TenNhaCungCap = ((DataRowView)cboNhaCungCap.SelectedItem).Row["TenNhaCungCap"].ToString().Trim();
                        //KhoiTao(NhaCungCap);
                    }
                    else
                    {
                        MessageBox.Show("Xin vui lòng chọn nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion


        #region Mặt Hàng
        //Lấy danh sách Mặt Hàng
        private void KhoiTaoComboMatHang()
        {
            //Load combo nhom hang
            DataTable BangMatHang = new clsMatHangBUS().LayBang();
            if (BangMatHang.Rows.Count > 0)
            {
                //Đưa dòng thứ 0 vào vị trí cuối cùng và sau đó gán lại dòng thứ ko là tất cả
                DataRow DongTam = BangMatHang.NewRow();
                DongTam["MaMatHang"] = BangMatHang.Rows[0]["MaMatHang"];
                DongTam["TenMatHang"] = BangMatHang.Rows[0]["TenMatHang"];
                BangMatHang.Rows.Add(DongTam);
                BangMatHang.Rows[0]["MaMatHang"] = "TatCa";
                BangMatHang.Rows[0]["TenMatHang"] = "<Tất cả>";
            }
            else
            {
                //Đưa dòng thứ 0 vào vị trí cuối cùng và sau đó gán lại dòng thứ ko là tất cả
                DataRow DongTam = BangMatHang.NewRow();

                DongTam["MaMatHang"] = "TatCa";
                DongTam["TenMatHang"] = "<Tất cả>";
                BangMatHang.Rows.Add(DongTam);
            }
            cboChonHang.DataSource = BangMatHang;
            cboChonHang.DisplayMember = "TenMatHang";
            cboChonHang.ValueMember = "MaMatHang";
        }

        private void cboChonHang_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (cboChonHang.SelectedIndex != -1)
                    {
                        clsMatHangDTO MatHang = new clsMatHangDTO();
                        MatHang.MaMatHang = ((DataRowView)cboChonHang.SelectedItem).Row["MaMatHang"].ToString().Trim();
                        MatHang.TenMatHang = ((DataRowView)cboChonHang.SelectedItem).Row["TenMatHang"].ToString().Trim();
                        //KhoiTao(MatHang);
                    }
                    else
                    {
                        MessageBox.Show("Xin vui lòng chọn Mặt Hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        private void KhoiTao()
        {
            if (grdvDSChiTietPhieuNhap.ColumnCount > 0)
            {
                grdvDSChiTietPhieuNhap.Columns.Clear();
                grdvDSChiTietPhieuNhap.DataSource = null;
                bindingSource1 = new BindingSource();
            }
            DataTable Bang;
            Bang = PhieuNhapBus.LayBang(dtpTuNgay.Value, dtpDenNgay.Value);
            bindingSource1.DataSource = Bang;
            string sql = "";
            if (cboNhaCungCap.SelectedValue.ToString() != "TatCa")
            {
                sql = " CONVERT([MaNhaCungCap], 'System.String') = '" + cboNhaCungCap.SelectedValue.ToString().Trim() + "' ";
            }

            if (cboChonHang.SelectedValue.ToString() != "TatCa")
            {
                if (cboNhaCungCap.SelectedValue.ToString() != "TatCa")
                {
                    sql += " AND "; 
                }

                sql += " CONVERT([MaMatHang], 'System.String') = '" + cboChonHang.SelectedValue.ToString().Trim() + "' ";
            }

            if (sql != "")
            {
                bindingSource1.Filter = sql;
            }

            grdvDSChiTietPhieuNhap.DataSource = bindingSource1;
            AnCotTrenLuoi();
            DinhDangCot();
            double TongCong = 0;
            for (int i = 0; i < grdvDSChiTietPhieuNhap.Rows.Count; i++)
            {
                TongCong += double.Parse(grdvDSChiTietPhieuNhap.Rows[i].Cells["ThanhTien"].Value.ToString().Replace(",", ""));
            }
            txtTongCong.Text = clsSupport.CurrencyNumber(TongCong.ToString()) + " (VNĐ)";
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                KhoiTao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DinhDangCot()
        {
            grdvDSChiTietPhieuNhap.Columns[0].Width = 35;
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdvDSChiTietPhieuNhap.Columns["STT"].DefaultCellStyle = CellStyle;
            for (int i = 1; i < grdvDSChiTietPhieuNhap.Columns.Count; i++)
            {
                if (grdvDSChiTietPhieuNhap.Columns[i].Visible == true)
                {
                    grdvDSChiTietPhieuNhap.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grdvDSChiTietPhieuNhap.Columns[i].ReadOnly = true;
                }
            }
            DataGridViewCellStyle CellStyleCurrency = new DataGridViewCellStyle();
            CellStyleCurrency.Alignment = DataGridViewContentAlignment.MiddleRight;
            CellStyleCurrency.Format = "#,##0.############";
            grdvDSChiTietPhieuNhap.Columns["SoLuong"].DefaultCellStyle = CellStyleCurrency;
            grdvDSChiTietPhieuNhap.Columns["SoLuong"].Width = 45;
            grdvDSChiTietPhieuNhap.Columns["DonGia"].DefaultCellStyle = CellStyleCurrency;
            grdvDSChiTietPhieuNhap.Columns["ChietKhau"].DefaultCellStyle = CellStyleCurrency;
            grdvDSChiTietPhieuNhap.Columns["ThueVAT"].DefaultCellStyle = CellStyleCurrency;
            grdvDSChiTietPhieuNhap.Columns["ThanhTien"].DefaultCellStyle = CellStyleCurrency;

            DataGridViewCellStyle CellStyleDate = new DataGridViewCellStyle();
            CellStyleDate.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CellStyleDate.Format = "dd/MM/yyyy";
            grdvDSChiTietPhieuNhap.Columns["NgayNhap"].DefaultCellStyle = CellStyleDate;
            grdvDSChiTietPhieuNhap.Columns["DonViTinh"].Width = 60;


        }

        private void AnCotTrenLuoi()
        {
            for (int i = 1; i < grdvDSChiTietPhieuNhap.ColumnCount; i++)
            {
                grdvDSChiTietPhieuNhap.Columns[i].Visible = false;
            }
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdvDSChiTietPhieuNhap.Columns["TenNhaCungCap"].Visible = true;
            grdvDSChiTietPhieuNhap.Columns["TenNhaCungCap"].HeaderText = "Nhà Cung Cấp";
            grdvDSChiTietPhieuNhap.Columns["NgayNhap"].Visible = true;
            grdvDSChiTietPhieuNhap.Columns["NgayNhap"].HeaderText = "Ngày Nhập";
            grdvDSChiTietPhieuNhap.Columns["MaPhieuNhap"].Visible = true;
            grdvDSChiTietPhieuNhap.Columns["MaPhieuNhap"].HeaderText = "Phiếu NK";
            grdvDSChiTietPhieuNhap.Columns["MaMatHang"].Visible = true;
            grdvDSChiTietPhieuNhap.Columns["MaMatHang"].HeaderText = "Mã Hàng";
            grdvDSChiTietPhieuNhap.Columns["TenMatHang"].Visible = true;
            grdvDSChiTietPhieuNhap.Columns["TenMatHang"].HeaderText = "Tên Hàng";
            grdvDSChiTietPhieuNhap.Columns["DonViTinh"].Visible = true;
            grdvDSChiTietPhieuNhap.Columns["DonViTinh"].HeaderText = "ĐVT";
            //grdvDSChiTietPhieuNhap.Columns["DonViTinh"].Width = 50;
            grdvDSChiTietPhieuNhap.Columns["SoLuong"].Visible = true;
            grdvDSChiTietPhieuNhap.Columns["SoLuong"].HeaderText = "Số Lượng";
            grdvDSChiTietPhieuNhap.Columns["SoLuong"].DefaultCellStyle = CellStyle;
            grdvDSChiTietPhieuNhap.Columns["DonGia"].Visible = true;
            grdvDSChiTietPhieuNhap.Columns["DonGia"].HeaderText = "Giá Mua";
            grdvDSChiTietPhieuNhap.Columns["DonGia"].DefaultCellStyle = CellStyle;
            grdvDSChiTietPhieuNhap.Columns["ChietKhau"].Visible = true;
            grdvDSChiTietPhieuNhap.Columns["ChietKhau"].HeaderText = "Chiết Khấu";
            grdvDSChiTietPhieuNhap.Columns["ChietKhau"].DefaultCellStyle = CellStyle;
            grdvDSChiTietPhieuNhap.Columns["ThueVAT"].Visible = true;
            grdvDSChiTietPhieuNhap.Columns["ThueVAT"].HeaderText = "VAT";
            grdvDSChiTietPhieuNhap.Columns["ThueVAT"].DefaultCellStyle = CellStyle;
            grdvDSChiTietPhieuNhap.Columns["ThanhTien"].Visible = true;
            grdvDSChiTietPhieuNhap.Columns["ThanhTien"].HeaderText = "Tổng Cộng";
            grdvDSChiTietPhieuNhap.Columns["ThanhTien"].DefaultCellStyle = CellStyle;
        }

        private void btnInRa_Click(object sender, EventArgs e)
        {
            In();
        }

        private void In()
        {
            try
            {
                if (grdvDSChiTietPhieuNhap.RowCount == 0)
                {
                    MessageBox.Show("Không có mặt hàng đã nhập trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptChiTietHangNhap chiTietHangNhap = new rptChiTietHangNhap();
                chiTietHangNhap.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                string MaMatHang = cboChonHang.SelectedValue.ToString().Trim();
                string MaNhaCungCap = cboNhaCungCap.SelectedValue.ToString().Trim();
                if (MaMatHang == "TatCa")
                    MaMatHang = "";
                if (MaNhaCungCap == "TatCa")
                    MaNhaCungCap = "";

                DataTable Bang = PhieuNhapBus.ReportChiTietHangNhap(dtpTuNgay.Value, dtpDenNgay.Value, MaMatHang, MaNhaCungCap);
                if (Bang.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();
                    DataSet cacBang = new DataSet();
                    cacBang.Tables.Add(Bang);
                    cacBang.Tables.Add(CongTy);

                    chiTietHangNhap.SetDataSource(cacBang);
                    chiTietHangNhap.SetParameterValue("@TuNgay", dtpTuNgay.Value);
                    chiTietHangNhap.SetParameterValue("@DenNgay", dtpDenNgay.Value);
                    chiTietHangNhap.SetParameterValue("@MaMatHang", MaMatHang);
                    chiTietHangNhap.SetParameterValue("@MaNhaCungCap", MaNhaCungCap);

                    chiTietHangNhap.SetParameterValue("@NgayTu", dtpTuNgay.Value);
                    chiTietHangNhap.SetParameterValue("@NgayDen", dtpDenNgay.Value);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = chiTietHangNhap;
                    dlgHienThi.ShowDialog();
                }
                else
                    MessageBox.Show("Không có mặt hàng đã nhập trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }     
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }
    }
}