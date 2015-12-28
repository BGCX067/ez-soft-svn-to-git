using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    public partial class frmThemKhachHang : Form
    {
        #region Thuộc tính
        private clsKhachHangBUS KhachHangBus = new clsKhachHangBUS();
        clsKhachHangDTO KhachHangDTO;
        public string ThucThi = "Them";
        #endregion

        public frmThemKhachHang()
        {
            InitializeComponent();
        }

        public frmThemKhachHang(clsKhachHangDTO _KhachHangDTO)
        {
            ThucThi ="Sua";
            KhachHangDTO = _KhachHangDTO;
            InitializeComponent();
        }

        private void frmThemKhachHang_Load(object sender, EventArgs e)
        {
            try
            {
                cboBaoGia.SelectedIndex = 0;
                if (ThucThi == "Them")//Them mặt hàng
                {
                    //load Ma mat hang
                    txtMaKhachHang.Text = KhachHangBus.LayMaKhachHangMoi();
                    txtChietKhau.Text = "0%";

                }
                else// Cập nhật mặt hàng
                {
                    Gan(KhachHangDTO);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối với cơ sở dữ liệu!");
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

        private void Gan(clsKhachHangDTO KhachHang)
        {
            txtMaKhachHang.Text = KhachHang.MaKhachHang;
            txtTenKhachHang.Text = KhachHang.TenKhachHang;
            txtDiaChi.Text = KhachHang.DiaChi;
            txtMaSoThue.Text = KhachHang.MaSoThue;
            txtDienThoai.Text = KhachHang.DienThoai;
            txtSoMayFax.Text = KhachHang.Fax;
            txtTenNguoiLienHe.Text = KhachHang.TenNguoiLienHe;
            txtChietKhau.Text = KhachHang.ChietKhau.ToString()+"%";
            //txtNoDauKy.Text = KhachHang.NoDauKy.ToString();
            cboBaoGia.SelectedItem = KhachHang.BaoGia.Trim();
            //chkNoDauKy.Checked = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            String Loi = "";
            try
            {
                Loi = "Xin vui lòng kiểm tra lại dữ liệu nhập";
                clsKhachHangDTO KhachHang = KhoiTao(ref Loi);
                if (KhachHang != null)
                {
                    Loi = "Lỗi kết nối cơ sở dữ liệu";
                    if (ThucThi == "Them")
                    {
                        if (KhachHangBus.Them(KhachHang) != -1)
                        {
                            MessageBox.Show("Lưu khách hàng " + KhachHang.TenKhachHang + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LamTuoi();
                        }
                        else
                        {
                            MessageBox.Show("Lưu khách hàng không thành công, nguyên nhân do khách hàng này đã tồn tại rồi. Xin vui lòng nhập tên khách hàng khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        if (KhachHangBus.Sua(KhachHang) != -1)
                        {
                            MessageBox.Show("Lưu khách hàng " + KhachHang.TenKhachHang + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //LamTuoi();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Lưu khách hàng không thành công, nguyên nhân do khách hàng này đã tồn tại rồi. Xin vui lòng nhập tên khách hàng khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Loi, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Loi, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private clsKhachHangDTO KhoiTao(ref string Loi)
        {
            clsKhachHangDTO KhachHang = new clsKhachHangDTO();
            if (txtTenKhachHang.Text.Trim() == "")
            {
                Loi = "Xin vui lòng nhập vào Tên nhà cung cấp";
                return null;
            }
            KhachHang.MaKhachHang = txtMaKhachHang.Text.Trim();
            KhachHang.TenKhachHang = txtTenKhachHang.Text.Trim();
            KhachHang.DiaChi = txtDiaChi.Text.Trim();
            KhachHang.MaSoThue = txtMaSoThue.Text.Trim();
            KhachHang.DienThoai = txtDienThoai.Text.Trim();
            KhachHang.Fax = txtSoMayFax.Text.Trim();
            KhachHang.TenNguoiLienHe = txtTenNguoiLienHe.Text.Trim();
            //KhachHang.NoDauKy = double.Parse(txtNoDauKy.Text.Trim());
            KhachHang.TrangThai = 1;
            double ChietKhau;
            if (double.TryParse(txtChietKhau.Text.Trim().Substring(0, txtChietKhau.Text.Trim().Length - 1), out ChietKhau) == true)
            {
                if (ChietKhau >= 0)
                {
                    KhachHang.ChietKhau = ChietKhau;
                }
                else
                {
                    Loi = "Xin vui lòng nhập Chiết khấu là số dương";
                    return null;
                }
            }
            else
            {
                Loi = "Xin vui lòng nhập Chiết khấu là số";
                return null;
            }
            KhachHang.BaoGia = cboBaoGia.SelectedItem.ToString();
            return KhachHang;
        }

        private void LamTuoi()
        {
            txtMaKhachHang.Text = KhachHangBus.LayMaKhachHangMoi();
            txtTenKhachHang.Text = "";
            txtDiaChi.Text = "";
            txtMaSoThue.Text = "";
            txtDienThoai.Text = "";
            txtSoMayFax.Text = "";
            txtChietKhau.Text = "0%";
            txtTenNguoiLienHe.Text = "";
            //chkNoDauKy.Checked = false;
            //txtNoDauKy.Text = "0";
            //txtNoDauKy.Enabled = false;
            cboBaoGia.SelectedIndex = 0;

        }


        //private void chkNoDauKy_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkNoDauKy.Checked == true)
        //    {
        //        txtNoDauKy.Enabled = true;
        //    }
        //    else
        //    {
        //        txtNoDauKy.Enabled = false;
        //    }
        //}

        private void txtTenKhachHang_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = clsSupport.KiemChuoiNot255(txtTenKhachHang.Text, "Tên khách hàng");
        }

        private void txtTenNguoiLienHe_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = clsSupport.KiemChuoiNot255(txtTenNguoiLienHe.Text, "Tên người liên hệ");
        }

        private void btnThoi_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        private void txtChietKhau_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = clsSupport.KiemTraPhanTram(txtChietKhau.Text, "Chiết khấu");
        }
    }
}