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
    public partial class frmDoanhThuTheoNVBH : frmTemplete
    {
        #region Thuộc tính
        private clsPhieuXuatBUS PhieuXuatBus = new clsPhieuXuatBUS();
        private clsNhanVienDTO XemNhanVien = new clsNhanVienDTO();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        DateTime XemTuNgay = new DateTime();
        DateTime XemDenNgay = new DateTime();
        #endregion

        public frmDoanhThuTheoNVBH()
        {
            InitializeComponent();
        }

        public frmDoanhThuTheoNVBH(DateTime _TuNgay, DateTime _DenNgay, string _MaNhanVien)
        {
            XemNhanVien.MaNhanVien = _MaNhanVien;
            XemTuNgay = _TuNgay;
            XemDenNgay = _DenNgay;
            InitializeComponent();
        }

        private void frmDoanhThuTheoNVBH_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboNhanVien();
                if (XemNhanVien.MaNhanVien != "")
                {
                    cboNhanVien.SelectedValue = XemNhanVien.MaNhanVien;
                    dtpDenNgay.Value = XemDenNgay;
                    dtpTuNgay.Value = XemTuNgay;
                    KhoiTao(XemTuNgay, XemDenNgay, XemNhanVien);

                }
                else
                {
                    dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    dtpDenNgay.Value = DateTime.Now;

                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Phím tắt
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //thông tin 
            if (keyData == (Keys.Control | Keys.I))
            {
                In();
            }
            //Thoát 
            if (keyData == Keys.Escape)
            {
                DongCuaSo();
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

        //Nhân viên
        private void KhoiTaoComboNhanVien()
        {
            DataTable BangNhanVien = new clsNhanVienBUS().LayBang();
            if (BangNhanVien.Rows.Count > 0)
            {
                //Đưa dòng thứ 0 vào vị trí cuối cùng và sau đó gán lại dòng thứ ko là tất cả
                DataRow DongTam = BangNhanVien.NewRow();
                DongTam["MaNhanVien"] = BangNhanVien.Rows[0]["MaNhanVien"];
                DongTam["TenNhanVien"] = BangNhanVien.Rows[0]["TenNhanVien"];
                DongTam["DienThoai"] = BangNhanVien.Rows[0]["DienThoai"];
                DongTam["DiaChi"] = BangNhanVien.Rows[0]["DiaChi"];
                DongTam["GhiChu"] = BangNhanVien.Rows[0]["GhiChu"];
                BangNhanVien.Rows.Add(DongTam);

                BangNhanVien.Rows[0]["MaNhanVien"] = "TatCa";
                BangNhanVien.Rows[0]["TenNhanVien"] = "<Tất cả>";
                BangNhanVien.Rows[0]["DienThoai"] = "";
                BangNhanVien.Rows[0]["DiaChi"] = "";
                BangNhanVien.Rows[0]["GhiChu"] = "";
            }
            else
            {
                DataRow DongTam = BangNhanVien.NewRow();
                DongTam["MaNhanVien"] = "TatCa";
                DongTam["TenNhanVien"] = "<Tất cả>";
                DongTam["DienThoai"] = "";
                DongTam["DiaChi"] = "";
                DongTam["GhiChu"] = "";
                BangNhanVien.Rows.Add(DongTam);
            }

            cboNhanVien.DataSource = BangNhanVien;
            cboNhanVien.DisplayMember = "TenNhanVien";
            cboNhanVien.ValueMember = "MaNhanVien";
            cboNhanVien.SelectedIndex = 0;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboNhanVien.SelectedIndex != -1)
                {
                    clsNhanVienDTO NhanVien = new clsNhanVienDTO();
                    NhanVien.MaNhanVien = ((DataRowView)cboNhanVien.SelectedItem).Row["MaNhanVien"].ToString().Trim();
                    NhanVien.TenNhanVien = ((DataRowView)cboNhanVien.SelectedItem).Row["TenNhanVien"].ToString().Trim();
                    KhoiTao(dtpTuNgay.Value, dtpDenNgay.Value, NhanVien);
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KhoiTao(DateTime TuNgay, DateTime DenNgay, clsNhanVienDTO NhanVien)
        {
            if (grdvDSDonHangDaBan.Rows.Count > 0)
            {
                grdvDSDonHangDaBan.Rows.Clear();
            }
            DataTable Bang;
            if (NhanVien.MaNhanVien == "TatCa")
            {
                Bang = PhieuXuatBus.TimKiemNV(dtpTuNgay.Value, dtpDenNgay.Value);
            }
            else
            {
                Bang = PhieuXuatBus.TimKiemNV(dtpTuNgay.Value, dtpDenNgay.Value, NhanVien);
            }

           
            double DoanhThuSum = 0;
            double DoanhThuCoThueSum = 0;
            for (int i = 0; i < Bang.Rows.Count; i++)
            {
                object[] Dong = new object[5];
                int STT = i + 1;
                Dong[0] = STT.ToString();
                Dong[1] = Bang.Rows[i]["TenNguoiDung"].ToString();
                Dong[2] = Bang.Rows[i]["TenNhanVien"].ToString();
                Dong[3] = Bang.Rows[i]["TienHang"]; // Doanh Thu chưa có thuế
                Dong[4] = Bang.Rows[i]["DoanhThuCoThue"]; // Doanh Thu có thuế

                DoanhThuSum += double.Parse(Bang.Rows[i]["TienHang"].ToString());
                DoanhThuCoThueSum += double.Parse(Bang.Rows[i]["DoanhThuCoThue"].ToString());

                grdvDSDonHangDaBan.Rows.Add(Dong);
            }
            txtTongCong.Text = clsSupport.CurrencyNumber(DoanhThuSum.ToString()) + " (VNĐ)";
            txtTongCongCoThue.Text = clsSupport.CurrencyNumber(DoanhThuCoThueSum.ToString()) + " (VNĐ)";
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            string MaNhanVien=((DataRowView)cboNhanVien.SelectedItem).Row["MaNhanVien"].ToString().Trim();
            if (MaNhanVien == null || MaNhanVien == "")
            {
                MaNhanVien = "TatCa";
            }
            frmDoanhThuTheoNVBH_ChiTiet f = new frmDoanhThuTheoNVBH_ChiTiet(dtpTuNgay.Value, dtpDenNgay.Value, MaNhanVien);
            f.Show();
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
                rptDoanhThuTheoNhanVien doanhThuTheoNV = new rptDoanhThuTheoNhanVien();
                doanhThuTheoNV.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                string MaNhanVien = cboNhanVien.SelectedValue.ToString().Trim();
                if (MaNhanVien == "TatCa")
                    MaNhanVien = "";

                DataTable Bang = PhieuXuatBus.ReportPhieuXuatTheoNV(dtpTuNgay.Value, dtpDenNgay.Value, MaNhanVien);
                DataTable CongTy = CongTyBus.ReportCongTy();
                DataSet CacBang = new DataSet();
                if (Bang.Rows.Count != 0)
                {
                    CacBang.Tables.Add(Bang);
                    CacBang.Tables.Add(CongTy);

                    doanhThuTheoNV.SetDataSource(CacBang);
                    doanhThuTheoNV.SetParameterValue("@TuNgay", dtpTuNgay.Value);
                    doanhThuTheoNV.SetParameterValue("@DenNgay", dtpDenNgay.Value);
                    doanhThuTheoNV.SetParameterValue("@MaNhanVien", MaNhanVien);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = doanhThuTheoNV;
                    dlgHienThi.ShowDialog();
                }
                else
                    MessageBox.Show("In không thành công vì không có phiếu xuất bán hàng của nhân viên đã chọn trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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