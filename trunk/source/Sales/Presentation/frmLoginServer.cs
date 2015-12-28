using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    public partial class frmLoginServer : frmTemplete
    {
        public static bool isConnect = false; 
        public frmLoginServer()
        {
            InitializeComponent();
        }

        private void frmLoginServer_Load(object sender, EventArgs e)
        {
            clsConnection.ReadKeys();

            if (clsConnection.ServerName != null)
                txtServer.Text = clsConnection.ServerName;
            if (clsConnection.Databasename != null)
                txtDatabase.Text = clsConnection.Databasename;
            if (clsConnection.LoginName != null)
                txtLoginname.Text = clsConnection.LoginName;
            if (clsConnection.LoginPassword != null)
                txtPas.Text = clsConnection.LoginPassword;
        }

        #region Phím tắt
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Thoát 
            if (keyData == Keys.Escape)
            {
                DongCuaSo();
            }

            if (keyData == Keys.Enter)
            {
                DangNhap();

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

        private void btnDongY_Click(object sender, EventArgs e)
        {
            DangNhap();
        }

        private void DangNhap()
        {
            try
            {
                if (!clsConnection.IsCheckConnect(this.txtServer.Text, txtDatabase.Text, txtLoginname.Text, txtPas.Text))
                {
                    txtPas.Select();
                    txtPas.SelectAll();
                    if (MessageBox.Show("Kết nối CSDL thất bại\r\n Bạn thoát chương trình?", "Lỗi kết nối", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Application.Exit();
                }
                else
                {
                    clsConnection.ServerName = this.txtServer.Text;
                    clsConnection.Databasename = this.txtDatabase.Text;
                    clsConnection.LoginName = this.txtLoginname.Text;
                    clsConnection.LoginPassword = this.txtPas.Text;
                    clsConnection.WriteKeys();
                    isConnect = true; 
                    MessageBox.Show("Kết nối CSDL thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DongCuaSo();
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Kết nối CSDL không thành công", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

    }
}