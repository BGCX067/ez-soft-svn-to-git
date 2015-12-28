using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    public partial class frmPhanQuyen : frmTemplete
    {
        #region Thuộc tính
        private clsNhanVienBUS NhanVienBus = new clsNhanVienBUS();
        #endregion

        public frmPhanQuyen()
        {
            InitializeComponent();
        }

        private void frmPhanQuyen_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboNhanVien();
                KhoiTaoChucNang();
                KhoiTaoQuyenHan();
                
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //Nhân Viên
        #region Nhân Viên
        //Lấy danh sách Nhân Viên
        private void KhoiTaoComboNhanVien()
        {
            //Load combo nhom hang
            DataTable BangNhanVien = new clsNhanVienBUS().LayBang();
            cboNhanVien.DataSource = BangNhanVien;
            cboNhanVien.DisplayMember = "TenNhanVien";
            cboNhanVien.ValueMember = "MaNhanVien";
            cboNhanVien.SelectedIndex = 0;
            //cboQuyenSuDung.SelectedValue = ((DataRowView)cboNhanVien.SelectedItem).Row["QuyenHan"].ToString();
            //HienThiChucNang(int.Parse(((DataRowView)cboNhanVien.SelectedItem).Row["QuyenHan"].ToString()));
        }

        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboNhanVien.SelectedItem != null)
                {
                    txtTenNguoiDung.Text = ((DataRowView)cboNhanVien.SelectedItem).Row["TenNguoiDung"].ToString();
                    cboQuyenSuDung.SelectedValue = ((DataRowView)cboNhanVien.SelectedItem).Row["QuyenHan"].ToString();
                    //HienThiChucNang(int.Parse(((DataRowView)cboNhanVien.SelectedItem).Row["QuyenHan"].ToString()));
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại nhân viên!");
            }
        }
        #endregion

        private void KhoiTaoQuyenHan()
        {
            cboQuyenSuDung.DataSource = new clsQuyenHanBUS().LayBang();
            cboQuyenSuDung.DisplayMember = "TenQuyenHan";
            cboQuyenSuDung.ValueMember = "MaQuyenHan";
        }

        private void KhoiTaoChucNang()
        {
            //if (grdvDSChucNang.RowCount > 0)
            //{
            //    grdvDSChucNang.Rows.Clear();
            //}
            DataTable BangChucNang= new clsChucNangBUS().LayBang();
            for (int i = 0; i < BangChucNang.Rows.Count; i++)
            {
                object[] Dong = new object[4];
                Dong[0] = BangChucNang.Rows[i]["STT"];
                Dong[1] = BangChucNang.Rows[i]["MaChucNang"];
                Dong[2] = BangChucNang.Rows[i]["TenChucNang"];
                Dong[3] = false;
                grdvDSChucNang.Rows.Add(Dong);
            }
        }

        private void HienThiChucNang(int MaQuyenHan)
        {
            for (int i = 0; i < grdvDSChucNang.RowCount; i++)
            {
                grdvDSChucNang.Rows[i].Cells["DuocDung"].Value = false;
            }
            clsQuyenHanDTO QuyenHan = new clsQuyenHanBUS().LayThongTin(MaQuyenHan);
            for (int i = 0; i < QuyenHan.DS_PhanQuyenChucNang.Count; i++)
            {
                for (int j = 0; j < grdvDSChucNang.RowCount; j++)
                {
                    if (grdvDSChucNang.Rows[j].Cells["MaChucNang"].Value.ToString().Trim() == QuyenHan.DS_PhanQuyenChucNang[i].ChucNang.MaChucNang.ToString().Trim())
                    {
                        grdvDSChucNang.Rows[j].Cells["DuocDung"].Value = true;
                        break;
                    }
                }
            }
        }

        private void cboQuyenSuDung_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboQuyenSuDung.SelectedItem != null)
                {
                    HienThiChucNang(int.Parse(((DataRowView)cboQuyenSuDung.SelectedItem).Row["MaQuyenHan"].ToString()));
                }
            }
            catch (Exception Loi)
            {
               // MessageBox.Show("Xin vui lòng thử chọn quyền sử dụng!");
            }
        }

        private void btnQuyenHan_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemQuyenHan f = new frmThemQuyenHan();
                f.ShowDialog();
                KhoiTaoQuyenHan();
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboNhanVien.SelectedItem != null)
                {
                    if (cboQuyenSuDung.SelectedItem != null)
                    {

                        if (NhanVienBus.DoiQuyenHanSuDung(((DataRowView)cboNhanVien.SelectedItem).Row["MaNhanVien"].ToString().Trim(), int.Parse(((DataRowView)cboQuyenSuDung.SelectedItem).Row["MaQuyenHan"].ToString())) != -1)
                        {
                            MessageBox.Show("Đổi Quyền sử dụng cho nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            KhoiTaoComboNhanVien();
                        }
                        else
                        {
                            MessageBox.Show("Đổi Quyền sử dụng cho nhân viên " + ((DataRowView)cboNhanVien.SelectedItem).Row["TenNhanVien"].ToString() + " không thành công, nguyên nhân do nhân viên này không tồn tại.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else 
                    {
                        MessageBox.Show("Xin vui lòng chọn quyền sử dụng cho nhân viên.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else 
                {
                    MessageBox.Show("Xin vui lòng chọn nhân viênc cần lưu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}