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
    public partial class frmChiTietHangXuat : frmTemplete
    {

        #region Thuộc tính
        private clsPhieuXuatBUS PhieuXuatBus = new clsPhieuXuatBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        #endregion

        public frmChiTietHangXuat()
        {
            InitializeComponent();
        }

        private void frmChiTietHangXuat_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboKhachHang();
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

            //in thông tin 
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


        #region Khách hàng
        //Lấy danh sáchKhách hàng
        private void KhoiTaoComboKhachHang()
        {

            DataTable BangKhachHang = new clsKhachHangBUS().LayBang();
            if (BangKhachHang.Rows.Count > 0)
            {
                //Đưa dòng thứ 0 vào vị trí cuối cùng và sau đó gán lại dòng thứ ko là tất cả
                DataRow DongTam = BangKhachHang.NewRow();
                DongTam["MaKhachHang"] = BangKhachHang.Rows[0]["MaKhachHang"];
                DongTam["TenKhachHang"] = BangKhachHang.Rows[0]["TenKhachHang"];
                DongTam["DiaChi"] = BangKhachHang.Rows[0]["DiaChi"];
                DongTam["DienThoai"] = BangKhachHang.Rows[0]["DienThoai"];
                DongTam["Fax"] = BangKhachHang.Rows[0]["Fax"];
                DongTam["MaSoThue"] = BangKhachHang.Rows[0]["MaSoThue"];
                DongTam["NoDauKy"] = BangKhachHang.Rows[0]["NoDauKy"];
                DongTam["BaoGia"] = BangKhachHang.Rows[0]["BaoGia"];
                DongTam["ChietKhau"] = BangKhachHang.Rows[0]["ChietKhau"];
                DongTam["TenNguoiLienHe"] = BangKhachHang.Rows[0]["TenNguoiLienHe"];
                BangKhachHang.Rows.Add(DongTam);
                BangKhachHang.Rows[0]["MaKhachHang"] = "TatCa";
                BangKhachHang.Rows[0]["TenKhachHang"] = "<Tất cả>";
                BangKhachHang.Rows[0]["DiaChi"] = "";
                BangKhachHang.Rows[0]["DienThoai"] = "";
                BangKhachHang.Rows[0]["Fax"] = "";
                BangKhachHang.Rows[0]["MaSoThue"] = "";
                BangKhachHang.Rows[0]["NoDauKy"] = 0;
                BangKhachHang.Rows[0]["BaoGia"] = "";
                BangKhachHang.Rows[0]["ChietKhau"] = 0;
                BangKhachHang.Rows[0]["TenNguoiLienHe"] = "";

            }
            else
            {
                DataRow DongTam = BangKhachHang.NewRow();
                DongTam["MaKhachHang"] = "TatCa";
                DongTam["TenKhachHang"] = "<Tất cả>";
                DongTam["DiaChi"] = "";
                DongTam["DienThoai"] = "";
                DongTam["Fax"] = "";
                DongTam["MaSoThue"] = "";
                DongTam["NoDauKy"] = 0;
                DongTam["BaoGia"] = "";
                DongTam["ChietKhau"] = 0;
                DongTam["TenNguoiLienHe"] = "";
                BangKhachHang.Rows.Add(DongTam);
            }
            cboKhachHang.DataSource = BangKhachHang;
            cboKhachHang.DisplayMember = "TenKhachHang";
            cboKhachHang.ValueMember = "MaKhachHang";
        }

        private void cboKhachHang_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (cboKhachHang.SelectedIndex != -1)
                    {
                        clsKhachHangDTO KhachHang = new clsKhachHangDTO();
                        KhachHang.MaKhachHang = ((DataRowView)cboKhachHang.SelectedItem).Row["MaKhachHang"].ToString().Trim();
                        KhachHang.TenKhachHang = ((DataRowView)cboKhachHang.SelectedItem).Row["TenKhachHang"].ToString().Trim();
                        //KhoiTao(KhachHang);
                    }
                    else
                    {
                        MessageBox.Show("Xin vui lòng chọnKhách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (grdvDSChiTietPhieuXuat.ColumnCount > 0)
            {
                grdvDSChiTietPhieuXuat.Columns.Clear();
                grdvDSChiTietPhieuXuat.DataSource = null;
                bindingSource1 = new BindingSource();
            }
            DataTable Bang;
            Bang = PhieuXuatBus.LayBang(dtpTuNgay.Value, dtpDenNgay.Value);
            bindingSource1.DataSource = Bang;
            string sql = "";

            if (cboKhachHang.SelectedValue.ToString() != "TatCa")
            {
                sql = " CONVERT([MaKhachHang], 'System.String') = '" + cboKhachHang.SelectedValue.ToString().Trim() + "' ";
            }

            if (cboChonHang.SelectedValue.ToString() != "TatCa")
            {
                if (cboKhachHang.SelectedValue.ToString() != "TatCa")
                {
                    sql += " AND ";
                }
                sql += " CONVERT([MaMatHang], 'System.String') = '" + cboChonHang.SelectedValue.ToString().Trim() + "' ";
            }

            if (sql != "")
            {
                bindingSource1.Filter = sql;
            }

            grdvDSChiTietPhieuXuat.DataSource = bindingSource1;
            AnCotTrenLuoi();
            DinhDangCot();
            double TongCong = 0;
            for (int i = 0; i < grdvDSChiTietPhieuXuat.Rows.Count; i++)
            {
                TongCong += double.Parse(grdvDSChiTietPhieuXuat.Rows[i].Cells["ThanhTien"].Value.ToString().Replace(",", ""));
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
            grdvDSChiTietPhieuXuat.Columns[0].Width = 35;
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdvDSChiTietPhieuXuat.Columns["STT"].DefaultCellStyle = CellStyle;
            for (int i = 1; i < grdvDSChiTietPhieuXuat.Columns.Count; i++)
            {
                if (grdvDSChiTietPhieuXuat.Columns[i].Visible == true)
                {
                    grdvDSChiTietPhieuXuat.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grdvDSChiTietPhieuXuat.Columns[i].ReadOnly = true;
                }
            }
            DataGridViewCellStyle CellStyleCurrency = new DataGridViewCellStyle();
            CellStyleCurrency.Alignment = DataGridViewContentAlignment.MiddleRight;
            CellStyleCurrency.Format = "#,##0.############";
            grdvDSChiTietPhieuXuat.Columns["SoLuong"].DefaultCellStyle = CellStyleCurrency;
            grdvDSChiTietPhieuXuat.Columns["SoLuong"].Width = 45;
            grdvDSChiTietPhieuXuat.Columns["DonGia"].DefaultCellStyle = CellStyleCurrency;
            grdvDSChiTietPhieuXuat.Columns["ChietKhau"].DefaultCellStyle = CellStyleCurrency;
            grdvDSChiTietPhieuXuat.Columns["ThueVAT"].DefaultCellStyle = CellStyleCurrency;
            grdvDSChiTietPhieuXuat.Columns["ThanhTien"].DefaultCellStyle = CellStyleCurrency;

            DataGridViewCellStyle CellStyleDate = new DataGridViewCellStyle();
            CellStyleDate.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CellStyleDate.Format = "dd/MM/yyyy";
            grdvDSChiTietPhieuXuat.Columns["NgayXuat"].DefaultCellStyle = CellStyleDate;
            grdvDSChiTietPhieuXuat.Columns["DonViTinh"].Width = 60;


        }

        private void AnCotTrenLuoi()
        {
            for (int i = 1; i < grdvDSChiTietPhieuXuat.ColumnCount; i++)
            {
                grdvDSChiTietPhieuXuat.Columns[i].Visible = false;
            }
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdvDSChiTietPhieuXuat.Columns["TenKhachHang"].Visible = true;
            grdvDSChiTietPhieuXuat.Columns["TenKhachHang"].HeaderText = "Khách Hàng";
            grdvDSChiTietPhieuXuat.Columns["NgayXuat"].Visible = true;
            grdvDSChiTietPhieuXuat.Columns["NgayXuat"].HeaderText = "Ngày Xuất";
            grdvDSChiTietPhieuXuat.Columns["MaPhieuXuat"].Visible = true;
            grdvDSChiTietPhieuXuat.Columns["MaPhieuXuat"].HeaderText = "Phiếu XK";
            grdvDSChiTietPhieuXuat.Columns["MaMatHang"].Visible = true;
            grdvDSChiTietPhieuXuat.Columns["MaMatHang"].HeaderText = "Mã Hàng";
            grdvDSChiTietPhieuXuat.Columns["TenMatHang"].Visible = true;
            grdvDSChiTietPhieuXuat.Columns["TenMatHang"].HeaderText = "Tên Hàng";
            grdvDSChiTietPhieuXuat.Columns["DonViTinh"].Visible = true;
            grdvDSChiTietPhieuXuat.Columns["DonViTinh"].HeaderText = "ĐVT";
            grdvDSChiTietPhieuXuat.Columns["SoLuong"].Visible = true;
            grdvDSChiTietPhieuXuat.Columns["SoLuong"].HeaderText = "Số Lượng";
            grdvDSChiTietPhieuXuat.Columns["SoLuong"].DefaultCellStyle = CellStyle;
            grdvDSChiTietPhieuXuat.Columns["DonGia"].Visible = true;
            grdvDSChiTietPhieuXuat.Columns["DonGia"].HeaderText = "Giá Mua";
            grdvDSChiTietPhieuXuat.Columns["DonGia"].DefaultCellStyle = CellStyle;
            grdvDSChiTietPhieuXuat.Columns["ChietKhau"].Visible = true;
            grdvDSChiTietPhieuXuat.Columns["ChietKhau"].HeaderText = "Chiết Khấu";
            grdvDSChiTietPhieuXuat.Columns["ChietKhau"].DefaultCellStyle = CellStyle;
            grdvDSChiTietPhieuXuat.Columns["ThueVAT"].Visible = true;
            grdvDSChiTietPhieuXuat.Columns["ThueVAT"].HeaderText = "VAT";
            grdvDSChiTietPhieuXuat.Columns["ThueVAT"].DefaultCellStyle = CellStyle;
            grdvDSChiTietPhieuXuat.Columns["ThanhTien"].Visible = true;
            grdvDSChiTietPhieuXuat.Columns["ThanhTien"].HeaderText = "Tổng Cộng";
            grdvDSChiTietPhieuXuat.Columns["ThanhTien"].DefaultCellStyle = CellStyle;
        }

        private void btnInRa_Click(object sender, EventArgs e)
        {
            In();
        }

        private void In()
        {
            try
            {
                if (grdvDSChiTietPhieuXuat.RowCount == 0)
                {
                    MessageBox.Show("Không có mặt hàng đã bán trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptChiTietHangXuat chiTietHangXuat = new rptChiTietHangXuat();
                chiTietHangXuat.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                string MaMatHang = cboChonHang.SelectedValue.ToString().Trim();
                string MaKhachHang = cboKhachHang.SelectedValue.ToString().Trim();
                if (MaMatHang == "TatCa")
                    MaMatHang = "";
                if (MaKhachHang == "TatCa")
                    MaKhachHang = "";

                DataTable Bang = PhieuXuatBus.ReportChiTietHangXuat(dtpTuNgay.Value, dtpDenNgay.Value, MaMatHang, MaKhachHang);
                if (Bang.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();
                    DataSet cacBang = new DataSet();
                    cacBang.Tables.Add(Bang);
                    cacBang.Tables.Add(CongTy);

                    chiTietHangXuat.SetDataSource(cacBang);
                    chiTietHangXuat.SetParameterValue("@TuNgay", dtpTuNgay.Value);
                    chiTietHangXuat.SetParameterValue("@DenNgay", dtpDenNgay.Value);
                    chiTietHangXuat.SetParameterValue("@MaMatHang", MaMatHang);
                    chiTietHangXuat.SetParameterValue("@MaKhachHang", MaKhachHang);

                    chiTietHangXuat.SetParameterValue("@NgayTu", dtpTuNgay.Value);
                    chiTietHangXuat.SetParameterValue("@NgayDen", dtpDenNgay.Value);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = chiTietHangXuat;
                    dlgHienThi.ShowDialog();
                }
                else
                    MessageBox.Show("Không có mặt hàng đã bán trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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