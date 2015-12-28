using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sales.Reports;

namespace Sales
{
    public partial class frmDSPhieuThu : frmTemplete
    {
        #region Thuộc tính
        private clsPhieuThuBanHangBUS PhieuThuHangBus = new clsPhieuThuBanHangBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        #endregion
        public frmDSPhieuThu()
        {
            InitializeComponent();
        }

        private void frmDSPhieuThu_Load(object sender, EventArgs e)
        {
            try
            {
                dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpDenNgay.Value = DateTime.Now;
                KhoiTaoComboKhachHang();
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

            //thông tin 
            if (keyData == (Keys.Control | Keys.I))
            {
                In();
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

        #region Khách Hàng
        //Lấy danh sách Khách Hàng
        private void KhoiTaoComboKhachHang()
        {

            DataTable BangKhachHang = new clsKhachHangBUS().LayBang();
            if (BangKhachHang.Rows.Count > 0)
            {
                //Đưa dòng thứ 0 vào vị trí cuối cùng và sau đó gán lại dòng thứ ko là tất cả
                DataRow DongTam = BangKhachHang.NewRow();
                DongTam["MaKhachHang"] = BangKhachHang.Rows[0]["MaKhachHang"];
                DongTam["TenKhachHang"] = BangKhachHang.Rows[0]["TenKhachHang"];
                DongTam["DiaChi"] = BangKhachHang.Rows[0]["DiaChi"];
                DongTam["DienThoai"] = BangKhachHang.Rows[0]["DienThoai"];
                DongTam["Fax"] = BangKhachHang.Rows[0]["Fax"];
                DongTam["MaSoThue"] = BangKhachHang.Rows[0]["MaSoThue"];
                DongTam["NoDauKy"] = BangKhachHang.Rows[0]["NoDauKy"];
                DongTam["BaoGia"] = BangKhachHang.Rows[0]["BaoGia"];
                DongTam["ChietKhau"] = BangKhachHang.Rows[0]["ChietKhau"];
                DongTam["TenNguoiLienHe"] = BangKhachHang.Rows[0]["TenNguoiLienHe"];
                BangKhachHang.Rows.Add(DongTam);
                BangKhachHang.Rows[0]["MaKhachHang"] = "";
                BangKhachHang.Rows[0]["TenKhachHang"] = "<Tất cả>";
                BangKhachHang.Rows[0]["DiaChi"] = "";
                BangKhachHang.Rows[0]["DienThoai"] = "";
                BangKhachHang.Rows[0]["Fax"] = "";
                BangKhachHang.Rows[0]["MaSoThue"] = "";
                BangKhachHang.Rows[0]["NoDauKy"] = 0;
                BangKhachHang.Rows[0]["BaoGia"] = "";
                BangKhachHang.Rows[0]["ChietKhau"] = 0;
                BangKhachHang.Rows[0]["TenNguoiLienHe"] = "";

            }
            else
            {
                DataRow DongTam = BangKhachHang.NewRow();
                DongTam["MaKhachHang"] = "";
                DongTam["TenKhachHang"] = "<Tất cả>";
                DongTam["DiaChi"] = "";
                DongTam["DienThoai"] = "";
                DongTam["Fax"] = "";
                DongTam["MaSoThue"] = "";
                DongTam["NoDauKy"] = 0;
                DongTam["BaoGia"] = "";
                DongTam["ChietKhau"] = 0;
                DongTam["TenNguoiLienHe"] = "";
                BangKhachHang.Rows.Add(DongTam);
            }
            cboKhachHang.DataSource = BangKhachHang;
            cboKhachHang.DisplayMember = "TenKhachHang";
            cboKhachHang.ValueMember = "MaKhachHang";
        }
        #endregion

        private void btnTim_Click(object sender, EventArgs e)
        {
            HienThiTimKiem();
        }

        private void HienThiTimKiem()
        {
            try
            {
                if (cboKhachHang.SelectedIndex != -1)
                {
                    clsKhachHangDTO KhachHang = new clsKhachHangDTO();
                    KhachHang.MaKhachHang = ((DataRowView)cboKhachHang.SelectedItem).Row["MaKhachHang"].ToString().Trim();
                    KhachHang.TenKhachHang = ((DataRowView)cboKhachHang.SelectedItem).Row["TenKhachHang"].ToString().Trim();
                    KhoiTao(KhachHang);
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void KhoiTao(clsKhachHangDTO KhachHang)
        {
            if (grdvDSPhieuThu.Rows.Count > 0)
            {
                grdvDSPhieuThu.Rows.Clear();
            }
            DataTable Bang;
            if (KhachHang.TenKhachHang == "<Tất cả>")
            {
                Bang = PhieuThuHangBus.TimKiem(dtpTuNgay.Value, dtpDenNgay.Value);
            }
            else
            {
                Bang = PhieuThuHangBus.TimKiem(dtpTuNgay.Value, dtpDenNgay.Value, KhachHang);
            }
            double TongCong = 0;
            if (radioTheoMa.Checked == true)
            {
                for (int i = 0; i < Bang.Rows.Count; i++)
                {
                    if (txtPhieuThu.Text.Trim() == Bang.Rows[i]["MaPhieuThu"].ToString())
                    {
                        if (!KiemTraDongTrung(Bang.Rows[i]["MaPhieuThu"].ToString().Trim()))
                        {
                            object[] Dong = DongHienThi(Bang, i);
                            grdvDSPhieuThu.Rows.Add(Dong);
                            TongCong += Double.Parse(Bang.Rows[i]["SoTien"].ToString());
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < Bang.Rows.Count; i++)
                {
                    if (!KiemTraDongTrung(Bang.Rows[i]["MaPhieuThu"].ToString().Trim()))
                    {
                        object[] Dong = DongHienThi(Bang, i);
                        grdvDSPhieuThu.Rows.Add(Dong);
                        TongCong += Double.Parse(Bang.Rows[i]["SoTien"].ToString());
                    }
                }
            }
            txtTongCong.Text = clsSupport.CurrencyNumber(TongCong.ToString()) + " (VNĐ)";
        }

        
        private Boolean KiemTraDongTrung(string MaPhieuThu)
        {
            for (int j = 0; j < grdvDSPhieuThu.RowCount; j++)
            {
                if (grdvDSPhieuThu.Rows[j].Cells["MaPhieuThu"].Value.ToString().Trim() == MaPhieuThu)
                {
                    return true;
                }
            }
            return false;
        }

        private object[] DongHienThi(DataTable Bang, int i)
        {
            object[] Dong = new object[8];
            int STT = i + 1;
            Dong[0] = STT.ToString();
            Dong[1] = Bang.Rows[i]["MaPhieuThu"].ToString();
            Dong[2] = Bang.Rows[i]["MaPhieuXuat"].ToString();
            Dong[3] = ChuyenDoiNgay(Bang.Rows[i]["NgayThu"].ToString());
            Dong[4] = Bang.Rows[i]["TenKhachHang"].ToString();
            Dong[5] = clsSupport.CurrencyNumber(Bang.Rows[i]["SoTien"].ToString());
            Dong[6] = Bang.Rows[i]["LyDo"].ToString();
            Dong[7] = Bang.Rows[i]["NguoiNop"].ToString();
            for (int j = i + 1; j < Bang.Rows.Count; j++)
            {
                if (Bang.Rows[i]["MaPhieuThu"].ToString().Trim() == Bang.Rows[j]["MaPhieuThu"].ToString().Trim())
                {
                    Dong[2] += ", " + Bang.Rows[j]["MaPhieuXuat"].ToString();
                }
            }
            return Dong;
        }

        private string ChuyenDoiNgay(string Ngay)
        {
            DateTime NgayNhap = DateTime.Parse(Ngay);
            string strNgay = NgayNhap.Day.ToString() + "/" + NgayNhap.Month.ToString() + "/" + NgayNhap.Year.ToString();
            return strNgay;
        }

        private void btnSuaLai_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdvDSPhieuThu.CurrentRow != null && grdvDSPhieuThu.CurrentRow.Index != -1)
                {
                    frmThuTien F = new frmThuTien(grdvDSPhieuThu.Rows[grdvDSPhieuThu.CurrentRow.Index].Cells["MaPhieuThu"].Value.ToString().Trim());
                    F.ShowDialog();
                    HienThiTimKiem();
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn phiếu thu tiền bán hàng cần sửa!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String Loi = "";
            try
            {
                if (grdvDSPhieuThu.CurrentRow != null && grdvDSPhieuThu.CurrentRow.Index != -1)
                {
                    DialogResult result = MessageBox.Show("Bạn có thật sự muốn hủy phiếu thu " + grdvDSPhieuThu.Rows[grdvDSPhieuThu.CurrentRow.Index].Cells["MaPhieuThu"].Value.ToString() + " (Chú ý: Khi hủy phiếu thu thì khôi phục lại tiền đã trả của khách hàng)", "Xác nhận thông tin", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Loi = "Lỗi kết nối cơ sở dữ liệu";
                        if (PhieuThuHangBus.Huy(grdvDSPhieuThu.Rows[grdvDSPhieuThu.CurrentRow.Index].Cells["MaPhieuThu"].Value.ToString().Trim()) != -1)
                        {
                            //B1: xoa chi tiet phieu thu ban hang
                            //B2: Xoa phieu thu ban hang va cap nhat lai cot da tra cua phieu xuat
                            Decimal SoTien = Decimal.Parse(grdvDSPhieuThu.Rows[grdvDSPhieuThu.CurrentRow.Index].Cells["SoTien"].Value.ToString());
                            grdvDSPhieuThu.Rows.RemoveAt(grdvDSPhieuThu.CurrentRow.Index);
                            Decimal TongTatCaTien = Decimal.Parse(txtTongCong.Text.Replace("(VNĐ)", "").Trim());
                            TongTatCaTien = TongTatCaTien - SoTien;
                            txtTongCong.Text = clsSupport.CurrencyNumber(TongTatCaTien.ToString()) + " (VNĐ)";
                        }
                        else
                        {
                            MessageBox.Show("Hủy phiếu thu tiền bán hàng không thành công. Xin vui lòng thử lại", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn phiếu thu hàng cần xóa!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Loi, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        private void btnInRa_Click(object sender, EventArgs e)
        {
            In();
        }

        private void In()
        {
            try
            {
                if (grdvDSPhieuThu.RowCount == 0)
                {
                    MessageBox.Show("In không thành công vì không có phiếu thu trong khoảng thời gian trên!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptDSPhieuThu dsPhieuThu = new rptDSPhieuThu();
                dsPhieuThu.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                string MaPhieuThu = "";
                string MaKhachHang = cboKhachHang.SelectedValue.ToString().Trim();

                if (radioTatCa.Checked == true)
                    MaPhieuThu = "";
                else
                {
                    MaPhieuThu = txtPhieuThu.Text;
                }


                DataTable Bang = PhieuThuHangBus.ReportDSPhieuThu(dtpTuNgay.Value, dtpDenNgay.Value, MaPhieuThu, MaKhachHang);
                if (Bang.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();
                    DataSet cacBang = new DataSet();
                    cacBang.Tables.Add(Bang);
                    cacBang.Tables.Add(CongTy);

                    dsPhieuThu.SetDataSource(cacBang);
                    dsPhieuThu.SetParameterValue("@TuNgay", dtpTuNgay.Value);
                    dsPhieuThu.SetParameterValue("@DenNgay", dtpDenNgay.Value);
                    dsPhieuThu.SetParameterValue("@MaPhieuThu", MaPhieuThu);
                    dsPhieuThu.SetParameterValue("@MaKhachHang", MaKhachHang);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = dsPhieuThu;
                    dlgHienThi.ShowDialog();
                }
                else
                    MessageBox.Show("In không thành công vì không có phiếu thu trong khoảng thời gian trên!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }       
        }
    }
}