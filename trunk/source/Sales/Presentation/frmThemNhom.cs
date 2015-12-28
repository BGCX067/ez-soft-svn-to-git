using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    public partial class frmThemNhom : Form
    {
        #region Thuộc tính
        private clsLoaiMatHangBUS LoaiMatHangBus = new clsLoaiMatHangBUS();
        clsLoaiMatHangDTO LoaiMatHangDTO;
        public string ThucThi = "Them";
        #endregion

        public frmThemNhom()
        {
            InitializeComponent();
        }
        public frmThemNhom(clsLoaiMatHangDTO _LoaiMatHangDTO)
        {
            ThucThi ="Sua";
            LoaiMatHangDTO = _LoaiMatHangDTO;
            InitializeComponent();
        }

        private void btnThoi_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            String Loi = "";
            try
            {
                Loi = "Xin vui lòng kiểm tra lại dữ liệu nhập";
                clsLoaiMatHangDTO LoaiMatHang = KhoiTao(ref Loi);
                if (LoaiMatHang != null)
                {
                    Loi = "Lỗi kết nối cơ sở dữ liệu";
                    if (ThucThi == "Them")
                    {
                        if (LoaiMatHangBus.Them(LoaiMatHang) != -1)
                        {
                            MessageBox.Show("Lưu nhóm hàng " + LoaiMatHang.TenLoaiMatHang + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LamTuoi();
                        }
                        else
                        {
                            MessageBox.Show("Lưu nhóm hàng không thành công, nguyên nhân do nhóm hàng này đã tồn tại rồi. Xin vui lòng nhập tên nhóm hàng khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        if (LoaiMatHangBus.Sua(LoaiMatHang) != -1)
                        {
                            MessageBox.Show("Lưu nhóm hàng " + LoaiMatHang.TenLoaiMatHang + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //LamTuoi();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Lưu nhóm hàng không thành công, nguyên nhân do nhóm hàng này đã tồn tại rồi. Xin vui lòng nhập tên nhóm hàng khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void frmThemNhom_Load(object sender, EventArgs e)
        {
            try
            {
                if (ThucThi == "Them")//Them mặt hàng
                {
                    //load Ma mat hang
                    txtMaNhom.Text = LoaiMatHangBus.LayMaLoaiMatHangMoi();
                }
                else// Cập nhật mặt hàng
                {
                    Gan(LoaiMatHangDTO);
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

        private void Gan(clsLoaiMatHangDTO LoaiMatHang)
        {
            //load Ma mat hang
            txtMaNhom.Text = LoaiMatHang.MaLoaiMatHang;
            txtTenNhomHang.Text = LoaiMatHang.TenLoaiMatHang;
            txtDienGiai.Text = LoaiMatHang.DienGiai;
        }

        private void LamTuoi()
        {
            //load Ma mat hang
            txtMaNhom.Text = LoaiMatHangBus.LayMaLoaiMatHangMoi();
            txtTenNhomHang.Text = "";
            txtDienGiai.Text = "";
        }
        private clsLoaiMatHangDTO KhoiTao(ref string Loi)
        {
            clsLoaiMatHangDTO LoaiMatHang = new clsLoaiMatHangDTO();
            LoaiMatHang.MaLoaiMatHang = txtMaNhom.Text.Trim();
            if(txtTenNhomHang.Text.Trim()=="")
            {
                Loi="Xin vui lòng nhập vào Tên nhóm hàng";
                return null;
            }
            LoaiMatHang.TenLoaiMatHang = txtTenNhomHang.Text.Trim();
            LoaiMatHang.DienGiai = txtDienGiai.Text.Trim();
            LoaiMatHang.TrangThai = 1;
            return LoaiMatHang;
        }

        private void txtTenNhomHang_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = clsSupport.KiemChuoiNot255(txtTenNhomHang.Text, "Tên nhóm hàng");
        }

    }
}