using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    public partial class frmThemNhaCungCap : Form
    {
        #region Thuộc tính
        private clsNhaCungCapBUS NhaCungCapBus = new clsNhaCungCapBUS();
        clsNhaCungCapDTO NhaCungCapDTO;
        public string ThucThi = "Them";
        #endregion


        public frmThemNhaCungCap()
        {
            InitializeComponent();
        }

         public frmThemNhaCungCap(clsNhaCungCapDTO _NhaCungCapDTO)
        {
            ThucThi ="Sua";
            NhaCungCapDTO = _NhaCungCapDTO;
            InitializeComponent();
        }

        private void frmThemNhaCungCap_Load(object sender, EventArgs e)
        {
            try
            {
                //txtNoDauKy.Enabled = false;
                if (ThucThi == "Them")//Them mặt hàng
                {
                    //load Ma mat hang
                    txtMaNhaCungCap.Text = NhaCungCapBus.LayMaNhaCungCapMoi();

                }
                else// Cập nhật mặt hàng
                {
                    Gan(NhaCungCapDTO);
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


        private void Gan(clsNhaCungCapDTO NhaCungCap)
        {
            txtMaNhaCungCap.Text = NhaCungCap.MaNhaCungCap;
            txtTenNhaCungCap.Text = NhaCungCap.TenNhaCungCap;
            txtDiaChi.Text = NhaCungCap.DiaChi;
            txtMaSoThue.Text = NhaCungCap.MaSoThue;
            txtDienThoai.Text = NhaCungCap.DienThoai;
            txtSoMayFax.Text = NhaCungCap.Fax;
            txtTenNguoiLienHe.Text = NhaCungCap.TenNguoiLienHe;
            //txtNoDauKy.Text = NhaCungCap.NoDauKy.ToString();
            //chkNoDauKy.Checked = false;
        }

        private void btnThoi_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }
        //Dung cho viec Lap tien no dau ky cho khach hang
        //private clsPhieuNhapDTO KhoiTaoPhieuNhap(clsNhaCungCapDTO NhaCungCap)
        //{
        //    clsPhieuNhapDTO PhieuNhap = new clsPhieuNhapDTO();
        //    PhieuNhap.MaPhieuNhap = new clsPhieuNhapBUS().LayMaPhieuNhapMoi();
        //    PhieuNhap.NgayNhap = DateTime.Now.Date;
        //    PhieuNhap.NhaCungCap.MaNhaCungCap = NhaCungCap.MaNhaCungCap;
        //    PhieuNhap.TongTien = NhaCungCap.NoDauKy;
        //    PhieuNhap.ConNo = PhieuNhap.TongTien;
        //    PhieuNhap.TrangThai = 2;
        //    return PhieuNhap;
        //}
        private void btnLuu_Click(object sender, EventArgs e)
        {
            String Loi = "";
            try
            {
                Loi = "Xin vui lòng kiểm tra lại dữ liệu nhập";
                clsNhaCungCapDTO NhaCungCap = KhoiTao(ref Loi);
                if (NhaCungCap != null)
                {
                    Loi = "Lỗi kết nối cơ sở dữ liệu";
                    if (ThucThi == "Them")
                    {
                        if (NhaCungCapBus.Them(NhaCungCap) != -1)
                        {
                            //Dung cho truong hop nha cung cap co no dau ky giai quyet bang cach them vo phieu xuat kho
                            //if (NhaCungCap.NoDauKy>0)
                           // {
                              //  clsPhieuNhapDTO PhieuNhap = KhoiTaoPhieuNhap(NhaCungCap);
                              //  new clsPhieuNhapBUS().Them(PhieuNhap);
                            //}
                            MessageBox.Show("Lưu nhà cung cấp " + NhaCungCap.TenNhaCungCap + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LamTuoi();
                        }
                        else
                        {
                            MessageBox.Show("Lưu nhà cung cấp không thành công, nguyên nhân do nhà cung cấp này đã tồn tại rồi. Xin vui lòng nhập tên nhà cung cấp khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        if (NhaCungCapBus.Sua(NhaCungCap) != -1)
                        {
                            MessageBox.Show("Lưu nhà cung cấp " + NhaCungCap.TenNhaCungCap + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //LamTuoi();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Lưu nhà cung cấp không thành công, nguyên nhân do nhà cung cấp này đã tồn tại rồi. Xin vui lòng nhập tên nhà cung cấp khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private clsNhaCungCapDTO KhoiTao(ref string Loi)
        {
            clsNhaCungCapDTO NhaCungCap = new clsNhaCungCapDTO();
            if (txtTenNhaCungCap.Text.Trim() == "")
            {
                Loi = "Xin vui lòng nhập vào Tên nhà cung cấp";
                return null;
            }
            NhaCungCap.MaNhaCungCap = txtMaNhaCungCap.Text.Trim();
            NhaCungCap.TenNhaCungCap = txtTenNhaCungCap.Text.Trim();
            NhaCungCap.DiaChi = txtDiaChi.Text.Trim();
            NhaCungCap.MaSoThue = txtMaSoThue.Text.Trim();
            NhaCungCap.DienThoai = txtDienThoai.Text.Trim();
            NhaCungCap.Fax = txtSoMayFax.Text.Trim();
            NhaCungCap.TenNguoiLienHe = txtTenNguoiLienHe.Text.Trim();
            //NhaCungCap.NoDauKy = double.Parse(txtNoDauKy.Text.Trim());
            NhaCungCap.TrangThai = 1;
            return NhaCungCap;
        }

        private void LamTuoi()
        {
            txtMaNhaCungCap.Text = NhaCungCapBus.LayMaNhaCungCapMoi();
            txtTenNhaCungCap.Text = "";
            txtDiaChi.Text = "";
            txtMaSoThue.Text = "";
            txtDienThoai.Text = "";
            txtSoMayFax.Text = "";
            txtTenNguoiLienHe.Text = "";
            //chkNoDauKy.Checked = false;
            //txtNoDauKy.Text = "0";
            //txtNoDauKy.Enabled = false;

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

        private void txtTenNhaCungCap_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = clsSupport.KiemChuoiNot255(txtTenNhaCungCap.Text, "Tên nhà cung cấp");
        }

        private void txtTenNguoiLienHe_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = clsSupport.KiemChuoiNot255(txtTenNguoiLienHe.Text, "Tên người liên hệ");
        }

    }
}