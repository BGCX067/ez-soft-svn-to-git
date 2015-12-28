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
    public partial class frmKhachHang : frmTemplete
    {

        #region Thuộc tính
        private clsKhachHangBUS KhachHangBus = new clsKhachHangBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        public clsKhachHangDTO KhachHangDTO = null;
        private string ThaoTac = "";
        #endregion

        public frmKhachHang()
        {
            InitializeComponent();
        }

        public frmKhachHang(string _ThaoTac)
        {
            ThaoTac = _ThaoTac;
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            txtKhachHang.Focus();
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
            if (grdvDSKhachHang.ColumnCount > 0)
            {
                grdvDSKhachHang.Columns.Clear();
                grdvDSKhachHang.DataSource = null;
                bindingSource1 = new BindingSource();
            }
            DataTable Bang;
            Bang = KhachHangBus.LayBang();
            bindingSource1.DataSource = Bang;
            string sql = "";
            if (radioTatCa.Checked == false)
            {
                if (radioTheoMa.Checked == true)
                {
                    sql = " CONVERT([MaKhachHang], 'System.String') = '" + txtKhachHang.Text.Trim() + "' ";
                }
                else
                {
                    sql = " CONVERT([TenKhachHang], 'System.String') LIKE '%" + txtKhachHang.Text.Trim() + "%' ";
                }
                if (txtKhachHang.Text.Trim() != "")
                {
                    bindingSource1.Filter = sql;
                }
                else
                {

                }
            }
            grdvDSKhachHang.DataSource = bindingSource1;
            AnCotTrenLuoi();
            DinhDangCot();


        }

        private void AnCotTrenLuoi()
        {
            for (int i = 1; i < grdvDSKhachHang.ColumnCount; i++)
            {
                grdvDSKhachHang.Columns[i].Visible = false;
            }
            grdvDSKhachHang.Columns["MaKhachHang"].Visible = true;
            grdvDSKhachHang.Columns["MaKhachHang"].HeaderText = "Mã KH";
            grdvDSKhachHang.Columns["TenKhachHang"].Visible = true;
            grdvDSKhachHang.Columns["TenKhachHang"].HeaderText = "Tên Khách Hàng";
            grdvDSKhachHang.Columns["DiaChi"].Visible = true;
            grdvDSKhachHang.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            grdvDSKhachHang.Columns["MaSoThue"].Visible = true;
            grdvDSKhachHang.Columns["MaSoThue"].HeaderText = "Mã Số Thuế";
            grdvDSKhachHang.Columns["DienThoai"].Visible = true;
            grdvDSKhachHang.Columns["DienThoai"].HeaderText = "Điện Thoại";
            grdvDSKhachHang.Columns["Fax"].Visible = true;
            grdvDSKhachHang.Columns["Fax"].HeaderText = "Fax";
            grdvDSKhachHang.Columns["TenNguoiLienHe"].Visible = true;
            grdvDSKhachHang.Columns["TenNguoiLienHe"].HeaderText = "Liên Hệ";
            grdvDSKhachHang.Columns["BaoGia"].Visible = true;
            grdvDSKhachHang.Columns["BaoGia"].HeaderText = "Báo Giá";
            grdvDSKhachHang.Columns["ChietKhau"].Visible = true;
            grdvDSKhachHang.Columns["ChietKhau"].HeaderText = "Chiết Khấu(%)";
        }

        private void DinhDangCot()
        {
            grdvDSKhachHang.Columns[0].Width = 35;
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdvDSKhachHang.Columns["STT"].DefaultCellStyle = CellStyle;
            for (int i = 1; i < grdvDSKhachHang.Columns.Count; i++)
            {
                if (grdvDSKhachHang.Columns[i].Visible == true)
                {
                    grdvDSKhachHang.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grdvDSKhachHang.Columns[i].ReadOnly = true;
                }
            }
        }

        private void txtKhachHang_KeyUp(object sender, KeyEventArgs e)
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
                if (grdvDSKhachHang.CurrentRow != null && grdvDSKhachHang.CurrentRow.Index != -1)
                {
                    frmThemKhachHang f = new frmThemKhachHang(KhoiTaoKhachHang());
                    f.ShowDialog();
                    KhoiTao();
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn khách hàng muốn sửa!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xin vui lòng chọn nhà cung cấp", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private clsKhachHangDTO KhoiTaoKhachHang()
        {
            int dong = grdvDSKhachHang.CurrentRow.Index;
            clsKhachHangDTO KhachHang = new clsKhachHangDTO();
            KhachHang.MaKhachHang = grdvDSKhachHang.Rows[dong].Cells["MaKhachHang"].Value.ToString();
            KhachHang.TenKhachHang = grdvDSKhachHang.Rows[dong].Cells["TenKhachHang"].Value.ToString();
            KhachHang.DiaChi = grdvDSKhachHang.Rows[dong].Cells["DiaChi"].Value.ToString();
            KhachHang.MaSoThue = grdvDSKhachHang.Rows[dong].Cells["MaSoThue"].Value.ToString();
            KhachHang.DienThoai = grdvDSKhachHang.Rows[dong].Cells["DienThoai"].Value.ToString();
            KhachHang.Fax = grdvDSKhachHang.Rows[dong].Cells["Fax"].Value.ToString();
            KhachHang.BaoGia = grdvDSKhachHang.Rows[dong].Cells["BaoGia"].Value.ToString();
            KhachHang.TenNguoiLienHe = grdvDSKhachHang.Rows[dong].Cells["TenNguoiLienHe"].Value.ToString();
            KhachHang.TrangThai = int.Parse(grdvDSKhachHang.Rows[dong].Cells["TrangThai"].Value.ToString());
            KhachHang.NoDauKy = double.Parse(grdvDSKhachHang.Rows[dong].Cells["NoDauKy"].Value.ToString());
            KhachHang.ChietKhau = double.Parse(grdvDSKhachHang.Rows[dong].Cells["ChietKhau"].Value.ToString());
            return KhachHang;
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemKhachHang f = new frmThemKhachHang();
                f.ShowDialog();
                KhoiTao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (grdvDSKhachHang.CurrentRow!=null && grdvDSKhachHang.CurrentRow.Index != -1)
                {
                    DialogResult result = MessageBox.Show("Bạn có thật sự muốn xóa khách hàng?" + grdvDSKhachHang.Rows[grdvDSKhachHang.CurrentRow.Index].Cells["TenKhachHang"].Value.ToString(), "Xác nhận thông tin", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Loi = "Lỗi kết nối cơ sở dữ liệu";
                        if (KhachHangBus.Xoa(grdvDSKhachHang.Rows[grdvDSKhachHang.CurrentRow.Index].Cells["MaKhachHang"].Value.ToString().Trim()) != -1)
                        {
                            grdvDSKhachHang.Rows.RemoveAt(grdvDSKhachHang.CurrentRow.Index);
                        }
                        else
                        {
                            MessageBox.Show("Không được phép xoá '" + grdvDSKhachHang.Rows[grdvDSKhachHang.CurrentRow.Index].Cells["TenKhachHang"].Value.ToString() + "' vì khách hàng này đã tham gia mua hàng!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn khách hàng muốn xóa!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Loi, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptDSKhachHang dsKhachHang = new rptDSKhachHang();
                dsKhachHang.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                string MaKhachHang = "";
                string TenKhachHang = "";
                if (radioTatCa.Checked == true)
                {
                    MaKhachHang = "";
                    TenKhachHang = "";
                }
                else
                {
                    if (radioTheoMa.Checked == true)
                    {
                        MaKhachHang = txtKhachHang.Text;
                        TenKhachHang = "";
                    }
                    else
                    {
                        MaKhachHang = "";
                        TenKhachHang = txtKhachHang.Text;
                    }
                }

                DataTable bang = KhachHangBus.ReportDSKhachHang(MaKhachHang, TenKhachHang);
                if (bang.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();
                    DataSet cacBang = new DataSet();
                    cacBang.Tables.Add(bang);
                    cacBang.Tables.Add(CongTy);

                    dsKhachHang.SetDataSource(cacBang);
                    dsKhachHang.SetParameterValue("@MaKhachHang", MaKhachHang);
                    dsKhachHang.SetParameterValue("@TenKhachHang", TenKhachHang);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = dsKhachHang;
                    dlgHienThi.ShowDialog();
                }
                else
                    MessageBox.Show("In không thành công vì không có thông tin về khách hàng.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}