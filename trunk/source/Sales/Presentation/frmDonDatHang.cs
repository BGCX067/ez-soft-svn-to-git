using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    public partial class frmDonDatHang : frmTemplete
    {
        public frmDonDatHang()
        {
            InitializeComponent();
        }


        private void frmDonDatHang_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboNhaCungCap();
                dtpNgayDat.Value = DateTime.Now;

            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Nhà cung cấp
        //Lấy danh sách nhà cung cấp
        private void KhoiTaoComboNhaCungCap()
        {
            //Load combo nhom hang
            DataTable BangNhaCungCap = new clsNhaCungCapBUS().LayBang();
            if (BangNhaCungCap.Rows.Count > 0)
            {
                //Đưa dòng thứ 0 vào vị trí cuối cùng và sau đó gán lại dòng thứ ko là tất cả
                DataRow DongTam = BangNhaCungCap.NewRow();
                DongTam["MaNhaCungCap"] = BangNhaCungCap.Rows[0]["MaNhaCungCap"];
                DongTam["TenNhaCungCap"] = BangNhaCungCap.Rows[0]["TenNhaCungCap"];
                DongTam["DiaChi"] = BangNhaCungCap.Rows[0]["DiaChi"];
                DongTam["DienThoai"] = BangNhaCungCap.Rows[0]["DienThoai"];
                DongTam["Fax"] = BangNhaCungCap.Rows[0]["Fax"];
                DongTam["MaSoThue"] = BangNhaCungCap.Rows[0]["MaSoThue"];
                DongTam["NoDauKy"] = BangNhaCungCap.Rows[0]["NoDauKy"];
                DongTam["TenNguoiLienHe"] = BangNhaCungCap.Rows[0]["TenNguoiLienHe"];
                BangNhaCungCap.Rows.Add(DongTam);
                BangNhaCungCap.Rows[0]["MaNhaCungCap"] = "";
                BangNhaCungCap.Rows[0]["TenNhaCungCap"] = "<Tất cả>";
                BangNhaCungCap.Rows[0]["DiaChi"] = "";
                BangNhaCungCap.Rows[0]["DienThoai"] = "";
                BangNhaCungCap.Rows[0]["Fax"] = "";
                BangNhaCungCap.Rows[0]["MaSoThue"] = "";
                BangNhaCungCap.Rows[0]["NoDauKy"] = 0;
            }
            else
            {
                //Đưa dòng thứ 0 vào vị trí cuối cùng và sau đó gán lại dòng thứ ko là tất cả
                DataRow DongTam = BangNhaCungCap.NewRow();
                DongTam["MaNhaCungCap"] = "";
                DongTam["TenNhaCungCap"] = "<Tất cả>";
                DongTam["DiaChi"] = "";
                DongTam["DienThoai"] = "";
                DongTam["Fax"] = "";
                DongTam["MaSoThue"] = "";
                DongTam["NoDauKy"] = 0;
                BangNhaCungCap.Rows.Add(DongTam);
            }
            cboNhaCungCap.DataSource = BangNhaCungCap;
            cboNhaCungCap.DisplayMember = "TenNhaCungCap";
            cboNhaCungCap.ValueMember = "MaNhaCungCap";
        }


        private void cboNhaCungCap_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (cboNhaCungCap.SelectedIndex != -1)
                    {
                        clsNhaCungCapDTO NhaCungCap = new clsNhaCungCapDTO();
                        NhaCungCap.MaNhaCungCap = ((DataRowView)cboNhaCungCap.SelectedItem).Row["MaNhaCungCap"].ToString().Trim();
                        NhaCungCap.TenNhaCungCap = ((DataRowView)cboNhaCungCap.SelectedItem).Row["TenNhaCungCap"].ToString().Trim();
                        //KhoiTao(NhaCungCap);
                    }
                    else
                    {
                        MessageBox.Show("Xin vui lòng chọn nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        private void cboNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboNhaCungCap.SelectedItem != null)
                {
                    //clsNhaCungCapDTO NhaCungCap = new clsNhaCungCapDTO();
                    //NhaCungCap.MaNhaCungCap = ((DataRowView)cboNhaCungCap.SelectedItem).Row["MaNhaCungCap"].ToString().Trim();
                    //NhaCungCap.TenNhaCungCap = ((DataRowView)cboNhaCungCap.SelectedItem).Row["TenNhaCungCap"].ToString().Trim();
                    txtDiaChi.Text = ((DataRowView)cboNhaCungCap.SelectedItem).Row["DiaChi"].ToString();
                    txtMaSoThue.Text = ((DataRowView)cboNhaCungCap.SelectedItem).Row["MaSoThue"].ToString();
                }
            }
            catch (Exception loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInRa_Click(object sender, EventArgs e)
        {

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

            ////thông tin 
            //if (keyData == (Keys.Control | Keys.I))
            //{
            //    In();
            //}

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
    }
}