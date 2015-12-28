using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    public partial class frmThemMatHang : Form
    {
        #region Thuộc tính
        private clsMatHangBUS MatHangBus = new clsMatHangBUS();
        clsMatHangDTO MatHangDTO;
        public string ThucThi = "Them";
        #endregion

        public frmThemMatHang()
        {
            InitializeComponent();
        }
        public frmThemMatHang(clsMatHangDTO _MatHangDTO)
        {
            ThucThi ="Sua";
            MatHangDTO = _MatHangDTO;
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmThemMatHang_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoControl();
                if (ThucThi == "Them")//Them mặt hàng
                {
                    //load Ma mat hang
                    txtMaHang.Text = MatHangBus.LayMaMatHangMoi();
                }
                else// Cập nhật mặt hàng
                {
                    Gan(MatHangDTO);
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


        private void KhoiTaoControl()
        {
            //Load combo xuat xu
            cboXuatXu.DataSource = new clsNuocSanXuatBUS().LayBang();
            cboXuatXu.DisplayMember = "TenNuocSanXuat";
            cboXuatXu.ValueMember = "TenNuocSanXuat";
            //Load combo don vi tinh
            cboDonViTinh.DataSource = new clsDonViTinhBUS().LayBang();
            cboDonViTinh.DisplayMember = "TenDonViTinh";
            cboDonViTinh.ValueMember = "TenDonViTinh";
            //Load combo nhom hang
            KhoiTaoComboNhomHang();
            //
            //chkSoLuongTon.Checked = false;
            isEnableSoLuong_Gia(false);
            //
            cboVAT.SelectedIndex = 0;
            //focus 
            txtTenHang.Focus();
        }

        private void KhoiTaoComboNhomHang()
        {
            cboNhomHang.DataSource = new clsLoaiMatHangBUS().LayBang();
            cboNhomHang.DisplayMember = "TenLoaiMatHang";
            cboNhomHang.ValueMember = "MaLoaiMatHang";
        }
        private void Gan(clsMatHangDTO MatHang)
        {
            //load Ma mat hang
            txtMaHang.Text = MatHang.MaMatHang;
            txtTenHang.Text =MatHang.TenMatHang;
            txtGia.Text = MatHang.DonGia.ToString();
            txtSoLuongTon.Text =MatHang.SoLuongTon.ToString();
            txtGiaMua.Text = MatHang.GiaMua.ToString();
            txtGiaBanLe.Text = MatHang.GiaBanLe.ToString();
            txtGiaBanSi.Text = MatHang.GiaBanSi.ToString();
            txtDatLe.Text = MatHang.PT_GiaBanLe.ToString()+ "%";
            txtDatSi.Text =MatHang.PT_GiaBanSi.ToString() + "%";
            txtDienGiai.Text = MatHang.DienGiai;
            txtMaVach.Text = MatHang.MaVach;
            cboDonViTinh.Text = MatHang.DonViTinh.Trim();
            cboNhomHang.SelectedValue=MatHang.LoaiMatHang.MaLoaiMatHang.Trim();
            cboXuatXu.Text = MatHang.XuatXu.Trim() ;
            cboVAT.Text = (MatHang.ThueVAT*100).ToString()+ "%";
            //if (MatHang.DonGia > 0)
            //{
            //    chkSoLuongTon.Checked = true;
            //}
            //else
            //{
            //    chkSoLuongTon.Checked = false;
            //}
            if (MatHang.TrangThai ==2)
            {
                chkHangNgungBan.Checked = true;
            }
            else
            {
                chkHangNgungBan.Checked = false;
            }
            
        }
        /// <summary>
        /// Sự kiện lưu mặt hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLuu_Click(object sender, EventArgs e)
        {
            String Loi = "";
            Luu(ref Loi);
        }

        private void Luu(ref String Loi)
        {
            try
            {
                Loi = "Xin vui lòng kiểm tra lại dữ liệu nhập";
                clsMatHangDTO MatHang = KhoiTao(ref Loi);
                if (MatHang != null)
                {
                    Loi = "Lỗi kết nối cơ sở dữ liệu";
                    if (ThucThi == "Them")
                    {
                        if (MatHangBus.Them(MatHang) != -1)
                        {
                            MessageBox.Show("Lưu mặt hàng " + MatHang.TenMatHang + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LamTuoi();
                            Loi = "Thành Công";
                        }
                        else
                        {
                            MessageBox.Show("Lưu mặt hàng không thành công, nguyên nhân do mặt hàng này đã tồn tại rồi. Xin vui lòng nhập tên mặt hàng khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        if (MatHangBus.Sua(MatHang) != -1)
                        {
                            MessageBox.Show("Lưu mặt hàng " + MatHang.TenMatHang + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //LamTuoi();
                            this.Close();
                            Loi = "Thành Công";
                        }
                        else
                        {
                            MessageBox.Show("Lưu mặt hàng không thành công, nguyên nhân do mặt hàng này đã tồn tại rồi. Xin vui lòng nhập tên mặt hàng khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void LamTuoi()
        {
            //load Ma mat hang
            txtMaHang.Text = MatHangBus.LayMaMatHangMoi();
            txtTenHang.Text = "";
            txtGia.Text = "0";
            txtSoLuongTon.Text = "0";
            txtGiaMua.Text = "0";
            txtGiaBanLe.Text = "0";
            txtGiaBanSi.Text = "0";
            txtDatLe.Text = "0%";
            txtDatSi.Text = "0%";
            txtDienGiai.Text = "";
            txtMaVach.Text = "";
            cboDonViTinh.SelectedIndex = 0;
            cboNhomHang.SelectedIndex = 0;
            cboXuatXu.SelectedIndex = 0;
            cboVAT.SelectedIndex = 0;
           // chkSoLuongTon.Checked = false;
            chkHangNgungBan.Checked = false;
        }

        /// <summary>
        /// Mặt hàng có Trạng thái:
        /// TrangThai=0: mặt hàng đã bị xóa
        /// TrangThai=1: mặt hàng đang hoạt động
        ///TrangThai=2: mặt hàng đã ngưng
        /// </summary>
        /// <returns></returns>
        private clsMatHangDTO KhoiTao(ref string Loi)
        {
            clsMatHangDTO MatHang = new clsMatHangDTO();
             if(txtTenHang.Text.Trim()=="")
            {
                Loi = "Xin vui lòng nhập Tên mặt hàng";
                return null;
            }
            MatHang.MaMatHang = txtMaHang.Text.Trim();
            MatHang.TenMatHang = txtTenHang.Text.Trim();
            MatHang.LoaiMatHang.MaLoaiMatHang = cboNhomHang.SelectedValue.ToString().Trim();
            MatHang.XuatXu = cboXuatXu.Text.Trim();
            MatHang.DonViTinh = cboDonViTinh.Text.Trim();
            if (txtGiaMua.Text.Trim() == "")
            {
                Loi = "Xin vui lòng nhập Giá mua";
                return null;
            }
            MatHang.GiaMua =double.Parse( txtGiaMua.Text);
            if (txtGiaBanSi.Text.Trim() == "")
            {
                Loi = "Xin vui lòng nhập Giá bán sỉ";
                return null;
            }
            MatHang.GiaBanSi = double.Parse(txtGiaBanSi.Text);
            if (txtGiaBanLe.Text.Trim() == "")
            {
                Loi = "Xin vui lòng nhập Giá bán lẻ";
                return null;
            }
            MatHang.GiaBanLe = double.Parse(txtGiaBanLe.Text);
            MatHang.PT_GiaBanLe = double.Parse(txtDatLe.Text.Substring(0, txtDatLe.Text.Length - 1));
            MatHang.PT_GiaBanSi = double.Parse(txtDatSi.Text.Substring(0, txtDatSi.Text.Length - 1));
            MatHang.MaVach = txtMaVach.Text.Trim();
            //if (chkSoLuongTon.Checked == true)
            //{
                MatHang.SoLuongTon = double.Parse(txtSoLuongTon.Text);
            //    MatHang.DonGia = double.Parse(txtGia.Text);
            //}
            //else
            //{
            //    MatHang.SoLuongTon = 0;
            //    MatHang.DonGia = 0;
            //}
            double ThueVAT ;
            //chua kiem tra Thue nhap vao
            if (double.TryParse(cboVAT.Text.Substring(0, cboVAT.Text.ToString().Length - 1), out ThueVAT) == true)
            {
                if (ThueVAT >= 0)
                {
                    MatHang.ThueVAT = ThueVAT;
                }
                else
                {
                    Loi = "Xin vui lòng nhập Thuế là số dương";
                    return null;
                }
            }
            else
            {
                Loi = "Xin vui lòng nhập Thuế là số";
                return null;
            }
            if (chkHangNgungBan.Checked == true)
            {
                MatHang.TrangThai = 2;
            }
            else
            {
                MatHang.TrangThai = 1;
            }
            MatHang.DienGiai = txtDienGiai.Text.Trim();
            return MatHang;
        }

        #region Bẫy lỗi nhập liệu
        private void txtGiaMua_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = clsSupport.KiemTraSoThuc(txtGiaMua.Text,"Giá mua");
        }

        private void txtGiaBanSi_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = clsSupport.KiemTraSoThuc(txtGiaBanSi.Text,"Giá bán sỉ");
        }

        private void txtGiaBanLe_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = clsSupport.KiemTraSoThuc(txtGiaBanLe.Text,"Giá bán lẻ");
        }

        private void txtGia_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = clsSupport.KiemTraSoThuc(txtGia.Text, "Giá");
        }

        private void txtTenHang_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = clsSupport.KiemChuoiNot255(txtTenHang.Text, "Tên hàng");
        }
        private void txtSoLuongTon_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = clsSupport.KiemTraSoNguyen(txtSoLuongTon.Text, "Số lượng tồn");
        }
        #endregion

        #region Kiểm tra số lượng tồn
        //private void chkSoLuongTon_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkSoLuongTon.Checked==true)
        //    {
        //        isEnableSoLuong_Gia(true);
        //    }
        //    else
        //    {
        //        isEnableSoLuong_Gia(false);
        //    }
        //}

        private void isEnableSoLuong_Gia(bool isEnable)
        {
            txtSoLuongTon.Enabled = isEnable;
            txtGia.Enabled = isEnable;
        }
        #endregion

        /// <summary>
        /// Sự kiện thoát
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnThoi_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        #region Sự kiện Tính giá bán thông qua giá mua
        private void txtGiaMua_KeyDown(object sender, KeyEventArgs e)
        {
           try
            {
                txtDatLe.Text = TinhPhanTramGiaMua(txtGiaMua.Text, txtGiaBanLe.Text).ToString() + "%";
                txtDatSi.Text = TinhPhanTramGiaMua(txtGiaMua.Text, txtGiaBanSi.Text).ToString() + "%";
            }
            catch (Exception ex)
            {

            }
        }

        private void txtGiaBanSi_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                txtDatSi.Text = TinhPhanTramGiaMua(txtGiaMua.Text, txtGiaBanSi.Text).ToString() + "%";
            }
            catch (Exception ex)
            {

            }
        }

        private void txtGiaBanLe_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                txtDatLe.Text = TinhPhanTramGiaMua(txtGiaMua.Text, txtGiaBanLe.Text).ToString() + "%";
            }
            catch (Exception ex)
            {

            }
        }

        private void txtDatSi_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string DatSi = txtDatSi.Text;
                if (DatSi.IndexOf("%") == -1)
                {
                    DatSi += "%"; 
                }
                DatSi = DatSi.Substring(0, DatSi.Length - 1);
                txtGiaBanSi.Text = TinhGiaBan(txtGiaMua.Text, DatSi).ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void txtDatLe_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string DatLe = txtDatLe.Text;
                if (DatLe.IndexOf("%") == -1)
                {
                    DatLe += "%"; 
                }
                DatLe = DatLe.Substring(0, DatLe.Length - 1);
                txtGiaBanLe.Text = TinhGiaBan(txtGiaMua.Text, DatLe).ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private double TinhPhanTramGiaMua(string GiaMua, string GiaBan)
        {
            double _GiaMua;
            double _GiaBan;
            if (double.TryParse(GiaMua, out _GiaMua) == true)
            {
                if (_GiaMua != 0)
                {
                    if (double.TryParse(GiaBan, out _GiaBan) == true)
                    {
                        return Math.Round(_GiaBan / _GiaMua, 2) * 100;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        private double TinhGiaBan(string GiaMua, string DatDuoc)
        {
            double _GiaMua;
            double _DatDuoc;
            if (double.TryParse(GiaMua, out _GiaMua) == true)
            {
                 if (_GiaMua != 0)
                {
                    if (double.TryParse(DatDuoc, out _DatDuoc) == true)
                    {
                        return Math.Round((_DatDuoc * _GiaMua) / 100);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        #endregion

        private void btnNhomHang_Click(object sender, EventArgs e)
        {
            frmNhomHang NhomHang = new frmNhomHang();
            NhomHang.ShowDialog();
            KhoiTaoComboNhomHang();
        }

        //private void frmThemMatHang_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    e.Cancel = KiemTraTruocKhiThoat();
        //}

        //private Boolean KiemTraTruocKhiThoat()
        //{
        //    if (txtMaHang.Text.Trim() !="")
        //    {
        //        DialogResult result = MessageBox.Show("Bạn có muốn lưu lại mặt hàng này không?", "Xac nhan", MessageBoxButtons.YesNoCancel);
        //        if (result == DialogResult.Yes)
        //        {
        //            string Loi = "";
        //            Luu(ref Loi);
        //            if (Loi == "Thành Công")
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //        else
        //        {
        //            if (result == DialogResult.Cancel)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    return false;
        //}


    }
}