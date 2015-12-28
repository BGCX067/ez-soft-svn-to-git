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
    public partial class frmTonKhoHangHoa : frmTemplete
    {
        #region Thuộc tính
        private clsMatHangBUS MatHangBus = new clsMatHangBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        #endregion

        public frmTonKhoHangHoa()
        {
            InitializeComponent();
        }

        private void frmTonKhoHangHoa_Load(object sender, EventArgs e)
        {
            try
            {
                dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpDenNgay.Value = DateTime.Now.Date;
                KhoiTaoComboNhomHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Phím tắt
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //in thông tin 
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

        private void KhoiTaoComboNhomHang()
        {
            //Load combo nhom hang
            DataTable BangNhomHang = new clsLoaiMatHangBUS().LayBang();

            if (BangNhomHang.Rows.Count > 0)
            {
                //Đưa dòng thứ 0 vào vị trí cuối cùng và sau đó gán lại dòng thứ ko là tất cả
                DataRow DongTam = BangNhomHang.NewRow();
                DongTam["MaLoaiMatHang"] = BangNhomHang.Rows[0]["MaLoaiMatHang"];
                DongTam["TenLoaiMatHang"] = BangNhomHang.Rows[0]["TenLoaiMatHang"];
                BangNhomHang.Rows.Add(DongTam);
                BangNhomHang.Rows[0]["MaLoaiMatHang"] = "TatCa";
                BangNhomHang.Rows[0]["TenLoaiMatHang"] = "Tất cả";

            }
            else
            {
                DataRow DongTam = BangNhomHang.NewRow();
                DongTam["MaLoaiMatHang"] = "TatCa";
                DongTam["TenLoaiMatHang"] = "Tất cả";
                BangNhomHang.Rows.Add(DongTam);
            }
            cboNhomHang.DataSource = BangNhomHang;
            cboNhomHang.DisplayMember = "TenLoaiMatHang";
            cboNhomHang.ValueMember = "MaLoaiMatHang";

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboNhomHang.SelectedIndex != -1)
                {
                    KhoiTao(cboNhomHang.SelectedValue.ToString().Trim());
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn nhóm hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KhoiTao(string MaNhomHang)
        {
            if (grdvDSMatHang.ColumnCount > 0)
            {
                grdvDSMatHang.Columns.Clear();
                grdvDSMatHang.DataSource = null;
                bindingSource1 = new BindingSource();
            }
            DataTable Bang;

            if (MaNhomHang == "TatCa")
            {
                Bang = MatHangBus.ThongKeHangTonKho(dtpTuNgay.Value,dtpDenNgay.Value,"TatCa");
            }
            else
            {
                Bang = MatHangBus.ThongKeHangTonKho(dtpTuNgay.Value, dtpDenNgay.Value, MaNhomHang);
            }
            //Loc thong tin mat hang
            bindingSource1.DataSource = Bang;
            string sql = "";
            if (radioTheoMa.Checked == true)
            {
                sql = " CONVERT([MaMatHang], 'System.String') = '" + txtMatHang.Text.Trim() + "' ";
            }
            else
            {
                sql = " CONVERT([TenMatHang], 'System.String') LIKE '%" + txtMatHang.Text.Trim() + "%' ";
            }
            if (txtMatHang.Text.Trim() != "")
            {
                bindingSource1.Filter = sql;
            }
            grdvDSMatHang.DataSource = bindingSource1;
            AnCotTrenLuoi();
            DinhDangCot();
            TinhTongTien();
        }

        private void TinhTongTien()
        {
            double TongCong = 0;
            for (int i = 0; i < grdvDSMatHang.Rows.Count; i++)
            {
                TongCong += double.Parse(grdvDSMatHang.Rows[i].Cells["TriGiaTonKho"].Value.ToString().Replace(",", ""));
            }
            txtTongCong.Text = clsSupport.CurrencyNumber(TongCong.ToString()) + " (VNĐ)";
        }

        private void AnCotTrenLuoi()
        {
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            grdvDSMatHang.Columns["MaMatHang"].HeaderText = "Mã Hàng";

            grdvDSMatHang.Columns["TenMatHang"].HeaderText = "Tên Hàng";
            grdvDSMatHang.Columns["TenMatHang"].Width = 120;

            grdvDSMatHang.Columns["XuatXu"].HeaderText = "Xuất Xứ";

            grdvDSMatHang.Columns["DonViTinh"].HeaderText = "ĐVT";

            grdvDSMatHang.Columns["GiaMuaBQ"].HeaderText = "Gia Mua BQ";
            grdvDSMatHang.Columns["GiaMuaBQ"].DefaultCellStyle = CellStyle;
            grdvDSMatHang.Columns["GiaMuaBQ"].Width = 90;

            grdvDSMatHang.Columns["SLDauKy"].HeaderText = "SL Đầu Kỳ";
            grdvDSMatHang.Columns["SLDauKy"].DefaultCellStyle = CellStyle;

            grdvDSMatHang.Columns["SLNhap"].HeaderText = "SL Nhập";
            grdvDSMatHang.Columns["SLNhap"].DefaultCellStyle = CellStyle;

            grdvDSMatHang.Columns["SLXuat"].HeaderText = "SL Xuất";
            grdvDSMatHang.Columns["SLXuat"].DefaultCellStyle = CellStyle;

            grdvDSMatHang.Columns["SLCuoiKy"].HeaderText = "SL Cuối Kỳ";
            grdvDSMatHang.Columns["SLCuoiKy"].DefaultCellStyle = CellStyle;

            grdvDSMatHang.Columns["TriGiaTonKho"].HeaderText = "Giá Trị Tồn Kho";
            grdvDSMatHang.Columns["TriGiaTonKho"].DefaultCellStyle = CellStyle;
            grdvDSMatHang.Columns["TriGiaTonKho"].Width = 90;

            grdvDSMatHang.Columns["LuongMin"].HeaderText = "Min";
            grdvDSMatHang.Columns["LuongMin"].DefaultCellStyle = CellStyle;

            grdvDSMatHang.Columns["LuongMax"].HeaderText = "Max";
            grdvDSMatHang.Columns["LuongMax"].DefaultCellStyle = CellStyle;

            grdvDSMatHang.Columns["BoSung"].HeaderText = "Bổ Sung";
            grdvDSMatHang.Columns["BoSung"].DefaultCellStyle = CellStyle;
        }

        private void DinhDangCot()
        {
            grdvDSMatHang.Columns[0].Width = 40;
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdvDSMatHang.Columns["STT"].DefaultCellStyle = CellStyle;
            for (int i = 1; i < grdvDSMatHang.Columns.Count; i++)
            {
                grdvDSMatHang.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grdvDSMatHang.Columns[i].ReadOnly = true;
            }
            DataGridViewCellStyle CellStyleCurrency = new DataGridViewCellStyle();
            CellStyleCurrency.Alignment = DataGridViewContentAlignment.MiddleRight;
            CellStyleCurrency.Format = "#,##0.############";
            grdvDSMatHang.Columns["LuongMin"].DefaultCellStyle = CellStyleCurrency;
            grdvDSMatHang.Columns["LuongMax"].DefaultCellStyle = CellStyleCurrency;
            grdvDSMatHang.Columns["GiaMuaBQ"].DefaultCellStyle = CellStyleCurrency;
            grdvDSMatHang.Columns["SLDauKy"].DefaultCellStyle = CellStyleCurrency;
            grdvDSMatHang.Columns["SLNhap"].DefaultCellStyle = CellStyleCurrency;
            grdvDSMatHang.Columns["SLXuat"].DefaultCellStyle = CellStyleCurrency;
            grdvDSMatHang.Columns["SLCuoiKy"].DefaultCellStyle = CellStyleCurrency;
            grdvDSMatHang.Columns["TriGiaTonKho"].DefaultCellStyle = CellStyleCurrency;
            grdvDSMatHang.Columns["BoSung"].DefaultCellStyle = CellStyleCurrency;
        }

        private void btnInRa_Click(object sender, EventArgs e)
        {
           
                In();
            
        }

        private void In()
        {
 try
            {
            if (grdvDSMatHang.RowCount == 0)
            {
                MessageBox.Show("Không có mặt hàng tồn kho trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmTheHienReport dlgHienThi = new frmTheHienReport();
            rptTonKhoHangHoa TonKhoHangHoa = new rptTonKhoHangHoa();
            TonKhoHangHoa.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

            string MaMatHang = "";
            string TenMatHang="";
            if (radioTheoMa.Checked == true)
            {
               MaMatHang= txtMatHang.Text.Trim();
            }
            else
            {
               TenMatHang= txtMatHang.Text.Trim() ;
            }
            DataTable bang = MatHangBus.ReportThongKeHangTonKho(dtpTuNgay.Value, dtpDenNgay.Value, cboNhomHang.SelectedValue.ToString().Trim(), MaMatHang,TenMatHang);
            if (bang.Rows.Count != 0)
            {
                DataTable CongTy = CongTyBus.ReportCongTy();
                DataSet CacBang = new DataSet();
                CacBang.Tables.Add(bang);
                CacBang.Tables.Add(CongTy);
                TonKhoHangHoa.SetDataSource(CacBang);
                TonKhoHangHoa.SetParameterValue("@TuNgay", dtpTuNgay.Value);
                TonKhoHangHoa.SetParameterValue("@DenNgay", dtpDenNgay.Value);
                TonKhoHangHoa.SetParameterValue("@MaLoaiMatHang", cboNhomHang.SelectedValue.ToString().Trim());
                TonKhoHangHoa.SetParameterValue("@MaMatHang", MaMatHang);
                TonKhoHangHoa.SetParameterValue("@TenMatHang", TenMatHang);
                TonKhoHangHoa.SetParameterValue("@HangHoa", txtMatHang.Text.Trim());
                TonKhoHangHoa.SetParameterValue("@TenLoaiMatHang", ((DataRowView)cboNhomHang.SelectedItem).Row["TenLoaiMatHang"].ToString().Trim());

                TonKhoHangHoa.SetParameterValue("@NgayTu", dtpTuNgay.Value);
                TonKhoHangHoa.SetParameterValue("@NgayDen", dtpDenNgay.Value);
                

                dlgHienThi.CrystalReportViewer_TheHien.ReportSource = TonKhoHangHoa;
                dlgHienThi.ShowDialog();
            }
            else
                MessageBox.Show("Không có mặt hàng tồn kho trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi kết nối", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        }

    }
}