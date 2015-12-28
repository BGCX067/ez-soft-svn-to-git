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
    public partial class frmDoanhThu_ChiPhi : frmTemplete
    {
        #region Attribute
        private clsPhieuXuatBUS PhieuXuatBUS = new clsPhieuXuatBUS();
        private clsPhieuChiKhacBUS PhieuChiKhacBUS = new clsPhieuChiKhacBUS();
        private clsPhieuThuKhacBUS PhieuThuKhacBUS = new clsPhieuThuKhacBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        #endregion
        public frmDoanhThu_ChiPhi()
        {
            InitializeComponent();
        }

        private void frmDoanhThu_ChiPhi_Load(object sender, EventArgs e)
        {
            dtpDenNgay.Value = DateTime.Now;
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTuNgay.Focus();
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

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpTuNgay.Value == dtpDenNgay.Value)
                {
                    MessageBox.Show("Chưa có thông tin. Vui lòng chọn lại Từ Ngày.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpTuNgay.Focus();
                    return;
                }
                DataTable bangDoanhThu = PhieuXuatBUS.GetDoanhThu(dtpTuNgay.Value, dtpDenNgay.Value);
                if (bangDoanhThu.Rows.Count != 0)
                    txtDoanhThu.Text = bangDoanhThu.Rows[0]["DoanhThu"].ToString();
                else
                    txtDoanhThu.Text = "0";

                DataTable bangGiaVon = PhieuXuatBUS.GetGiaVon(dtpTuNgay.Value, dtpDenNgay.Value);
                if (bangGiaVon.Rows.Count != 0)
                    txtGiaVon.Text = bangGiaVon.Rows[0]["GiaVon"].ToString();
                else
                    txtGiaVon.Text = "0";

                double LaiGop = double.Parse(bangDoanhThu.Rows[0]["DoanhThu"].ToString()) - double.Parse(bangGiaVon.Rows[0]["GiaVon"].ToString());
                txtLaiGop.Text = LaiGop.ToString();

                DataTable bangChiPhiKhac = PhieuChiKhacBUS.GetChiPhiKhac(dtpTuNgay.Value, dtpDenNgay.Value);
                if (bangChiPhiKhac.Rows.Count != 0)
                    txtChiPhiKhac.Text = bangChiPhiKhac.Rows[0]["ChiPhiKhac"].ToString();
                else
                    txtChiPhiKhac.Text ="0";

                DataTable bangPhieuThuKhac = PhieuThuKhacBUS.GetThuNhapKhac(dtpTuNgay.Value, dtpDenNgay.Value);
                if (bangPhieuThuKhac.Rows.Count != 0)
                    txtThuNhapKhac.Text = bangPhieuThuKhac.Rows[0]["ThuNhapKhac"].ToString();
                else
                    txtThuNhapKhac.Text = "0";

                double LaiRong = double.Parse(txtLaiGop.Text) - double.Parse(bangChiPhiKhac.Rows[0]["ChiPhiKhac"].ToString()) + double.Parse(bangPhieuThuKhac.Rows[0]["ThuNhapKhac"].ToString());
                txtLaiRong.Text = LaiRong.ToString();

                btnInRa.Enabled = true;

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

        private void btnInRa_Click(object sender, EventArgs e)
        {
            In();
        }

        private void In()
        {
            try
            {
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptDoanhThu_ChiPhi doanhThuChiPhi = new rptDoanhThu_ChiPhi();
                doanhThuChiPhi.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                DataTable CongTy = CongTyBus.ReportCongTy();

                doanhThuChiPhi.SetDataSource(CongTy);
                doanhThuChiPhi.SetParameterValue("@TuNgay", dtpTuNgay.Value);
                doanhThuChiPhi.SetParameterValue("@DenNgay", dtpDenNgay.Value);
                doanhThuChiPhi.SetParameterValue("@DoanhThu", txtDoanhThu.Text);
                doanhThuChiPhi.SetParameterValue("@GiaVon", txtGiaVon.Text);
                doanhThuChiPhi.SetParameterValue("@LaiGop", txtLaiGop.Text);
                doanhThuChiPhi.SetParameterValue("@ChiPhiKhac", txtChiPhiKhac.Text);
                doanhThuChiPhi.SetParameterValue("@ThuNhapKhac", txtThuNhapKhac.Text);
                doanhThuChiPhi.SetParameterValue("@LaiRong", txtLaiRong.Text);

                dlgHienThi.CrystalReportViewer_TheHien.ReportSource = doanhThuChiPhi;
                dlgHienThi.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}