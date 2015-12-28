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
    public partial class frmCongNoKhachHang : frmTemplete
    {
        #region Thuộc tính
        private clsPhieuXuatBanSiBUS PhieuXuatBanSiBus = new clsPhieuXuatBanSiBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        #endregion

        public frmCongNoKhachHang()
        {
            InitializeComponent();
        }

        private void frmCongNoKhachHang_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboKhachHang();
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

            //Lưu và in thông tin Phiếu xuất
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

        #region Khách Hàng
        //Lấy danh sách Khách Hàng
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
                DongTam["TenNguoiLienHe"] = BangKhachHang.Rows[0]["TenNguoiLienHe"];
                DongTam["ChietKhau"] = BangKhachHang.Rows[0]["ChietKhau"];

                BangKhachHang.Rows.Add(DongTam);
                BangKhachHang.Rows[0]["MaKhachHang"] = "";
                BangKhachHang.Rows[0]["TenKhachHang"] = "<Tất cả>";
                BangKhachHang.Rows[0]["DiaChi"] = "";
                BangKhachHang.Rows[0]["DienThoai"] = "";
                BangKhachHang.Rows[0]["Fax"] = "";
                BangKhachHang.Rows[0]["MaSoThue"] = "";
                BangKhachHang.Rows[0]["NoDauKy"] = 0;
                BangKhachHang.Rows[0]["BaoGia"] = "";
                BangKhachHang.Rows[0]["TenNguoiLienHe"] = "";
                BangKhachHang.Rows[0]["ChietKhau"] = 0;
            }
            else
            {
                //Đưa dòng thứ 0 vào vị trí cuối cùng và sau đó gán lại dòng thứ ko là tất cả
                DataRow DongTam = BangKhachHang.NewRow();
                DongTam["MaKhachHang"] = "";
                DongTam["TenKhachHang"] = "<Tất cả>";
                DongTam["DiaChi"] = "";
                DongTam["DienThoai"] = "";
                DongTam["Fax"] = "";
                DongTam["MaSoThue"] = "";
                DongTam["NoDauKy"] = 0;
                DongTam["BaoGia"] = "";
                DongTam["TenNguoiLienHe"] = "";
                DongTam["ChietKhau"] = 0;
                BangKhachHang.Rows.Add(DongTam);
            }
            cboKhachHang.DataSource = BangKhachHang;
            cboKhachHang.DisplayMember = "TenKhachHang";
            cboKhachHang.ValueMember = "MaKhachHang";

        }
        #endregion

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
                    MessageBox.Show("Xin vui lòng chọn khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KhoiTao(clsKhachHangDTO KhachHang)
        {
            if (grdvDSDonHangDaBan.Rows.Count > 0)
            {
                grdvDSDonHangDaBan.Rows.Clear();
                //bindingSource1 = new BindingSource();
            }
            DataTable Bang;
            if (KhachHang.TenKhachHang == "<Tất cả>")
            {
                Bang = PhieuXuatBanSiBus.CongNoKhachHang(new DateTime(2000, 1, 1), dtpDenNgay.Value);
            }
            else
            {
                Bang = PhieuXuatBanSiBus.CongNoKhachHangTheoKH(new DateTime(2000, 1, 1), dtpDenNgay.Value, KhachHang);
            }
            double TongCong = 0;
            for (int i = 0; i < Bang.Rows.Count; i++)
            {
                object[] Dong = new object[9];
                int STT = i + 1;
                Dong[0] = STT.ToString();
                Dong[1] = Bang.Rows[i]["MaKhachHang"].ToString();
                Dong[2] = Bang.Rows[i]["TenKhachHang"].ToString();
                Dong[3] = Bang.Rows[i]["MaPhieuXuat"].ToString();
                Dong[4] = ChuyenDoiNgay(Bang.Rows[i]["NgayXuat"].ToString());
                DateTime NgayXuat = DateTime.Parse(Bang.Rows[i]["NgayXuat"].ToString());
                int TuoiNo = DateTime.Now.DayOfYear - NgayXuat.DayOfYear;
                Dong[5] = TuoiNo.ToString();
                Dong[6] = clsSupport.CurrencyNumber(Bang.Rows[i]["TongTien"].ToString());
                double ConThu = double.Parse(Bang.Rows[i]["TongTien"].ToString()) - double.Parse(Bang.Rows[i]["DaTra"].ToString());
                Dong[7] = clsSupport.CurrencyNumber(Bang.Rows[i]["DaTra"].ToString());
                Dong[8] = clsSupport.CurrencyNumber(ConThu.ToString());
                TongCong += ConThu;
                grdvDSDonHangDaBan.Rows.Add(Dong);
            }
            txtTongCong.Text = clsSupport.CurrencyNumber(TongCong.ToString()) + " (VNĐ)";
        }

        private string ChuyenDoiNgay(string Ngay)
        {
            DateTime NgayNhap = DateTime.Parse(Ngay);
            string strNgay = NgayNhap.Day.ToString() + "/" + NgayNhap.Month.ToString() + "/" + NgayNhap.Year.ToString();
            return strNgay;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        private void btnInRa_Click(object sender, EventArgs e)
        {
            In();
        }

        private void In()
        {
            try
            {
                if (grdvDSDonHangDaBan.RowCount == 0)
                {
                    MessageBox.Show("Không có phiếu xuất bán hàng của khách hàng đã chọn trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptCongNoKhachHang congNoKH = new rptCongNoKhachHang();
                congNoKH.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);
                string MaKhachHang;
                if(cboKhachHang.SelectedItem!=null && cboKhachHang.SelectedIndex!=-1)
                {
                    MaKhachHang = cboKhachHang.SelectedValue.ToString().Trim();
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn khách hàng!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataTable Bang = PhieuXuatBanSiBus.ReportCongNoKhachHang(dtpDenNgay.Value, MaKhachHang);
                DataTable CongTy = CongTyBus.ReportCongTy();
                DataSet CacBang = new DataSet();
                if (Bang.Rows.Count != 0)
                {
                    CacBang.Tables.Add(Bang);
                    CacBang.Tables.Add(CongTy);

                    congNoKH.SetDataSource(CacBang);
                    congNoKH.SetParameterValue("@DenNgay", dtpDenNgay.Value);
                    congNoKH.SetParameterValue("@KhachHang", MaKhachHang);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = congNoKH;
                    dlgHienThi.ShowDialog();
                }
                else
                    MessageBox.Show("Không có phiếu xuất bán hàng của khách hàng đã chọn trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}