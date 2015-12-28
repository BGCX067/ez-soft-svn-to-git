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
    public partial class frmXemDonHangDaBan : frmTemplete
    {
        #region Thuộc tính
        private clsPhieuXuatBUS PhieuXuatBus = new clsPhieuXuatBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        #endregion

        public frmXemDonHangDaBan()
        {
            InitializeComponent();
        }

        private void frmXemDonHangDaBan_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboKhachHang();
                //KhoiTaoComboNhanVien();
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

        //Khách Hàng
        #region Khách Hàng
        //Lấy danh sách Khách Hàng

        private void KhoiTaoComboKhachHang()
        {
            DataTable BangKhachHang = new clsKhachHangBUS().LayBang();
            //Đưa dòng thứ 0 vào vị trí cuối cùng và sau đó gán lại dòng thứ ko là tất cả
            if (BangKhachHang.Rows.Count >= 2)
            {
                DataRow DongTam = GanDongKhachHang(BangKhachHang, 0);
                DataRow DongTam1 = GanDongKhachHang(BangKhachHang, 1);
                BangKhachHang.Rows.Add(DongTam);
                BangKhachHang.Rows.Add(DongTam1);
                GanKhachHang(ref BangKhachHang, 0, "TatCa", "<Tất cả>");
                GanKhachHang(ref BangKhachHang, 1, "BanLe", "<Khách vãng lai>");
            }
            else
            {
                if (BangKhachHang.Rows.Count == 1)
                {
                    DataRow DongTam = BangKhachHang.NewRow();
                    DongTam = GanDongKhachHang(BangKhachHang, 0);
                    GanKhachHang(ref BangKhachHang, 0, "TatCa", "<Tất cả>");
                    BangKhachHang.Rows.Add(KhoiTaoMoiKhachHang(BangKhachHang, "BanLe", "<Khách vãng lai>"));
                    BangKhachHang.Rows.Add(DongTam);
                }
                else
                {
                    BangKhachHang.Rows.Add(KhoiTaoMoiKhachHang(BangKhachHang, "TatCa", "<Tất cả>"));
                    BangKhachHang.Rows.Add(KhoiTaoMoiKhachHang(BangKhachHang, "BanLe", "<Khách vãng lai>"));
                }
            }

            cboKhachHang.DataSource = BangKhachHang;
            cboKhachHang.DisplayMember = "TenKhachHang";
            cboKhachHang.ValueMember = "MaKhachHang";
        }

        private DataRow KhoiTaoMoiKhachHang(DataTable BangKhachHang, string GiaTri, string HienThi)
        {
            DataRow DongTam = BangKhachHang.NewRow();
            DongTam["MaKhachHang"] = GiaTri;
            DongTam["TenKhachHang"] = HienThi;
            DongTam["DiaChi"] = "";
            DongTam["DienThoai"] = "";
            DongTam["Fax"] = "";
            DongTam["MaSoThue"] = "";
            DongTam["NoDauKy"] = 0;
            DongTam["BaoGia"] = "";
            DongTam["ChietKhau"] = 0;
            DongTam["TenNguoiLienHe"] = "";
            return DongTam;
        }

        private DataRow GanDongKhachHang(DataTable BangKhachHang, int dong)
        {
            DataRow DongTam = BangKhachHang.NewRow();
            DongTam["MaKhachHang"] = BangKhachHang.Rows[dong]["MaKhachHang"];
            DongTam["TenKhachHang"] = BangKhachHang.Rows[dong]["TenKhachHang"];
            DongTam["DiaChi"] = BangKhachHang.Rows[dong]["DiaChi"];
            DongTam["DienThoai"] = BangKhachHang.Rows[dong]["DienThoai"];
            DongTam["Fax"] = BangKhachHang.Rows[dong]["Fax"];
            DongTam["MaSoThue"] = BangKhachHang.Rows[dong]["MaSoThue"];
            DongTam["NoDauKy"] = BangKhachHang.Rows[dong]["NoDauKy"];
            DongTam["BaoGia"] = BangKhachHang.Rows[dong]["BaoGia"];
            DongTam["ChietKhau"] = BangKhachHang.Rows[dong]["ChietKhau"];
            DongTam["TenNguoiLienHe"] = BangKhachHang.Rows[dong]["TenNguoiLienHe"];
            return DongTam;
        }

        private void GanKhachHang(ref DataTable BangKhachHang, int dong, string GiaTri, string HienThi)
        {
            BangKhachHang.Rows[dong]["MaKhachHang"] = GiaTri;
            BangKhachHang.Rows[dong]["TenKhachHang"] = HienThi;
            BangKhachHang.Rows[dong]["DiaChi"] = "";
            BangKhachHang.Rows[dong]["DienThoai"] = "";
            BangKhachHang.Rows[dong]["Fax"] = "";
            BangKhachHang.Rows[dong]["MaSoThue"] = "";
            BangKhachHang.Rows[dong]["NoDauKy"] = 0;
            BangKhachHang.Rows[dong]["BaoGia"] = "";
            BangKhachHang.Rows[dong]["ChietKhau"] = 0;
            BangKhachHang.Rows[dong]["TenNguoiLienHe"] = "";
        }
        #endregion


        //Nhân viên
        //private void KhoiTaoComboNhanVien()
        //{
        //    DataTable BangNhanVien = new clsNhanVienBUS().LayBang();
        //    if (BangNhanVien.Rows.Count > 0)
        //    {
        //        //Đưa dòng thứ 0 vào vị trí cuối cùng và sau đó gán lại dòng thứ ko là tất cả
        //        DataRow DongTam = BangNhanVien.NewRow();
        //        DongTam["MaNhanVien"] = BangNhanVien.Rows[0]["MaNhanVien"];
        //        DongTam["TenNhanVien"] = BangNhanVien.Rows[0]["TenNhanVien"];
        //        DongTam["DienThoai"] = BangNhanVien.Rows[0]["DienThoai"];
        //        DongTam["DiaChi"] = BangNhanVien.Rows[0]["DiaChi"];
        //        DongTam["GhiChu"] = BangNhanVien.Rows[0]["GhiChu"];
        //        BangNhanVien.Rows.Add(DongTam);

        //        BangNhanVien.Rows[0]["MaNhanVien"] = "TatCa";
        //        BangNhanVien.Rows[0]["TenNhanVien"] = "<Tất cả>";
        //        BangNhanVien.Rows[0]["DienThoai"] = "";
        //        BangNhanVien.Rows[0]["DiaChi"] = "";
        //        BangNhanVien.Rows[0]["GhiChu"] = "";
        //    }
        //    else
        //    {
        //        DataRow DongTam = BangNhanVien.NewRow();
        //        DongTam["MaNhanVien"] = "TatCa";
        //        DongTam["TenNhanVien"] = "<Tất cả>";
        //        DongTam["DienThoai"] = "";
        //        DongTam["DiaChi"] = "";
        //        DongTam["GhiChu"] = "";
        //        BangNhanVien.Rows.Add(DongTam);
        //    }

        //    cboNhanVienBH.DataSource = BangNhanVien;
        //    cboNhanVienBH.DisplayMember = "TenNhanVien";
        //    cboNhanVienBH.ValueMember = "MaNhanVien";
        //    cboNhanVienBH.SelectedIndex = 0;
        //}


        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboKhachHang.SelectedIndex != -1)
                {
                    clsKhachHangDTO KhachHang = new clsKhachHangDTO();
                    KhachHang.MaKhachHang = ((DataRowView)cboKhachHang.SelectedItem).Row["MaKhachHang"].ToString().Trim();
                    KhachHang.TenKhachHang = ((DataRowView)cboKhachHang.SelectedItem).Row["TenKhachHang"].ToString().Trim();
                    KhoiTao(KhachHang);
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

        private void KhoiTao(clsKhachHangDTO KhachHang)
        {
            if (grdvDSDonHangDaBan.ColumnCount > 0)
            {
                grdvDSDonHangDaBan.Columns.Clear();
                grdvDSDonHangDaBan.DataSource = null;
                bindingSource1 = new BindingSource();
            }
            DataTable Bang;
            if (cboKhachHang.SelectedValue.ToString() == "TatCa" || cboKhachHang.SelectedValue.ToString() == "BanLe")
            {
                Bang = PhieuXuatBus.TimKiem(dtpTuNgay.Value, dtpDenNgay.Value);
            }
            else
            {
                Bang = PhieuXuatBus.TimKiem(dtpTuNgay.Value, dtpDenNgay.Value, KhachHang);
            }

            //foreach (DataRow Dong in Bang.Rows)
            //{
            //    Dong["ThueVAT"] = Double.Parse(Dong["ThueVAT"].ToString()) / 100;
            //}
            //Loc thong tin mat hang
            bindingSource1.DataSource = Bang;
            string sql = "";
            if (cboKhachHang.SelectedValue == "BanLe")
            {
                sql = " CONVERT([LoaiPhieuXuat], 'System.String') LIKE '" + "Xuất bán lẻ" + "' ";
                //if (cboNhanVienBH.SelectedValue.ToString() != "TatCa")
                //{
                sql += " AND CONVERT([MaNhanVien], 'System.String') = '" + clsUser.MaNhanVien.Trim() + "' ";
                //}
            }
            else
            {
                //if (cboNhanVienBH.SelectedValue.ToString() != "TatCa")
                //{
                    sql = " CONVERT([MaNhanVien], 'System.String') = '" + clsUser.MaNhanVien.Trim() + "' ";
                //}
            }

            if (sql != "")
            {
                bindingSource1.Filter = sql;
            }
            
            grdvDSDonHangDaBan.DataSource = bindingSource1;
            AnCotTrenLuoi();
            DinhDangCot();
            double TongCong = 0;
            for (int i = 0; i < grdvDSDonHangDaBan.Rows.Count; i++)
            {
                TongCong += double.Parse(grdvDSDonHangDaBan.Rows[i].Cells["TongTien"].Value.ToString().Replace(",", ""));
            }
            txtTongCong.Text = clsSupport.CurrencyNumber(TongCong.ToString()) + " (VNĐ)";
        }
        private void AnCotTrenLuoi()
        {
            for (int i = 1; i < grdvDSDonHangDaBan.ColumnCount; i++)
            {
                grdvDSDonHangDaBan.Columns[i].Visible = false;
            }
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdvDSDonHangDaBan.Columns["LoaiPhieuXuat"].Visible = true;
            grdvDSDonHangDaBan.Columns["LoaiPhieuXuat"].HeaderText = "Loại Phiếu Xuất";
            grdvDSDonHangDaBan.Columns["MaKhachHang"].Visible = true;
            grdvDSDonHangDaBan.Columns["MaKhachHang"].HeaderText = "Mã KH";
            grdvDSDonHangDaBan.Columns["TenKhachHang"].Visible = true;
            grdvDSDonHangDaBan.Columns["TenKhachHang"].HeaderText = "Tên Khách Hàng";
            grdvDSDonHangDaBan.Columns["TenNhanVien"].Visible = true;
            grdvDSDonHangDaBan.Columns["TenNhanVien"].HeaderText = "NVBH";
            grdvDSDonHangDaBan.Columns["MaPhieuXuat"].Visible = true;
            grdvDSDonHangDaBan.Columns["MaPhieuXuat"].HeaderText = "Phiếu XK";
            grdvDSDonHangDaBan.Columns["NgayXuat"].Visible = true;
            grdvDSDonHangDaBan.Columns["NgayXuat"].HeaderText = "Ngày Xuất";
            grdvDSDonHangDaBan.Columns["TienHang"].Visible = true;
            grdvDSDonHangDaBan.Columns["TienHang"].HeaderText = "Tiền Hàng";
            grdvDSDonHangDaBan.Columns["TienHang"].DefaultCellStyle = CellStyle;
            grdvDSDonHangDaBan.Columns["ChietKhau"].Visible = true;
            grdvDSDonHangDaBan.Columns["ChietKhau"].HeaderText = "Chiết Khấu";
            grdvDSDonHangDaBan.Columns["ChietKhau"].DefaultCellStyle = CellStyle;
            grdvDSDonHangDaBan.Columns["ThueVAT"].Visible = true;
            grdvDSDonHangDaBan.Columns["ThueVAT"].HeaderText = "VAT";
            grdvDSDonHangDaBan.Columns["ThueVAT"].DefaultCellStyle = CellStyle;
            grdvDSDonHangDaBan.Columns["TongTien"].Visible = true;
            grdvDSDonHangDaBan.Columns["TongTien"].HeaderText = "Tổng Cộng";
            grdvDSDonHangDaBan.Columns["TongTien"].DefaultCellStyle = CellStyle;
        }

        private void DinhDangCot()
        {
            grdvDSDonHangDaBan.Columns[0].Width = 40;
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdvDSDonHangDaBan.Columns["STT"].DefaultCellStyle = CellStyle;
            for (int i = 1; i < grdvDSDonHangDaBan.Columns.Count; i++)
            {
                if (grdvDSDonHangDaBan.Columns[i].Visible == true)
                {
                    grdvDSDonHangDaBan.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grdvDSDonHangDaBan.Columns[i].ReadOnly = true;
                }
            }
            DataGridViewCellStyle CellStyleCurrency = new DataGridViewCellStyle();
            CellStyleCurrency.Alignment = DataGridViewContentAlignment.MiddleRight;
            CellStyleCurrency.Format = "#,##0.############";
            grdvDSDonHangDaBan.Columns["TienHang"].DefaultCellStyle = CellStyleCurrency;
            grdvDSDonHangDaBan.Columns["ChietKhau"].DefaultCellStyle = CellStyleCurrency;
            grdvDSDonHangDaBan.Columns["ThueVAT"].DefaultCellStyle = CellStyleCurrency;
            grdvDSDonHangDaBan.Columns["TongTien"].DefaultCellStyle = CellStyleCurrency;

            DataGridViewCellStyle CellStyleDate= new DataGridViewCellStyle();
            CellStyleDate.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CellStyleDate.Format = "dd/MM/yyyy";
            grdvDSDonHangDaBan.Columns["NgayXuat"].DefaultCellStyle = CellStyleDate;


        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String Loi = "";
            try
            {
                if (grdvDSDonHangDaBan.CurrentRow != null && grdvDSDonHangDaBan.CurrentRow.Index != -1)
                {
                    double TongTien = double.Parse(grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["TongTien"].Value.ToString().Replace(",", ""));
                    double DaTra = double.Parse(grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["DaTra"].Value.ToString().Replace(",", ""));
                    string MaPhieuXuat = grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["MaPhieuXuat"].Value.ToString().Trim();
                    string LoaiPhieuXuat = grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["LoaiPhieuXuat"].Value.ToString();
                    if (LoaiPhieuXuat.Trim() == "Xuất bán sỉ")
                    {
                        if (DaTra == 0)
                        {
                            Loi = ThucThiHuyPhieuXuat(Loi, TongTien, MaPhieuXuat);
                        }
                        else
                        {
                            MessageBox.Show("phiếu xuất kho này không được phép hủy vì đã được trả tiền", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                       // Loi = ThucThiHuyPhieuXuat(Loi, TongTien, MaPhieuXuat);
                        MessageBox.Show("phiếu xuất kho này không được phép hủy vì đã được trả tiền", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn phiếu xuất cần xóa!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Loi, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private String ThucThiHuyPhieuXuat(String Loi, double TongTien, string MaPhieuXuat)
        {
            DialogResult result = MessageBox.Show("Bạn có thật sự muốn hủy phiếu xuất " + MaPhieuXuat + " (Chú ý: Khi hủy phiếu xuất kho thì số lượng các mặt hàng trong phiếu xuất đó được phục hồi", "Xác nhận thông tin", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Loi = "Lỗi kết nối cơ sở dữ liệu";
                if (PhieuXuatBus.Huy(MaPhieuXuat) != -1)
                {
                    grdvDSDonHangDaBan.Rows.RemoveAt(grdvDSDonHangDaBan.CurrentRow.Index);
                    double TongTatCaTien = double.Parse(txtTongCong.Text.Replace("(VNĐ)", "").Trim());
                    TongTatCaTien = TongTatCaTien - TongTien;
                    txtTongCong.Text = clsSupport.CurrencyNumber(TongTatCaTien.ToString()) + " (VNĐ)";
                }
                else
                {
                    MessageBox.Show("Hủy phiếu xuất không thành công. Xin vui lòng thử lại", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return Loi;
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdvDSDonHangDaBan.CurrentRow != null && grdvDSDonHangDaBan.CurrentRow.Index != -1)
                {
                    double TongTien = double.Parse(grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["TongTien"].Value.ToString().Replace(",", ""));
                    double DaTra = double.Parse(grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["DaTra"].Value.ToString().Replace(",", ""));
                    string MaPhieuXuat = grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["MaPhieuXuat"].Value.ToString().Trim();
                    string LoaiPhieuXuat = grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["LoaiPhieuXuat"].Value.ToString();
                    if (LoaiPhieuXuat.Trim() == "Xuất bán sỉ")
                    {
                        if (DaTra == 0)
                        {
                            frmBanSi F = new frmBanSi(grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["MaPhieuXuat"].Value.ToString().Trim(), true, "QuanLyDonHangDaBan");
                            F.ShowDialog();

                        }
                        else
                        {
                            frmBanSi F = new frmBanSi(grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["MaPhieuXuat"].Value.ToString().Trim(), false, "QuanLyDonHangDaBan");
                            F.ShowDialog();
                        }
                        if (cboKhachHang.SelectedIndex != -1)
                        {
                            clsKhachHangDTO KhachHang = new clsKhachHangDTO();
                            KhachHang.MaKhachHang = ((DataRowView)cboKhachHang.SelectedItem).Row["MaKhachHang"].ToString().Trim();
                            KhachHang.TenKhachHang = ((DataRowView)cboKhachHang.SelectedItem).Row["TenKhachHang"].ToString().Trim();
                            KhoiTao(KhachHang);
                        }
                    }
                    else
                    {
                        frmBanLe F = new frmBanLe(grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["MaPhieuXuat"].Value.ToString().Trim(), "QuanLyDonHangDaBan");
                        F.ShowDialog();
                        if (cboKhachHang.SelectedIndex != -1)
                        {
                            clsKhachHangDTO KhachHang = new clsKhachHangDTO();
                            KhachHang.MaKhachHang = ((DataRowView)cboKhachHang.SelectedItem).Row["MaKhachHang"].ToString().Trim();
                            KhachHang.TenKhachHang = ((DataRowView)cboKhachHang.SelectedItem).Row["TenKhachHang"].ToString().Trim();
                            KhoiTao(KhachHang);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn phiếu xuất!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        private void grdvDSDonHangDaBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string LoaiPhieuXuat = grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["LoaiPhieuXuat"].Value.ToString();
           
            if (LoaiPhieuXuat.Trim() == "Xuất bán sỉ")
            {
                double DaTra = double.Parse(grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["DaTra"].Value.ToString().Replace(",", ""));
                if (DaTra != 0)
                {
                    btnXoa.Enabled = false;
                }
                else
                {
                    btnXoa.Enabled = true;
                }
            }
            else
            {
                btnXoa.Enabled = false;
            }
        }

        private void btnInRa_Click(object sender, EventArgs e)
        {
            In();
        }

        private void In()
        {
            try
            {
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptXemDonHangDaBan xemDonHangDaBan = new rptXemDonHangDaBan();
                xemDonHangDaBan.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                string MaKhachHang = cboKhachHang.SelectedValue.ToString().Trim();
                //string MaNhanVien = cboNhanVienBH.SelectedValue.ToString().Trim();
                string MaNhanVien = clsUser.MaNhanVien.Trim();
                if (MaKhachHang == "TatCa")
                    MaKhachHang = "";
                //if (MaNhanVien == "TatCa")
                  //  MaNhanVien = "";

                DataTable Bang = PhieuXuatBus.ReportPhieuXuatTheoTK(dtpTuNgay.Value, dtpDenNgay.Value, MaKhachHang, MaNhanVien);
                if (Bang.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();
                    DataSet cacBang = new DataSet();
                    cacBang.Tables.Add(Bang);
                    cacBang.Tables.Add(CongTy);

                    xemDonHangDaBan.SetDataSource(cacBang);
                    xemDonHangDaBan.SetParameterValue("@TuNgay", dtpTuNgay.Value);
                    xemDonHangDaBan.SetParameterValue("@DenNgay", dtpDenNgay.Value);
                    xemDonHangDaBan.SetParameterValue("@MaKhachHang", MaKhachHang);
                    xemDonHangDaBan.SetParameterValue("@MaNhanVien", MaNhanVien);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = xemDonHangDaBan;
                    dlgHienThi.ShowDialog();
                }
                else
                    MessageBox.Show("In không thành công vì không có đơn hàng nào đã bán trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        
        }
    }
}