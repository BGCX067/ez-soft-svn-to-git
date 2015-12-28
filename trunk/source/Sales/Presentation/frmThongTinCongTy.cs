using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    public partial class frmThongTinCongTy : frmTemplete
    {
        public frmThongTinCongTy()
        {
            InitializeComponent();
        }

        private void frmThongTinCongTy_Load(object sender, EventArgs e)
        {
            try
            {
                Gan();

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

        private void Gan()
        {
            clsCongTyDTO CongTy = new clsCongTyBUS().LayThongTin();
            txtTenCongTy.Text = CongTy.TenCongTy;
            txtDiaChi.Text = CongTy.DiaChi;
            txtDienThoai.Text = CongTy.DienThoai;
            txtMaSoThue.Text = CongTy.MaSoThue;
            txtFax.Text = CongTy.Fax;
            txtEMail.Text = CongTy.Email;
            txtWebsite.Text = CongTy.Website;
            pictureBox1.ImageLocation = CongTy.Logo;

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string Loi="";
            try
            {
                clsCongTyDTO Congty = KhoiTao(ref Loi);
                if(Congty!=null)
                {
                    if(new clsCongTyBUS().Sua(Congty)!=-1)
                    {
                         MessageBox.Show("Cập nhật thông tin công ty thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thông tin công ty không thành công, Nguyên nhân do không kết nối được cơ sở dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(Loi, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private clsCongTyDTO KhoiTao(ref string Loi )
        {
            clsCongTyDTO CongTy = new clsCongTyDTO();
            if (txtTenCongTy.Text.Trim() == "")
            {
                Loi = "Xin vui lòng nhập vào tên công ty";
                return null;
            }
            CongTy.TenCongTy=txtTenCongTy.Text;
            CongTy.DiaChi=txtDiaChi.Text ;
            CongTy.DienThoai=txtDienThoai.Text ;
            CongTy.MaSoThue=txtMaSoThue.Text;
            CongTy.Fax=txtFax.Text ;
            CongTy.Email=txtEMail.Text ;
            CongTy.Website=txtWebsite.Text;
           //CongTy.LogopictureBox1.ImageLocation ;
            return CongTy;

        }
    }
}