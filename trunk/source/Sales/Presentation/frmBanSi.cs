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
    public partial class frmBanSi : frmTemplete
    {
        #region Thuộc tính
        private clsPhieuXuatBanSiBUS PhieuXuatBanSiBus = new clsPhieuXuatBanSiBUS();
        private clsPhieuXuatBanLeBUS PhieuXuatBanLeBus = new clsPhieuXuatBanLeBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        public string ThaoTac = "Them";
        public string MaPhieuXuatCanXem = null;
        string LienKet = "";
        private List<clsChiTietPhieuXuatDTO> CacCPX;
        private List<clsChiTietPhieuXuatDTO> CacMHDaBanCuaCTPX;
        bool IsEdit = false;
        #endregion

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
            //Thêm mặt hàng cần bán
            if (keyData == Keys.F3)
            {
                try
                {
                    NhapThemHang();
                }
                catch (Exception Loi)
                {
                    MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!");
                }

            }

            //Sửa thông tin hàng đã bán
            if (keyData == Keys.F5)
            {
                SuaHangDaNhap();
            }

            //Xóa thông tin hàng đã bán
            if (keyData == Keys.F6)
            {
                XoaHangDaNhap();
            }
            //Lưu thông tin phiếu xuất
            if (keyData == (Keys.Control | Keys.L))
            {
                string Loi = "";
                LuuPhieuXuat(ref Loi);
            }

            //Lưu và in thông tin phiếu bán
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

         #region Sự kiện màn hình Bán Sỉ

        public frmBanSi()
        {
            InitializeComponent();
        }

        public frmBanSi(string _MaPhieuXuat,bool _IsEdit, string _LienKet)
        {
            MaPhieuXuatCanXem = _MaPhieuXuat;
            LienKet = _LienKet;
            IsEdit = _IsEdit;
            InitializeComponent();
        }

        private void frmBanSi_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboKhachHang();
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
                    if (LienKet == "QuanLyDonHangDaBan" && IsEdit == true)
                    {
                        AnCacVungNhapLieu(true);
                        
                    }
                    else
                    {
                        AnCacVungNhapLieu(false);
                    }
                    btnInRa.Enabled = true;
                    cboNhanVienBH.Enabled = false;
                }
                else
                {
                    txtMaPhieuXuat.Text = PhieuXuatBanSiBus.LayMaPhieuXuatMoi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

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
                cboChonHang.Text = "< Chọn mặt hàng >";
            }
        }



        #region Khách Hàng
        //Lấy danh sách Khách Hàng
        private void KhoiTaoComboKhachHang()
        {

            DataTable BangKhachHang = new clsKhachHangBUS().LayBang();
            //Đưa dòng thứ 0 vào vị trí cuối cùng và sau đó gán lại dòng thứ ko là tất cả
            DataRow DongTam = BangKhachHang.NewRow();
            cboKhachHang.DataSource = BangKhachHang;
            cboKhachHang.DisplayMember = "TenKhachHang";
            cboKhachHang.ValueMember = "MaKhachHang";
            if (BangKhachHang.Rows.Count == 0)
            {
                cboKhachHang.SelectedIndex = -1;
                cboKhachHang.Text = "< Không có Khách hàng! >";
            }
            else
            {
                cboKhachHang.SelectedIndex = -1;
                cboKhachHang.Text = "< Chọn Khách hàng! >";
            }
        }
        #endregion

        private void XemPhieuXuatTheoMaPhieuXuat(string MaPhieuXuat)
        {
            LamTuoi();
            clsPhieuXuatBanSiDTO PhieuXuat = PhieuXuatBanSiBus.LayThongTin(MaPhieuXuat);
            if (PhieuXuat != null)
            {
                if (PhieuXuat.DaTra!= 0)
                {
                    AnCacVungNhapLieu(false);
                }
                else
                {
                    ThaoTac = "CapNhat";
                    AnCacVungNhapLieu(true);
                    clsPhieuXuatBanSiDTO PhieuXuatXuLy = PhieuXuatBanSiBus.LayThongTinTheoPhieuNhap(MaPhieuXuat);
                    CacCPX = PhieuXuatXuLy.DS_ChiTietPhieuXuat;
                    for (int i = 0;i< PhieuXuatXuLy.DS_ChiTietPhieuXuat.Count; i++)
                    {
                        clsChiTietPhieuXuatDTO CTPX =PhieuXuatXuLy.DS_ChiTietPhieuXuat[i];
                        CacMHDaBanCuaCTPX.Add(CTPX); 
                    }
                }
                cboKhachHang.SelectedValue = PhieuXuat.KhachHang.MaKhachHang;
                txtDiaChi.Text = ((DataRowView)cboKhachHang.SelectedItem).Row["DiaChi"].ToString();
                txtDienThoai.Text = ((DataRowView)cboKhachHang.SelectedItem).Row["DienThoai"].ToString();
                dtpNgayXuat.Value = PhieuXuat.NgayXuat;
                cboNhanVienBH.SelectedValue = PhieuXuat.NhanVien.MaNhanVien;
                for (int i = 0; i < PhieuXuat.DS_ChiTietPhieuXuat.Count; i++)
                {
                    object[] Dong = new object[10];
                    int STT = (grdvBanSi.RowCount + 1);
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
                    grdvBanSi.Rows.Add(Dong);
                }
                //Tinh tien hang
                txtTienHang.Text = TinhTongTienHang().ToString();
                //Tinh tien thueVAT
                txtThueVAT.Text = TinhTongTienThue().ToString();
                //Tinh tien chiet khau
                txtChietKhau.Text = TinhTongTienChietKhau().ToString();
                //Tinh tong tien
                txtTongCong.Text = TinhTongTien().ToString();
                if (PhieuXuat.NhanVien.MaNhanVien.Trim() == clsUser.MaNhanVien.Trim())
                {
                    AnCacVungNhapLieu(true);
                }
                else
                {
                    AnCacVungNhapLieu(false);
                }
                cboNhanVienBH.Enabled = false;
            }
            else
            {
                ThaoTac = "Them";
                clsPhieuXuatBanLeDTO PXBanLe = PhieuXuatBanLeBus.LayThongTin(MaPhieuXuat);
                if (PXBanLe != null)
                {
                    MessageBox.Show("Không có phiếu xuất bán sỉ " + txtMaPhieuXuat.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                }
                            }
                            else
                            {
                                Loi = "Xin vui lòng nhập Phiếu xuất có dạng như sau: PXK + Số thứ tự (Số nguyên dương) vd: PXK1, PXK2,... !";
                                MessageBox.Show(Loi, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtMaPhieuXuat.Focus();
                            }
                        }
                        else
                        {
                            Loi = "Xin vui lòng nhập Phiếu xuất có dạng như sau: PXK + Số thứ tự (Số nguyên dương) vd: PXK1, PXK2,... !";
                            MessageBox.Show(Loi, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtMaPhieuXuat.Focus();
                        }
                    }
                }            
            }
        }

        private void LamTuoi()
        {
            try
            {
                cboKhachHang.SelectedIndex = -1;
                cboKhachHang.Text = "< Chọn Khách hàng >";
                txtDienThoai.Text = "";
                txtDiaChi.Text = "";
                txtTienHang.Text = "";
                txtThueVAT.Text = "0";
                txtChietKhau.Text = "";
                txtTongCong.Text = "0";
                txtSoLuong.Text = "1";
                txtSuaChietKhau.Text = "0%";
                txtSuaGia.Text = "0";
                dtpNgayXuat.Value = DateTime.Now;
                cboNhanVienBH.SelectedValue = clsUser.MaNhanVien;
                cboNhanVienBH.Enabled = true;
                cboChonHang.SelectedIndex = -1;
                cboChonHang.Text = "< Chọn mặt hàng >";
                grdvBanSi.Rows.Clear();
                CacCPX = new List<clsChiTietPhieuXuatDTO>();
                CacMHDaBanCuaCTPX = new List<clsChiTietPhieuXuatDTO>();
                KhoiTaoComboChonHang();
                cboChonHang.Focus();
                ThaoTac = "Them";
            }
            catch (Exception Loi)
            {

            }
        }

        private void AnCacVungNhapLieu(Boolean isAn)
        {
            cboChonHang.Enabled = isAn;
            cboKhachHang.Enabled = isAn;
            btnThemKhachHang.Enabled = isAn;
            txtDiaChi.Enabled = isAn;
            txtDienThoai.Enabled = isAn;
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
        }

        //Tính tiền
        #region Tính tiền
        //Tính tiền hàng
        private double TinhTongTienHang()
        {
            double TienHang = 0;
            for (int i = 0; i < grdvBanSi.Rows.Count; i++)
            {
                double DonGia = double.Parse(grdvBanSi.Rows[i].Cells["DonGia"].Value.ToString().Trim());
                double SoLuong = double.Parse(grdvBanSi.Rows[i].Cells["SoLuong"].Value.ToString().Trim());
                TienHang += DonGia * SoLuong;
            }
            return TienHang;
        }

        //Tính tiền Thuế
        private double TinhTongTienThue()
        {
            double TienThue = 0;
            for (int i = 0; i < grdvBanSi.Rows.Count; i++)
            {
                double DonGia = double.Parse(grdvBanSi.Rows[i].Cells["DonGia"].Value.ToString().Trim());
                double SoLuong = double.Parse(grdvBanSi.Rows[i].Cells["SoLuong"].Value.ToString().Trim());
                string ThueVAT = "";
                if (grdvBanSi.Rows[i].Cells["ThueVAT"].Value.ToString().IndexOf("%") != -1)
                {
                    ThueVAT = grdvBanSi.Rows[i].Cells["ThueVAT"].Value.ToString().Replace("%", "");
                }
                else
                {
                    ThueVAT = grdvBanSi.Rows[i].Cells["ThueVAT"].Value.ToString();
                }
                TienThue += (DonGia * SoLuong * double.Parse(ThueVAT)) / 100;
            }
            return TienThue;
        }

        //Tính tiền chiết khấu
        private double TinhTongTienChietKhau()
        {
            double TienChietKhau = 0;
            for (int i = 0; i < grdvBanSi.Rows.Count; i++)
            {
                double DonGia = double.Parse(grdvBanSi.Rows[i].Cells["DonGia"].Value.ToString().Trim());
                double SoLuong = double.Parse(grdvBanSi.Rows[i].Cells["SoLuong"].Value.ToString().Trim());
                string ThueVAT = "";
                if (grdvBanSi.Rows[i].Cells["ThueVAT"].Value.ToString().IndexOf("%") != -1)
                {
                    ThueVAT = grdvBanSi.Rows[i].Cells["ThueVAT"].Value.ToString().Replace("%", "");
                }
                else
                {
                    ThueVAT = grdvBanSi.Rows[i].Cells["ThueVAT"].Value.ToString();
                }
                string ChietKhau = "";
                if (grdvBanSi.Rows[i].Cells["ChietKhau"].Value.ToString().IndexOf("%") != -1)
                {
                    ChietKhau = grdvBanSi.Rows[i].Cells["ChietKhau"].Value.ToString().Replace("%", "");
                }
                else
                {
                    ChietKhau = grdvBanSi.Rows[i].Cells["ChietKhau"].Value.ToString();
                }
                double ThanhTienCoThue = (SoLuong * DonGia) + ((SoLuong * DonGia) * double.Parse(ThueVAT) / 100);

                TienChietKhau += (ThanhTienCoThue * double.Parse(ChietKhau)) / 100;
            }
            return TienChietKhau;
        }

        //Tính tổng tiền 
        private double TinhTongTien()
        {
            double TongTien = 0;
            for (int i = 0; i < grdvBanSi.Rows.Count; i++)
            {
                double ThanhTien = double.Parse(grdvBanSi.Rows[i].Cells["ThanhTien"].Value.ToString().Trim());
                TongTien += ThanhTien;
            }
            return TongTien;
        }
        #endregion


        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                NhapThemHang();
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng cần bán!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Thêm thông tin mặt hàng
        /// </summary>
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
                                    ctpn.ChietKhau = CacMHDaBanCuaCTPX[i].ChietKhau;
                                    ctpn.ThueVAT = CacMHDaBanCuaCTPX[i].ThueVAT;
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
                    int STT = (grdvBanSi.RowCount + 1);
                    Dong[0] = STT.ToString();
                    Dong[1] = ((DataRowView)cboChonHang.SelectedItem).Row["MaMatHang"].ToString();
                    Dong[2] = ((DataRowView)cboChonHang.SelectedItem).Row["TenMatHang"].ToString();
                    Dong[3] = ((DataRowView)cboChonHang.SelectedItem).Row["DonViTinh"].ToString();
                    Dong[4] = txtSoLuong.Text.Trim();
                    Dong[5] = txtSuaGia.Text.Trim();
                    Dong[6] = ChietKhau + "%";
                    Dong[7] = ((DataRowView)cboChonHang.SelectedItem).Row["ThueVAT"].ToString() + "%";
                    Double ThanhTienChuaVAT = int.Parse(txtSoLuong.Text) * double.Parse(txtSuaGia.Text.Trim());
                    Double TienThueVAT = ThanhTienChuaVAT * (double.Parse(((DataRowView)cboChonHang.SelectedItem).Row["ThueVAT"].ToString()) / 100);
                    //Thanh tien da co thue VAT nhung chua co chiet khau
                    Double ThanhTien = ThanhTienChuaVAT + TienThueVAT;

                    //Thanh tien da co chiet khau
                    ThanhTien = ThanhTien - (ThanhTien * double.Parse(ChietKhau)) / 100;
                    Dong[8] = clsSupport.CurrencyNumber(ThanhTien.ToString());
                    int SoLuongTon = int.Parse(((DataRowView)cboChonHang.SelectedItem).Row["SoLuongTon"].ToString());
                    int SoLuongNhap = int.Parse(txtSoLuong.Text);
                    int LuongTonConLai = SoLuongTon - SoLuongNhap;
                    Dong[9] = LuongTonConLai.ToString();
                    grdvBanSi.Rows.Add(Dong);
                    //Tinh tien hang
                    txtTienHang.Text = TinhTongTienHang().ToString();
                    //Tinh tien thueVAT
                    txtThueVAT.Text = TinhTongTienThue().ToString();
                    //Tinh tien chiet khau
                    txtChietKhau.Text = TinhTongTienChietKhau().ToString();
                    //Tinh tong tien
                    txtTongCong.Text = TinhTongTien().ToString();
                }
            }
        }

        //Kiểm tra mặt hàng này bán chưa
        private Boolean KiemTraMatHang(string MaMatHang)
        {
            Boolean KetQua = true;
            for (int i = 0; i < grdvBanSi.Rows.Count; i++)
            {
                if (grdvBanSi.Rows[i].Cells["MaMatHang"].Value.ToString().Trim() == MaMatHang)
                {
                    KetQua = false;
                    break;
                }
            }
            return KetQua;
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
            int LuongMin = int.Parse(((DataRowView)cboChonHang.SelectedItem).Row["LuongMin"].ToString());
            int SoLuongNhap = int.Parse(txtSoLuong.Text);
            if (SoLuongTon == 0)
            {
                MessageBox.Show("Mặt hàng " + ((DataRowView)cboChonHang.SelectedItem).Row["TenMatHang"].ToString() + " đã hết hàng", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (SoLuongNhap > SoLuongTon)
                {
                    MessageBox.Show("Xin vui lòng nhập vào số lượng bán <= số lượng tồn: " + SoLuongTon.ToString(), "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (SoLuongTon - SoLuongNhap <= LuongMin)
                    {
                        MessageBox.Show("Hàng đã đạt ngưỡng Min,lưu ý nhập bổ sung hàng thêm!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return true;
                }
            }
            return false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string Loi = "";
            LuuPhieuXuat(ref Loi);
        }

        private void LuuPhieuXuat(ref String Loi)
        {
            Loi = "";
            try
            {
                Loi = "Xin vui lòng kiểm tra lại dữ liệu nhập";
                clsPhieuXuatBanSiDTO PhieuXuat = KhoiTaoPhieuXuat(ref Loi);
                if (PhieuXuat != null)
                {
                    Loi = "Lỗi kết nối cơ sở dữ liệu";
                    if (ThaoTac == "Them")
                    {
                        if (PhieuXuatBanSiBus.Them(PhieuXuat) != -1)
                        {
                            //MessageBox.Show("Lưu Phiếu xuất " + PhieuXuat.MaPhieuXuat + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult result = MessageBox.Show("Lưu phiếu xuất " + txtMaPhieuXuat.Text + " thành công! Bạn có muốn In phiếu xuất này không?", "Xac nhan", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                // In hoa don ban si
                                In();
                            }
                            LamTuoi();
                            txtMaPhieuXuat.Text = PhieuXuatBanSiBus.LayMaPhieuXuatMoi();
                            Loi = "Thành Công";
                           
                        }
                        else
                        {
                            MessageBox.Show("Lưu phiếu xuất không thành công, nguyên nhân do phiếu xuất này đã tồn tại rồi. Xin vui lòng nhập phiếu xuất khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else//Thao tac cap nhat lai phieu xuất
                    {
                        if (PhieuXuatBanSiBus.Sua(PhieuXuat) != -1)
                        {
                            //MessageBox.Show("Lưu phiếu xuất " + PhieuXuat.MaPhieuXuat + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult result = MessageBox.Show("Lưu phiếu xuất " + txtMaPhieuXuat.Text + " thành công! Bạn có muốn In phiếu xuất này không?", "Xac nhan", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                // In hoa don ban si
                                In();
                            }
                            LamTuoi();
                            txtMaPhieuXuat.Text = PhieuXuatBanSiBus.LayMaPhieuXuatMoi();
                            Loi = "Thành Công";
                            ThaoTac ="Them";
                            if (LienKet.Trim() == "QuanLyDonHangDaBan")
                            {
                                DongCuaSo();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lưu phiếu xuất không thành công, nguyên nhân do phiếu xuất này đã tồn tại rồi. Xin vui lòng nhập phiếu xuất khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Loi, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Loi, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Khởi tạo Phiếu xuất
        private clsPhieuXuatBanSiDTO KhoiTaoPhieuXuat(ref string Loi)
        {
            clsPhieuXuatBanSiDTO PhieuXuat = new clsPhieuXuatBanSiDTO();
            if (grdvBanSi.Rows.Count == 0)
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
            Loi = "Xin vui lòng chọn khách hàng!";
            if (cboKhachHang.SelectedItem == null || ((DataRowView)cboKhachHang.SelectedItem).Row["MaKhachHang"].ToString().Trim() == "")
            {
                cboKhachHang.Focus();
                return null;
            }
            PhieuXuat.KhachHang.MaKhachHang = ((DataRowView)cboKhachHang.SelectedItem).Row["MaKhachHang"].ToString().Trim();

            Loi = "Xin vui lòng chọn nhân viên bán hàng!";
            if (cboNhanVienBH.SelectedItem == null || ((DataRowView)cboNhanVienBH.SelectedItem).Row["MaNhanVien"].ToString().Trim() == "")
            {
                cboNhanVienBH.Focus();
                return null;
            }
            PhieuXuat.NhanVien.MaNhanVien = ((DataRowView)cboNhanVienBH.SelectedItem).Row["MaNhanVien"].ToString().Trim();
            Double TongCong = double.Parse(txtTongCong.Text);        
            PhieuXuat.NgayXuat = dtpNgayXuat.Value;
            //PhieuXuat.KhachHang = txtKhachHang.Text;
            PhieuXuat.TongTien = double.Parse(txtTongCong.Text);
            //PhieuXuat.DaTra = PhieuXuat.TongTien;
            PhieuXuat.DaTra = 0;
            Loi = "Xin vui lòng kiểm tra lại các mặt hàng đã chọn!";
            PhieuXuat.DS_ChiTietPhieuXuat = KhoiTaoChiTietPhieuXuat(PhieuXuat.MaPhieuXuat);
            return PhieuXuat;
        }

        //Khởi tạo chi tiết phiếu xuất bán sỉ
        private List<clsChiTietPhieuXuatDTO> KhoiTaoChiTietPhieuXuat(string MaPhieuXuat)
        {
            //List<clsChiTietPhieuXuatDTO> DS_CTPX = new List<clsChiTietPhieuXuatDTO>();
            //for (int i = 0; i < grdvBanSi.Rows.Count; i++)
            //{
            //    clsChiTietPhieuXuatDTO CTTPX = new clsChiTietPhieuXuatDTO();
            //    CTTPX.MaPhieuXuat = MaPhieuXuat;
            //    CTTPX.MatHang.MaMatHang = grdvBanSi.Rows[i].Cells["MaMatHang"].Value.ToString().Trim();
            //    //CTTPX.MatHang.SoLuongTon = int.Parse(grdvBanSi.Rows[i].Cells["LuongTonConLai"].Value.ToString());
            //    CTTPX.SoLuong = int.Parse(grdvBanSi.Rows[i].Cells["SoLuong"].Value.ToString());
            //    CTTPX.DonGia = double.Parse(grdvBanSi.Rows[i].Cells["DonGia"].Value.ToString());
            //    CTTPX.ThueVAT = double.Parse(grdvBanSi.Rows[i].Cells["ThueVAT"].Value.ToString().Replace("%", ""));
            //    CTTPX.ChietKhau = double.Parse(grdvBanSi.Rows[i].Cells["ChietKhau"].Value.ToString().Replace("%", ""));
            //    CTTPX.ThanhTien = double.Parse(grdvBanSi.Rows[i].Cells["ThanhTien"].Value.ToString());
            //    DS_CTPX.Add(CTTPX);
            //}
            // return DS_CTPX;

            for (int i = 0; i < CacCPX.Count; i++)
            {
                CacCPX[i].MaPhieuXuat = MaPhieuXuat;
            }
            return CacCPX;
        }

        private void txtMaPhieuXuat_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    LienKet = "";
                    XemPhieuXuatTheoMaPhieuXuat(txtMaPhieuXuat.Text.Trim());
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử nhập lại Phiếu xuất khác!");
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            SuaHangDaNhap();
        }
        
        private void SuaHangDaNhap()
        {
            try
            {
                
                if (grdvBanSi.CurrentRow != null)
                {
                    if (ThaoTac == "Them")
                    {
                        int DongDangSua = grdvBanSi.CurrentRow.Index;
                        if (KiemTraLuongTon(grdvBanSi.Rows[DongDangSua].Cells["MaMatHang"].Value.ToString()) == true)
                        {
                            for (int i = 0; i < CacCPX.Count; i++)
                            {
                                if (CacCPX[i].MatHang.MaMatHang.Trim() == grdvBanSi.Rows[DongDangSua].Cells["MaMatHang"].Value.ToString())
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

                            grdvBanSi.Rows[DongDangSua].Cells["DonGia"].Value = clsSupport.CurrencyNumber(txtSuaGia.Text);
                            grdvBanSi.Rows[DongDangSua].Cells["SoLuong"].Value = txtSoLuong.Text;
                            grdvBanSi.Rows[DongDangSua].Cells["ChietKhau"].Value = ChietKhau + "%";
                            //Tinh Tien chua co VAT
                            Double ThanhTienChuaVAT = int.Parse(txtSoLuong.Text) * double.Parse(txtSuaGia.Text.Trim());
                            //Tinh tien VAT
                            Double TienThueVAT = ThanhTienChuaVAT * (double.Parse(((DataRowView)cboChonHang.SelectedItem).Row["ThueVAT"].ToString()) / 100);
                            //Thanh tien da co thue VAT nhung chua co chiet khau
                            Double ThanhTien = ThanhTienChuaVAT + TienThueVAT;
                            //Thanh tien da co chiet khau
                            ThanhTien = ThanhTien - (ThanhTien * double.Parse(ChietKhau)) / 100;
                            grdvBanSi.Rows[DongDangSua].Cells["ThanhTien"].Value = clsSupport.CurrencyNumber(ThanhTien.ToString());
                            //Tinh tien hang
                            txtTienHang.Text = TinhTongTienHang().ToString();
                            //Tinh tien thueVAT
                            txtThueVAT.Text = TinhTongTienThue().ToString();
                            //Tinh tien chiet khau
                            txtChietKhau.Text = TinhTongTienChietKhau().ToString();
                            //Tinh tong tien
                            txtTongCong.Text = TinhTongTien().ToString();
                        }
                    }
                    else // sua 
                    {
                        int LuongTonConLai = int.Parse(grdvBanSi.Rows[grdvBanSi.CurrentRow.Index].Cells["LuongTonConLai"].Value.ToString());
                        if (LuongTonConLai == 0)
                        {
                            int SoLuong = int.Parse(grdvBanSi.Rows[grdvBanSi.CurrentRow.Index].Cells["SoLuong"].Value.ToString());
                            int SoLuongTon = int.Parse(((DataRowView)cboChonHang.SelectedItem).Row["SoLuongTon"].ToString());
                            //Khoi phuc lai luong ton
                            LuongTonConLai = SoLuong + SoLuongTon;
                            grdvBanSi.Rows[grdvBanSi.CurrentRow.Index].Cells["LuongTonConLai"].Value = LuongTonConLai;

                        }
                        if (KiemTraLuongTonKhiSua() == true)
                        {
                            int DongDangSua = grdvBanSi.CurrentRow.Index;
                            int SoLuong = int.Parse(txtSoLuong.Text);
                            Double DonGia = Double.Parse(txtSuaGia.Text);
                            string ChietKhau = "";
                            if (txtSuaChietKhau.Text.IndexOf("%") != -1)
                            {
                                ChietKhau = txtSuaChietKhau.Text.Replace("%", "");
                            }
                            else
                            {
                                ChietKhau = txtSuaChietKhau.Text;
                            }
                            Double PTChietKhau = Double.Parse(ChietKhau);
                            if (int.Parse(grdvBanSi.Rows[DongDangSua].Cells["SoLuong"].Value.ToString()) != SoLuong || 
                                Double.Parse(grdvBanSi.Rows[DongDangSua].Cells["DonGia"].Value.ToString())!=DonGia ||
                                 Double.Parse(grdvBanSi.Rows[DongDangSua].Cells["ChietKhau"].Value.ToString().Replace('%',' ')) != PTChietKhau)
                            {
                                string MaMatHang = grdvBanSi.Rows[DongDangSua].Cells["MaMatHang"].Value.ToString().Trim();
                                List<clsChiTietPhieuNhapDTO> ChonMatHang = new clsPhieuNhapBUS().ChonMatHangNhapVoiGiaCao(MaMatHang, int.Parse(txtSoLuong.Text));
                                if (ChonMatHang == null)
                                {
                                    ChonMatHang = new List<clsChiTietPhieuNhapDTO>();
                                }
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
                                            ctpn.DonGia = CacMHDaBanCuaCTPX[i].DonGia;
                                            ctpn.SoLuongTon = CacMHDaBanCuaCTPX[i].SoLuong;
                                            ctpn.ChietKhau = CacMHDaBanCuaCTPX[i].ChietKhau;
                                            ctpn.ThueVAT = CacMHDaBanCuaCTPX[i].ThueVAT;
                                            ChonMatHang.Add(ctpn);
                                        }
                                    }
                                }
                                // Xoa di tat cac cac chi tiet mat hang da co
                                for (int i = 0; i < CacCPX.Count; i++)
                                {
                                    if (CacCPX[i].MatHang.MaMatHang.Trim() == MaMatHang)
                                    {
                                        CacCPX.RemoveAt(i);
                                        i = -1;
                                    }
                                }
                                //Sap xep va chon cac mat hang theo so luong
                                ChonMatHang = new clsPhieuNhapBUS().ChonMatHangNhapVoiGiaCao(SoLuong, ChonMatHang);
                                // Them cac mat hang vao
                                for (int i = 0; i < ChonMatHang.Count; i++)
                                {
                                    clsChiTietPhieuXuatDTO PhieuXuat = new clsChiTietPhieuXuatDTO();
                                    PhieuXuat.MatHang.MaMatHang = ChonMatHang[i].MatHang.MaMatHang;
                                    PhieuXuat.MatHang.TenMatHang = ChonMatHang[i].MatHang.TenMatHang;
                                    PhieuXuat.SoLuong = ChonMatHang[i].SoLuongTon;
                                    PhieuXuat.MaPhieuNhap = ChonMatHang[i].MaPhieuNhap;
                                    PhieuXuat.ChietKhau = double.Parse(txtSuaChietKhau.Text.Replace("%", ""));
                                    PhieuXuat.ThueVAT = double.Parse(grdvBanSi.Rows[DongDangSua].Cells["ThueVAT"].Value.ToString().Replace("%", ""));
                                    PhieuXuat.DonGia = double.Parse(txtSuaGia.Text);
                                    Double ThanhTienCT = ChonMatHang[i].SoLuongTon * PhieuXuat.DonGia + (PhieuXuat.DonGia * (PhieuXuat.ThueVAT / 100) * ChonMatHang[i].SoLuongTon);
                                    PhieuXuat.ThanhTien = ThanhTienCT;
                                    CacCPX.Add(PhieuXuat);
                                }
                               
                                // Cap nhat lai so luong mat hang can sua tren luoi
                                grdvBanSi.Rows[grdvBanSi.CurrentRow.Index].Cells["SoLuong"].Value = int.Parse(txtSoLuong.Text);
                                grdvBanSi.Rows[grdvBanSi.CurrentRow.Index].Cells["DonGia"].Value = clsSupport.CurrencyNumber(txtSuaGia.Text);
                                grdvBanSi.Rows[grdvBanSi.CurrentRow.Index].Cells["ChietKhau"].Value = ChietKhau+"%";
                                //Thanh tien chua thue va chua chiet khau
                                double ThanhTien = int.Parse(txtSoLuong.Text) * double.Parse(txtSuaGia.Text);
                                string ThueVAT = grdvBanSi.Rows[grdvBanSi.CurrentRow.Index].Cells["ThueVAT"].Value.ToString();
                                if (ThueVAT.IndexOf("%") != -1)
                                {
                                    ThueVAT = ThueVAT.Replace("%", "");
                                }
                                //Thanh tien co thue va chua chiet khau
                                ThanhTien = ThanhTien + ThanhTien * double.Parse(ThueVAT) / 100;
                                //Thanh tien co thue va co chiet khau
                                ThanhTien = ThanhTien - (ThanhTien * double.Parse(ChietKhau)) / 100;
                                grdvBanSi.Rows[grdvBanSi.CurrentRow.Index].Cells["ThanhTien"].Value = clsSupport.CurrencyNumber(ThanhTien.ToString());

                            }
                            //Tinh tien hang
                            txtTienHang.Text = TinhTongTienHang().ToString();
                            //Tinh tien thueVAT
                            txtThueVAT.Text = TinhTongTienThue().ToString();
                            //Tinh tien chiet khau
                            txtChietKhau.Text = TinhTongTienChietKhau().ToString();
                            //Tinh tong tien
                            txtTongCong.Text = TinhTongTien().ToString();
                        }
                        else
                        {
                            txtSoLuong.Focus();
                        }
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
            int SoLuongTonConLai = int.Parse(grdvBanSi.Rows[grdvBanSi.CurrentRow.Index].Cells["LuongTonConLai"].Value.ToString());
            int SoLuongNhap = int.Parse(txtSoLuong.Text);
            int LuongMin = int.Parse(((DataRowView)cboChonHang.SelectedItem).Row["LuongMin"].ToString());
            if (SoLuongNhap > SoLuongTonConLai)
            {
                MessageBox.Show("Xin vui lòng nhập vào số lượng bán <= số lượng tồn: " + SoLuongTonConLai.ToString(), "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (SoLuongTonConLai - SoLuongNhap <= LuongMin)
                {
                    MessageBox.Show("Hàng đã đạt ngưỡng Min,lưu ý nhập bổ sung hàng thêm!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return true;
            }
            return false;
        }

        //Chọn hàng vừa cần bán
        private void grdvBanSi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    cboChonHang.SelectedValue = grdvBanSi.Rows[e.RowIndex].Cells["MaMatHang"].Value.ToString().Trim();
                    txtSuaGia.Text = grdvBanSi.Rows[e.RowIndex].Cells["DonGia"].Value.ToString().Trim();
                    txtSoLuong.Text = grdvBanSi.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString().Trim();
                    txtSuaChietKhau.Text = grdvBanSi.Rows[e.RowIndex].Cells["ChietKhau"].Value.ToString().Trim();
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!");
            }
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
                cboChonHang.Focus();
                txtMaPhieuXuat.Text = PhieuXuatBanSiBus.LayMaPhieuXuatMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            XoaHangDaNhap();
        }

        //Xóa thông tin hàng đã bán
        private void XoaHangDaNhap()
        {
            try
            {
                if (grdvBanSi.CurrentRow != null)
                {
                    DialogResult result = MessageBox.Show("Bạn có thật sự muốn xóa mặt hàng này không?", "Xac nhan", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        for (int i = 0; i < CacCPX.Count; i++)
                        {
                            if (CacCPX[i].MatHang.MaMatHang.Trim() == grdvBanSi.Rows[grdvBanSi.CurrentRow.Index].Cells["MaMatHang"].Value.ToString())
                            {
                                CacCPX.RemoveAt(i);
                                i = -1;
                            }
                        }
                        grdvBanSi.Rows.RemoveAt(grdvBanSi.CurrentRow.Index);
                        CapNhatSTT();
                        //Tinh tien hang
                        txtTienHang.Text = TinhTongTienHang().ToString();
                        //Tinh tien thueVAT
                        txtThueVAT.Text = TinhTongTienThue().ToString();
                        //Tinh tien chiet khau
                        txtChietKhau.Text = TinhTongTienChietKhau().ToString();
                        //Tinh tong tien
                        txtTongCong.Text = TinhTongTien().ToString();
                    }
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!");
            }
        }

        //Cập nhật lại số thứ tự
        private void CapNhatSTT()
        {
            for (int i = 0; i < grdvBanSi.Rows.Count; i++)
            {
                int stt = (i + 1);
                grdvBanSi.Rows[i].Cells["STT"].Value = stt.ToString();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        // Thêm khách hàng
        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            try
            {
                frmKhachHang f = new frmKhachHang("ChonKhachHang");
                f.ShowDialog();
                KhoiTaoComboKhachHang();
                if (f.KhachHangDTO != null)
                {
                    clsKhachHangDTO KhachHangDTO = f.KhachHangDTO;
                    cboKhachHang.SelectedValue = KhachHangDTO.MaKhachHang;
                    cboKhachHang.Focus();
                }
            }
            catch (Exception loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSuaChietKhau_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = clsSupport.KiemTraPhanTram(txtSuaChietKhau.Text, "Chiết khấu");
        }

        private void In()
        {
            try
            {
                if (grdvBanSi.RowCount == 0)
                {
                    MessageBox.Show("In không thành công vì không có thông tin về phiếu xuất bán hàng! " + txtMaPhieuXuat.Text.Trim(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptHoaDonBanSi hoaDonBanSi = new rptHoaDonBanSi();
                hoaDonBanSi.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                DataTable bang = PhieuXuatBanSiBus.ReportHoaDonBanSi(txtMaPhieuXuat.Text.Trim());
                DataTable bangCT = PhieuXuatBanSiBus.ReportCTHoaDonBanSi(txtMaPhieuXuat.Text.Trim());
                if (bangCT.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();
                    DataSet ds = new DataSet();
                    ds.Tables.Add(bang);
                    ds.Tables.Add(bangCT);
                    ds.Tables.Add(CongTy);
                    hoaDonBanSi.SetDataSource(ds);
                    hoaDonBanSi.SetParameterValue("@MaPhieuXuat", txtMaPhieuXuat.Text.Trim());
                    hoaDonBanSi.SetParameterValue("@TienHang", decimal.Parse(txtTienHang.Text.Trim()));
                    hoaDonBanSi.SetParameterValue("@ThueVAT", decimal.Parse(txtThueVAT.Text.Trim()));
                    hoaDonBanSi.SetParameterValue("@ChietKhau", decimal.Parse(txtChietKhau.Text.Trim()));
                    double TongCong = double.Parse(txtTongCong.Text.Trim());
                    hoaDonBanSi.SetParameterValue("@TienBangChu", clsSupport.ConvertMoneyToText(TongCong.ToString()));

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = hoaDonBanSi;
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
                if (cboKhachHang.SelectedIndex != -1)
                {
                    if (((DataRowView)cboKhachHang.SelectedItem).Row["BaoGia"].ToString().Trim() == "Giá bán sỉ")
                    {
                        txtSuaGia.Text = ((DataRowView)cboChonHang.SelectedItem).Row["GiaBanSi"].ToString();
                    }
                    else
                    {
                        txtSuaGia.Text = ((DataRowView)cboChonHang.SelectedItem).Row["GiaBanLe"].ToString();
                    }
                    txtSuaChietKhau.Text = ((DataRowView)cboKhachHang.SelectedItem).Row["ChietKhau"].ToString() + "%";
                    txtSoLuong.Text = "1";
                }
                else
                {
                    //txtSuaGia.Text = ((DataRowView)cboChonHang.SelectedItem).Row["GiaBanSi"].ToString();
                    //txtSuaChietKhau.Text = "0%";
                    //txtSoLuong.Text = "1";
                    MessageBox.Show("Xin vui lòng chọn Khách mua hàng trước khi chọn mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboKhachHang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                txtDiaChi.Text = ((DataRowView)cboKhachHang.SelectedItem).Row["DiaChi"].ToString();
                txtDienThoai.Text = ((DataRowView)cboKhachHang.SelectedItem).Row["DienThoai"].ToString();
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}