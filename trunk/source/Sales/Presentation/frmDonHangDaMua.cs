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
    public partial class frmDonHangDaMua : frmTemplete
    {
        #region Thuộc tính
        private clsPhieuNhapBUS PhieuNhapBus = new clsPhieuNhapBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        #endregion

        #region Sự kiện màn hình Các hóa đơn mua hàng
        public frmDonHangDaMua()
        {
            InitializeComponent();
        }

        private void frmDonHangDaMua_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboNhaCungCap();
                dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpDenNgay.Value = DateTime.Now;

            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

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
        }

        #endregion

        private void btnTim_Click(object sender, EventArgs e)
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
            if (grdvDSDonHangDaBan.Rows.Count > 0)
            {
                grdvDSDonHangDaBan.Rows.Clear();
                //bindingSource1 = new BindingSource();
            }
            DataTable Bang;
            if (NhaCungCap.TenNhaCungCap == "<Tất cả>")
            {
                Bang = PhieuNhapBus.TimKiem(dtpTuNgay.Value,dtpDenNgay.Value);
            }
            else
            {
                Bang = PhieuNhapBus.TimKiem(dtpTuNgay.Value, dtpDenNgay.Value, NhaCungCap);
            }
            double TongCong = 0;
            for (int i = 0; i < Bang.Rows.Count; i++)
            {
                object[] Dong = new object[10];
                int STT = i+1;
                Dong[0] = STT.ToString();
                Dong[1] = Bang.Rows[i]["MaNhaCungCap"].ToString();
                Dong[2] = Bang.Rows[i]["TenNhaCungCap"].ToString();
                Dong[3] = Bang.Rows[i]["MaPhieuNhap"].ToString();
                Dong[4] = ChuyenDoiNgay(Bang.Rows[i]["NgayNhap"].ToString());
                Dong[5] = clsSupport.CurrencyNumber(Bang.Rows[i]["TienHang"].ToString());
                Dong[6] = clsSupport.CurrencyNumber(Bang.Rows[i]["ChietKhau"].ToString());
                Dong[7] = clsSupport.CurrencyNumber(Bang.Rows[i]["ThueVAT"].ToString());
                Dong[8] = clsSupport.CurrencyNumber(Bang.Rows[i]["TongTien"].ToString());
                Dong[9] = clsSupport.CurrencyNumber(Bang.Rows[i]["ConNo"].ToString());
                TongCong += double.Parse(Bang.Rows[i]["TongTien"].ToString());
                grdvDSDonHangDaBan.Rows.Add(Dong);
            }
            txtTongCong.Text =  clsSupport.CurrencyNumber(TongCong.ToString()) + " (VNĐ)";

        }

        private string ChuyenDoiNgay(string Ngay)
        {
            DateTime NgayNhap = DateTime.Parse(Ngay);
            string strNgay = NgayNhap.Day.ToString() + "/" + NgayNhap.Month.ToString() + "/" + NgayNhap.Year.ToString();
            return strNgay;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String Loi = "";
            try
            {
                if (grdvDSDonHangDaBan.CurrentRow!=null && grdvDSDonHangDaBan.CurrentRow.Index != -1)
                {
                    double TongTien = double.Parse(grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["TongCong"].Value.ToString().Replace(",", ""));
                    double ConNo=double.Parse(grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["ConNo"].Value.ToString().Replace(",",""));
                    string MaPhieuNhap = grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["MaPhieuNhap"].Value.ToString().Trim();
                    if (TongTien == ConNo)
                    {
                        if (PhieuNhapBus.KiemTraHuyPhieuNhap(MaPhieuNhap)==true)
                        {
                            DialogResult result = MessageBox.Show("Bạn có thật sự muốn hủy phiếu nhập " + MaPhieuNhap + " (Chú ý: Khi hủy phiếu nhập kho thì số lượng các mặt hàng trong phiếu xuất đó được phục hồi", "Xác nhận thông tin", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                Loi = "Lỗi kết nối cơ sở dữ liệu";
                                if (PhieuNhapBus.Huy(MaPhieuNhap) != -1)
                                {
                                    grdvDSDonHangDaBan.Rows.RemoveAt(grdvDSDonHangDaBan.CurrentRow.Index);
                                    double TongTatCaTien = double.Parse(txtTongCong.Text.Replace("(VNĐ)", "").Trim());
                                    TongTatCaTien = TongTatCaTien - TongTien;
                                    txtTongCong.Text = clsSupport.CurrencyNumber(TongTatCaTien.ToString()) + " (VNĐ)";
                                }
                                else
                                {
                                    MessageBox.Show("Hủy phiếu nhập không thành công. Xin vui lòng thử lại", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Số lượng các mặt hàng trong phiếu nhập " + MaPhieuNhap + " đã được xuất kho nên không cho phép hủy phiếu nhập này.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Phiếu nhập kho này không được phép hủy vì đã được trả tiền", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn phiếu nhập!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Loi, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaLai_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdvDSDonHangDaBan.CurrentRow!=null && grdvDSDonHangDaBan.CurrentRow.Index != -1)
                {
                    frmNhapHangVaoKho F = new frmNhapHangVaoKho(grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["MaPhieuNhap"].Value.ToString().Trim());
                    F.ShowDialog();
                    if (cboNhaCungCap.SelectedIndex != -1)
                    {
                        clsNhaCungCapDTO NhaCungCap = new clsNhaCungCapDTO();
                        NhaCungCap.MaNhaCungCap = ((DataRowView)cboNhaCungCap.SelectedItem).Row["MaNhaCungCap"].ToString().Trim();
                        NhaCungCap.TenNhaCungCap = ((DataRowView)cboNhaCungCap.SelectedItem).Row["TenNhaCungCap"].ToString().Trim();
                        KhoiTao(NhaCungCap);
                    }
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn phiếu nhập!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptDonHangDaMua donHangDaMua = new rptDonHangDaMua();
                donHangDaMua.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                string MaNhaCungCap = cboNhaCungCap.SelectedValue.ToString().Trim();

                DataTable Bang = PhieuNhapBus.ReportDonHangDaMua(dtpTuNgay.Value, dtpDenNgay.Value, MaNhaCungCap);
                if (Bang.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();

                    DataSet cacBang = new DataSet();
                    cacBang.Tables.Add(Bang);
                    cacBang.Tables.Add(CongTy);

                    donHangDaMua.SetDataSource(cacBang);
                    donHangDaMua.SetParameterValue("@TuNgay", dtpTuNgay.Value);
                    donHangDaMua.SetParameterValue("@DenNgay", dtpDenNgay.Value);
                    donHangDaMua.SetParameterValue("@MaNhaCungCap", MaNhaCungCap);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = donHangDaMua;
                    dlgHienThi.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có đơn hàng nào đã mua trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}