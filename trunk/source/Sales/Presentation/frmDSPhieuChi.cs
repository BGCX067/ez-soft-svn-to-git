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
    public partial class frmDSPhieuChi : frmTemplete
    {
        #region Thuộc tính
        private clsPhieuChiHangBUS PhieuChiHangBus = new clsPhieuChiHangBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        #endregion

        public frmDSPhieuChi()
        {
            InitializeComponent();
        }

        private void frmDSPhieuChi_Load(object sender, EventArgs e)
        {
            try
            {
                dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpDenNgay.Value = DateTime.Now;
                KhoiTaoComboNhaCungCap();
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Phím tắt
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //thông tin 
            if (keyData == (Keys.Control | Keys.I))
            {
                In();
            }
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
                HienThiTimKiem();
            }
        }

        #endregion

        private void HienThiTimKiem()
        {
            try
            {
                if (cboNhaCungCap.SelectedIndex != -1)
                {
                    clsNhaCungCapDTO NhaCungCap = new clsNhaCungCapDTO();
                    NhaCungCap.MaNhaCungCap = ((DataRowView)cboNhaCungCap.SelectedItem).Row["MaNhaCungCap"].ToString().Trim();
                    NhaCungCap.TenNhaCungCap = ((DataRowView)cboNhaCungCap.SelectedItem).Row["TenNhaCungCap"].ToString().Trim();
                    KhoiTao(NhaCungCap);
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

        private void KhoiTao(clsNhaCungCapDTO NhaCungCap)
        {
            if (grdvDSPhieuChi.Rows.Count > 0)
            {
                grdvDSPhieuChi.Rows.Clear();
                //bindingSource1 = new BindingSource();
            }
            DataTable Bang;
            if (NhaCungCap.TenNhaCungCap == "<Tất cả>")
            {
                Bang = PhieuChiHangBus.TimKiem(dtpTuNgay.Value, dtpDenNgay.Value);
            }
            else
            {
                Bang = PhieuChiHangBus.TimKiem(dtpTuNgay.Value, dtpDenNgay.Value, NhaCungCap);
            }
            double TongCong = 0;
            if (radioTheoMa.Checked == true)
            {
                for (int i = 0; i < Bang.Rows.Count; i++)
                {
                    if (txtPhieuChi.Text.Trim() == Bang.Rows[i]["MaPhieuChi"].ToString())
                    {
                        if (!KiemTraDongTrung(Bang.Rows[i]["MaPhieuChi"].ToString().Trim()))
                        {
                            object[] Dong = DongHienThi(Bang, i);
                            grdvDSPhieuChi.Rows.Add(Dong);
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
                    if (!KiemTraDongTrung(Bang.Rows[i]["MaPhieuChi"].ToString().Trim()))
                     {
                        object[] Dong = DongHienThi(Bang, i);
                        grdvDSPhieuChi.Rows.Add(Dong);
                        TongCong += Double.Parse(Bang.Rows[i]["SoTien"].ToString());
                    }
                }
            }
            txtTongCong.Text = clsSupport.CurrencyNumber(TongCong.ToString()) + " (VNĐ)";

        }

        private object[] DongHienThi(DataTable Bang, int i)
        {
            object[] Dong = new object[8];
            int STT = i + 1;
            Dong[0] = STT.ToString();
            Dong[1] = Bang.Rows[i]["MaPhieuChi"].ToString();
            Dong[2] = Bang.Rows[i]["MaPhieuNhap"].ToString();
            Dong[3] = ChuyenDoiNgay(Bang.Rows[i]["NgayChi"].ToString());
            Dong[4] = Bang.Rows[i]["TenNhaCungCap"].ToString();
            Dong[5] = clsSupport.CurrencyNumber(Bang.Rows[i]["SoTien"].ToString());
            Dong[6] = Bang.Rows[i]["LyDo"].ToString();
            Dong[7] = Bang.Rows[i]["NguoiNhan"].ToString();
            for (int j = i + 1; j < Bang.Rows.Count;j++ )
            {
                if (Bang.Rows[i]["MaPhieuChi"].ToString().Trim() == Bang.Rows[j]["MaPhieuChi"].ToString().Trim())
                {
                    Dong[2] += ", " + Bang.Rows[j]["MaPhieuNhap"].ToString();
                }
            }
            return Dong;
        }
        private Boolean KiemTraDongTrung(string MaPhieuChi)
        {
            for (int j = 0; j < grdvDSPhieuChi.RowCount; j++)
            {
                if (grdvDSPhieuChi.Rows[j].Cells["MaPhieuChi"].Value.ToString().Trim() == MaPhieuChi)
                {
                    return true;
                }
            }
            return false;
        }
        private string ChuyenDoiNgay(string Ngay)
        {
            DateTime NgayNhap = DateTime.Parse(Ngay);
            string strNgay = NgayNhap.Day.ToString() + "/" + NgayNhap.Month.ToString() + "/" + NgayNhap.Year.ToString();
            return strNgay;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            HienThiTimKiem();
        }

        private void btnSuaLai_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdvDSPhieuChi.CurrentRow != null && grdvDSPhieuChi.CurrentRow.Index != -1)
                {
                    frmTraTien F = new frmTraTien(grdvDSPhieuChi.Rows[grdvDSPhieuChi.CurrentRow.Index].Cells["MaPhieuChi"].Value.ToString().Trim());
                    F.ShowDialog();
                    HienThiTimKiem();
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn phiếu chi hàng!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (grdvDSPhieuChi.CurrentRow != null && grdvDSPhieuChi.CurrentRow.Index != -1)
                {
                        DialogResult result = MessageBox.Show("Bạn có thật sự muốn hủy phiếu Chi " + grdvDSPhieuChi.Rows[grdvDSPhieuChi.CurrentRow.Index].Cells["MaPhieuChi"].Value.ToString()+ " (Chú ý: Khi hủy phiếu chi thì khôi phục lại công nợ của nhà cung cấp)", "Xác nhận thông tin", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            Loi = "Lỗi kết nối cơ sở dữ liệu";
                            if (PhieuChiHangBus.Huy(grdvDSPhieuChi.Rows[grdvDSPhieuChi.CurrentRow.Index].Cells["MaPhieuChi"].Value.ToString().Trim()) != -1)
                            {   
                                //B1: xoa chi tiet phieu chi hang
                                //B2: Xoa phieu chi hang va cap nhat lai cot con no cua phieu nhap
                                Decimal SoTien = Decimal.Parse(grdvDSPhieuChi.Rows[grdvDSPhieuChi.CurrentRow.Index].Cells["SoTien"].Value.ToString());
                                grdvDSPhieuChi.Rows.RemoveAt(grdvDSPhieuChi.CurrentRow.Index);
                                Decimal TongTatCaTien = Decimal.Parse(txtTongCong.Text.Replace("(VNĐ)", "").Trim());
                                TongTatCaTien = TongTatCaTien - SoTien;
                                txtTongCong.Text = clsSupport.CurrencyNumber(TongTatCaTien.ToString()) + " (VNĐ)";
                            }
                            else
                            {
                                MessageBox.Show("Hủy phiếu chi hàng không thành công. Xin vui lòng thử lại", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn phiếu Chi hàng!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Loi, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInRa_Click(object sender, EventArgs e)
        {
            In();
        }

        private void In()
        {
            try
            {
                if (grdvDSPhieuChi.RowCount == 0)
                {
                    MessageBox.Show("In không thành công vì không có phiếu chi trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptDSPhieuChi dsPhieuChi = new rptDSPhieuChi();
                dsPhieuChi.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                string MaPhieuChi = "";
                string MaNhaCungCap = cboNhaCungCap.SelectedValue.ToString().Trim();

                if (radioTatCa.Checked == true)
                    MaPhieuChi = "";
                else
                {
                    MaPhieuChi = txtPhieuChi.Text;
                }


                DataTable Bang = PhieuChiHangBus.ReportDSPhieuChi(dtpTuNgay.Value, dtpDenNgay.Value, MaPhieuChi, MaNhaCungCap);
                if (Bang.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();
                    DataSet cacBang = new DataSet();
                    cacBang.Tables.Add(Bang);
                    cacBang.Tables.Add(CongTy);

                    dsPhieuChi.SetDataSource(cacBang);
                    dsPhieuChi.SetParameterValue("@TuNgay", dtpTuNgay.Value);
                    dsPhieuChi.SetParameterValue("@DenNgay", dtpDenNgay.Value);
                    dsPhieuChi.SetParameterValue("@MaPhieuChi", MaPhieuChi);
                    dsPhieuChi.SetParameterValue("@MaNhaCungCap", MaNhaCungCap);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = dsPhieuChi;
                    dlgHienThi.ShowDialog();
                }
                else
                    MessageBox.Show("Không có phiếu chi trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }
    }
}