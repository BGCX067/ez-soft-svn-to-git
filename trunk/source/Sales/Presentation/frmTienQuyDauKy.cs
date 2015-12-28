using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    public partial class frmTienQuyDauKy : Form
    {
        #region Thuộc tính
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        #endregion

        public frmTienQuyDauKy()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Bạn có thật sự muốn Cập nhật Tiền tồn đầu kỳ không?", "Xác nhận thông tin", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (CongTyBus.CapNhatTienTonDauKy((Double)Decimal.Parse(txtTienQuyDauKy.Text)) != -1)
                    {
                        MessageBox.Show("Cập nhật Tiền tồn đầu kỳ thành công!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật Tiền tồn đầu kỳ không thành công. Xin vui lòng thử lại", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmTienQuyDauKy_Load(object sender, EventArgs e)
        {
            try
            {
                Double TienTonDauKy = CongTyBus.LayTienTonDauKy();
                txtTienQuyDauKy.Text = clsSupport.CurrencyNumber(TienTonDauKy.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}