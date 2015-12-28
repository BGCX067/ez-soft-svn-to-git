using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    public partial class frmThemNhanVien : Form
    {
        #region Thuộc tính
        private clsNhanVienBUS NhanVienBus = new clsNhanVienBUS();
        clsNhanVienDTO NhanVienDTO;
        public string ThucThi = "Them";
        #endregion

        public frmThemNhanVien()
        {
            InitializeComponent();
        }

        public frmThemNhanVien(clsNhanVienDTO _NhanVienDTO)
        {
            ThucThi ="Sua";
            NhanVienDTO = _NhanVienDTO;
            InitializeComponent();
        }

        private void frmThemNhanVien_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoQuyenHan();
                if (ThucThi == "Them")//Them nhân viên
                {
                    //load Ma mat hang
                    txtMaNhanVien.Text = NhanVienBus.LayMaNhanVienMoi();

                }
                else// Cập nhật nhân viên
                {
                    Gan(NhanVienDTO);
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

        private void KhoiTaoQuyenHan()
        {
            //Load combo don vi tinh
            cboQuyenSuDung.DataSource = new clsQuyenHanBUS().LayBang();
            cboQuyenSuDung.DisplayMember = "TenQuyenHan";
            cboQuyenSuDung.ValueMember = "MaQuyenHan";
        }

        private void Gan(clsNhanVienDTO NhanVien)
        {
            txtMaNhanVien.Text = NhanVien.MaNhanVien;
            txtTenNhanVien.Text = NhanVien.TenNhanVien;
            txtDiaChi.Text = NhanVien.DiaChi;
            txtDienThoai.Text = NhanVien.DienThoai;
            txtGhiChu.Text = NhanVien.GhiChu;
            txtTenNguoiDung.Text = NhanVien.TenNguoiDung;
            txtMatKhau.Text = NhanVien.MatKhau;
            cboQuyenSuDung.SelectedValue = NhanVien.QuyenHan.MaQuyenHan.ToString();
        }

        private void btnThoi_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        //private void frmThemNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    e.Cancel = KiemTraTruocKhiThoat();
        //}

        //private Boolean KiemTraTruocKhiThoat()
        //{
        //    if (txtTenNhanVien.Text.Trim() != "")
        //    {
        //        DialogResult result = MessageBox.Show("Bạn có muốn lưu lại nhân viên này không?", "Xac nhan", MessageBoxButtons.YesNoCancel);
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string Loi="";
            Luu(ref  Loi);
        }

        private void Luu(ref string Loi)
        {
            try
            {
                Loi = "Xin vui lòng kiểm tra lại dữ liệu nhập";
                clsNhanVienDTO NhanVien = KhoiTao(ref Loi);
                if (NhanVien != null)
                {
                    Loi = "Lỗi kết nối cơ sở dữ liệu";
                    if (ThucThi == "Them")
                    {
                        int kq = NhanVienBus.Them(NhanVien);
                        if (kq != -1 && kq != -2)
                        {
                            MessageBox.Show("Lưu nhân viên " + NhanVien.TenNhanVien + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LamTuoi();
                            Loi = "Thành Công";
                        }
                        else
                        {
                            if (kq == -1)
                            {
                                MessageBox.Show("Lưu nhân viên không thành công, nguyên nhân do nhân viên này đã tồn tại rồi. Xin vui lòng nhập tên nhân viên khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show("Lưu nhân viên không thành công, nguyên nhân do Tên người dùng này đã tồn tại rồi. Xin vui lòng nhập Tên người dùng khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        
                    }
                    else
                    {
                        int kq = NhanVienBus.Sua(NhanVien);
                        if (kq != -1 && kq != -2)
                        {
                            MessageBox.Show("Lưu nhân viên " + NhanVien.TenNhanVien + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //LamTuoi();
                            Loi = "Thành Công";
                            this.Close();
                        }
                        else
                        {
                            if (kq == -1)
                            {
                                MessageBox.Show("Lưu nhân viên không thành công, nguyên nhân do nhân viên này đã tồn tại rồi. Xin vui lòng nhập tên nhân viên khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show("Lưu nhân viên không thành công, nguyên nhân do Tên người dùng này đã tồn tại rồi. Xin vui lòng nhập Tên người dùng khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
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

        private clsNhanVienDTO KhoiTao(ref string Loi)
        {
            clsNhanVienDTO NhanVien = new clsNhanVienDTO();
            if (txtTenNhanVien.Text.Trim() == "")
            {
                Loi = "Xin vui lòng nhập vào Tên nhân viên";
                return null;
            }
            NhanVien.MaNhanVien = txtMaNhanVien.Text.Trim();
            NhanVien.TenNhanVien = txtTenNhanVien.Text.Trim();
            NhanVien.DiaChi = txtDiaChi.Text.Trim();
            NhanVien.DienThoai = txtDienThoai.Text.Trim();
            NhanVien.GhiChu = txtGhiChu.Text;
            if (txtTenNguoiDung.Text.Trim() == "" )
            {
                Loi = "Xin vui lòng nhập vào Tên người dùng!";
                return null;
            }
            NhanVien.TenNguoiDung = txtTenNguoiDung.Text;
            if (txtMatKhau.Text.Trim() == "" ||!( txtMatKhau.Text.Length >=4 && txtMatKhau.Text.Length <=30))
            {
                Loi = "Xin vui lòng nhập vào mật khẩu có độ dài lớn hơn bằng 4 ký tự và nhỏ hơn bằng 30 ký tự !";
                return null;
            }
            NhanVien.MatKhau = txtMatKhau.Text;
            if (cboQuyenSuDung.SelectedIndex==-1)
            {
                Loi = "Xin vui lòng chọn quyền hạn sử dụng!";
                return null;
            }
            NhanVien.QuyenHan.MaQuyenHan =int.Parse( cboQuyenSuDung.SelectedValue.ToString());
            return NhanVien;
        }

        private void LamTuoi()
        {
            txtMaNhanVien.Text = NhanVienBus.LayMaNhanVienMoi();
            txtTenNhanVien.Text = "";
            txtDiaChi.Text = "";
            txtGhiChu.Text = "";
            txtDienThoai.Text = "";
            txtTenNguoiDung.Text = "";
            txtMatKhau.Text = "";
            cboQuyenSuDung.SelectedIndex = 0;
        }

        private void txtTenNguoiDung_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = clsSupport.KiemTraTenDangNhap(txtTenNguoiDung.Text, "Tên người dùng");
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


    }
}