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
    public partial class frmDoanhThuTheoNVBH_ChiTiet : frmTemplete
    {
        #region Thuộc tính
        private clsPhieuXuatBUS PhieuXuatBus = new clsPhieuXuatBUS();
        private clsNhanVienDTO XemNhanVien = new clsNhanVienDTO();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        DateTime XemTuNgay = new DateTime();
        DateTime XemDenNgay = new DateTime();
        #endregion

        public frmDoanhThuTheoNVBH_ChiTiet()
        {
            InitializeComponent();
        }

        public frmDoanhThuTheoNVBH_ChiTiet(DateTime _TuNgay, DateTime _DenNgay, string _MaNhanVien)
        {
            XemNhanVien.MaNhanVien = _MaNhanVien;
            XemTuNgay = _TuNgay;
            XemDenNgay = _DenNgay;
            InitializeComponent();
        }
       
        private void frmDoanhThuTheoNVBH_ChiTiet_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboNhanVien();
                if (XemNhanVien.MaNhanVien != "")
                {
                    cboNhanVien.SelectedValue = XemNhanVien.MaNhanVien;
                    dtpDenNgay.Value =XemDenNgay ;
                    dtpTuNgay.Value = XemTuNgay;
                    KhoiTao(XemTuNgay, XemDenNgay, XemNhanVien);

                }
                else
                {
                    dtpDenNgay.Value = DateTime.Now;
                    dtpTuNgay.Value = DateTime.Now;
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
                    KhoiTao(dtpTuNgay.Value, dtpDenNgay.Value,NhanVien);
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
                Bang = PhieuXuatBus.TimKiemNV_CT(TuNgay,DenNgay);
            }
            else
            {
                Bang = PhieuXuatBus.TimKiemNV_CT(TuNgay,DenNgay, NhanVien);
            }

            //Loai bo di nhung ten nguoi dung trùng và số thứ tự
            for (int i = 0; i < Bang.Rows.Count-1; i++)
            {
                if (Bang.Rows[i]["TenNhanVien"].ToString() != "")
                {
                    for (int j = i + 1; j < Bang.Rows.Count; j++)
                    {
                        if (Bang.Rows[i]["TenNhanVien"].ToString() == Bang.Rows[j]["TenNhanVien"].ToString())
                        {
                            Bang.Rows[j]["TenNhanVien"] = "";
                        }
                    }
                }
            }
           
            double DoanhThuSum = 0;
            int STT = 0;
            for (int i = 0; i < Bang.Rows.Count; i++)
            {
                object[] Dong = new object[11];
                if (Bang.Rows[i]["TenNhanVien"].ToString() != "")
                {
                    STT ++;
                    Dong[0] = STT.ToString();
                }
                else
                {
                    Dong[0] = "";
                }
                //Dong[1] = Bang.Rows[i]["TenNguoiDung"].ToString();
                Dong[2] = Bang.Rows[i]["TenNhanVien"].ToString();
                Dong[3] = Bang.Rows[i]["TenKhachHang"].ToString();
                Dong[4] = Bang.Rows[i]["LoaiPhieuXuat"].ToString();
                Dong[5] = Bang.Rows[i]["MaPhieuXuat"].ToString();
                Dong[6] = ChuyenDoiNgay(Bang.Rows[i]["NgayXuat"].ToString());
                Dong[7] = Bang.Rows[i]["TienHang"];
                Dong[8] = Bang.Rows[i]["ChietKhau"];
                Dong[9] = Bang.Rows[i]["ThueVAT"];
                Dong[10] = Bang.Rows[i]["TongTien"];

                DoanhThuSum += double.Parse(Bang.Rows[i]["TongTien"].ToString());

                grdvDSDonHangDaBan.Rows.Add(Dong);
            }
            txtTongCong.Text = clsSupport.CurrencyNumber(DoanhThuSum.ToString()) + " (VNĐ)";
            
        }

        private string ChuyenDoiNgay(string Ngay)
        {
            DateTime NgayXuat = DateTime.Parse(Ngay);
            string strNgay = NgayXuat.Day.ToString() + "/" + NgayXuat.Month.ToString() + "/" + NgayXuat.Year.ToString();
            return strNgay;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        private void btnTongHop_Click(object sender, EventArgs e)
        {
            string MaNhanVien = ((DataRowView)cboNhanVien.SelectedItem).Row["MaNhanVien"].ToString().Trim();
            if (MaNhanVien == null || MaNhanVien == "")
            {
                MaNhanVien = "TatCa";
            }
            frmDoanhThuTheoNVBH f = new frmDoanhThuTheoNVBH(dtpTuNgay.Value, dtpDenNgay.Value, MaNhanVien);
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
                rptDoanhThuTheoNV_ChiTiet doanhThuTheoNV_CT = new rptDoanhThuTheoNV_ChiTiet();

                doanhThuTheoNV_CT.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);
                string MaNhanVien = cboNhanVien.SelectedValue.ToString().Trim();
                if (MaNhanVien == "TatCa")
                    MaNhanVien = "";

                DataTable Bang = PhieuXuatBus.ReportCT_PhieuXuatTheoNV(dtpTuNgay.Value, dtpDenNgay.Value, MaNhanVien);
                DataTable CongTy = CongTyBus.ReportCongTy();
                DataSet CacBang = new DataSet();
                if (Bang.Rows.Count != 0)
                {
                    CacBang.Tables.Add(Bang);
                    CacBang.Tables.Add(CongTy);

                    doanhThuTheoNV_CT.SetDataSource(CacBang);
                    doanhThuTheoNV_CT.SetParameterValue("@TuNgay", dtpTuNgay.Value);
                    doanhThuTheoNV_CT.SetParameterValue("@DenNgay", dtpDenNgay.Value);
                    doanhThuTheoNV_CT.SetParameterValue("@MaNhanVien", MaNhanVien);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = doanhThuTheoNV_CT;
                    dlgHienThi.ShowDialog();
                }
                else
                    MessageBox.Show("In Không thành công vì không có phiếu xuất bán hàng nào của nhân viên đã chọn trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }
     
    }
}