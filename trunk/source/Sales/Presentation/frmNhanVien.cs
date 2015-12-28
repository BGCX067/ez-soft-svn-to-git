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
    public partial class frmNhanVien : frmTemplete
    {
        #region Thuộc tính
        private clsNhanVienBUS NhanVienBus = new clsNhanVienBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        public clsNhanVienDTO NhanVienDTO = null;
        private string ThaoTac = ""; 
        #endregion
        public frmNhanVien()
        {
            InitializeComponent();
        }

        public frmNhanVien(string _ThaoTac)
        {
            ThaoTac = _ThaoTac;
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            txtNhanVien.Focus();
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

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                KhoiTao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KhoiTao()
        {
            if (grdvDSNhanVien.ColumnCount > 0)
            {
                grdvDSNhanVien.Columns.Clear();
                grdvDSNhanVien.DataSource = null;
                bindingSource1 = new BindingSource();
            }
            DataTable Bang;
            Bang = NhanVienBus.LayBangChoQuanTri();
            //Loc thong tin mat hang
            bindingSource1.DataSource = Bang;
            string sql = "";
            if (radioTatCa.Checked == false)
            {
                if (radioTheoTenNguoiDung.Checked == true)
                {
                    sql = " CONVERT([TenNguoiDung], 'System.String') = '" + txtNhanVien.Text.Trim() + "' ";
                }
                else
                {
                    sql = " CONVERT([TenNhanVien], 'System.String') LIKE '%" + txtNhanVien.Text.Trim() + "%' ";
                }
                if (txtNhanVien.Text.Trim() != "")
                {
                    bindingSource1.Filter = sql;
                }
            }
            grdvDSNhanVien.DataSource = bindingSource1;
            AnCotTrenLuoi();
            DinhDangCot();

        }

        private void AnCotTrenLuoi()
        {
            for (int i = 1; i < grdvDSNhanVien.ColumnCount; i++)
            {
                grdvDSNhanVien.Columns[i].Visible = false;
            }
            grdvDSNhanVien.Columns["TenNguoiDung"].Visible = true;
            grdvDSNhanVien.Columns["TenNguoiDung"].HeaderText = "Tên Người Dùng";
            grdvDSNhanVien.Columns["MaNhanVien"].Visible = true;
            grdvDSNhanVien.Columns["MaNhanVien"].HeaderText = "Mã NV";
            grdvDSNhanVien.Columns["TenNhanVien"].Visible = true;
            grdvDSNhanVien.Columns["TenNhanVien"].HeaderText = "Tên Nhân Viên";
            grdvDSNhanVien.Columns["DiaChi"].Visible = true;
            grdvDSNhanVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            grdvDSNhanVien.Columns["DienThoai"].Visible = true;
            grdvDSNhanVien.Columns["DienThoai"].HeaderText = "Điện Thoại";
            grdvDSNhanVien.Columns["GhiChu"].Visible = true;
            grdvDSNhanVien.Columns["GhiChu"].HeaderText = "Ghi Chú";
        }

        private void DinhDangCot()
        {
            grdvDSNhanVien.Columns[0].Width = 40;
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdvDSNhanVien.Columns["STT"].DefaultCellStyle = CellStyle;
            for (int i = 1; i < grdvDSNhanVien.Columns.Count; i++)
            {
                if (grdvDSNhanVien.Columns[i].Visible == true)
                {
                    grdvDSNhanVien.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grdvDSNhanVien.Columns[i].ReadOnly = true;
                }
            }
        }

        private void txtNhanVien_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    KhoiTao();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSuaLai_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdvDSNhanVien.CurrentRow != null && grdvDSNhanVien.CurrentRow.Index != -1)
                {
                    frmThemNhanVien f = new frmThemNhanVien(KhoiTaoNhanVien());
                    f.ShowDialog();
                    KhoiTao();
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn nhân viên cần sửa thông tin!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xin vui lòng chọn nhân viên", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private clsNhanVienDTO KhoiTaoNhanVien()
        {
            int dong = grdvDSNhanVien.CurrentRow.Index;
            clsNhanVienDTO NhanVien = new clsNhanVienDTO();
            NhanVien.MaNhanVien = grdvDSNhanVien.Rows[dong].Cells["MaNhanVien"].Value.ToString();
            NhanVien.TenNhanVien = grdvDSNhanVien.Rows[dong].Cells["TenNhanVien"].Value.ToString();
            NhanVien.DiaChi = grdvDSNhanVien.Rows[dong].Cells["DiaChi"].Value.ToString();
            NhanVien.DienThoai = grdvDSNhanVien.Rows[dong].Cells["DienThoai"].Value.ToString();
            NhanVien.GhiChu = grdvDSNhanVien.Rows[dong].Cells["GhiChu"].Value.ToString();
            NhanVien.TenNguoiDung = grdvDSNhanVien.Rows[dong].Cells["TenNguoiDung"].Value.ToString();
            NhanVien.MatKhau = grdvDSNhanVien.Rows[dong].Cells["MatKhau"].Value.ToString();
            NhanVien.QuyenHan.MaQuyenHan =int.Parse( grdvDSNhanVien.Rows[dong].Cells["QuyenHan"].Value.ToString());
            return NhanVien;
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemNhanVien f = new frmThemNhanVien();
                f.ShowDialog();
                KhoiTao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                if (grdvDSNhanVien.CurrentRow!=null && grdvDSNhanVien.CurrentRow.Index != -1)
                {
                    DialogResult result = MessageBox.Show("Bạn có thật sự muốn xóa nhân viên " + grdvDSNhanVien.Rows[grdvDSNhanVien.CurrentRow.Index].Cells["TenNhanVien"].Value.ToString(), "Xác nhận thông tin", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Loi = "Lỗi kết nối cơ sở dữ liệu";
                        if (NhanVienBus.Xoa(grdvDSNhanVien.Rows[grdvDSNhanVien.CurrentRow.Index].Cells["MaNhanVien"].Value.ToString().Trim()) != -1)
                        {
                            grdvDSNhanVien.Rows.RemoveAt(grdvDSNhanVien.CurrentRow.Index);
                        }
                        else
                        {
                            MessageBox.Show("Xóa nhân viên không thành công. Xin vui lòng thử lại", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn nhân viên!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Loi, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdvDSNhanVien_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ThaoTac == "ChonNhanVien")
            {
                NhanVienDTO = KhoiTaoNhanVien();
                this.Close();
            }
        }

        private void grdvDSNhanVien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && ThaoTac == "ChonNhanVien")
            {
                NhanVienDTO = KhoiTaoNhanVien();
                this.Close();
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
                if (grdvDSNhanVien.RowCount == 0)
                {
                    MessageBox.Show("In không thành công vì không có thông tin nhân viên.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptDSNhanVien dsNhanVien = new rptDSNhanVien();
                dsNhanVien.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                string TenNhanVien = "";
                string TenNguoiDung = "";
                if (radioTatCa.Checked == true)
                {
                    TenNhanVien = "";
                    TenNguoiDung = "";
                }
                else
                {
                    if (radioTheoTen.Checked == true)
                    {
                        TenNhanVien = txtNhanVien.Text;
                        TenNguoiDung = "";
                    }
                    else
                    {
                        TenNhanVien = "";
                        TenNguoiDung = txtNhanVien.Text;
                    }
                }

                DataTable bang = NhanVienBus.ReportDSNhanVien(TenNhanVien, TenNguoiDung);
                if (bang.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();

                    DataSet cacBang = new DataSet();
                    cacBang.Tables.Add(bang);
                    cacBang.Tables.Add(CongTy);

                    dsNhanVien.SetDataSource(cacBang);
                    dsNhanVien.SetParameterValue("@TenNhanVien", TenNhanVien);
                    dsNhanVien.SetParameterValue("@TenNguoiDung", TenNguoiDung);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = dsNhanVien;
                    dlgHienThi.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có thông tin nhân viên.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}