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
    public partial class frmThuTien : frmTemplete
    {

        #region Thuộc tính
        private clsPhieuXuatBUS PhieuXuatBus = new clsPhieuXuatBUS();
        private clsPhieuThuBanHangBUS PhieuThuBanHangBus = new clsPhieuThuBanHangBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        public string ThaoTac = "Them";
        public string MaPhieuThuCanXem = null;
        #endregion

        public frmThuTien()
        {
            InitializeComponent();
        }

        public frmThuTien(string _MaPhieuThu)
        {
            MaPhieuThuCanXem = _MaPhieuThu;
            InitializeComponent();
        }

        private void frmThuTien_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboKhachHang();
                dtpNgayThu.Value = DateTime.Now;
                cboKhachHang.Focus();
                if (MaPhieuThuCanXem != null)
                {
                    
                    XemPhieuThuTheoMaPhieuThu(MaPhieuThuCanXem);
                }
                else
                {
                    txtMaPhieuThu.Text = PhieuThuBanHangBus.LayMaPhieuThuMoi();
                }

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

            //in thông tin 
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

        //Khách Hàng
        #region Khách Hàng
        //Lấy danh sách Khách Hàng
        private void KhoiTaoComboKhachHang()
        {
            DataTable BangKhachHang = new clsKhachHangBUS().LayBang();
                if (BangKhachHang.Rows.Count == 0)
                {
                    cboKhachHang.SelectedIndex = -1;
                    cboKhachHang.Text = "< Không có Khách hàng! >";
                }

            cboKhachHang.DataSource = BangKhachHang;
            cboKhachHang.DisplayMember = "TenKhachHang";
            cboKhachHang.ValueMember = "MaKhachHang";
        }
        #endregion

        private void XemPhieuThuTheoMaPhieuThu(string MaPhieuThu)
        {
           
            clsPhieuThuBanHangDTO PhieuThu = PhieuThuBanHangBus.LayThongTin(MaPhieuThu);
            if (PhieuThu != null)
            {
                AnCacVungNhapLieu(false);
                ThaoTac = "CapNhat";
                cboKhachHang.SelectedValue = PhieuThu.DS_ChiTietPhieuThu[0].PhieuXuat.KhachHang.MaKhachHang;
                LamTuoi();
                cboKhachHang.Enabled = false;
                txtMaPhieuThu.Text = MaPhieuThu;
                txtMaSoThue.Text = ((DataRowView)cboKhachHang.SelectedItem).Row["MaSoThue"].ToString();
                txtDiaChi.Text = ((DataRowView)cboKhachHang.SelectedItem).Row["DiaChi"].ToString();
                txtSoTien.Text = "0";
                txtTienThuKyNay.Text = "";
                dtpNgayThu.Value = PhieuThu.NgayThu;
                txtTenNguoiNop.Text = PhieuThu.NguoiNop;
                txtLyDo.Text = PhieuThu.LyDo;
                double TongTienDaThu = 0;
                for (int i = 0; i < PhieuThu.DS_ChiTietPhieuThu.Count; i++)
                {
                    object[] Dong = new object[9];
                    int STT = i + 1;
                    Dong[0] = STT.ToString();
                    Dong[1] = PhieuThu.DS_ChiTietPhieuThu[i].PhieuXuat.MaPhieuXuat;
                    Dong[2] = ChuyenDoiNgay(PhieuThu.DS_ChiTietPhieuThu[i].PhieuXuat.NgayXuat.ToShortDateString());
                    DateTime NgayXuat = PhieuThu.DS_ChiTietPhieuThu[i].PhieuXuat.NgayXuat;
                    int TuoiNo = DateTime.Now.DayOfYear - NgayXuat.DayOfYear;
                    Dong[3] = TuoiNo.ToString();
                    Dong[4] = clsSupport.CurrencyNumber(PhieuThu.DS_ChiTietPhieuThu[i].PhieuXuat.TongTien.ToString());
                    double ConThu = PhieuThu.DS_ChiTietPhieuThu[i].PhieuXuat.TongTien - PhieuThu.DS_ChiTietPhieuThu[i].PhieuXuat.DaTra;
                    Dong[5] = clsSupport.CurrencyNumber(PhieuThu.DS_ChiTietPhieuThu[i].PhieuXuat.DaTra.ToString());
                    Dong[6] = clsSupport.CurrencyNumber(ConThu.ToString());
                    Dong[7] = clsSupport.CurrencyNumber(PhieuThu.DS_ChiTietPhieuThu[i].SoTien.ToString());
                    Dong[8] = clsSupport.CurrencyNumber(PhieuThu.DS_ChiTietPhieuThu[i].SoTien.ToString());
                    TongTienDaThu += PhieuThu.DS_ChiTietPhieuThu[i].SoTien;
                    grdvDSDonHangDaBan.Rows.Add(Dong);
                }
                grdvDSDonHangDaBan.Columns["ThuKyNay"].HeaderText = "Đã Thu Kỳ Này";
                txtSoTien.Text = TongTienDaThu.ToString();
                txtSoTienBangChu.Text = "(Viết bằng chữ): " + clsSupport.ConvertMoneyToText(TongTienDaThu.ToString());
            }
            else
            {
                ThaoTac = "Them";
                LamTuoi();
                grdvDSDonHangDaBan.Columns["ThuKyNay"].HeaderText = "Thu Kỳ Này";
                AnCacVungNhapLieu(true);
                cboKhachHang.Enabled = true;
            }
        }

        private void LamTuoi()
        {
            try
            {
                txtTenNguoiNop.Text = "";
                txtDiaChi.Text = "";
                txtMaSoThue.Text = "";
                txtLyDo.Text = "Thu tiền hàng";
                txtSoTien.Text = "0";
                txtSoTienBangChu.Text = "";
                txtTienThuKyNay.Text = "0";
                dtpNgayThu.Value = DateTime.Now;
                cboKhachHang.Enabled = true;
                //cboKhachHang.SelectedIndex = -1;
                grdvDSDonHangDaBan.Rows.Clear();
            }
            catch (Exception Loi)
            {

            }

        }

        private void AnCacVungNhapLieu(Boolean isEnable)
        {
            cboKhachHang.Enabled = isEnable;
            btnThemKhachHang.Enabled = isEnable;
        }

        private string ChuyenDoiNgay(string Ngay)
        {
            DateTime NgayXuat = DateTime.Parse(Ngay);
            string strNgay = NgayXuat.Day.ToString() + "/" + NgayXuat.Month.ToString() + "/" + NgayXuat.Year.ToString();
            return strNgay;
        }

        private void HienThiCacPhieuXuat()
        {
            clsKhachHangDTO KhachHang = new clsKhachHangDTO();
            KhachHang.MaKhachHang = ((DataRowView)cboKhachHang.SelectedItem).Row["MaKhachHang"].ToString().Trim();
            KhachHang.TenKhachHang = ((DataRowView)cboKhachHang.SelectedItem).Row["TenKhachHang"].ToString().Trim();
            txtDiaChi.Text = ((DataRowView)cboKhachHang.SelectedItem).Row["DiaChi"].ToString();
            txtMaSoThue.Text = ((DataRowView)cboKhachHang.SelectedItem).Row["MaSoThue"].ToString();
            txtSoTien.Text = "0";
            txtTienThuKyNay.Text = "0";
            KhoiTao(KhachHang);
            txtMaPhieuThu.Text = PhieuThuBanHangBus.LayMaPhieuThuMoi();
            AnCacVungNhapLieu(true);
        
        }
        private void cboKhachHang_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && cboKhachHang.SelectedItem != null)
                {
                    HienThiCacPhieuXuat();
                }
            }
            catch (Exception loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KhoiTao(clsKhachHangDTO KhachHang)
        {
            if (grdvDSDonHangDaBan.Rows.Count > 0)
            {
                grdvDSDonHangDaBan.Rows.Clear();
            }
            DataTable Bang = PhieuXuatBus.LayBangConThu(KhachHang);
            double TongCong = 0;
            for (int i = 0; i < Bang.Rows.Count; i++)
            {
                object[] Dong = new object[8];
                int STT = i + 1;
                Dong[0] = STT.ToString();
                Dong[1] = Bang.Rows[i]["MaPhieuXuat"].ToString();
                Dong[2] = ChuyenDoiNgay(Bang.Rows[i]["NgayXuat"].ToString());
                DateTime NgayXuat = DateTime.Parse(Bang.Rows[i]["NgayXuat"].ToString());
                int TuoiNo = DateTime.Now.DayOfYear - NgayXuat.DayOfYear;
                Dong[3] = TuoiNo.ToString();
                Dong[4] = clsSupport.CurrencyNumber(Bang.Rows[i]["TongTien"].ToString());
                double ConThu = double.Parse(Bang.Rows[i]["TongTien"].ToString()) - double.Parse(Bang.Rows[i]["DaTra"].ToString());
                Dong[5] = clsSupport.CurrencyNumber(Bang.Rows[i]["DaTra"].ToString());
                Dong[6] = clsSupport.CurrencyNumber(ConThu.ToString());
                Dong[7] = "0";
                TongCong += double.Parse(Bang.Rows[i]["DaTra"].ToString());
                grdvDSDonHangDaBan.Rows.Add(Dong);
            }
        }

        private void grdvDSDonHangDaBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (ThaoTac == "Them")
                    {
                        txtTienThuKyNay.Text = grdvDSDonHangDaBan.Rows[e.RowIndex].Cells["ConThu"].Value.ToString();
                    }
                    else
                    {
                        txtTienThuKyNay.Text = grdvDSDonHangDaBan.Rows[e.RowIndex].Cells["ThuKyNay"].Value.ToString();
                    }
                    txtTienThuKyNay.Focus();
                }

            }
            catch
            {
                MessageBox.Show("Xin vui lòng chọn phiếu xuất muốn thu tiền!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtTienThuKyNay_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && grdvDSDonHangDaBan.CurrentRow.Index != -1)
                {
                    string ChuoiTienConThu = grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["ConThu"].Value.ToString();
                    Decimal TienConThu = Decimal.Parse(ChuoiTienConThu);
                    Decimal TienThuKyNay = Decimal.Parse(txtTienThuKyNay.Text);
                    if (ThaoTac == "Them")
                    {
                        if (TienThuKyNay > TienConThu)
                        {
                            MessageBox.Show("Xin vui lòng nhập số tiền thu kỳ này nhỏ hơn hoặc bằng " + ChuoiTienConThu + " VNĐ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["ThuKyNay"].Value = txtTienThuKyNay.Text;
                            Decimal TongTien = 0;
                            for (int i = 0; i < grdvDSDonHangDaBan.RowCount; i++)
                            {
                                TongTien += Decimal.Parse(grdvDSDonHangDaBan.Rows[i].Cells["ThuKyNay"].Value.ToString());
                            }
                            txtSoTien.Text = TongTien.ToString();
                            txtSoTienBangChu.Text = "(Viết bằng chữ): " + clsSupport.ConvertMoneyToText(TongTien.ToString());
                        }
                    }
                    else
                    {
                        Decimal DaThuKyNay = Decimal.Parse(grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["DaThuKyNay"].Value.ToString());
                        Decimal DaThu = Decimal.Parse(grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["DaThu"].Value.ToString());
                        Decimal KhoiPhucTienTruocKhiThu = DaThu - DaThuKyNay;
                        Decimal TongCong = Decimal.Parse(grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["TongCong"].Value.ToString());
                        Decimal ConThu = TongCong - KhoiPhucTienTruocKhiThu;
                        if (TienThuKyNay > ConThu)
                        {
                            MessageBox.Show("Xin vui lòng nhập số tiền thu kỳ này nhỏ hơn hoặc bằng " + clsSupport.CurrencyNumber(ConThu.ToString()).ToString() + " VNĐ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["ThuKyNay"].Value = txtTienThuKyNay.Text;
                            DaThu = KhoiPhucTienTruocKhiThu + TienThuKyNay;
                            grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["DaThu"].Value = clsSupport.CurrencyNumber(DaThu.ToString()).ToString();
                            ConThu = TongCong - DaThu;
                            grdvDSDonHangDaBan.Rows[grdvDSDonHangDaBan.CurrentRow.Index].Cells["ConThu"].Value = clsSupport.CurrencyNumber(ConThu.ToString()).ToString();
                            Decimal TongTien = 0;
                            for (int i = 0; i < grdvDSDonHangDaBan.RowCount; i++)
                            {
                                TongTien += Decimal.Parse(grdvDSDonHangDaBan.Rows[i].Cells["ThuKyNay"].Value.ToString());
                            }
                            txtSoTien.Text = TongTien.ToString();
                            txtSoTienBangChu.Text = "(Viết bằng chữ): " + clsSupport.ConvertMoneyToText(TongTien.ToString());
                        }
                    }

                }

            }
            catch
            {
                MessageBox.Show("Xin vui lòng chọn phiếu xuất muốn thu tiền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string Loi = "";
            try
            {
                Loi = "Xin vui lòng kiểm tra lại dữ liệu nhập";
                clsPhieuThuBanHangDTO PhieuThu = KhoiTaoPhieuThu(ref Loi);
                if (PhieuThu != null)
                {
                    Loi = "Lỗi kết nối cơ sở dữ liệu";
                    if (ThaoTac == "Them")
                    {
                        if (PhieuThuBanHangBus.LayThongTin(PhieuThu.MaPhieuThu) == null)//Phieu thu hang nay chua ton tai
                        {
                            if (PhieuThuBanHangBus.Them(PhieuThu) != -1)
                            {
                                DialogResult result = MessageBox.Show("Lưu phiếu Thu tiền " + txtMaPhieuThu.Text + " thành công! Bạn có muốn In phiếu thu tiền này không?", "Xac nhan", MessageBoxButtons.YesNo);
                                if (result == DialogResult.Yes)
                                {
                                    //In hoa don ban le
                                    In();
                                }
                                //MessageBox.Show("Lưu phiếu thu " + PhieuThu.MaPhieuThu + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LamTuoi();
                                txtMaPhieuThu.Text = PhieuThuBanHangBus.LayMaPhieuThuMoi();
                                Loi = "Thành Công";
                            }
                            else
                            {
                                MessageBox.Show("Lưu phiếu thu không thành công, nguyên nhân do phiếu thu này đã tồn tại rồi. Xin vui lòng nhập phiếu thu khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lưu phiếu thu " + PhieuThu.MaPhieuThu + " không thành công, nguyên nhân do phiếu thu này đã tồn tại rồi. Xin vui lòng nhập phiếu thu khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else//Thao tac cap nhat lai phieu thu chua duoc thu tien
                    {
                        if (PhieuThuBanHangBus.LayThongTin(PhieuThu.MaPhieuThu) != null)
                        {
                            DialogResult result = MessageBox.Show("Bạn có thật sự muốn cập nhật phiếu Thu " + PhieuThu.MaPhieuThu + " không?", "Xác nhận thông tin", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                if (PhieuThuBanHangBus.Sua(PhieuThu) != -1)
                                {
                                    //MessageBox.Show("Lưu phiếu thu " + PhieuThu.MaPhieuThu + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    DialogResult result1 = MessageBox.Show("Lưu phiếu Thu tiền " + txtMaPhieuThu.Text + " thành công! Bạn có muốn In phiếu thu tiền này không?", "Xac nhan", MessageBoxButtons.YesNo);
                                    if (result1 == DialogResult.Yes)
                                    {
                                        //In hoa don ban le
                                        In();
                                    }
                                    LamTuoi();
                                    txtMaPhieuThu.Text = PhieuThuBanHangBus.LayMaPhieuThuMoi();
                                    AnCacVungNhapLieu(true);
                                    ThaoTac = "Them";
                                    Loi = "Thành Công";
                                }
                                else
                                {
                                    MessageBox.Show("Lưu phiếu thu không thành công, nguyên nhân do phiếu thu này đã tồn tại rồi. Xin vui lòng nhập phiếu thu khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lưu phiếu thu " + PhieuThu.MaPhieuThu + " không thành công, nguyên nhân do phiếu thu này không tồn tại rồi. Xin vui lòng nhập phiếu thu khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //Khởi tạo phiếu thu
        private clsPhieuThuBanHangDTO KhoiTaoPhieuThu(ref string Loi)
        {
            clsPhieuThuBanHangDTO PhieuThu = new clsPhieuThuBanHangDTO();
            PhieuThu.NguoiThu = clsUser.MaNhanVien;
            if (txtSoTien.Text.Trim() == "0")
            {
                Loi = "Xin vui lòng nhập tiền thu kỳ này!";
                return null;
            }
            if (txtMaPhieuThu.Text.Length >= 3)
            {
                if (txtMaPhieuThu.Text.Substring(0, 2) == "PT")
                {
                    int SoPhieuThu = -1;
                    if (int.TryParse(txtMaPhieuThu.Text.Substring(2, (txtMaPhieuThu.Text.Length - 2)), out SoPhieuThu) == true && SoPhieuThu > 0)
                    {
                        PhieuThu.MaPhieuThu = txtMaPhieuThu.Text;
                    }
                    else
                    {
                        Loi = "Xin vui lòng nhập Phiếu thu có dạng như sau: PT + Số thứ tự (Số nguyên dương) vd: PT1, PT2,... !";
                        return null;
                    }
                }
                else
                {
                    Loi = "Xin vui lòng nhập Phiếu thu có dạng như sau: PT + Số thứ tự (Số nguyên dương) vd: PT1, PT2,... !";
                    return null;
                }
            }
            if (txtTenNguoiNop.Text.Trim() == "")
            {
                Loi = "Xin vui lòng nhập họ tên người nộp tiền";
                return null;
            }
            else
            {
                PhieuThu.NguoiNop = txtTenNguoiNop.Text.Trim();
            }
            PhieuThu.NgayThu = dtpNgayThu.Value;
            Loi = "Xin vui lòng chọn khách hàng!";
            if (cboKhachHang.SelectedItem == null || ((DataRowView)cboKhachHang.SelectedItem).Row["MaKhachHang"].ToString().Trim() == "")
            {
                return null;
            }
            PhieuThu.KhachHang = ((DataRowView)cboKhachHang.SelectedItem).Row["MaKhachHang"].ToString();
            PhieuThu.SoTien = double.Parse(txtSoTien.Text);
            PhieuThu.LyDo = txtLyDo.Text;
            Loi = "Xin vui lòng kiểm tra lại các phiếu xuất muốn thu tiền!";
            PhieuThu.DS_ChiTietPhieuThu = KhoiTaoChiTietPhieuThu(PhieuThu.MaPhieuThu);
            return PhieuThu;
        }

        //Khởi tạo chi tiết phiếu thu tiền hàng đã bán
        private List<clsChiTietPhieuThuDTO> KhoiTaoChiTietPhieuThu(string MaPhieuThu)
        {
            List<clsChiTietPhieuThuDTO> DS_CTPT = new List<clsChiTietPhieuThuDTO>();
            for (int i = 0; i < grdvDSDonHangDaBan.Rows.Count; i++)
            {
                Double TienThuKyNay = double.Parse(grdvDSDonHangDaBan.Rows[i].Cells["ThuKyNay"].Value.ToString());
                Double TienConThu = double.Parse(grdvDSDonHangDaBan.Rows[i].Cells["ConThu"].Value.ToString());
                
                if (TienThuKyNay > 0 )
                {
                    clsChiTietPhieuThuDTO CTPT = new clsChiTietPhieuThuDTO();
                    CTPT.MaPhieuThu = MaPhieuThu;
                    CTPT.PhieuXuat.MaPhieuXuat = grdvDSDonHangDaBan.Rows[i].Cells["MaPhieuXuat"].Value.ToString().Trim();
                    //Cap nhat lai so tien da tra
                    CTPT.PhieuXuat.DaTra += TienThuKyNay;
                    CTPT.SoTien = TienThuKyNay;
                    DS_CTPT.Add(CTPT);
                }
            }
            return DS_CTPT;
        }

        private void txtMaPhieuThu_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    XemPhieuThuTheoMaPhieuThu(txtMaPhieuThu.Text.Trim());
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử nhập lại Phiếu thu khác!");
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
                if (grdvDSDonHangDaBan.RowCount == 0)
                {
                    MessageBox.Show("In không thành công vì không có thông tin về phiếu thu " + txtMaPhieuThu.Text.Trim(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptPhieuThu phieuThu = new rptPhieuThu();
                phieuThu.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                DataTable bang = PhieuThuBanHangBus.ReportPhieuThu(txtMaPhieuThu.Text.Trim());

                if (bang.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();
                    DataSet cacBang = new DataSet();
                    cacBang.Tables.Add(bang);
                    cacBang.Tables.Add(CongTy);

                    phieuThu.SetDataSource(cacBang);
                    phieuThu.SetParameterValue("@MaPhieuThu", txtMaPhieuThu.Text.Trim());
                    phieuThu.SetParameterValue("@TienBangChu", txtSoTienBangChu.Text.Trim());

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = phieuThu;
                    dlgHienThi.ShowDialog();
                }
                else
                    MessageBox.Show("In không thành công vì không có thông tin về phiếu thu " + txtMaPhieuThu.Text.Trim(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void cboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboKhachHang.SelectedItem != null && cboKhachHang.SelectedIndex != -1)
                {
                    HienThiCacPhieuXuat();
                }
            }
            catch (Exception loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}