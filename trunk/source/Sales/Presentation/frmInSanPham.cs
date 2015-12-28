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
    public partial class frmInSanPham : Form
    {
        #region Thuộc tính
        private clsMatHangBUS MatHangBus = new clsMatHangBUS();
        #endregion

        public frmInSanPham()
        {
            InitializeComponent();
        }

        private void btnGiaSi_Click(object sender, EventArgs e)
        {
            //frmTheHienReport dlgHienThi = new frmTheHienReport();
            //rptBaoGiaSi baoGiaSi = new rptBaoGiaSi();
            //string MaMatHang = "";
            //string TenMatHang = "";
            //if (radioTatCa.Checked == true)
            //{
            //    MaMatHang = "";
            //    TenMatHang = "";
            //}
            //else
            //{
            //    if (radioTheoMa.Checked == true)
            //    {
            //        MaMatHang = txtMatHang.Text;
            //        TenMatHang = "";
            //    }
            //    else
            //    {
            //        MaMatHang = "";
            //        TenMatHang = txtMatHang.Text;
            //    }
            //}

            //DataTable bang = MatHangBus.ReportBaoGiaSi(MaMatHang, TenMatHang);


            //baoGiaSi.SetDataSource(bang);
            //baoGiaSi.SetParameterValue("@MaMatHang", MaMatHang);
            //baoGiaSi.SetParameterValue("@TenMatHang", TenMatHang);

            //dlgHienThi.CrystalReportViewer_TheHien.ReportSource = baoGiaSi;
            //dlgHienThi.ShowDialog();
        }
    }
}