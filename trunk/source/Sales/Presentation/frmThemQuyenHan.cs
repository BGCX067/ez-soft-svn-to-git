using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    public partial class frmThemQuyenHan : frmTemplete
    {
        #region Thuộc tính
        private clsQuyenHanBUS QuyenHanBus = new clsQuyenHanBUS();
        #endregion

        public frmThemQuyenHan()
        {
            InitializeComponent();
        }

        private void frmThemQuyenHan_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoChucNang();
                KhoiTaoQuyenHan();
                
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void KhoiTaoQuyenHan()
        {
            DataTable BangQuyenHan = new clsQuyenHanBUS().LayBang();
            //Đưa dòng thứ 0 vào vị trí cuối cùng và sau đó gán lại dòng thứ ko là tất cả
            DataRow DongTam = BangQuyenHan.NewRow();
            DongTam["MaQuyenHan"] = BangQuyenHan.Rows[0]["MaQuyenHan"];
            DongTam["TenQuyenHan"] = BangQuyenHan.Rows[0]["TenQuyenHan"];
            BangQuyenHan.Rows.Add(DongTam);
            BangQuyenHan.Rows[0]["MaQuyenHan"] = (Object)0;
            BangQuyenHan.Rows[0]["TenQuyenHan"] = "< Thêm mới >";

            cboQuyenSuDung.DataSource = BangQuyenHan;
            cboQuyenSuDung.DisplayMember = "TenQuyenHan";
            cboQuyenSuDung.ValueMember = "MaQuyenHan";
            cboQuyenSuDung.SelectedIndex=-1;
        }

        private void KhoiTaoChucNang()
        {
            //if (grdvDSChucNang.RowCount > 0)
            //{
            //    grdvDSChucNang.Rows.Clear();
            //}
            DataTable BangChucNang= new clsChucNangBUS().LayBang();
            for (int i = 0; i < BangChucNang.Rows.Count; i++)
            {
                object[] Dong = new object[4];
                Dong[0] = BangChucNang.Rows[i]["STT"];
                Dong[1] = BangChucNang.Rows[i]["MaChucNang"];
                Dong[2] = BangChucNang.Rows[i]["TenChucNang"];
                Dong[3] = false;
                grdvDSChucNang.Rows.Add(Dong);
            }
        }

        private void HienThiChucNang(int MaQuyenHan)
        {
            for (int i = 0; i < grdvDSChucNang.RowCount; i++)
            {
                grdvDSChucNang.Rows[i].Cells["DuocDung"].Value = false;
            }
            clsQuyenHanDTO QuyenHan = new clsQuyenHanBUS().LayThongTin(MaQuyenHan);
            for (int i = 0; i < QuyenHan.DS_PhanQuyenChucNang.Count; i++)
            {
                for (int j = 0; j < grdvDSChucNang.RowCount; j++)
                {
                    if (grdvDSChucNang.Rows[j].Cells["MaChucNang"].Value.ToString().Trim() == QuyenHan.DS_PhanQuyenChucNang[i].ChucNang.MaChucNang.ToString().Trim())
                    {
                        grdvDSChucNang.Rows[j].Cells["DuocDung"].Value = true;
                        break;
                    }
                }
            }
        }

        private void cboQuyenSuDung_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboQuyenSuDung.SelectedItem != null)
                {
                    if (cboQuyenSuDung.SelectedValue.ToString() == "0")
                    {
                        for (int i = 0; i < grdvDSChucNang.RowCount; i++)
                        {
                            grdvDSChucNang.Rows[i].Cells["DuocDung"].Value = false;
                        }
                        blNhapQuyenDung.Visible = true;
                        txtNhapQuyenHan.Visible = true;
                    }
                    else
                    {
                        blNhapQuyenDung.Visible = false;
                        txtNhapQuyenHan.Visible = false;
                        HienThiChucNang(Int32.Parse(cboQuyenSuDung.SelectedValue.ToString()));
                    }
                }
            }
            catch (Exception Loi)
            {
               // MessageBox.Show("Xin vui lòng thử chọn quyền sử dụng!");
            }
        }

        private void grdvDSChucNang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (grdvDSChucNang.Columns[e.ColumnIndex].Name == "DuocDung")
                {
                    if ((Boolean)grdvDSChucNang.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == false)
                    {
                        grdvDSChucNang.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                    }
                    else
                    {
                        grdvDSChucNang.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                    }
                }
            }
            catch (Exception Loi)
            {
                // MessageBox.Show("Xin vui lòng thử chọn quyền sử dụng!");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string Loi="";
            try
            {
                clsQuyenHanDTO QuyenHan = KhoiTao(ref Loi);
                //Them moi mot quyen han su dung
                if (cboQuyenSuDung.SelectedValue.ToString() == "0")
                {
                    if (QuyenHan != null)
                    {
                        if (QuyenHanBus.Them(QuyenHan) != -1)
                        {
                            MessageBox.Show("Lưu Quyền hạn sử dụng " + QuyenHan.TenQuyenHan + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LamTuoi();
                        }
                        else
                        {
                            MessageBox.Show("Lưu Quyền hạn sử dụng "+ QuyenHan.TenQuyenHan + " không thành công, nguyên nhân do quyền hạn sử dụng này đã tồn tại rồi.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(Loi, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else //cap nhat mot quyen han su dung
                {
                    if (QuyenHan != null)
                    {
                        if (QuyenHanBus.Sua(QuyenHan) != -1)
                        {
                            MessageBox.Show("Lưu Quyền hạn sử dụng " + QuyenHan.TenQuyenHan + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Lưu Quyền hạn sử dụng " + QuyenHan.TenQuyenHan + " không thành công, nguyên nhân do quyền hạn sử dụng này đã tồn tại rồi.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(Loi, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private clsQuyenHanDTO KhoiTao(ref string Loi)
        {
            clsQuyenHanDTO QuyenHan = new clsQuyenHanDTO();
            if (cboQuyenSuDung.SelectedValue.ToString() == "0")//Them moi
            {
                QuyenHan.MaQuyenHan = 0;
                if (txtNhapQuyenHan.Text.Trim() != "")
                {
                    QuyenHan.TenQuyenHan = txtNhapQuyenHan.Text.Trim();
                }
                else
                {
                    Loi = "Xin vui lòng nhập tên quyền hạn sử dụng!";
                    return null;
                }
            }
            else//Cap nhat
            {
                QuyenHan.MaQuyenHan = int.Parse(cboQuyenSuDung.SelectedValue.ToString());
                if (((DataRowView)cboQuyenSuDung.SelectedItem).Row["TenQuyenHan"].ToString().Trim() != "")
                {
                    QuyenHan.TenQuyenHan = ((DataRowView)cboQuyenSuDung.SelectedItem).Row["TenQuyenHan"].ToString();
                }
                else
                {
                    Loi = "Xin vui lòng chọn quyền hạn sử dụng!";
                    return null;
                }
            }
            //Khoi tao chi tiet cac chuc nang cho tung quyen han
            for (int i = 0; i < grdvDSChucNang.Rows.Count; i++)
            {
                if ((Boolean)grdvDSChucNang.Rows[i].Cells["DuocDung"].Value == true)
                {
                    clsPhanQuyenChucNangDTO PhanQuyen = new clsPhanQuyenChucNangDTO();
                    PhanQuyen.MaQuyenHan = int.Parse(cboQuyenSuDung.SelectedValue.ToString());
                    PhanQuyen.ChucNang.MaChucNang = int.Parse(grdvDSChucNang.Rows[i].Cells["MaChucNang"].Value.ToString());
                    QuyenHan.DS_PhanQuyenChucNang.Add(PhanQuyen);
                }
            }
            if (QuyenHan.DS_PhanQuyenChucNang.Count==0)
            {
                Loi = "Xin vui lòng chọn các chức năng!";
                return null;
            }
            return QuyenHan;
        }

        private void LamTuoi()
        {
            txtNhapQuyenHan.Visible = false;
            blNhapQuyenDung.Visible = false;
            KhoiTaoQuyenHan();
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

    }
}