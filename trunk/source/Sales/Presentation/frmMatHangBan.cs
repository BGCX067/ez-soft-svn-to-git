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
    public partial class frmMatHangBan : frmTemplete
    {
        #region Thuộc tính
        private clsMatHangBUS MatHangBus = new clsMatHangBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        //clsMatHangDTO MatHangDTO;
        #endregion

        public frmMatHangBan()
        {
            InitializeComponent();
        }

        private void frmMatHang_Load(object sender, EventArgs e)
        {
            try
            {
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
                BangNhomHang.Rows[0]["MaLoaiMatHang"] = "";
                BangNhomHang.Rows[0]["TenLoaiMatHang"] = "Tất cả";

            }
            else
            {
                DataRow DongTam = BangNhomHang.NewRow();
                DongTam["MaLoaiMatHang"] = "";
                DongTam["TenLoaiMatHang"] = "Tất cả";
                BangNhomHang.Rows.Add(DongTam);
            }
            cboNhomHang.DataSource = BangNhomHang;
            cboNhomHang.DisplayMember = "TenLoaiMatHang";
            cboNhomHang.ValueMember = "MaLoaiMatHang";

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
            if (MaNhomHang == "")
            {
               Bang = MatHangBus.LayBang();
            }
            else
            {
                Bang = MatHangBus.LayBang(MaNhomHang);
            }
            DataColumn Cot =new DataColumn();
            Cot.ColumnName="NgungBan";
            Cot.Caption="Ngưng Bán";
            Bang.Columns.Add(Cot);
            foreach (DataRow Dong in Bang.Rows)
            {
                Dong["ThueVAT"] =Double.Parse(Dong["ThueVAT"].ToString())/100;
                if (Dong["TrangThai"].ToString().Trim() == "2")
                {
                    Dong["NgungBan"] = "Ngưng bán";
                }
                else
                {
                    Dong["NgungBan"] = "";
                }
            }
            //Bang.Columns["TrangThai"].DataType =
            //Loc thong tin mat hang
            bindingSource1.DataSource = Bang;
            string sql = "";
            if (radioTheoMa.Checked==true)
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

        }

        private void AnCotTrenLuoi()
        {
            for (int i = 1; i < grdvDSMatHang.ColumnCount; i++)
            {
                grdvDSMatHang.Columns[i].Visible = false;
            }
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdvDSMatHang.Columns["MaMatHang"].Visible = true;
            grdvDSMatHang.Columns["MaMatHang"].HeaderText = "Mã Hàng";
            grdvDSMatHang.Columns["TenMatHang"].Visible = true;
            grdvDSMatHang.Columns["TenMatHang"].HeaderText = "Tên Hàng";
            grdvDSMatHang.Columns["XuatXu"].Visible = true;
            grdvDSMatHang.Columns["XuatXu"].HeaderText = "Xuất Xứ";
            grdvDSMatHang.Columns["SoLuongTon"].Visible = true;
            grdvDSMatHang.Columns["SoLuongTon"].HeaderText = "SL Tồn";
            grdvDSMatHang.Columns["SoLuongTon"].DefaultCellStyle = CellStyle;
            grdvDSMatHang.Columns["DonViTinh"].Visible = true;
            grdvDSMatHang.Columns["DonViTinh"].HeaderText = "ĐVT";
            //grdvDSMatHang.Columns["GiaMua"].Visible = true;
            //grdvDSMatHang.Columns["GiaMua"].HeaderText = "Giá Mua";
            //grdvDSMatHang.Columns["GiaMua"].DefaultCellStyle = CellStyle;
            grdvDSMatHang.Columns["GiaBanSi"].Visible = true;
            grdvDSMatHang.Columns["GiaBanSi"].HeaderText = "Giá Bán Sỉ";
            grdvDSMatHang.Columns["GiaBanSi"].DefaultCellStyle = CellStyle;
            grdvDSMatHang.Columns["GiaBanLe"].Visible =true;
            grdvDSMatHang.Columns["GiaBanLe"].HeaderText = "Giá Bán Lẻ";
            grdvDSMatHang.Columns["GiaBanLe"].DefaultCellStyle = CellStyle;
            grdvDSMatHang.Columns["ThueVAT"].Visible = true;
            grdvDSMatHang.Columns["ThueVAT"].HeaderText = "VAT";
            grdvDSMatHang.Columns["ThueVAT"].Width = 40;
            grdvDSMatHang.Columns["ThueVAT"].DefaultCellStyle = CellStyle;
            grdvDSMatHang.Columns["DienGiai"].Visible = true;
            grdvDSMatHang.Columns["DienGiai"].HeaderText = "Diễn Giải";
            grdvDSMatHang.Columns["NgungBan"].Visible = true;
            grdvDSMatHang.Columns["NgungBan"].HeaderText = "Trạng Thái";
        }

        private void DinhDangCot()
        {
            grdvDSMatHang.Columns[0].Width = 40;
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdvDSMatHang.Columns["STT"].DefaultCellStyle = CellStyle;
            for (int i = 1; i < grdvDSMatHang.Columns.Count; i++)
            {
                if (grdvDSMatHang.Columns[i].Visible == true)
                {
                    grdvDSMatHang.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grdvDSMatHang.Columns[i].ReadOnly = true;
                }
            }
            DataGridViewCellStyle CellStyleCurrency = new DataGridViewCellStyle();
            CellStyleCurrency.Alignment = DataGridViewContentAlignment.MiddleRight;
            CellStyleCurrency.Format = "#,##0.############";
            grdvDSMatHang.Columns["GiaBanSi"].DefaultCellStyle = CellStyleCurrency;
            grdvDSMatHang.Columns["GiaBanLe"].DefaultCellStyle = CellStyleCurrency;
            CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            CellStyle.Format = "0.00%";
            grdvDSMatHang.Columns["ThueVAT"].DefaultCellStyle = CellStyle;

            DataGridViewCellStyle CellStyleT = new DataGridViewCellStyle();
            CellStyleT.ForeColor = Color.Red;
            grdvDSMatHang.Columns["NgungBan"].DefaultCellStyle = CellStyleT;
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemMatHang fMatHang = new frmThemMatHang();
                fMatHang.ShowDialog();
                KhoiTao(cboNhomHang.SelectedValue.ToString().Trim());
             }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaLai_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdvDSMatHang.CurrentRow != null && grdvDSMatHang.CurrentRow.Index != -1)
                {
                    frmThemMatHang fMatHang = new frmThemMatHang(KhoiTaoMatHang());
                    fMatHang.ShowDialog();
                    KhoiTao(cboNhomHang.SelectedValue.ToString().Trim());
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn mặt hàng!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xin vui lòng chọn mặt hàng", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private clsMatHangDTO KhoiTaoMatHang()
        {
            int dong = grdvDSMatHang.CurrentRow.Index;
            clsMatHangDTO MatHang = new clsMatHangDTO();
            MatHang.MaMatHang= grdvDSMatHang.Rows[dong].Cells["MaMatHang"].Value.ToString();
            MatHang.TenMatHang = grdvDSMatHang.Rows[dong].Cells["TenMatHang"].Value.ToString();
            MatHang.XuatXu = grdvDSMatHang.Rows[dong].Cells["XuatXu"].Value.ToString();
            MatHang.DonViTinh = grdvDSMatHang.Rows[dong].Cells["DonViTinh"].Value.ToString();
            MatHang.GiaMua =double.Parse( grdvDSMatHang.Rows[dong].Cells["GiaMua"].Value.ToString());
            MatHang.GiaBanSi = double.Parse(grdvDSMatHang.Rows[dong].Cells["GiaBanSi"].Value.ToString());
            MatHang.GiaBanLe = double.Parse(grdvDSMatHang.Rows[dong].Cells["GiaBanLe"].Value.ToString());
            MatHang.ThueVAT = double.Parse(grdvDSMatHang.Rows[dong].Cells["ThueVAT"].Value.ToString());
            MatHang.DienGiai = grdvDSMatHang.Rows[dong].Cells["DienGiai"].Value.ToString();
            MatHang.LoaiMatHang.MaLoaiMatHang = grdvDSMatHang.Rows[dong].Cells["MaLoaiMatHang"].Value.ToString();
            MatHang.PT_GiaBanSi = double.Parse(grdvDSMatHang.Rows[dong].Cells["PT_GiaBanSi"].Value.ToString());
            MatHang.PT_GiaBanLe = double.Parse(grdvDSMatHang.Rows[dong].Cells["PT_GiaBanLe"].Value.ToString());
            MatHang.LuongMin = int.Parse(grdvDSMatHang.Rows[dong].Cells["LuongMin"].Value.ToString());
            MatHang.LuongMax = int.Parse(grdvDSMatHang.Rows[dong].Cells["LuongMax"].Value.ToString());
            MatHang.SoLuongTon = double.Parse(grdvDSMatHang.Rows[dong].Cells["SoLuongTon"].Value.ToString());
            MatHang.MaVach = grdvDSMatHang.Rows[dong].Cells["MaVach"].Value.ToString();
            MatHang.TrangThai = int.Parse(grdvDSMatHang.Rows[dong].Cells["TrangThai"].Value.ToString());
            return MatHang;
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
                if (grdvDSMatHang.CurrentRow.Index != -1)
                {
                    DialogResult result = MessageBox.Show("Bạn có thật sự muốn xóa mặt hàng " + grdvDSMatHang.Rows[grdvDSMatHang.CurrentRow.Index].Cells["TenMatHang"].Value.ToString(), "Xác nhận thông tin", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Loi = "Lỗi kết nối cơ sở dữ liệu";
                        if (MatHangBus.Xoa(grdvDSMatHang.Rows[grdvDSMatHang.CurrentRow.Index].Cells["MaMatHang"].Value.ToString().Trim()) != -1)
                        {
                            grdvDSMatHang.Rows.RemoveAt(grdvDSMatHang.CurrentRow.Index);
                        }
                        else
                        {
                            MessageBox.Show("Xóa mặt hàng không thành công. Xin vui lòng thử lại", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                   MessageBox.Show("Xin vui lòng chọn mặt hàng!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtMatHang_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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
                string MaMatHang = "";
                string TenMatHang = "";
                if (chkInGiaSi.Checked == true && chkInGiaLe.Checked == true)
                {
                    rptBaoGiaSiLe baoGia = new rptBaoGiaSiLe();
                    baoGia.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                    if (radioTheoMa.Checked == true)
                    {
                        MaMatHang = txtMatHang.Text.Trim();
                    }
                    else
                    {
                        TenMatHang = txtMatHang.Text.Trim();
                    }

                    DataTable bang = MatHangBus.ReportBaoGia(cboNhomHang.SelectedValue.ToString().Trim(), MaMatHang, TenMatHang);
                    if (bang.Rows.Count != 0)
                    {
                        DataTable CongTy = CongTyBus.ReportCongTy();
                        DataSet cacBang = new DataSet();
                        cacBang.Tables.Add(CongTy);
                        cacBang.Tables.Add(bang);

                        baoGia.SetDataSource(cacBang);
                        baoGia.SetParameterValue("@MaNhomHang", cboNhomHang.SelectedValue.ToString().Trim());
                        baoGia.SetParameterValue("@MaMatHang", MaMatHang);
                        baoGia.SetParameterValue("@TenMatHang", TenMatHang);

                        dlgHienThi.CrystalReportViewer_TheHien.ReportSource = baoGia;
                    }
                    else
                    {
                        MessageBox.Show("In không thành công vì không có dữ liệu.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    if (chkInGiaLe.Checked == true)
                    {
                        rptBaoGiaLe baoGia = new rptBaoGiaLe();
                        baoGia.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);
                      
                        if (radioTheoMa.Checked == true)
                        {
                            MaMatHang = txtMatHang.Text.Trim();
                        }
                        else
                        {
                            TenMatHang = txtMatHang.Text.Trim();
                        }

                        DataTable bang = MatHangBus.ReportBaoGia(cboNhomHang.SelectedValue.ToString().Trim(), MaMatHang, TenMatHang);
                        if (bang.Rows.Count != 0)
                        {
                            DataTable CongTy = CongTyBus.ReportCongTy();
                            DataSet cacBang = new DataSet();
                            cacBang.Tables.Add(CongTy);
                            cacBang.Tables.Add(bang);

                            baoGia.SetDataSource(cacBang);
                            baoGia.SetParameterValue("@MaNhomHang", cboNhomHang.SelectedValue.ToString().Trim());
                            baoGia.SetParameterValue("@MaMatHang", MaMatHang);
                            baoGia.SetParameterValue("@TenMatHang", TenMatHang);

                            dlgHienThi.CrystalReportViewer_TheHien.ReportSource = baoGia;
                        }
                        else
                        {
                            MessageBox.Show("In không thành công vì không có dữ liệu.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        rptBaoGiaSi baoGia = new rptBaoGiaSi();
                        baoGia.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                        if (radioTheoMa.Checked == true)
                        {
                            MaMatHang = txtMatHang.Text.Trim();
                        }
                        else
                        {
                            TenMatHang = txtMatHang.Text.Trim();
                        }
                        
                        DataTable bang = MatHangBus.ReportBaoGia(cboNhomHang.SelectedValue.ToString().Trim(), MaMatHang, TenMatHang);
                        if (bang.Rows.Count != 0)
                        {
                            DataTable CongTy = CongTyBus.ReportCongTy();
                            DataSet cacBang = new DataSet();
                            cacBang.Tables.Add(CongTy);
                            cacBang.Tables.Add(bang);

                            baoGia.SetDataSource(cacBang);
                            baoGia.SetParameterValue("@MaNhomHang", cboNhomHang.SelectedValue.ToString().Trim());
                            baoGia.SetParameterValue("@MaMatHang", MaMatHang);
                            baoGia.SetParameterValue("@TenMatHang", TenMatHang);

                            dlgHienThi.CrystalReportViewer_TheHien.ReportSource = baoGia;
                        }
                        else
                        {
                            MessageBox.Show("In không thành công vì không có dữ liệu.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                }

                dlgHienThi.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}