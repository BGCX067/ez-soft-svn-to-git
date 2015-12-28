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
    public partial class frmCongNoNhaCungCap : frmTemplete
    {
        #region Thuộc tính
        private clsPhieuNhapBUS PhieuNhapBus = new clsPhieuNhapBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        #endregion

        public frmCongNoNhaCungCap()
        {
            InitializeComponent();
        }

        private void frmCongNoNhaCungCap_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboNhaCungCap();
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
                BangNhaCungCap.Rows[0]["MaNhaCungCap"] = "";
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
                DongTam["MaNhaCungCap"] = "";
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
                        KhoiTao(NhaCungCap);
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

        private void KhoiTao(clsNhaCungCapDTO NhaCungCap)
        {
            if (grdvDSDonHangDaBan.Rows.Count > 0)
            {
                grdvDSDonHangDaBan.Rows.Clear();
                //bindingSource1 = new BindingSource();
            }
            DataTable Bang;
            if (NhaCungCap.TenNhaCungCap == "<Tất cả>")
            {
                Bang = PhieuNhapBus.CongNoNhaCungCap(new DateTime(2000, 1, 1), dtpDenNgay.Value);
            }
            else
            {
                Bang = PhieuNhapBus.CongNoNhaCungCapTheoNCC(new DateTime(2000, 1, 1), dtpDenNgay.Value, NhaCungCap);
            }
            double TongCong = 0;
            for (int i = 0; i < Bang.Rows.Count; i++)
            {
                
                object[] Dong = new object[9];
                int STT = i + 1;
                Dong[0] = STT.ToString();
                Dong[1] = Bang.Rows[i]["MaNhaCungCap"].ToString();
                Dong[2] = Bang.Rows[i]["TenNhaCungCap"].ToString();
                Dong[3] = Bang.Rows[i]["MaPhieuNhap"].ToString();
                Dong[4] = ChuyenDoiNgay(Bang.Rows[i]["NgayNhap"].ToString());
                DateTime NgayNhap = DateTime.Parse(Bang.Rows[i]["NgayNhap"].ToString());
                int ThuoiNo = DateTime.Now.DayOfYear - NgayNhap.DayOfYear;
                Dong[5] = ThuoiNo.ToString();
                Dong[6] = clsSupport.CurrencyNumber(Bang.Rows[i]["TongTien"].ToString());
                double DaTra = double.Parse(Bang.Rows[i]["TongTien"].ToString()) - double.Parse(Bang.Rows[i]["ConNo"].ToString());
                Dong[7] = clsSupport.CurrencyNumber(DaTra.ToString());
                Dong[8] = clsSupport.CurrencyNumber(Bang.Rows[i]["ConNo"].ToString());
                TongCong += double.Parse(Bang.Rows[i]["ConNo"].ToString());
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

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboNhaCungCap.SelectedIndex != -1)
                {
                    clsNhaCungCapDTO NhaCungCap = new clsNhaCungCapDTO();
                    NhaCungCap.MaNhaCungCap = ((DataRowView)cboNhaCungCap.SelectedItem).Row["MaNhaCungCap"].ToString().Trim();
                    NhaCungCap.TenNhaCungCap = ((DataRowView)cboNhaCungCap.SelectedItem).Row["TenNhaCungCap"].ToString().Trim();
                    KhoiTao(NhaCungCap);
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
                    MessageBox.Show("Không có phiếu nhập hàng của khách hàng đã chọn trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptCongNoNhaCungCap congNoNCC = new rptCongNoNhaCungCap();
                congNoNCC.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                string MaNhaCungCap;
                if (cboNhaCungCap.SelectedItem != null && cboNhaCungCap.SelectedIndex != -1)
                {
                    MaNhaCungCap = cboNhaCungCap.SelectedValue.ToString().Trim();
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn Nhà cung cấp!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataTable Bang = PhieuNhapBus.ReportCongNoNhaCungCap(dtpDenNgay.Value, MaNhaCungCap);
                DataTable CongTy = CongTyBus.ReportCongTy();
                DataSet CacBang = new DataSet();
                if (Bang.Rows.Count != 0)
                {
                    CacBang.Tables.Add(Bang);
                    CacBang.Tables.Add(CongTy);

                    congNoNCC.SetDataSource(CacBang);
                    congNoNCC.SetParameterValue("@DenNgay", dtpDenNgay.Value);
                    congNoNCC.SetParameterValue("@MaNhaCungCap", MaNhaCungCap);
                    congNoNCC.SetParameterValue("@NgayDen", dtpDenNgay.Value);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = congNoNCC;
                    dlgHienThi.ShowDialog();
                }
                else
                    MessageBox.Show("Không có phiếu nhập hàng của nhà cung cấp đã chọn trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}