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
    public partial class frmNhomHang : frmTemplete
    {
        #region Thuộc tính
        private clsLoaiMatHangBUS LoaiMatHangBus = new clsLoaiMatHangBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        //clsLoaiMatHangDTO LoaiMatHangDTO;
        #endregion

        public frmNhomHang()
        {
            InitializeComponent();
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

        private void KhoiTao()
        {
            if (grdvDSLoaiMatHang.ColumnCount > 0)
            {
                grdvDSLoaiMatHang.Columns.Clear();
                grdvDSLoaiMatHang.DataSource = null;
                bindingSource1 = new BindingSource();
            }
            DataTable Bang;
            Bang = LoaiMatHangBus.LayBang();
            //Loc thong tin mat hang
            bindingSource1.DataSource = Bang;
            string sql = "";
            if (radioTheoMa.Checked == true)
            {
                sql = " CONVERT([MaLoaiMatHang], 'System.String') = '" + txtLoaiMatHang.Text.Trim() + "' ";
            }
            else
            {
                sql = " CONVERT([TenLoaiMatHang], 'System.String') LIKE '%" + txtLoaiMatHang.Text.Trim() + "%' ";
            }
            if (txtLoaiMatHang.Text.Trim() != "")
            {
                bindingSource1.Filter = sql;
            }
            grdvDSLoaiMatHang.DataSource = bindingSource1;
            AnCotTrenLuoi();
            DinhDangCot();

        }

        private void AnCotTrenLuoi()
        {
            grdvDSLoaiMatHang.Columns["MaLoaiMatHang"].Visible = true;
            grdvDSLoaiMatHang.Columns["MaLoaiMatHang"].HeaderText = "Mã Nhóm Hàng";
            grdvDSLoaiMatHang.Columns["TenLoaiMatHang"].Visible = true;
            grdvDSLoaiMatHang.Columns["TenLoaiMatHang"].HeaderText = "Tên Nhóm Hàng";
            grdvDSLoaiMatHang.Columns["DienGiai"].Visible = true;
            grdvDSLoaiMatHang.Columns["DienGiai"].HeaderText = "Ghi Chú";
            grdvDSLoaiMatHang.Columns["NgayTao"].Visible = false;
            grdvDSLoaiMatHang.Columns["TrangThai"].Visible = false;
        }

        private void DinhDangCot()
        {
            grdvDSLoaiMatHang.Columns[0].Width = 40;
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdvDSLoaiMatHang.Columns["STT"].DefaultCellStyle = CellStyle;
            for (int i = 1; i < grdvDSLoaiMatHang.Columns.Count; i++)
            {
                if (grdvDSLoaiMatHang.Columns[i].Visible == true)
                {
                    grdvDSLoaiMatHang.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grdvDSLoaiMatHang.Columns[i].ReadOnly = true;
                }
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            try
            {

                frmThemNhom fLoaiMatHang = new frmThemNhom();
                fLoaiMatHang.ShowDialog();
                KhoiTao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cở sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaLai_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdvDSLoaiMatHang.CurrentRow != null && grdvDSLoaiMatHang.CurrentRow.Index != -1)
                {
                    frmThemNhom fLoaiMatHang = new frmThemNhom(KhoiTaoLoaiMatHang());
                    fLoaiMatHang.ShowDialog();
                    KhoiTao();
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn nhóm hàng!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cở sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private clsLoaiMatHangDTO KhoiTaoLoaiMatHang()
        {
            int dong = grdvDSLoaiMatHang.CurrentRow.Index;
            clsLoaiMatHangDTO LoaiMatHang = new clsLoaiMatHangDTO();
            LoaiMatHang.MaLoaiMatHang = grdvDSLoaiMatHang.Rows[dong].Cells["MaLoaiMatHang"].Value.ToString();
            LoaiMatHang.TenLoaiMatHang = grdvDSLoaiMatHang.Rows[dong].Cells["TenLoaiMatHang"].Value.ToString();
            LoaiMatHang.DienGiai = grdvDSLoaiMatHang.Rows[dong].Cells["DienGiai"].Value.ToString();
            LoaiMatHang.TrangThai = int.Parse(grdvDSLoaiMatHang.Rows[dong].Cells["TrangThai"].Value.ToString());
            return LoaiMatHang;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String Loi = "";
            try
            {
                if (grdvDSLoaiMatHang.CurrentRow !=null && grdvDSLoaiMatHang.CurrentRow.Index != -1)
                {
                    DialogResult result = MessageBox.Show("Bạn có thật sự muốn xóa nhóm hàng " + grdvDSLoaiMatHang.Rows[grdvDSLoaiMatHang.CurrentRow.Index].Cells["TenLoaiMatHang"].Value.ToString(), "Xác nhận thông tin", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Loi = "Lỗi kết nối cơ sở dữ liệu";
                        if (LoaiMatHangBus.Xoa(grdvDSLoaiMatHang.Rows[grdvDSLoaiMatHang.CurrentRow.Index].Cells["MaLoaiMatHang"].Value.ToString().Trim()) != -1)
                        {
                            grdvDSLoaiMatHang.Rows.RemoveAt(grdvDSLoaiMatHang.CurrentRow.Index);
                        }
                        else
                        {
                            MessageBox.Show("Không được phép xoá '" + grdvDSLoaiMatHang.Rows[grdvDSLoaiMatHang.CurrentRow.Index].Cells["TenLoaiMatHang"].Value.ToString() + "' vì nhóm hàng này đã được nhập mặt hàng!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn nhóm hàng!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Loi, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                KhoiTao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmNhomHang_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtLoaiMatHang_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    KhoiTao();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                if (grdvDSLoaiMatHang.RowCount == 0)
                {
                    MessageBox.Show("In không thành công vì không có thông tin nhóm hàng.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptDSNhomHang dsNhomHang = new rptDSNhomHang();
                dsNhomHang.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                string MaLoaiNhomHang = "";
                string TenLoaiNhomHang = "";
                if (txtLoaiMatHang.Text == "")
                {
                    MaLoaiNhomHang = "";
                    TenLoaiNhomHang = "";
                }
                else
                {
                    if (radioTheoMa.Checked == true)
                    {
                        MaLoaiNhomHang = txtLoaiMatHang.Text;
                        TenLoaiNhomHang = "";
                    }
                    else
                    {
                        MaLoaiNhomHang = "";
                        TenLoaiNhomHang = txtLoaiMatHang.Text;
                    }
                }

                DataTable bang = LoaiMatHangBus.ReportDSLoaiMatHang(MaLoaiNhomHang, TenLoaiNhomHang);
                if (bang.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();

                    DataSet cacBang = new DataSet();
                    cacBang.Tables.Add(bang);
                    cacBang.Tables.Add(CongTy);

                    dsNhomHang.SetDataSource(cacBang);
                    dsNhomHang.SetParameterValue("@MaLoaiMatHang", MaLoaiNhomHang);
                    dsNhomHang.SetParameterValue("@TenLoaiMatHang", TenLoaiNhomHang);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = dsNhomHang;
                    dlgHienThi.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có thông tin nhóm hàng.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}