using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    public partial class frmThietLapDinhMuc : frmTemplete
    {
        #region Thuộc tính
        private clsMatHangBUS MatHangBus = new clsMatHangBUS();
        #endregion

        public frmThietLapDinhMuc()
        {
            InitializeComponent();
        }

        private void frmThietLapDinhMuc_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboNhomHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xin vui lòng chọn mặt hàng", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Phím tắt
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            //Thiet lap dinh muc
            if (keyData == Keys.F5)
            {
                ThietLapDinhMucChoMatHang();
            }
            //Lưu thông tin phiếu nhập
            if (keyData == (Keys.Control | Keys.L))
            {
                string Loi = "";
                Luu(ref Loi);
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
                Bang = MatHangBus.LayBang();
            }
            else
            {
                Bang = MatHangBus.LayBang(MaNhomHang);
            }
            DataColumn Cot = new DataColumn("ThucHien");
            Cot.DefaultValue = "BinhThuong";
            Bang.Columns.Add(Cot);
            foreach (DataRow Dong in Bang.Rows)
            {
                Dong["ThueVAT"] = Double.Parse(Dong["ThueVAT"].ToString()) / 100;
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
            grdvDSMatHang.Columns["DonViTinh"].Visible = true;
            grdvDSMatHang.Columns["DonViTinh"].HeaderText = "ĐVT";
            grdvDSMatHang.Columns["SoLuongTon"].Visible = true;
            grdvDSMatHang.Columns["SoLuongTon"].HeaderText = "SL Cuối Kỳ";
            grdvDSMatHang.Columns["SoLuongTon"].DefaultCellStyle = CellStyle;
            grdvDSMatHang.Columns["GiaMua"].Visible = true;
            grdvDSMatHang.Columns["GiaMua"].HeaderText = "Giá Mua";
            grdvDSMatHang.Columns["GiaMua"].DefaultCellStyle = CellStyle;
            grdvDSMatHang.Columns["LuongMin"].Visible = true;
            grdvDSMatHang.Columns["LuongMin"].HeaderText = "Lượng Min";
            grdvDSMatHang.Columns["LuongMin"].DefaultCellStyle = CellStyle;
            grdvDSMatHang.Columns["LuongMax"].Visible = true;
            grdvDSMatHang.Columns["LuongMax"].HeaderText = "Lượng Max";
            grdvDSMatHang.Columns["LuongMax"].DefaultCellStyle = CellStyle;
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
            grdvDSMatHang.Columns["LuongMin"].DefaultCellStyle = CellStyleCurrency;
            grdvDSMatHang.Columns["LuongMax"].DefaultCellStyle = CellStyleCurrency;
            grdvDSMatHang.Columns["GiaMua"].DefaultCellStyle = CellStyleCurrency;
            grdvDSMatHang.Columns["SoLuongTon"].DefaultCellStyle = CellStyleCurrency;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        private void frmThietLapDinhMuc_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = KiemTraTruocKhiThoat();
        }

        private Boolean KiemTraTruocKhiThoat()
        {
            //if (grdvNhapHang.Rows.Count > 0 && btnLuu.Enabled == true)
            //{
                DialogResult result = MessageBox.Show("Bạn có muốn lưu lại thiết lập định mức này không?", "Xac nhan", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    string Loi = "";
                    Luu(ref Loi);
                    if (Loi == "Thành Công")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (result == DialogResult.Cancel)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
           // }
            return false;
        }

        private void grdvDSMatHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    txtLuongMin.Text = grdvDSMatHang.Rows[e.RowIndex].Cells["LuongMin"].Value.ToString().Trim();
                    txtLuongMax.Text = grdvDSMatHang.Rows[e.RowIndex].Cells["LuongMax"].Value.ToString().Trim();
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!");
            }
        }

        private void btnThietLap_Click(object sender, EventArgs e)
        {
            ThietLapDinhMucChoMatHang();
        }

        //Chi tiết Sửa thông tin mặt hàng đã nhập
        private void ThietLapDinhMucChoMatHang()
        {
            try
            {
                if (grdvDSMatHang.CurrentRow != null)
                {
                    if (int.Parse(txtLuongMin.Text) < int.Parse(txtLuongMax.Text))
                    {
                        int DongDangSua = grdvDSMatHang.CurrentRow.Index;
                        grdvDSMatHang.Rows[DongDangSua].Cells["LuongMin"].Value = txtLuongMin.Text;
                        grdvDSMatHang.Rows[DongDangSua].Cells["LuongMax"].Value = txtLuongMax.Text;
                        grdvDSMatHang.Rows[DongDangSua].Cells["ThucHien"].Value = "Sua";
                    }
                    else
                    {
                        MessageBox.Show("Xin vui lòng nhập Lượng Min nhỏ hơn Lượng Max!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng cần thiết lập định mức!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Luu(ref String Loi)
        {
            Loi = "";
            try
            {
                Loi = "Xin vui lòng kiểm tra lại dữ liệu nhập";
               List< clsMatHangDTO> DanhSachMatHang = KhoiTaoCacMatHang(ref Loi);
               if (DanhSachMatHang.Count != 0)
                {
                    Loi = "Lỗi kết nối cơ sở dữ liệu";
                    if (MatHangBus.ThietLapDinhMucDanhSachMatHang(DanhSachMatHang) != -1)
                        {
                            MessageBox.Show("Lưu các định mức của các mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            KhoiTao(cboNhomHang.SelectedValue.ToString().Trim());
                            Loi = "Thành Công";
                        }
                        else
                        {
                            MessageBox.Show("Lưu các định mức của các mặt hàng không thành công. Xin vui lòng thử lại", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                   
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn mặt hàng cần thiết lập", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Loi, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Khởi tạo phiếu nhập hàng vào kho
        private List< clsMatHangDTO> KhoiTaoCacMatHang(ref string Loi)
        {
            List<clsMatHangDTO> DS_MatHang = new List<clsMatHangDTO>();
            if (grdvDSMatHang.RowCount == 0)
            {
                Loi = "Hiện tại chưa có mặt hàng cần thiết lập định mức!";
                return null;
            }
            for (int i = 0; i < grdvDSMatHang.RowCount; i++ )
            {
                if (grdvDSMatHang.Rows[i].Cells["ThucHien"].Value.ToString().Trim() == "Sua")
                {
                    DS_MatHang.Add(KhoiTaoMatHang(i));
                }
            }
            return DS_MatHang;
        }

        clsMatHangDTO KhoiTaoMatHang(int i)
        {
            clsMatHangDTO MatHang = new clsMatHangDTO();
            MatHang.MaMatHang = grdvDSMatHang.Rows[i].Cells["MaMatHang"].Value.ToString().Trim();
            MatHang.LuongMin = int.Parse(grdvDSMatHang.Rows[i].Cells["LuongMin"].Value.ToString().Trim());
            MatHang.LuongMax = int.Parse(grdvDSMatHang.Rows[i].Cells["LuongMax"].Value.ToString().Trim());
            return MatHang;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            String Loi="";
            Luu(ref Loi);
        }
    }
}