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
    public partial class frmNhapHangVaoKho : frmTemplete
    {
        #region Thuộc tính
        private clsPhieuNhapBUS PhieuNhapBus = new clsPhieuNhapBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        public string ThaoTac = "Them";
        public string MaPhieuNhapCanXem=null;
        #endregion

        #region Sự kiện màn hình nhập hàng vào kho
        public frmNhapHangVaoKho()
        {
            InitializeComponent();
        }

        public frmNhapHangVaoKho(string _MaPhieuNhap)
        {
            MaPhieuNhapCanXem=_MaPhieuNhap;
            InitializeComponent();
        }

        private void frmNhapHangVaoKho_Load(object sender, EventArgs e)
        {
            try
            {     
                KhoiTaoComboNhaCungCap();
                KhoiTaoComboMatHang();
                dtpNgayNhap.Value = DateTime.Now;
                cboNhaCungCap.Focus();
                if (MaPhieuNhapCanXem != null)
                {
                    txtMaPhieuNhap.Text = MaPhieuNhapCanXem;
                    XemPhieuNhapTheoMaPhieuNhap(MaPhieuNhapCanXem);
                }
                else
                {
                    txtMaPhieuNhap.Text = PhieuNhapBus.LayMaPhieuNhapMoi();
                }

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
                        MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!");
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
            //Lưu thông tin phiếu nhập
            if (keyData == (Keys.Control | Keys.L))
            {
                string Loi = "";
                LuuPhieuNhap(ref Loi); 
            }

            //Lưu và in thông tin phiếu nhập
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

        //Nhà Cung Cấp
        #region Nhà cung cấp
        //Lấy danh sách nhà cung cấp
        private void KhoiTaoComboNhaCungCap()
        {
            //Load combo nhom hang
            DataTable BangNhaCungCap = new clsNhaCungCapBUS().LayBang();
            if (BangNhaCungCap.Rows.Count == 0)
            {
                cboNhaCungCap.SelectedIndex = -1;
                cboNhaCungCap.Text = "< Không có Nhà cung cấp! >";
            }
            
            cboNhaCungCap.DataSource = BangNhaCungCap;
            cboNhaCungCap.DisplayMember = "TenNhaCungCap";
            cboNhaCungCap.ValueMember = "MaNhaCungCap";
        }

        private void cboNhaCungCap_OpenSearchForm(object sender, EventArgs e)
        {
            try
            {
                FormSearch F = new FormSearch(cboNhaCungCap);
                F.ShowDialog();
            }
            catch (Exception loi)
            {

            }
        }

        private void cboNhaCungCap_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && cboNhaCungCap.SelectedText == "<Tạo mới>")
                {
                    frmThemNhaCungCap f= new frmThemNhaCungCap();
                    f.ShowDialog();
                    KhoiTaoComboNhaCungCap();
                    cboNhaCungCap.Focus();
                }
             }
            catch (Exception loi)
            {
                 MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Thêm nhà cung cấp
        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            try
            {
                frmNhaCungCap f = new frmNhaCungCap("ChonNhaCungCap");
                f.ShowDialog();
                KhoiTaoComboNhaCungCap();
                if (f.NhaCungCapDTO != null)
                {
                    clsNhaCungCapDTO NhaCungCapDTO = f.NhaCungCapDTO;
                    cboNhaCungCap.SelectedValue = NhaCungCapDTO.MaNhaCungCap;
                    cboNhaCungCap.Focus();
                }
            }
            catch (Exception loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        //Nhập hàng đã chọn
        ////private void cboChonHang_KeyUp(object sender, KeyEventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (e.KeyCode == Keys.Enter)
        ////        {
        ////            string TenMatHang = ((DataRowView)cboChonHang.SelectedItem).Row["TenMatHang"].ToString();
        ////            txtSuaGia.Text = ((DataRowView)cboChonHang.SelectedItem).Row["GiaMua"].ToString();
        ////            txtSuaChietKhau.Text = "0%";
        ////            txtSoLuong.Text = "1";
        ////            cboChonHang.Text = TenMatHang;
        ////        }
        ////    }
        ////    catch (Exception Loi)
        ////    {
        ////        MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!");
        ////    }
        ////}

        //Thêm thông tin mặt hàng

        /// <summary>
        /// Thêm thông tin mặt hàng
        /// </summary>
        /// 
        private void NhapThemHang()
        {
            if (KiemTraMatHang(((DataRowView)cboChonHang.SelectedItem).Row["MaMatHang"].ToString().Trim()) == true)
            {
                object[] Dong = new object[9];
                int STT = (grdvNhapHang.RowCount + 1);
                Dong[0] = STT.ToString();
                Dong[1] = ((DataRowView)cboChonHang.SelectedItem).Row["MaMatHang"].ToString();
                Dong[2] = ((DataRowView)cboChonHang.SelectedItem).Row["TenMatHang"].ToString();
                Dong[3] = ((DataRowView)cboChonHang.SelectedItem).Row["DonViTinh"].ToString();
                Dong[4] = txtSoLuong.Text.Trim();
                Dong[5] = txtSuaGia.Text.Trim();
                Dong[6] = txtSuaChietKhau.Text.Trim();
                Dong[7] = ((DataRowView)cboChonHang.SelectedItem).Row["ThueVAT"].ToString() + "%";
                Double ThanhTien = int.Parse(txtSoLuong.Text) * double.Parse(txtSuaGia.Text.Trim()) + (double.Parse(txtSuaGia.Text.Trim()) * double.Parse(((DataRowView)cboChonHang.SelectedItem).Row["ThueVAT"].ToString()) / 100) * int.Parse(txtSoLuong.Text);
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
                ThanhTien = ThanhTien - (ThanhTien * double.Parse(ChietKhau)) / 100;
                Dong[8] =clsSupport.CurrencyNumber(ThanhTien.ToString());
                grdvNhapHang.Rows.Add(Dong);
                //Tinh tien hang
                txtTienHang.Text = TinhTongTienHang().ToString();
                //Tinh tien thueVAT
                txtThueVAT.Text = TinhTongTienThue().ToString();
                //Tinh tien chiet khau
                txtChietKhau.Text = TinhTongTienChietKhau().ToString();
                //Tinh tong tien
                txtTongCong.Text = TinhTongTien().ToString();
                //grdvNhapHang.Focus();
            }
        }

        //Lấy danh sách mặt hàng
        /// <summary>
        /// 
        /// </summary>
        private void KhoiTaoComboMatHang()
        {
            //Load combo nhom hang
            DataTable BangMatHang = new clsMatHangBUS().LayBangHangBan();
            cboChonHang.DataSource = BangMatHang;
            cboChonHang.DisplayMember = "TenMatHang";
            cboChonHang.ValueMember = "MaMatHang";
            if (BangMatHang.Rows.Count == 0)
            {
                cboChonHang.SelectedIndex = -1;
                cboChonHang.Text = "< Không có mặt hàng! >";
            }
        }

        //Tính tiền
        #region Tính tiền
        //Tính tiền hàng
        private double TinhTongTienHang()
        {
            double TienHang = 0;
            for(int i = 0; i < grdvNhapHang.Rows.Count; i++)
            {
                double DonGia = double.Parse(grdvNhapHang.Rows[i].Cells["DonGia"].Value.ToString().Trim());
                double SoLuong = double.Parse(grdvNhapHang.Rows[i].Cells["SoLuong"].Value.ToString().Trim());
                TienHang += DonGia * SoLuong;
            }
            return TienHang;
        }

        //Tính tiền Thuế
        private double TinhTongTienThue()
        {
            double TienThue = 0;
            for (int i = 0; i < grdvNhapHang.Rows.Count; i++)
            {
                double DonGia = double.Parse(grdvNhapHang.Rows[i].Cells["DonGia"].Value.ToString().Trim());
                double SoLuong = double.Parse(grdvNhapHang.Rows[i].Cells["SoLuong"].Value.ToString().Trim());
                string ThueVAT = "";
                if (grdvNhapHang.Rows[i].Cells["ThueVAT"].Value.ToString().IndexOf("%") != -1)
                {
                    ThueVAT = grdvNhapHang.Rows[i].Cells["ThueVAT"].Value.ToString().Replace("%", "");
                }
                else
                {
                    ThueVAT = grdvNhapHang.Rows[i].Cells["ThueVAT"].Value.ToString();
                }
                TienThue += (DonGia * SoLuong* double.Parse(ThueVAT))/100;
            }
            return TienThue;
        }

        //Tính tiền chiết khấu
        private double TinhTongTienChietKhau()
        {
            double TienChietKhau= 0;
            for (int i = 0; i < grdvNhapHang.Rows.Count; i++)
            {
                double DonGia = double.Parse(grdvNhapHang.Rows[i].Cells["DonGia"].Value.ToString().Trim());
                double SoLuong = double.Parse(grdvNhapHang.Rows[i].Cells["SoLuong"].Value.ToString().Trim());
                string ThueVAT = "";
                if (grdvNhapHang.Rows[i].Cells["ThueVAT"].Value.ToString().IndexOf("%") != -1)
                {
                    ThueVAT = grdvNhapHang.Rows[i].Cells["ThueVAT"].Value.ToString().Replace("%", "");
                }
                else
                {
                    ThueVAT = grdvNhapHang.Rows[i].Cells["ThueVAT"].Value.ToString();
                }
                string ChietKhau = "";
                if (grdvNhapHang.Rows[i].Cells["ChietKhau"].Value.ToString().IndexOf("%") != -1)
                {
                    ChietKhau = grdvNhapHang.Rows[i].Cells["ChietKhau"].Value.ToString().Replace("%", "");
                }
                else
                {
                    ChietKhau = grdvNhapHang.Rows[i].Cells["ChietKhau"].Value.ToString();
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
            for (int i = 0; i < grdvNhapHang.Rows.Count; i++)
            {
                double ThanhTien = double.Parse(grdvNhapHang.Rows[i].Cells["ThanhTien"].Value.ToString().Trim());
                TongTien += ThanhTien;
            }
            return TongTien;
        }

        #endregion

        //Kiểm tra mặt hàng này nhập chưa
        private Boolean KiemTraMatHang( string MaMatHang)
        {
            Boolean KetQua = true;
            for (int i = 0; i < grdvNhapHang.Rows.Count; i++)
            {
                if (grdvNhapHang.Rows[i].Cells["MaMatHang"].Value.ToString().Trim() == MaMatHang)
                {
                    KetQua = false;
                    break;
                }
            }
            return KetQua;
        }

        //Chọn mặt hàng cần nhập
        private void cboChonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtSuaGia.Text = ((DataRowView)cboChonHang.SelectedItem).Row["GiaMua"].ToString();
                //txtThueVAT.Text = ((DataRowView)cboChonHang.SelectedItem).Row["ThueVAT"].ToString()+"%";
                txtSuaChietKhau.Text="0%";
                txtSoLuong.Text = "1";
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!");
            }
        }

        //Sửa thông tin mặt hàng đã nhập
        private void BtnSua_Click(object sender, EventArgs e)
        {
            SuaHangDaNhap();
        }

        //Chi tiết Sửa thông tin mặt hàng đã nhập
        private void SuaHangDaNhap()
        {
            try
            {
                if (grdvNhapHang.CurrentRow != null)
                {
                    int DongDangSua = grdvNhapHang.CurrentRow.Index;
                    grdvNhapHang.Rows[DongDangSua].Cells["DonGia"].Value = txtSuaGia.Text;
                    grdvNhapHang.Rows[DongDangSua].Cells["SoLuong"].Value = txtSoLuong.Text;
                    grdvNhapHang.Rows[DongDangSua].Cells["ChietKhau"].Value = txtSuaChietKhau.Text;
                    Double ThanhTien = int.Parse(txtSoLuong.Text) * double.Parse(txtSuaGia.Text.Trim()) + (double.Parse(txtSuaGia.Text.Trim()) * double.Parse(((DataRowView)cboChonHang.SelectedItem).Row["ThueVAT"].ToString()) / 100) * int.Parse(txtSoLuong.Text);
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
                    ThanhTien = ThanhTien - (ThanhTien * double.Parse(ChietKhau)) / 100;
                    grdvNhapHang.Rows[DongDangSua].Cells["ThanhTien"].Value =clsSupport.CurrencyNumber(ThanhTien.ToString());
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
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!");
            }
        }

        //Chọn hàng cần vừa nhập
        private void grdvNhapHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    cboChonHang.SelectedValue = grdvNhapHang.Rows[e.RowIndex].Cells["MaMatHang"].Value.ToString().Trim();
                    txtSuaGia.Text = grdvNhapHang.Rows[e.RowIndex].Cells["DonGia"].Value.ToString().Trim();
                    txtSoLuong.Text = grdvNhapHang.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString().Trim();
                    txtSuaChietKhau.Text = grdvNhapHang.Rows[e.RowIndex].Cells["ChietKhau"].Value.ToString().Trim();
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!");
            }
        }

        //Xóa thông tin mặt hàng vừa nhập
        private void btnXoa_Click(object sender, EventArgs e)
        {
            XoaHangDaNhap();
        }

        //Xóa thông tin hàng đã nhập
        private void XoaHangDaNhap()
        {
            try
            {
                if (grdvNhapHang.CurrentRow != null)
                {
                    DialogResult result = MessageBox.Show("Bạn có thật sự muốn xóa mặt hàng này không?", "Xac nhan", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        grdvNhapHang.Rows.RemoveAt(grdvNhapHang.CurrentRow.Index);
                        CapNhatSTT();
                        //Tinh tien hang
                        txtTienHang.Text = TinhTongTienHang().ToString();
                        //Tinh tien thueVAT
                        txtThueVAT.Text = TinhTongTienThue().ToString();
                        //Tinh tien chiet khau
                        txtChietKhau.Text = TinhTongTienChietKhau().ToString();
                        //Tinh tong tien
                        txtTongCong.Text = TinhTongTien().ToString();
                        //grdvNhapHang.Focus();
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
            for (int i = 0; i < grdvNhapHang.Rows.Count; i++)
            {
                int stt = (i + 1);
                grdvNhapHang.Rows[i].Cells["STT"].Value = stt.ToString();
            }
        }

        //Nhập Thêm mặt hàng 
        private void btnThem_Click(object sender, EventArgs e)
        {
            try 
            {
                NhapThemHang();
                cboChonHang.Focus();
             }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại mặt hàng!");
            }
        }

        //Lưu thông tin phiếu nhập kho
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string Loi = "";
            LuuPhieuNhap(ref Loi);
        }

        private void LuuPhieuNhap(ref String Loi)
        {
            Loi = "";
            try
            {
                Loi = "Xin vui lòng kiểm tra lại dữ liệu nhập";
                clsPhieuNhapDTO PhieuNhap = KhoiTaoPhieuNhap(ref Loi);
                if (PhieuNhap != null)
                {
                    Loi = "Lỗi kết nối cơ sở dữ liệu";
                    if (PhieuNhapBus.LayThongTin(PhieuNhap.MaPhieuNhap) == null)//Phieu nhap nay chua ton tai
                    {
                        if (PhieuNhapBus.Them(PhieuNhap) != -1)
                        {
                            // Hoi co muon cap nhat lai gia mua, gia ban si, gia ban le dua tren % cua mat hang ko?

                            //MessageBox.Show("Lưu phiếu nhập " + PhieuNhap.MaPhieuNhap + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DialogResult result = MessageBox.Show("Lưu phiếu nhập " + txtMaPhieuNhap.Text + " thành công! Bạn có muốn In phiếu nhập này không?", "Xac nhan", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                // In phiếu nhập hàng
                                In();
                            }
                            LamTuoi();
                            txtMaPhieuNhap.Text = PhieuNhapBus.LayMaPhieuNhapMoi();
                            ThaoTac = "Them";
                            Loi = "Thành Công";
                        }
                        else
                        {
                            MessageBox.Show("Lưu phiếu nhập không thành công, nguyên nhân do phiếu nhập này đã tồn tại rồi. Xin vui lòng nhập phiếu nhập kho khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else//Thao tac cap nhat lai phieu nhap chua duoc tra tien
                    {
                        if (PhieuNhapBus.Sua(PhieuNhap) != -1)
                        {
                            MessageBox.Show("Lưu phiếu nhập " + PhieuNhap.MaPhieuNhap + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LamTuoi();
                            txtMaPhieuNhap.Text = PhieuNhapBus.LayMaPhieuNhapMoi();
                            ThaoTac = "Them";
                            Loi = "Thành Công";
                        }
                        else
                        {
                            MessageBox.Show("Lưu phiếu nhập không thành công, nguyên nhân do phiếu nhập này đã tồn tại rồi. Xin vui lòng nhập phiếu nhập kho khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //Khởi tạo phiếu nhập hàng vào kho
        private clsPhieuNhapDTO KhoiTaoPhieuNhap(ref string Loi)
        {
            clsPhieuNhapDTO PhieuNhap = new clsPhieuNhapDTO();
            PhieuNhap.NguoiNhap = clsUser.MaNhanVien;
            if (grdvNhapHang.Rows.Count==0)
            {
                Loi = "Xin vui lòng nhập mặt hàng cần nhập vào kho!";
                return null;
            }
            if (txtMaPhieuNhap.Text.Length >=4)
            {
                if (txtMaPhieuNhap.Text.Substring(0, 3) == "PNK")
                {
                    int SoPhieuNhap =-1;
                    if (int.TryParse(txtMaPhieuNhap.Text.Substring(3, (txtMaPhieuNhap.Text.Length - 3)), out SoPhieuNhap) == true && SoPhieuNhap>0)
                    {
                        PhieuNhap.MaPhieuNhap = txtMaPhieuNhap.Text;
                    }
                    else
                    {
                        Loi = "Xin vui lòng nhập Phiếu nhập có dạng như sau: PNK + Số thứ tự (Số nguyên dương) vd: PNK1, PNK2,... !";
                        return null;
                    }
                }
                else
                {
                    Loi = "Xin vui lòng nhập Phiếu nhập có dạng như sau: PNK + Số thứ tự (Số nguyên dương) vd: PNK1, PNK2,... !";
                    return null;
                }
            }
            PhieuNhap.NgayNhap = dtpNgayNhap.Value;
            Loi = "Xin vui lòng chọn nhà cung cấp!";
            if (cboNhaCungCap.SelectedItem==null ||((DataRowView)cboNhaCungCap.SelectedItem).Row["MaNhaCungCap"].ToString().Trim() == "")
            {
                return null;
            }
            PhieuNhap.NhaCungCap.MaNhaCungCap = ((DataRowView)cboNhaCungCap.SelectedItem).Row["MaNhaCungCap"].ToString();
            PhieuNhap.TongTien = double.Parse(txtTongCong.Text);
            PhieuNhap.ConNo = PhieuNhap.TongTien;
            Loi = "Xin vui lòng kiểm tra lại các mặt hàng đã nhập!";
            PhieuNhap.DS_ChiTietPhieuNhap = KhoiTaoChiTietPhieuNhap(PhieuNhap.MaPhieuNhap);
            
            return PhieuNhap;
        }
        //Khởi tạo chi tiết phiếu nhập hàng vào kho
        private List<clsChiTietPhieuNhapDTO> KhoiTaoChiTietPhieuNhap(string MaPhieuNhap)
        {
            List<clsChiTietPhieuNhapDTO> DS_CTPN = new List<clsChiTietPhieuNhapDTO>();
            for(int i=0;i<grdvNhapHang.Rows.Count;i++)
            {
                clsChiTietPhieuNhapDTO CTTPN=new clsChiTietPhieuNhapDTO();
                CTTPN.MaPhieuNhap=MaPhieuNhap;
                CTTPN.MatHang.MaMatHang=grdvNhapHang.Rows[i].Cells["MaMatHang"].Value.ToString().Trim();
                CTTPN.SoLuong=int.Parse(grdvNhapHang.Rows[i].Cells["SoLuong"].Value.ToString());
                CTTPN.DonGia=double.Parse(grdvNhapHang.Rows[i].Cells["DonGia"].Value.ToString());
                CTTPN.ThueVAT=double.Parse(grdvNhapHang.Rows[i].Cells["ThueVAT"].Value.ToString().Replace("%",""));
                CTTPN.ChietKhau=double.Parse(grdvNhapHang.Rows[i].Cells["ChietKhau"].Value.ToString().Replace("%",""));
                CTTPN.ThanhTien=double.Parse(grdvNhapHang.Rows[i].Cells["ThanhTien"].Value.ToString());
                DS_CTPN.Add(CTTPN);
            }
            return DS_CTPN;
        }

        // in thông tin phiếu nhập kho
        private void btnInRa_Click(object sender, EventArgs e)
        {
            try
            {
                In();
            }
           
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void In()
        {
           

            frmTheHienReport dlgHienThi = new frmTheHienReport();
            rptNhapHangVaoKho nhapHangVaoKho = new rptNhapHangVaoKho();
            nhapHangVaoKho.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

            DataTable bang = PhieuNhapBus.ReportPhieuNhapHang(txtMaPhieuNhap.Text.Trim());
            if (bang.Rows.Count != 0)
            {
                DataTable bang1 = PhieuNhapBus.ReportCT_PhieuNhapHang(txtMaPhieuNhap.Text.Trim());
                DataTable CongTy = CongTyBus.ReportCongTy();

                DataSet cacBang = new DataSet();
                cacBang.Tables.Add(bang);
                cacBang.Tables.Add(bang1);
                cacBang.Tables.Add(CongTy);

                nhapHangVaoKho.SetDataSource(cacBang);
                nhapHangVaoKho.SetParameterValue("@MaPhieuNhap", txtMaPhieuNhap.Text.Trim());
                nhapHangVaoKho.SetParameterValue("@TienHang", decimal.Parse(txtTienHang.Text.Trim()));
                nhapHangVaoKho.SetParameterValue("@ThueVAT", decimal.Parse(txtThueVAT.Text.Trim()));
                nhapHangVaoKho.SetParameterValue("@ChietKhau", decimal.Parse(txtChietKhau.Text.Trim()));
                double TongCong = double.Parse(txtTongCong.Text.Trim());
                nhapHangVaoKho.SetParameterValue("@TienBangChu", clsSupport.ConvertMoneyToText(TongCong.ToString()));

                dlgHienThi.CrystalReportViewer_TheHien.ReportSource = nhapHangVaoKho;
                dlgHienThi.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có thông tin về phiếu nhập hàng này", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void txtSuaChietKhau_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = clsSupport.KiemTraPhanTram(txtSuaChietKhau.Text, "Chiết khấu");
        }

        private void txtMaPhieuNhap_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    XemPhieuNhapTheoMaPhieuNhap(txtMaPhieuNhap.Text.Trim());
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử nhập lại Phiếu nhập khác!");
            }
        }

        private void XemPhieuNhapTheoMaPhieuNhap(string MaPhieuNhap)
        {
            LamTuoi();
            clsPhieuNhapDTO PhieuNhap = PhieuNhapBus.LayThongTin(MaPhieuNhap);
            if (PhieuNhap != null)
            {
                if (PhieuNhap.TongTien != PhieuNhap.ConNo)
                {
                    AnCacVungNhapLieu(false);
                    btnInRa.Enabled = true;
                }
                else
                {
                    if (PhieuNhapBus.KiemTraHuyPhieuNhap(MaPhieuNhap) == true)
                    {
                        ThaoTac = "CapNhat";
                        AnCacVungNhapLieu(true);
                    }
                    else
                    {
                        AnCacVungNhapLieu(false);
                        btnInRa.Enabled = true;
                    }
                    
                }
                cboNhaCungCap.SelectedValue = PhieuNhap.NhaCungCap.MaNhaCungCap;
                txtDiaChi.Text = ((DataRowView)cboNhaCungCap.SelectedItem).Row["DiaChi"].ToString();
                txtDienThoai.Text = ((DataRowView)cboNhaCungCap.SelectedItem).Row["DienThoai"].ToString();
                dtpNgayNhap.Value = PhieuNhap.NgayNhap;
                for (int i = 0; i < PhieuNhap.DS_ChiTietPhieuNhap.Count; i++)
                {
                    object[] Dong = new object[9];
                    int STT = (grdvNhapHang.RowCount + 1);
                    Dong[0] = STT.ToString();
                    Dong[1] = PhieuNhap.DS_ChiTietPhieuNhap[i].MatHang.MaMatHang;
                    Dong[2] = PhieuNhap.DS_ChiTietPhieuNhap[i].MatHang.TenMatHang;
                    Dong[3] = PhieuNhap.DS_ChiTietPhieuNhap[i].MatHang.DonViTinh;
                    Dong[4] = PhieuNhap.DS_ChiTietPhieuNhap[i].SoLuong.ToString(); ;
                    Dong[5] = clsSupport.CurrencyNumber(PhieuNhap.DS_ChiTietPhieuNhap[i].DonGia.ToString());
                    Dong[6] = PhieuNhap.DS_ChiTietPhieuNhap[i].ChietKhau.ToString() + "%";
                    Dong[7] = PhieuNhap.DS_ChiTietPhieuNhap[i].ThueVAT.ToString() + "%";
                    Dong[8] = clsSupport.CurrencyNumber(PhieuNhap.DS_ChiTietPhieuNhap[i].ThanhTien.ToString());
                    grdvNhapHang.Rows.Add(Dong);
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
                ThaoTac = "Them";
                string Loi = "";
                if (txtMaPhieuNhap.Text.Length >= 4)
                {
                    if (txtMaPhieuNhap.Text.Substring(0, 3) == "PNK")
                    {
                        int SoPhieuNhap = -1;
                        if (int.TryParse(txtMaPhieuNhap.Text.Substring(3, (txtMaPhieuNhap.Text.Length - 3)), out SoPhieuNhap) == true && SoPhieuNhap > 0)
                        {
                            DialogResult result = MessageBox.Show("Phiếu nhập kho " + txtMaPhieuNhap.Text + " không tồn tại. Bạn có muốn tạo mới phiếu nhập kho này không?" , "Xac nhan", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                AnCacVungNhapLieu(true);
                                ThaoTac = "Them";
                            }
                        }
                        else
                        {
                            Loi = "Xin vui lòng nhập Phiếu nhập có dạng như sau: PNK + Số thứ tự (Số nguyên dương) vd: PNK1, PNK2,... !";
                            MessageBox.Show(Loi, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        Loi = "Xin vui lòng nhập Phiếu nhập có dạng như sau: PNK + Số thứ tự (Số nguyên dương) vd: PNK1, PNK2,... !";
                        MessageBox.Show(Loi, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void LamTuoi()
        {
            try
            {
                txtDiaChi.Text = "";
                txtDienThoai.Text = "";
                txtThueVAT.Text = "0";
                txtTongCong.Text = "0";
                txtTienHang.Text = "0";
                txtChietKhau.Text = "0";
                txtSoLuong.Text = "1";
                txtSuaChietKhau.Text = "0%";
                txtSuaGia.Text = "0";
                dtpNgayNhap.Value = DateTime.Now;
                cboNhaCungCap.SelectedIndex = 0;
                cboChonHang.SelectedIndex = 0;
                grdvNhapHang.Rows.Clear();
            }
            catch(Exception Loi)
            {

            }
            
        }

        private void AnCacVungNhapLieu(Boolean isAn)
        {
            cboChonHang.Enabled = isAn;
            cboNhaCungCap.Enabled = isAn;
            txtSuaGia.Enabled = isAn;
            txtSoLuong.Enabled= isAn;
            txtSuaChietKhau.Enabled = isAn;
            btnTimHang.Enabled= isAn;
            btnThem.Enabled = isAn;
            BtnSua.Enabled = isAn;
            btnLuu.Enabled = isAn;
            btnInRa.Enabled = isAn;
            btnThemNCC.Enabled = isAn;
            btnXoa.Enabled = isAn;
            dtpNgayNhap.Enabled = isAn;
            grdvNhapHang.SelectionMode= DataGridViewSelectionMode.RowHeaderSelect;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        private Boolean KiemTraTruocKhiThoat()
        {
            if (grdvNhapHang.Rows.Count > 0 && btnLuu.Enabled == true)
            {
                DialogResult result = MessageBox.Show("Bạn có muốn lưu lại phiếu nhập này không?", "Xac nhan", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    string Loi ="";
                    LuuPhieuNhap(ref Loi);
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

        private void frmNhapHangVaoKho_FormClosing(object sender, FormClosingEventArgs e)
        {

            e.Cancel = KiemTraTruocKhiThoat();
        }

        private void btnTimHang_Click(object sender, EventArgs e)
        {
            frmMatHangMua F = new frmMatHangMua("ChonHangNhap");
            F.ShowDialog();
            KhoiTaoComboMatHang();
            if (F.MatHangDTO != null)
            {
                clsMatHangDTO MatHangDTO=F.MatHangDTO;
                cboChonHang.SelectedValue = MatHangDTO.MaMatHang;
                cboChonHang.Focus();
            }
        }

        private void cboNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtDiaChi.Text = ((DataRowView)cboNhaCungCap.SelectedItem).Row["DiaChi"].ToString();
                txtDienThoai.Text = ((DataRowView)cboNhaCungCap.SelectedItem).Row["DienThoai"].ToString();
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử chọn lại nhà cung cấp!");
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            KhoiTaoMoi();
        }

        void KhoiTaoMoi()
        {
            try
            {
                LamTuoi();
                AnCacVungNhapLieu(true);
                dtpNgayNhap.Value = DateTime.Now;
                cboNhaCungCap.Focus();
                txtMaPhieuNhap.Text = PhieuNhapBus.LayMaPhieuNhapMoi();
                ThaoTac = "Them";

            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}