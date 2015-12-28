using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    public partial class frmLogin : Form
    {
        public static bool isLogin = false;
        public static clsUser User = new clsUser();

        public frmLogin()
        {
            InitializeComponent();
        }

        #region Phím tắt
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                DangNhap();

            }

            //Thoát 
            if (keyData == Keys.Escape)
            {
                Thoat();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
       
        #endregion

        private void btnDongY_Click(object sender, EventArgs e)
        {
            DangNhap();
        }

        private void DangNhap()
        {
            try
            {
                if (txtUsername.Text.Trim() != "")
                {
                    if (txtPassword.Text.Trim() != "")
                    {
                        User = new clsUser(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                        if (User.DangNhap(chkRemember.Checked) == true)
                        {
                            User.LayThongTinNguoiDung();
                            isLogin = true;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Đăng nhập không thành công! Xin vui lòng kiểm tra lại Tên đăng nhập hoặc Mật khẩu.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtUsername.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Xin vui lòng nhập Mật khẩu!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPassword.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Xin vui lòng nhập Tên đăng nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Kết nối CSDL không thành công!", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Thoat();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            isLogin = false;
            User.DocThongTinGhiNho();

            if (User.TenNguoiDung != null)
                txtUsername.Text = User.TenNguoiDung;
            if (User.MatKhau != null)
                txtPassword.Text = User.MatKhau;
            if (User.TenNguoiDung != null && User.MatKhau != null)
                chkRemember.Checked = true;
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Thoat();
        }

        private void Thoat()
        {
            try
            {
                if (isLogin == false)
                {
                    Application.Exit();
                }
                //else
                //{
                //    this.Close();
                //}
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}