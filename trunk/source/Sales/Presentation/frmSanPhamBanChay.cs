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
    public partial class frmSanPhamBanChay : frmTemplete
    {
        #region Thuộc tính
        private clsPhieuXuatBUS PhieuXuatBus = new clsPhieuXuatBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        #endregion
        public frmSanPhamBanChay()
        {
            InitializeComponent();
        }

        private void frmSanPhamBanChay_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;        
        }

        #region Phím tắt
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Thoát 
            if (keyData == Keys.Escape)
            {
                DongCuaSo();
            }

            ////thông tin 
            //if (keyData == (Keys.Control | Keys.I))
            //{
            //    In();
            //}

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

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                //if (dtpTuNgay.Value == dtpDenNgay.Value)
                //{
                //    MessageBox.Show("Chưa có thông tin. Vui lòng chọn lại Từ Ngày.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    dtpTuNgay.Focus();
                //    return;
                //}

                if (grdvDoanhSo.Rows.Count > 0)
                    grdvDoanhSo.Rows.Clear();

                DataTable bangDoanhSo = PhieuXuatBus.GetSPBanChay_DoanhSo(dtpTuNgay.Value, dtpDenNgay.Value);
                for (int i = 0; i < bangDoanhSo.Rows.Count; i++)
                {
                    object[] Dong = new object[3];
                    int STT = 0;
                    STT = i + 1;
                    Dong[0] = STT.ToString();
                    Dong[1] = bangDoanhSo.Rows[i]["TenMatHang"].ToString();
                    Dong[2] = clsSupport.CurrencyNumber(bangDoanhSo.Rows[i]["DoanhSo"].ToString());
                    grdvDoanhSo.Rows.Add(Dong);
                }

                if (grdvSoLuong.Rows.Count > 0)
                    grdvSoLuong.Rows.Clear();
                    DataTable bangSoLuong = PhieuXuatBus.GetSPBanChay_SoLuong(dtpTuNgay.Value, dtpDenNgay.Value);
                    for (int i = 0; i < bangSoLuong.Rows.Count; i++)
                    {
                        object[] Dong = new object[3];
                        int STT = 0;
                        STT = i + 1;
                        Dong[0] = STT.ToString();
                        Dong[1] = bangSoLuong.Rows[i]["TenMatHang"].ToString();
                        Dong[2] = clsSupport.CurrencyNumber(bangSoLuong.Rows[i]["SoLuong"].ToString());
                        grdvSoLuong.Rows.Add(Dong);
                    }
                btnDoanhSo.Enabled = true;
                btnSoLuong.Enabled = true;
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

        private void btnSoLuong_Click(object sender, EventArgs e)
        {
            try
            {
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptSanPhamBanChay_SoLuong sanPhamBanChay = new rptSanPhamBanChay_SoLuong();
                sanPhamBanChay.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                DataTable soLuong = PhieuXuatBus.GetSPBanChay_SoLuong(dtpTuNgay.Value, dtpDenNgay.Value);

                if (soLuong.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();
                    DataSet CacBang = new DataSet();

                    CacBang.Tables.Add(soLuong);
                    CacBang.Tables.Add(CongTy);

                    sanPhamBanChay.SetDataSource(CacBang);
                    sanPhamBanChay.SetParameterValue("@TuNgay", dtpTuNgay.Value);
                    sanPhamBanChay.SetParameterValue("@DenNgay", dtpDenNgay.Value);

                    // lay lai de add cho report
                    sanPhamBanChay.SetParameterValue("@NgayTu", dtpTuNgay.Value);
                    sanPhamBanChay.SetParameterValue("@NgayDen", dtpDenNgay.Value);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = sanPhamBanChay;
                    dlgHienThi.ShowDialog();
                }
                else
                    MessageBox.Show("Chưa có sản phẩm.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDoanhSo_Click(object sender, EventArgs e)
        {
            try
            {
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptSanPhamBanChay_DoanhSo sanPhamBanChay = new rptSanPhamBanChay_DoanhSo();
                sanPhamBanChay.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                DataTable doanhSo = PhieuXuatBus.GetSPBanChay_DoanhSo(dtpTuNgay.Value, dtpDenNgay.Value);

                if (doanhSo.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();
                    DataSet CacBang = new DataSet();

                    CacBang.Tables.Add(doanhSo);
                    CacBang.Tables.Add(CongTy);

                    sanPhamBanChay.SetDataSource(CacBang);
                    sanPhamBanChay.SetParameterValue("@TuNgay", dtpTuNgay.Value);
                    sanPhamBanChay.SetParameterValue("@DenNgay", dtpDenNgay.Value);

                    // lay lai de add cho report
                    sanPhamBanChay.SetParameterValue("@NgayTu", dtpTuNgay.Value);
                    sanPhamBanChay.SetParameterValue("@NgayDen", dtpDenNgay.Value);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = sanPhamBanChay;
                    dlgHienThi.ShowDialog();
                }
                else
                    MessageBox.Show("Chưa có sản phẩm.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}