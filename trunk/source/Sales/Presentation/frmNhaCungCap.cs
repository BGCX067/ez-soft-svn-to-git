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
    public partial class frmNhaCungCap : frmTemplete
    {
        #region Thuộc tính
        private clsNhaCungCapBUS NhaCungCapBus = new clsNhaCungCapBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        public clsNhaCungCapDTO NhaCungCapDTO = null;
        private string ThaoTac = ""; 
        #endregion
        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        public frmNhaCungCap(string _ThaoTac)
        {
            ThaoTac = _ThaoTac;
            InitializeComponent();
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            txtNhaCungCap.Focus();
        }

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
            if (grdvDSNhaCungCap.ColumnCount > 0)
            {
                grdvDSNhaCungCap.Columns.Clear();
                grdvDSNhaCungCap.DataSource = null;
                bindingSource1 = new BindingSource();
            }
            DataTable Bang;
            Bang = NhaCungCapBus.LayBang();
            //Loc thong tin mat hang
            bindingSource1.DataSource = Bang;
            string sql = "";
            if (radioTatCa.Checked == false)
            {
                if (radioTheoMa.Checked == true)
                {
                    sql = " CONVERT([MaNhaCungCap], 'System.String') = '" + txtNhaCungCap.Text.Trim() + "' ";
                }
                else
                {
                    sql = " CONVERT([TenNhaCungCap], 'System.String') LIKE '%" + txtNhaCungCap.Text.Trim() + "%' ";
                }
                if (txtNhaCungCap.Text.Trim() != "")
                {
                    bindingSource1.Filter = sql;
                }
                else
                {

                }
            }
            grdvDSNhaCungCap.DataSource = bindingSource1;
            AnCotTrenLuoi();
            DinhDangCot();

        }

        private void AnCotTrenLuoi()
        {
            for (int i = 1; i < grdvDSNhaCungCap.ColumnCount; i++)
            {
                grdvDSNhaCungCap.Columns[i].Visible = false;
            }
            grdvDSNhaCungCap.Columns["MaNhaCungCap"].Visible = true;
            grdvDSNhaCungCap.Columns["MaNhaCungCap"].HeaderText = "Mã NCC";
            grdvDSNhaCungCap.Columns["TenNhaCungCap"].Visible = true;
            grdvDSNhaCungCap.Columns["TenNhaCungCap"].HeaderText = "Nhà Cung Cấp";
            grdvDSNhaCungCap.Columns["DiaChi"].Visible = true;
            grdvDSNhaCungCap.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            grdvDSNhaCungCap.Columns["MaSoThue"].Visible = true;
            grdvDSNhaCungCap.Columns["MaSoThue"].HeaderText = "Mã Số Thuế";
            grdvDSNhaCungCap.Columns["DienThoai"].Visible = true;
            grdvDSNhaCungCap.Columns["DienThoai"].HeaderText = "Điện Thoại";
            grdvDSNhaCungCap.Columns["Fax"].Visible = true;
            grdvDSNhaCungCap.Columns["Fax"].HeaderText = "Fax";
            grdvDSNhaCungCap.Columns["TenNguoiLienHe"].Visible = true;
            grdvDSNhaCungCap.Columns["TenNguoiLienHe"].HeaderText = "Liên hệ";
        }

        private void DinhDangCot()
        {
            grdvDSNhaCungCap.Columns[0].Width = 40;
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdvDSNhaCungCap.Columns["STT"].DefaultCellStyle = CellStyle;
            for (int i = 1; i < grdvDSNhaCungCap.Columns.Count; i++)
            {
                if (grdvDSNhaCungCap.Columns[i].Visible == true)
                {
                    grdvDSNhaCungCap.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grdvDSNhaCungCap.Columns[i].ReadOnly = true;
                }
            }
        }

        private void txtNhaCungCap_KeyUp(object sender, KeyEventArgs e)
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
                if (grdvDSNhaCungCap.CurrentRow != null && grdvDSNhaCungCap.CurrentRow.Index != -1)
                {
                    frmThemNhaCungCap f = new frmThemNhaCungCap(KhoiTaoNhaCungCap());
                    f.ShowDialog();
                    KhoiTao();
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn nhà cung cấp!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xin vui lòng chọn nhà cung cấp", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private clsNhaCungCapDTO KhoiTaoNhaCungCap()
        {
            int dong = grdvDSNhaCungCap.CurrentRow.Index;
            clsNhaCungCapDTO NhaCungCap = new clsNhaCungCapDTO();
            NhaCungCap.MaNhaCungCap = grdvDSNhaCungCap.Rows[dong].Cells["MaNhaCungCap"].Value.ToString();
            NhaCungCap.TenNhaCungCap = grdvDSNhaCungCap.Rows[dong].Cells["TenNhaCungCap"].Value.ToString();
            NhaCungCap.DiaChi = grdvDSNhaCungCap.Rows[dong].Cells["DiaChi"].Value.ToString();
            NhaCungCap.MaSoThue = grdvDSNhaCungCap.Rows[dong].Cells["MaSoThue"].Value.ToString();
            NhaCungCap.DienThoai = grdvDSNhaCungCap.Rows[dong].Cells["DienThoai"].Value.ToString();
            NhaCungCap.Fax = grdvDSNhaCungCap.Rows[dong].Cells["Fax"].Value.ToString();
            NhaCungCap.TenNguoiLienHe = grdvDSNhaCungCap.Rows[dong].Cells["TenNguoiLienHe"].Value.ToString();
            NhaCungCap.TrangThai = int.Parse(grdvDSNhaCungCap.Rows[dong].Cells["TrangThai"].Value.ToString());
            NhaCungCap.NoDauKy = double.Parse(grdvDSNhaCungCap.Rows[dong].Cells["NoDauKy"].Value.ToString());
            return NhaCungCap;
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemNhaCungCap f = new frmThemNhaCungCap();
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
                if (grdvDSNhaCungCap.CurrentRow != null && grdvDSNhaCungCap.CurrentRow.Index != -1)
                {
                    DialogResult result = MessageBox.Show("Bạn có thật sự muốn xóa nhà cung cấp " + grdvDSNhaCungCap.Rows[grdvDSNhaCungCap.CurrentRow.Index].Cells["TenNhaCungCap"].Value.ToString(), "Xác nhận thông tin", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Loi = "Lỗi kết nối cơ sở dữ liệu";
                        if (NhaCungCapBus.Xoa(grdvDSNhaCungCap.Rows[grdvDSNhaCungCap.CurrentRow.Index].Cells["MaNhaCungCap"].Value.ToString().Trim()) != -1)
                        {
                            grdvDSNhaCungCap.Rows.RemoveAt(grdvDSNhaCungCap.CurrentRow.Index);
                        }
                        else
                        {
                            MessageBox.Show("Không được phép xoá '" + grdvDSNhaCungCap.Rows[grdvDSNhaCungCap.CurrentRow.Index].Cells["TenNhaCungCap"].Value.ToString() + "' vì nhà cung cấp này đã tồn tại trong phiếu nhập hàng!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn nhà cung cấp!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Loi, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdvDSNhaCungCap_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ThaoTac == "ChonNhaCungCap")
            {
                NhaCungCapDTO = KhoiTaoNhaCungCap();
                this.Close();
            }
        }

        private void grdvDSNhaCungCap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && ThaoTac == "ChonNhaCungCap")
            {
                NhaCungCapDTO = KhoiTaoNhaCungCap();
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
                 if(grdvDSNhaCungCap.RowCount==0)
                {
                    MessageBox.Show("In không thành cồng vì không có thông tin Nhà cung cấp.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptDSNhaCungCap dsNhaCungCap = new rptDSNhaCungCap();
                dsNhaCungCap.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                string MaNhaCungCap = "";
                string TenNhaCungCap = "";
                if (radioTatCa.Checked == true)
                {
                    MaNhaCungCap = "";
                    TenNhaCungCap = "";
                }
                else
                {
                    if (radioTheoMa.Checked == true)
                    {
                        MaNhaCungCap = txtNhaCungCap.Text;
                        TenNhaCungCap = "";
                    }
                    else
                    {
                        MaNhaCungCap = "";
                        TenNhaCungCap = txtNhaCungCap.Text;
                    }
                }

                DataTable bang = NhaCungCapBus.ReportDSNhaCungCap(MaNhaCungCap, TenNhaCungCap);
                if (bang.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();

                    DataSet cacBang = new DataSet();
                    cacBang.Tables.Add(bang);
                    cacBang.Tables.Add(CongTy);

                    dsNhaCungCap.SetDataSource(cacBang);
                    dsNhaCungCap.SetParameterValue("@MaNhaCungCap", MaNhaCungCap);
                    dsNhaCungCap.SetParameterValue("@TenNhaCungCap", TenNhaCungCap);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = dsNhaCungCap;
                    dlgHienThi.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có thông tin Nhà cung cấp.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}