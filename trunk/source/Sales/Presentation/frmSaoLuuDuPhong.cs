using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;


namespace Sales
{
    public partial class frmSaoLuuDuPhong : Form
    {
       #region Bien
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
       #endregion
        public frmSaoLuuDuPhong()
        {
            InitializeComponent();
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        private void btnDuongDan_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.Description = "CHỌN ĐƯỜNG DẪN CẦN SAO LƯU DỰ PHÒNG";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtDuongDan.Text = folderBrowserDialog1.SelectedPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkSaoLuuNgay.Checked == true)
                {
                        if (txtDuongDan.Text != "")
                        {
                            String TenTapTin = "Backup_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".bak";
                            String DuongDan;
                            if (txtDuongDan.Text.LastIndexOf(@"\") == txtDuongDan.Text.Length - 1)
                            {
                                DuongDan = txtDuongDan.Text + TenTapTin;
                            }
                            else
                            {
                                DuongDan = txtDuongDan.Text + @"\" + TenTapTin;
                            }
                            if (CongTyBus.SaoLuuDuLieu(DuongDan, "BMBez2010") != -1)
                            {
                                MessageBox.Show("Sao lưu dữ liệu thành công với tên tâp tin : !" + TenTapTin, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Sao lưu dữ liệu thất bại! Xin vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        
                        }
                        else
                        {
                            MessageBox.Show("Xin vui lòng chọn đường dẫn cần Lưu dữ liệu cần sao lưu!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}