using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Sales.Reports;

namespace Sales
{
    public partial class frmBanLe : frmTemplete
    {
        #region Thuộc tính
        private clsPhieuXuatBanLeBUS PhieuXuatBanLeBus = new clsPhieuXuatBanLeBUS();
       // private clsPhieuXuatBanSiBUS PhieuXuatBanSiBus = new clsPhieuXuatBanSiBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        public string ThaoTac = "Them";
        public string MaPhieuXuatCanXem = null;
        string  LienKet = "";
        List<clsChiTietPhieuXuatDTO> CacCPX;
        private List<clsChiTietPhieuXuatDTO> CacMHDaBanCuaCTPX;
        #endregion

        #region Sự kiện màn hình Bán Lẻ
        public frmBanLe()
        {
            InitializeComponent();
        }

        public frmBanLe(string _MaPhieuXuat, string _LienKet)
        {
            MaPhieuXuatCanXem=_MaPhieuXuat;
            LienKet = _LienKet;
            InitializeComponent();
        }

        private void frmBanLe_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboNhanVien();
                KhoiTaoComboChonHang();
                dtpNgayXuat.Value = DateTime.Now;
                cboChonHang.Focus();
                CacCPX = new List<clsChiTietPhieuXuatDTO>();
                CacMHDaBanCuaCTPX = new List<clsChiTietPhieuXuatDTO>();
                if (MaPhieuXuatCanXem != null)
                {
                    txtMaPhieuXuat.Text = MaPhieuXuatCanXem;
                    XemPhieuXuatTheoMaPhieuXuat(MaPhieuXuatCanXem);
                    //if (LienKet == "QuanLyDonHangDaBan")
                    //{
                    //    AnCacVungNhapLieu(true);
                    //}
                }
                else
                {
                    txtMaPhieuXuat.Text = PhieuXuatBanLeBus.LayMaPhieuXuatMoi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XemPhieuXuatTheoMaPhieuXuat(string MaPhieuXuat)
        {
            LamTuoi();
            clsPhieuXuatBanLeDTO PhieuXuat = PhieuXuatBanLeBus.LayThongTin(MaPhieuXuat);
            if (PhieuXuat != null)
            {
                //Chinh sua phieu xuat ban le
                //clsPhieuXuatBanLeDTO PhieuXuatXuLy = PhieuXuatBanLeBus.LayThongTinTheoPhieuNhap(MaPhieuXuat);
                //CacCPX = PhieuXuatXuLy.DS_ChiTietPhieuXuat;

                ////ThaoTac = "CapNhat";
                AnCacVungNhapLieu(false);
                //btnInRa.Enabled = true;
                    clsPhieuXuatBanLeDTO PhieuXuatXuLy = PhieuXuatBanLeBus.LayThongTinTheoPhieuNhap(MaPhieuXuat);
                    CacCPX = PhieuXuatXuLy.DS_ChiTietPhieuXuat;
                    for (int i = 0;i< PhieuXuatXuLy.DS_ChiTietPhieuXuat.Count; i++)
                    {
                        clsChiTietPhieuXuatDTO CTPX =PhieuXuatXuLy.DS_ChiTietPhieuXuat[i];
                        CacMHDaBanCuaCTPX.Add(CTPX); 
                    }
                dtpNgayXuat.Value = PhieuXuat.NgayXuat;
                txtKhachHang.Text = PhieuXuat.KhachHang;
                cboNhanVienBH.SelectedValue = PhieuXuat.NhanVien.MaNhanVien ;
                for (int i = 0; i < PhieuXuat.DS_ChiTietPhieuXuat.Count; i++)
                {
                    object[] Dong = new object[10];
                    int STT = (grdvBanLe.RowCount + 1);
                    Dong[0] = STT.ToString();
                    Dong[1] = PhieuXuat.DS_ChiTietPhieuXuat[i].MatHang.MaMatHang;
                    Dong[2] = PhieuXuat.DS_ChiTietPhieuXuat[i].MatHang.TenMatHang;
                    Dong[3] = PhieuXuat.DS_ChiTietPhieuXuat[i].MatHang.DonViTinh;
                    Dong[4] = PhieuXuat.DS_ChiTietPhieuXuat[i].SoLuong.ToString(); ;
                    Dong[5] = clsSupport.CurrencyNumber(PhieuXuat.DS_ChiTietPhieuXuat[i].DonGia.ToString());
                    Dong[6] = PhieuXuat.DS_ChiTietPhieuXuat[i].ChietKhau.ToString() + "%";
                    Dong[7] = PhieuXuat.DS_ChiTietPhieuXuat[i].ThueVAT.ToString() + "%";
                    Dong[8] = clsSupport.CurrencyNumber(PhieuXuat.DS_ChiTietPhieuXuat[i].ThanhTien.ToString());
                    Dong[9] = 0;
                    grdvBanLe.Rows.Add(Dong);
                }
                
                //Tinh tong tien
                txtTongCong.Text = clsSupport.CurrencyNumber(PhieuXuat.TongTien.ToString());
                txtKhachDua.Text = clsSupport.CurrencyNumber(PhieuXuat.DaTra.ToString());
                if (PhieuXuat.NhanVien.MaNhanVien.Trim() == clsUser.MaNhanVien.Trim())
                {
                    btnInRa.Enabled = true;
                }
                else
                {
                    btnInRa.Enabled = false;
                }
                cboNhanVienBH.Enabled = false;
            }
            else
            {
                ThaoTac = "Them";
                clsPhieuXuatBanLeDTO PXBanLe = PhieuXuatBanLeBus.LayThongTin(MaPhieuXuat);
                if (PXBanLe != null)
                {
                    MessageBox.Show("Không có phiếu xuất bán lẻ " + txtMaPhieuXuat.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMaPhieuXuat.Focus();
                    return;
                }
                else
                {
                    string Loi = "";
                    if (txtMaPhieuXuat.Text.Length >= 4)
                    {
                        if (txtMaPhieuXuat.Text.Substring(0, 3) == "PXK")
                        {
                            int SoPhieuXuat = -1;
                            if (int.TryParse(txtMaPhieuXuat.Text.Substring(3, (txtMaPhieuXuat.Text.Length - 3)), out SoPhieuXuat) == true && SoPhieuXuat > 0)
                            {
                                DialogResult result = MessageBox.Show("Phiếu xuất " + txtMaPhieuXuat.Text + " không tồn tại. Bạn có muốn tạo mới phiếu xuất này không?", "Xac nhan", MessageBoxButtons.YesNo);
                                if (result == DialogResult.Yes)
                                {
                                    AnCacVungNhapLieu(true);
                                    LamTuoi();
                                    HienCacNutNhan(false);
                                }
                            }
                            else
                            {
                                Loi = "Xin vui lòng nhập Phiếu xuất có dạng như sau: PXK + Số thứ tự (Số nguyên dương) vd: PXK1, PXK2,... !";
                                MessageBox.Show(Loi, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            Loi = "Xin vui lòng nhập Phiếu xuất có dạng như sau: PXK + Số thứ tự (Số nguyên dương) vd: PXK1, PXK2,... !";
                            MessageBox.Show(Loi, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
               
            }
        }

        private void LamTuoi()
        {
            try
            {
                txtKhachHang.Text = "";
                txtTongCong.Text = "0";
                txtKhachDua.Text = "0";
                txtThoiLai.Text = "0";
                txtSoLuong.Text = "1";
                txtSuaChietKhau.Text = "0%";
                txtSuaGia.Text = "0";
                dtpNgayXuat.Value = DateTime.Now;
                cboNhanVienBH.SelectedValue = clsUser.MaNhanVien;
                cboChonHang.SelectedIndex = -1;
                cboChonHang.Text = "< Chọn mặt hàng! >";
                grdvBanLe.Rows.Clear();
                CacCPX = new List<clsChiTietPhieuXuatDTO>();
                CacMHDaBanCuaCTPX = new List<clsChiTietPhieuXuatDTO>();
                txtKhachDua.TabStop = false;
                //KhoiTaoComboChonHang();
                cboChonHang.Focus();
            }
            catch (Exception Loi)
            {

            }
        }
        # endregion

        #region Phím tắt
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                KhoiTaoMoi();

            }

            if (keyData == Keys.F2)
            {
                try
                {
                    frmMatHangMua MatHang = new frmMatHangMua();
                    MatHang.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            //Thêm mặt hàng cần nhập
            if (keyData == Keys.F3)
            {
                try
                {
                    try
                    {
                        NhapThemHang();
                    }
                    catch (Exception Loi)
                    {
                        MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            //Sửa thông tin hàng đã nhập
            if (keyData == Keys.F5)
            {
                SuaHangDaNhap();
            }

            //Xóa thông tin hàng đã nhập
            if (keyData == Keys.F6)
            {
                XoaHangDaNhap();

            }
            //Lưu thông tin Phiếu xuất
            if (keyData == (Keys.Control | Keys.L))
            {
                string Loi = "";
                LuuPhieuXuat(ref Loi);
            }

            //Lưu và in thông tin Phiếu xuất
            if (keyData == (Keys.Control | Keys.I))
            {
                In();
            }

            //Thoát 
            if (keyData == Keys.Escape)
            {
                this.Close();
            }

            //Lưu thông tin Phiếu xuất
            if (keyData == Keys.F12)
            {
               HienCacNutNhan(true);
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        // Thêm thông tin mặt hàng
        private void NhapThemHang()
        {
            string MaMatHang = ((DataRowView)cboChonHang.SelectedItem).Row["MaMatHang"].ToString().Trim();
            if (KiemTraMatHang(MaMatHang) == true)
            {
                if (KiemTraLuongTon(MaMatHang) == true)
                {
                    //Them chi tiet phieu nhap 
                    List<clsChiTietPhieuNhapDTO> ChonMatHang = new clsPhieuNhapBUS().ChonMatHangNhapVoiGiaCao(MaMatHang, int.Parse(txtSoLuong.Text));
                    if (ThaoTac != "Them")
                    {
                        //Khoi phuc so luong ton cac mat hang trong chi tiet phieu nhap
                        for (int i = 0; i < CacMHDaBanCuaCTPX.Count; i++)
                        {
                            if (CacMHDaBanCuaCTPX[i].MatHang.MaMatHang.Trim() == MaMatHang)
                            {
                                Boolean Co = false;//Mat hang nay khong co trong phieu nhap
                                for (int j = 0; j < ChonMatHang.Count; j++)
                                {
                                    if (CacMHDaBanCuaCTPX[i].MaPhieuNhap == ChonMatHang[j].MaPhieuNhap)
                                    {
                                        ChonMatHang[j].SoLuongTon += CacMHDaBanCuaCTPX[i].SoLuong;
                                        Co = true;
                                        break;
                                    }
                                }
                                if (Co == false)
                                {
                                    clsChiTietPhieuNhapDTO ctpn = new clsChiTietPhieuNhapDTO();
                                    ctpn.MaPhieuNhap = CacMHDaBanCuaCTPX[i].MaPhieuNhap;
                                    ctpn.MatHang.MaMatHang = CacMHDaBanCuaCTPX[i].MatHang.MaMatHang;
                                    ctpn.MatHang.TenMatHang = CacMHDaBanCuaCTPX[i].MatHang.TenMatHang;
                                    ctpn.SoLuongTon = CacMHDaBanCuaCTPX[i].SoLuong;
                                    ChonMatHang.Add(ctpn);
                                }
                            }
                        }

                        //Sap xep va chon cac mat hang theo so luong
                        ChonMatHang = new clsPhieuNhapBUS().ChonMatHangNhapVoiGiaCao(int.Parse(txtSoLuong.Text), ChonMatHang);
                        if (ChonMatHang == null)
                        {
                            ChonMatHang = new List<clsChiTietPhieuNhapDTO>();
                        }
                    }
                    //Tinh thanh tien khi cho chiet khau 
                    string ChietKhau = "";
                    if (txtSuaChietKhau.Text.IndexOf("%") != -1)
                    {
                        ChietKhau = txtSuaChietKhau.Text.Replace("%", "");
                    }
                    else
                    {
                        ChietKhau = txtSuaChietKhau.Text;
                    }
                    // Them cac mat hang vao
                    for (int i = 0; i < ChonMatHang.Count; i++)
                    {
                        clsChiTietPhieuXuatDTO PhieuXuat = new clsChiTietPhieuXuatDTO();
                        PhieuXuat.MatHang.MaMatHang = ((DataRowView)cboChonHang.SelectedItem).Row["MaMatHang"].ToString();
                        PhieuXuat.MatHang.TenMatHang = ((DataRowView)cboChonHang.SelectedItem).Row["TenMatHang"].ToString();
                        PhieuXuat.SoLuong = ChonMatHang[i].SoLuongTon;
                        PhieuXuat.MaPhieuNhap = ChonMatHang[i].MaPhieuNhap;
                        PhieuXuat.ThueVAT = double.Parse(((DataRowView)cboChonHang.SelectedItem).Row["ThueVAT"].ToString());
                        PhieuXuat.DonGia = double.Parse(txtSuaGia.Text);
                        PhieuXuat.ChietKhau = double.Parse(ChietKhau);
                        Double GiaMoi = double.Parse(txtSuaGia.Text);
                        Double ThanhTienChuaVATCT = ChonMatHang[i].SoLuongTon * GiaMoi;
                        Double TienThueVATCT = (ThanhTienChuaVATCT * PhieuXuat.ThueVAT) / 100;
                        Double ThanhTienChuaChietKhauCT = ThanhTienChuaVATCT + TienThueVATCT;
                        PhieuXuat.ThanhTien = ThanhTienChuaChietKhauCT - (ThanhTienChuaChietKhauCT * double.Parse(ChietKhau)) / 100;
                        CacCPX.Add(PhieuXuat);
                    }

                    object[] Dong = new object[10];
                    int STT = (grdvBanLe.RowCount + 1);
                    Dong[0] = STT.ToString();
                    Dong[1] = ((DataRowView)cboChonHang.SelectedItem).Row["MaMatHang"].ToString();
                    Dong[2] = ((DataRowView)cboChonHang.SelectedItem).Row["TenMatHang"].ToString();
                    Dong[3] = ((DataRowView)cboChonHang.SelectedItem).Row["DonViTinh"].ToString();
                    Dong[4] = txtSoLuong.Text.Trim();
                    Dong[5] = txtSuaGia.Text.Trim();
                    Dong[6] = ChietKhau + "%";
                    Dong[7] = ((DataRowView)cboChonHang.SelectedItem).Row["ThueVAT"].ToString() + "%";
                    Double ThanhTienChuaVAT = int.Parse(txtSoLuong.Text) * double.Parse(txtSuaGia.Text.Trim());
                    Double TienThueVAT= ThanhTienChuaVAT * (double.Parse(((DataRowView)cboChonHang.SelectedItem).Row["ThueVAT"].ToString()) / 100) ;
                    //Thanh tien da co thue VAT nhung chua co chiet khau
                    Double ThanhTien =  ThanhTienChuaVAT + TienThueVAT;
                    
                    //Thanh tien da co chiet khau
                    ThanhTien = ThanhTien - (ThanhTien * double.Parse(ChietKhau)) / 100;
                    Dong[8] = clsSupport.CurrencyNumber(ThanhTien.ToString());
                    int SoLuongTon = int.Parse(((DataRowView)cboChonHang.SelectedItem).Row["SoLuongTon"].ToString());
                    int SoLuongNhap = int.Parse(txtSoLuong.Text);
                    int LuongTonConLai = SoLuongTon - SoLuongNhap;
                    Dong[9] = LuongTonConLai.ToString();
                    grdvBanLe.Rows.Add(Dong);
                    //Tinh tong tien
                    txtTongCong.Text = TinhTongTien().ToString();

                }
            }
        }

        private bool KiemTraLuongTon(string MaMatHang)
        {
            int SoLuongTon = int.Parse(((DataRowView)cboChonHang.SelectedItem).Row["SoLuongTon"].ToString());
            //Cong don so luong mat hang ton
            for (int i = 0; i < CacMHDaBanCuaCTPX.Count; i++)
            {
                if (CacMHDaBanCuaCTPX[i].MatHang.MaMatHang.Trim() == MaMatHang)
                {
                    SoLuongTon += CacMHDaBanCuaCTPX[i].SoLuong;
                }
            }
            int LuongMin =int.Parse( ((DataRowView)cboChonHang.SelectedItem).Row["LuongMin"].ToString());
            int SoLuongNhap = int.Parse(txtSoLuong.Text);
            if (SoLuongTon == 0)
            {
                MessageBox.Show("Mặt hàng " + ((DataRowView)cboChonHang.SelectedItem).Row["TenMatHang"].ToString() + " đã hết hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (SoLuongNhap > SoLuongTon)
                {
                    MessageBox.Show("Xin vui lòng nhập vào số lượng nhập <= số lượng tồn: " + SoLuongTon.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (SoLuongTon - SoLuongNhap <= LuongMin)
                    {
                        MessageBox.Show("Hàng đã đạt ngưỡng Min,lưu ý nhập bổ sung hàng thêm!" , "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return true;
                }
            }
            return false;
        }
        //Tinh tong tien
        private double TinhTongTien()
        {
            double TongTien = 0;
            for (int i = 0; i < grdvBanLe.Rows.Count; i++)
            {
                double ThanhTien = double.Parse(grdvBanLe.Rows[i].Cells["ThanhTien"].Value.ToString().Trim());
                TongTien += ThanhTien;
            }
            return TongTien;
        }

        //Kiểm tra mặt hàng này nhập chưa
        private Boolean KiemTraMatHang(string MaMatHang)
        {
            Boolean KetQua = true;
            for (int i = 0; i < grdvBanLe.Rows.Count; i++)
            {
                if (grdvBanLe.Rows[i].Cells["MaMatHang"].Value.ToString().Trim() == MaMatHang)
                {
                    KetQua = false;
                    break;
                }
            }
            return KetQua;
        }

        //Chi tiết Sửa thông tin mặt hàng đã nhập
        private void SuaHangDaNhap()
        {
            try
            {
                if (grdvBanLe.CurrentRow != null)
                {
                    if (ThaoTac == "Them")
                    {
                        int DongDangSua = grdvBanLe.CurrentRow.Index;
                        if (KiemTraLuongTon(grdvBanLe.Rows[DongDangSua].Cells["MaMatHang"].Value.ToString()) == true)
                        {
                            for (int i = 0; i < CacCPX.Count; i++)
                            {
                                if (CacCPX[i].MatHang.MaMatHang.Trim() == grdvBanLe.Rows[DongDangSua].Cells["MaMatHang"].Value.ToString())
                                {
                                    CacCPX.RemoveAt(i);
                                    i = -1;
                                }
                            }
                            List<clsChiTietPhieuNhapDTO> ChonMatHang = new clsPhieuNhapBUS().ChonMatHangNhapVoiGiaCao(((DataRowView)cboChonHang.SelectedItem).Row["MaMatHang"].ToString(), int.Parse(txtSoLuong.Text));
                            
                            if (ChonMatHang == null)
                            {
                                ChonMatHang = new List<clsChiTietPhieuNhapDTO>();
                            }
                            //Tinh thanh tien khi cho chiet khau 
                            string ChietKhau = "";
                            if (txtSuaChietKhau.Text.IndexOf("%") != -1)
                            {
                                ChietKhau = txtSuaChietKhau.Text.Replace("%", "");
                            }
                            else
                            {
                                ChietKhau = txtSuaChietKhau.Text;
                            }
                            for (int i = 0; i < ChonMatHang.Count; i++)
                            {
                                clsChiTietPhieuXuatDTO PhieuXuat = new clsChiTietPhieuXuatDTO();
                                PhieuXuat.MatHang.MaMatHang = ((DataRowView)cboChonHang.SelectedItem).Row["MaMatHang"].ToString();
                                PhieuXuat.MatHang.TenMatHang = ((DataRowView)cboChonHang.SelectedItem).Row["TenMatHang"].ToString();
                                PhieuXuat.SoLuong = ChonMatHang[i].SoLuongTon;
                                PhieuXuat.MaPhieuNhap = ChonMatHang[i].MaPhieuNhap;
                                PhieuXuat.ThueVAT = double.Parse(((DataRowView)cboChonHang.SelectedItem).Row["ThueVAT"].ToString());
                                PhieuXuat.DonGia = double.Parse(txtSuaGia.Text);
                                PhieuXuat.ChietKhau = double.Parse(ChietKhau);
                                Double GiaMoi = double.Parse(txtSuaGia.Text);
                                Double ThanhTienChuaVATCT = ChonMatHang[i].SoLuongTon * GiaMoi;
                                Double TienThueVATCT = (ThanhTienChuaVATCT * PhieuXuat.ThueVAT) / 100;
                                Double ThanhTienChuaChietKhauCT = ThanhTienChuaVATCT + TienThueVATCT;
                                PhieuXuat.ThanhTien = ThanhTienChuaChietKhauCT - (ThanhTienChuaChietKhauCT * double.Parse(ChietKhau)) / 100;
                                CacCPX.Add(PhieuXuat);
                            }

                            grdvBanLe.Rows[DongDangSua].Cells["DonGia"].Value = txtSuaGia.Text;
                            grdvBanLe.Rows[DongDangSua].Cells["SoLuong"].Value = txtSoLuong.Text;
                            grdvBanLe.Rows[DongDangSua].Cells["ChietKhau"].Value = ChietKhau + "%";
                            //Tinh Tien chua co VAT
                            Double ThanhTienChuaVAT = int.Parse(txtSoLuong.Text) * double.Parse(txtSuaGia.Text.Trim());
                            //Tinh tien VAT
                            Double TienThueVAT = ThanhTienChuaVAT * (double.Parse(((DataRowView)cboChonHang.SelectedItem).Row["ThueVAT"].ToString()) / 100);
                            //Thanh tien da co thue VAT nhung chua co chiet khau
                            Double ThanhTien = ThanhTienChuaVAT + TienThueVAT;
                            //Thanh tien da co chiet khau
                            ThanhTien = ThanhTien - (ThanhTien * double.Parse(ChietKhau)) / 100;
                            grdvBanLe.Rows[DongDangSua].Cells["ThanhTien"].Value = clsSupport.CurrencyNumber(ThanhTien.ToString());
                            //Tinh tong tien
                            txtTongCong.Text = TinhTongTien().ToString();
                        }
                    }
                    else // sua 
                    {
                        ////int LuongTonConLai = int.Parse(grdvBanLe.Rows[grdvBanLe.CurrentRow.Index].Cells["LuongTonConLai"].Value.ToString());
                        ////if (LuongTonConLai == 0)
                        ////{
                        ////    int SoLuong = int.Parse(grdvBanLe.Rows[grdvBanLe.CurrentRow.Index].Cells["SoLuong"].Value.ToString());
                        ////    int SoLuongTon = int.Parse(((DataRowView)cboChonHang.SelectedItem).Row["SoLuongTon"].ToString());
                        ////    //Khoi phuc lai luong ton
                        ////    LuongTonConLai = SoLuong + SoLuongTon;
                        ////    grdvBanLe.Rows[grdvBanLe.CurrentRow.Index].Cells["LuongTonConLai"].Value = LuongTonConLai;

                        ////}
                        ////if (KiemTraLuongTonKhiSua() == true)
                        ////{
                        ////    int DongDangSua = grdvBanLe.CurrentRow.Index;
                        ////    int SoLuong = int.Parse(txtSoLuong.Text);
                        ////    if (int.Parse(grdvBanLe.Rows[DongDangSua].Cells["SoLuong"].Value.ToString()) != SoLuong)
                        ////    {
                        ////        string MaMatHang = grdvBanLe.Rows[DongDangSua].Cells["MaMatHang"].Value.ToString().Trim();
                        ////        List<clsChiTietPhieuNhapDTO> ChonMatHang = new clsPhieuNhapBUS().ChonMatHangNhapVoiGiaCao(MaMatHang, int.Parse(txtSoLuong.Text));
                        ////        if (ChonMatHang == null)
                        ////        {
                        ////            ChonMatHang = new   List<clsChiTietPhieuNhapDTO>();
                        ////        }
                        ////        //Khoi phuc so luong ton cac mat hang trong chi tiet phieu nhap
                        ////        for (int i = 0; i < CacMHDaBanCuaCTPX.Count; i++)
                        ////        {
                        ////            if (CacMHDaBanCuaCTPX[i].MatHang.MaMatHang.Trim() == MaMatHang)
                        ////            {
                        ////                Boolean Co = false;//Mat hang nay khong co trong phieu nhap
                                       
                        ////                    for (int j = 0; j < ChonMatHang.Count; j++)
                        ////                    {
                        ////                        if (CacMHDaBanCuaCTPX[i].MaPhieuNhap == ChonMatHang[j].MaPhieuNhap)
                        ////                        {
                        ////                            ChonMatHang[j].SoLuongTon += CacMHDaBanCuaCTPX[i].SoLuong;
                        ////                            Co = true;
                        ////                            break;
                        ////                        }
                        ////                    }

                        ////                if (Co == false)
                        ////                {
                        ////                    clsChiTietPhieuNhapDTO ctpn = new clsChiTietPhieuNhapDTO();
                        ////                    ctpn.MaPhieuNhap = CacMHDaBanCuaCTPX[i].MaPhieuNhap;
                        ////                    ctpn.MatHang.MaMatHang = CacMHDaBanCuaCTPX[i].MatHang.MaMatHang;
                        ////                    ctpn.MatHang.TenMatHang = CacMHDaBanCuaCTPX[i].MatHang.TenMatHang;
                        ////                    ctpn.SoLuongTon = CacMHDaBanCuaCTPX[i].SoLuong;
                        ////                    ChonMatHang.Add(ctpn);
                        ////                }
                        ////            }
                        ////        }
                        ////        // Xoa di tat cac cac chi tiet mat hang da co
                        ////        for (int i = 0; i < CacCPX.Count; i++)
                        ////        {
                        ////            if (CacCPX[i].MatHang.MaMatHang.Trim() == MaMatHang)
                        ////            {
                        ////                CacCPX.RemoveAt(i);
                        ////                i = -1;
                        ////            }
                        ////        }
                        ////        //Sap xep va chon cac mat hang theo so luong
                        ////        ChonMatHang = new clsPhieuNhapBUS().ChonMatHangNhapVoiGiaCao(SoLuong, ChonMatHang);
                        ////        // Them cac mat hang vao
                        ////        for (int i = 0; i < ChonMatHang.Count; i++)
                        ////        {
                        ////            clsChiTietPhieuXuatDTO PhieuXuat = new clsChiTietPhieuXuatDTO();
                        ////            PhieuXuat.MatHang.MaMatHang = ChonMatHang[i].MatHang.MaMatHang;
                        ////            PhieuXuat.MatHang.TenMatHang = ChonMatHang[i].MatHang.TenMatHang;
                        ////            PhieuXuat.SoLuong = ChonMatHang[i].SoLuongTon;
                        ////            PhieuXuat.MaPhieuNhap = ChonMatHang[i].MaPhieuNhap;
                        ////            PhieuXuat.ChietKhau = double.Parse(txtSuaChietKhau.Text.Replace("%", ""));
                        ////            PhieuXuat.ThueVAT = double.Parse(grdvBanLe.Rows[DongDangSua].Cells["ThueVAT"].Value.ToString().Replace("%", ""));
                        ////            PhieuXuat.DonGia = double.Parse(txtSuaGia.Text);
                        ////            Double ThanhTienCT = ChonMatHang[i].SoLuongTon * PhieuXuat.DonGia + (PhieuXuat.DonGia * (PhieuXuat.ThueVAT / 100) * ChonMatHang[i].SoLuongTon);
                        ////            PhieuXuat.ThanhTien = ThanhTienCT;
                        ////            CacCPX.Add(PhieuXuat);
                        ////        }
                        ////        // Cap nhat lai so luong mat hang can sua tren luoi
                        ////        grdvBanLe.Rows[grdvBanLe.CurrentRow.Index].Cells["SoLuong"].Value = int.Parse(txtSoLuong.Text);
                        ////        grdvBanLe.Rows[grdvBanLe.CurrentRow.Index].Cells["DonGia"].Value = double.Parse(txtSuaGia.Text);
                        ////        grdvBanLe.Rows[grdvBanLe.CurrentRow.Index].Cells["ChietKhau"].Value = txtSuaChietKhau.Text;
                        ////        double ThanhTien = int.Parse(txtSoLuong.Text) * double.Parse(txtSuaGia.Text);
                        ////        string ThueVAT = grdvBanLe.Rows[grdvBanLe.CurrentRow.Index].Cells["ThueVAT"].Value.ToString();
                        ////        if (ThueVAT.IndexOf("%") != -1)
                        ////        {
                        ////            ThueVAT = ThueVAT.Replace("%", "");
                        ////        }
                        ////        ThanhTien = ThanhTien + ThanhTien * double.Parse(ThueVAT) / 100;
                        ////        string ChietKhau = "";
                        ////        if (txtSuaChietKhau.Text.IndexOf("%") != -1)
                        ////        {
                        ////            ChietKhau = txtSuaChietKhau.Text.Replace("%", "");
                        ////        }
                        ////        else
                        ////        {
                        ////            ChietKhau = txtSuaChietKhau.Text;
                        ////        }
                        ////        ThanhTien = ThanhTien - (ThanhTien * double.Parse(ChietKhau)) / 100;
                        ////        grdvBanLe.Rows[grdvBanLe.CurrentRow.Index].Cells["ThanhTien"].Value = clsSupport.CurrencyNumber(ThanhTien.ToString());
                       /////
                        ////    }
                            //Tinh tong tien
                        ////    txtTongCong.Text = TinhTongTien().ToString();
                        ////}
                        ////else
                        ////{
                        ////    txtSoLuong.Focus();
                        ////}
                        //Tinh tong tien
                            txtTongCong.Text = TinhTongTien().ToString();
                    }

                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool KiemTraLuongTonKhiSua()
        {
            int SoLuongTonConLai = int.Parse(grdvBanLe.Rows[grdvBanLe.CurrentRow.Index].Cells["LuongTonConLai"].Value.ToString());
            int SoLuongNhap = int.Parse(txtSoLuong.Text);
            int LuongMin = int.Parse(((DataRowView)cboChonHang.SelectedItem).Row["LuongMin"].ToString());
            if (SoLuongNhap > SoLuongTonConLai)
            {
                MessageBox.Show("Xin vui lòng nhập vào số lượng bán <= số lượng tồn: " + SoLuongTonConLai.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (SoLuongTonConLai - SoLuongNhap <= LuongMin)
                {
                    MessageBox.Show("Hàng đã đạt ngưỡng Min,lưu ý nhập bổ sung hàng thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return true;
            }
            return false;
        }

        //private bool KiemTraLuongTon(string MaMatHang)
        //{
        //    int SoLuongTon = int.Parse(((DataRowView)cboChonHang.SelectedItem).Row["SoLuongTon"].ToString());
        //    //Cong don so luong mat hang ton
        //    for (int i = 0; i < CacMHDaBanCuaCTPX.Count; i++)
        //    {
        //        if (CacMHDaBanCuaCTPX[i].MatHang.MaMatHang.Trim() == MaMatHang)
        //        {
        //            SoLuongTon += CacMHDaBanCuaCTPX[i].SoLuong;
        //        }
        //    }
        //    int LuongMin = int.Parse(((DataRowView)cboChonHang.SelectedItem).Row["LuongMin"].ToString());
        //    int SoLuongNhap = int.Parse(txtSoLuong.Text);
        //    if (SoLuongTon == 0)
        //    {
        //        MessageBox.Show("Mặt hàng " + ((DataRowView)cboChonHang.SelectedItem).Row["TenMatHang"].ToString() + " đã hết hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //    else
        //    {
        //        if (SoLuongNhap > SoLuongTon)
        //        {
        //            MessageBox.Show("Xin vui lòng nhập vào số lượng bán <= số lượng tồn: " + SoLuongTon.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //        else
        //        {
        //            if (SoLuongTon - SoLuongNhap <= LuongMin)
        //            {
        //                MessageBox.Show("Hàng đã đạt ngưỡng Min,lưu ý nhập bổ sung hàng thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            return true;
        //        }
        //    }
        //    return false;
        //}
         //Xóa thông tin hàng đã nhập
        private void XoaHangDaNhap()
        {
            try
            {
                if (grdvBanLe.CurrentRow != null)
                {
                    DialogResult result = MessageBox.Show("Bạn có thật sự muốn xóa mặt hàng này không?", "Xac nhan", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        for (int i = 0; i < CacCPX.Count; i++)
                        {
                            if (CacCPX[i].MatHang.MaMatHang.Trim() == grdvBanLe.Rows[grdvBanLe.CurrentRow.Index].Cells["MaMatHang"].Value.ToString())
                            {
                                CacCPX.RemoveAt(i);
                                i = -1;
                            }
                        }
                        grdvBanLe.Rows.RemoveAt(grdvBanLe.CurrentRow.Index);
                        CapNhatSTT();
                        //Tinh tong tien
                        txtTongCong.Text = TinhTongTien().ToString();
                    }
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Cập nhật lại số thứ tự
        private void CapNhatSTT()
        {
            for (int i = 0; i < grdvBanLe.Rows.Count; i++)
            {
                int stt = (i + 1);
                grdvBanLe.Rows[i].Cells["STT"].Value = stt.ToString();
            }
        }

        private void LuuPhieuXuat(ref String Loi)
        {
            Loi = "";
            try
            {
                Loi = "Xin vui lòng kiểm tra lại dữ liệu nhập";
                clsPhieuXuatBanLeDTO PhieuXuat = KhoiTaoPhieuXuat(ref Loi);
                if (PhieuXuat != null)
                {
                    Loi = "Lỗi kết nối cơ sở dữ liệu";
                    if (ThaoTac == "Them")
                    {
                        if (PhieuXuatBanLeBus.Them(PhieuXuat) != -1)
                        {
                            //MessageBox.Show("Lưu Phiếu xuất " + PhieuXuat.MaPhieuXuat + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult result = MessageBox.Show("Lưu phiếu xuất " + txtMaPhieuXuat.Text + " thành công! Bạn có muốn In phiếu xuất này không?", "Xac nhan", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                //In hoa don ban le
                                In();
                            }
                            LamTuoi();
                            txtMaPhieuXuat.Text = PhieuXuatBanLeBus.LayMaPhieuXuatMoi();
                            Loi = "Thành Công";
                            HienCacNutNhan(false);
                        }
                        else
                        {
                            MessageBox.Show("Lưu phiếu xuất không thành công, nguyên nhân do phiếu xuất này đã tồn tại rồi. Xin vui lòng nhập phiếu xuất khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else//Thao tac cap nhat lai phieu xuất
                    {
                        if (PhieuXuatBanLeBus.Sua(PhieuXuat) != -1)
                        {
                            //MessageBox.Show("Lưu phiếu xuất " + PhieuXuat.MaPhieuXuat + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult result = MessageBox.Show("Lưu phiếu xuất " + txtMaPhieuXuat.Text + " thành công! Bạn có muốn In phiếu xuất này không?", "Xac nhan", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                //In hoa don ban le
                                In();
                            }
                            LamTuoi();
                            txtMaPhieuXuat.Text = PhieuXuatBanLeBus.LayMaPhieuXuatMoi();
                            Loi = "Thành Công";
                        }
                        else
                        {
                            MessageBox.Show("Lưu phiếu xuất không thành công, nguyên nhân do phiếu xuất này đã tồn tại rồi. Xin vui lòng nhập phiếu xuất khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Loi, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Loi, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Khởi tạo Phiếu xuất
        private clsPhieuXuatBanLeDTO KhoiTaoPhieuXuat(ref string Loi)
        {
            clsPhieuXuatBanLeDTO PhieuXuat = new clsPhieuXuatBanLeDTO();
            if (grdvBanLe.Rows.Count == 0)
            {
                Loi = "Xin vui lòng nhập mặt hàng cần bán!";
                cboChonHang.Focus();
                return null;
            }
            if (txtMaPhieuXuat.Text.Length >= 4)
            {
                if (txtMaPhieuXuat.Text.Substring(0, 3) == "PXK")
                {
                    int SoPhieuXuat = -1;
                    if (int.TryParse(txtMaPhieuXuat.Text.Substring(3, (txtMaPhieuXuat.Text.Length - 3)), out SoPhieuXuat) == true && SoPhieuXuat > 0)
                    {
                        PhieuXuat.MaPhieuXuat = txtMaPhieuXuat.Text;
                    }
                    else
                    {
                        Loi = "Xin vui lòng nhập Phiếu xuất có dạng như sau: PXK + Số thứ tự (Số nguyên dương) vd: PXK1, PXK2,... !";
                        txtMaPhieuXuat.Focus();
                        return null;
                    }
                }
                else
                {
                    Loi = "Xin vui lòng nhập Phiếu xuất có dạng như sau: PXK + Số thứ tự (Số nguyên dương) vd: PXK1, PXK2,... !";
                    txtMaPhieuXuat.Focus();
                    return null;
                }
            }
            Loi = "Xin vui lòng chọn nhân viên bán hàng!";
            if (cboNhanVienBH.SelectedItem == null || ((DataRowView)cboNhanVienBH.SelectedItem).Row["MaNhanVien"].ToString().Trim() == "")
            {
                cboNhanVienBH.Focus();
                return null;
            }
            PhieuXuat.NhanVien.MaNhanVien = ((DataRowView)cboNhanVienBH.SelectedItem).Row["MaNhanVien"].ToString().Trim();
            Double TongCong = double.Parse(txtTongCong.Text);
            Loi = "Xin vui lòng nhập vào số tiền khách đưa phải >= " + clsSupport.CurrencyNumber(TongCong.ToString());
            double TienKhachDua = double.Parse(txtKhachDua.Text);
            if (TienKhachDua < TongCong)
            {
                txtKhachDua.Focus();
                return null;
            }
            PhieuXuat.NgayXuat = dtpNgayXuat.Value;
            PhieuXuat.KhachHang = txtKhachHang.Text;
            PhieuXuat.TongTien = double.Parse(txtTongCong.Text);
            PhieuXuat.DaTra = PhieuXuat.TongTien;
            Loi = "Xin vui lòng kiểm tra lại các mặt hàng đã chọn!";
            PhieuXuat.DS_ChiTietPhieuXuat = KhoiTaoChiTietPhieuXuat(PhieuXuat.MaPhieuXuat);
            return PhieuXuat;
        }

        //Khởi tạo chi tiết phiếu xuất bán lẻ
        private List<clsChiTietPhieuXuatDTO> KhoiTaoChiTietPhieuXuat(string MaPhieuXuat)
        {
            for (int i = 0; i < CacCPX.Count; i++)
            {
                CacCPX[i].MaPhieuXuat = MaPhieuXuat;
            }
            return CacCPX;
        }

        //Nhân viên
        private void KhoiTaoComboNhanVien()
        {
            DataTable BangNhanVien = new clsNhanVienBUS().LayBang();
            //Đưa dòng thứ 0 vào vị trí cuối cùng và sau đó gán lại dòng thứ ko là tất cả
            cboNhanVienBH.DataSource = BangNhanVien;
            cboNhanVienBH.DisplayMember = "TenNhanVien";
            cboNhanVienBH.ValueMember = "MaNhanVien";
            cboNhanVienBH.SelectedIndex = -1;
            cboNhanVienBH.Text = "< Chọn người bán >";
            if (BangNhanVien.Rows.Count == 0)
            {
                cboNhanVienBH.Text = "< Không có NVBH! >";
            }
            if (clsUser.MaNhanVien != "")
            {
                cboNhanVienBH.SelectedValue = clsUser.MaNhanVien;
            }
        }

        //Lấy danh sách mặt hàng
        private void KhoiTaoComboChonHang()
        {
            DataTable BangMatHang = new clsMatHangBUS().LayBangHangBan();
            cboChonHang.DataSource = BangMatHang;
            cboChonHang.DisplayMember = "TenMatHang";
            cboChonHang.ValueMember = "MaMatHang";
            if (BangMatHang.Rows.Count == 0)
            {
                cboChonHang.SelectedIndex = -1;
                cboChonHang.Text = "< Không có mặt hàng! >";
            }
           else
            {
                cboChonHang.SelectedIndex = -1;
                cboChonHang.Text = "< Chọn mặt hàng! >";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                NhapThemHang();
                cboChonHang.Focus();
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            SuaHangDaNhap();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            XoaHangDaNhap();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string Loi = "";
            LuuPhieuXuat(ref Loi);
        }

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdvBanLe.RowCount > 0)
                {
                    //txtKhachDua.TabStop = true;
                    HienCacNutNhan(true);
                }
                else
                {
                    MessageBox.Show("Xin vui lòng nhập vào mặt hàng cần bán trước khi tính tiền!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Tính tiền không thành công!Xin vui lòng nhập vào mặt hàng cần bán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void HienCacNutNhan(Boolean isbool)
        {
            btnLuu.Enabled = isbool;
            btnInRa.Enabled = isbool;
            txtKhachDua.ReadOnly = !isbool;
            if (isbool == true)
            {
                txtKhachDua.Focus();
            }
            else
            {
                cboChonHang.Focus();
            }
        }

        private void txtKhachDua_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double TongCong = 0;
                try
                {
                    string Loi = "";
                    LuuPhieuXuat(ref Loi);
                }
                catch(Exception Loi)
                {
                    MessageBox.Show("Xin vui lòng nhập vào số tiền khách đưa phải >= " + clsSupport.CurrencyNumber(TongCong.ToString()), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtKhachDua_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double TienKhachDua = double.Parse(txtKhachDua.Text);
                double TongCong = double.Parse(txtTongCong.Text);
                double ThoiLai = TienKhachDua - TongCong;
                if (ThoiLai > 0)
                {
                    txtThoiLai.Text = ThoiLai.ToString();
                }
                else
                {
                    txtThoiLai.Text = "0";
                }
               
            }
            catch (Exception Loi)
            {
            }
        }

        private void txtMaPhieuXuat_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    XemPhieuXuatTheoMaPhieuXuat(txtMaPhieuXuat.Text.Trim());
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử nhập lại Phiếu xuất khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            KhoiTaoMoi();
        }

        private void KhoiTaoMoi()
        {
            try
            {
                LamTuoi();
                AnCacVungNhapLieu(true);
                dtpNgayXuat.Value = DateTime.Now;
                txtMaPhieuXuat.Text = PhieuXuatBanLeBus.LayMaPhieuXuatMoi();
                HienCacNutNhan(false);
                cboChonHang.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AnCacVungNhapLieu(Boolean isAn)
        {
            cboChonHang.Enabled = isAn;
            txtKhachHang.Enabled = isAn;
            cboNhanVienBH.Enabled = isAn;
            txtSuaGia.Enabled = isAn;
            txtSoLuong.Enabled = isAn;
            txtSuaChietKhau.Enabled = isAn;
            btnTimHang.Enabled = isAn;
            btnThem.Enabled = isAn;
            BtnSua.Enabled = isAn;
            btnLuu.Enabled = isAn;
            btnInRa.Enabled = isAn;
            btnXoa.Enabled = isAn;
            dtpNgayXuat.Enabled = isAn;
            btnTinhTien.Enabled = isAn;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Boolean KiemTraTruocKhiThoat()
        {
            if (grdvBanLe.Rows.Count > 0 && btnLuu.Enabled == true)
            {
                DialogResult result = MessageBox.Show("Bạn có muốn lưu lại phiếu xuất này không?", "Xac nhan", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    string Loi = "";
                    LuuPhieuXuat(ref Loi);
                    if (Loi == "Thành Công")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (result == DialogResult.Cancel)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        private void frmBanLe_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = KiemTraTruocKhiThoat();
        }

        private void btnTimHang_Click(object sender, EventArgs e)
        {
            frmMatHangMua F = new frmMatHangMua("ChonHangNhap");
            F.ShowDialog();
            KhoiTaoComboChonHang();
            if (F.MatHangDTO != null)
            {
                clsMatHangDTO MatHangDTO = F.MatHangDTO;
                cboChonHang.SelectedValue = MatHangDTO.MaMatHang;
                cboChonHang.Focus();
            }
        }

        private void txtSuaChietKhau_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = clsSupport.KiemTraPhanTram(txtSuaChietKhau.Text, "Chiết khấu");
        }

        private void grdvBanLe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    cboChonHang.SelectedValue = grdvBanLe.Rows[e.RowIndex].Cells["MaMatHang"].Value.ToString().Trim();
                    txtSuaGia.Text = grdvBanLe.Rows[e.RowIndex].Cells["DonGia"].Value.ToString().Trim();
                    txtSoLuong.Text = grdvBanLe.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString().Trim();
                    txtSuaChietKhau.Text = grdvBanLe.Rows[e.RowIndex].Cells["ChietKhau"].Value.ToString().Trim();
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!");
            }
        }

        private void In()
        {
            try
            {
                if ( grdvBanLe.RowCount == 0)
                {
                    MessageBox.Show("In không thành công vì không có thông tin về phiếu xuất bán hàng! " + txtMaPhieuXuat.Text.Trim(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptHoaDonBanLe hoaDonBanLe = new rptHoaDonBanLe();
                hoaDonBanLe.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                DataTable bang = PhieuXuatBanLeBus.ReportHoaDonBanLe(txtMaPhieuXuat.Text.Trim());
                DataTable bangCT = PhieuXuatBanLeBus.ReportCTHoaDonBanLe(txtMaPhieuXuat.Text.Trim());
                if (bangCT.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();
                    DataSet ds = new DataSet();
                    ds.Tables.Add(bang);
                    ds.Tables.Add(bangCT);
                    ds.Tables.Add(CongTy);
                    hoaDonBanLe.SetDataSource(ds);
                    hoaDonBanLe.SetParameterValue("@MaPhieuXuat", txtMaPhieuXuat.Text.Trim());
                    hoaDonBanLe.SetParameterValue("@KhachDua", decimal.Parse(txtKhachDua.Text.Trim()));
                    hoaDonBanLe.SetParameterValue("@ThoiLai", decimal.Parse(txtThoiLai.Text.Trim()));

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = hoaDonBanLe;
                    dlgHienThi.ShowDialog();
                }
                else
                    MessageBox.Show("In không thành công vì không có thông tin về phiếu xuất " + txtMaPhieuXuat.Text.Trim(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInRa_Click(object sender, EventArgs e)
        {
            In();
        }

        private void cboChonHang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                txtSuaGia.Text = ((DataRowView)cboChonHang.SelectedItem).Row["GiaBanLe"].ToString();
                //txtThueVAT.Text = ((DataRowView)cboChonHang.SelectedItem).Row["ThueVAT"].ToString()+"%";
                txtSuaChietKhau.Text = "0%";
                txtSoLuong.Text = "1";
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}