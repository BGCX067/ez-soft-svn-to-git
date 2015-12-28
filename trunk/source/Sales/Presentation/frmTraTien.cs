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
    public partial class frmTraTien : frmTemplete
    {
        #region Thuộc tính
        private clsPhieuNhapBUS PhieuNhapBus = new clsPhieuNhapBUS();
        private clsPhieuChiHangBUS PhieuChiHangBus = new clsPhieuChiHangBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        public string ThaoTac = "Them";
        public string MaPhieuChiCanXem = null;
        #endregion

        public frmTraTien()
        {
            InitializeComponent();
        }

        public frmTraTien(string _MaPhieuChi)
        {
            MaPhieuChiCanXem=_MaPhieuChi;
            ThaoTac = "CapNhat";
            InitializeComponent();
        }

        private void frmTraTien_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboNhaCungCap();
                dtpNgayChi.Value = DateTime.Now;
                cboNhaCungCap.Focus();
                if (MaPhieuChiCanXem != null)
                {
                    
                    XemPhieuChiTheoMaPhieuChi(MaPhieuChiCanXem);
                }
                else
                {
                    txtMaPhieuChi.Text = PhieuChiHangBus.LayMaPhieuChiMoi();
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

        private void cboNhaCungCap_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && cboNhaCungCap.SelectedItem!=null)
                {
                    HienThiCacPhieuNhapKho();
                }
            }
            catch (Exception loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiCacPhieuNhapKho()
        {
            clsNhaCungCapDTO NhaCungCap = new clsNhaCungCapDTO();
            NhaCungCap.MaNhaCungCap = ((DataRowView)cboNhaCungCap.SelectedItem).Row["MaNhaCungCap"].ToString().Trim();
            NhaCungCap.TenNhaCungCap = ((DataRowView)cboNhaCungCap.SelectedItem).Row["TenNhaCungCap"].ToString().Trim();
            txtDiaChi.Text = ((DataRowView)cboNhaCungCap.SelectedItem).Row["DiaChi"].ToString();
            txtMaSoThue.Text = ((DataRowView)cboNhaCungCap.SelectedItem).Row["MaSoThue"].ToString();
            txtSoTien.Text = "0";
            txtTienTraKyNay.Text = "0";
            KhoiTao(NhaCungCap);
            txtMaPhieuChi.Text = PhieuChiHangBus.LayMaPhieuChiMoi();
            AnCacVungNhapLieu(true);
        }
        #endregion

        private void KhoiTao(clsNhaCungCapDTO NhaCungCap)
        {
            if (grdvDSDonHangDaMua.Rows.Count > 0)
            {
                grdvDSDonHangDaMua.Rows.Clear();
                //bindingSource1 = new BindingSource();
            }
            DataTable Bang = PhieuNhapBus.LayBangConNo (NhaCungCap);
            double TongCong = 0;
            for (int i = 0; i < Bang.Rows.Count; i++)
            {
                object[] Dong = new object[9];
                int STT = i + 1;
                Dong[0] = STT.ToString();
                Dong[1] = Bang.Rows[i]["MaPhieuNhap"].ToString();
                Dong[2] = ChuyenDoiNgay(Bang.Rows[i]["NgayNhap"].ToString());
                DateTime NgayNhap = DateTime.Parse(Bang.Rows[i]["NgayNhap"].ToString());
                int ThuoiNo = DateTime.Now.DayOfYear - NgayNhap.DayOfYear;
                Dong[3] = ThuoiNo.ToString();
                Dong[4] = clsSupport.CurrencyNumber(Bang.Rows[i]["TongTien"].ToString());
                double DaTra = double.Parse(Bang.Rows[i]["TongTien"].ToString()) - double.Parse(Bang.Rows[i]["ConNo"].ToString());
                Dong[5] = clsSupport.CurrencyNumber(DaTra.ToString());
                Dong[6] = clsSupport.CurrencyNumber(Bang.Rows[i]["ConNo"].ToString());
                Dong[7] = "0";
                Dong[8] = "0";
                TongCong += double.Parse(Bang.Rows[i]["ConNo"].ToString());
                grdvDSDonHangDaMua.Rows.Add(Dong);
            }
            //txtTongCong.Text = clsSupport.CurrencyNumber(TongCong.ToString()) + " (VNĐ)";

        }

        private string ChuyenDoiNgay(string Ngay)
        {
            DateTime NgayNhap = DateTime.Parse(Ngay);
            string strNgay = NgayNhap.Day.ToString() + "/" + NgayNhap.Month.ToString() + "/" + NgayNhap.Year.ToString();
            return strNgay;
        }

        private void btnTimNCC_Click(object sender, EventArgs e)
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
                    txtDiaChi.Text = ((DataRowView)cboNhaCungCap.SelectedItem).Row["DiaChi"].ToString();
                    txtMaSoThue.Text = ((DataRowView)cboNhaCungCap.SelectedItem).Row["MaSoThue"].ToString();
                    txtDiaChi.Text = ((DataRowView)cboNhaCungCap.SelectedItem).Row["DiaChi"].ToString();
                    txtSoTien.Text = "0";
                    txtTienTraKyNay.Text = "0";
                    KhoiTao(NhaCungCapDTO);
                    cboNhaCungCap.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdvDSDonHangDaMua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (ThaoTac == "Them")
                    {
                        txtTienTraKyNay.Text = grdvDSDonHangDaMua.Rows[e.RowIndex].Cells["ConNo"].Value.ToString();
                    }
                    else
                    {
                        txtTienTraKyNay.Text = grdvDSDonHangDaMua.Rows[e.RowIndex].Cells["TraKyNay"].Value.ToString();
                    }
                    txtTienTraKyNay.Focus();
                }
                  
            }
            catch
            {
                MessageBox.Show("Xin vui lòng chọn phiếu nhập kho muốn trả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtTienTraKyNay_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if ( e.KeyCode == Keys.Enter && grdvDSDonHangDaMua.CurrentRow.Index != -1)
                {
                    string ChuoiTienConNo = grdvDSDonHangDaMua.Rows[grdvDSDonHangDaMua.CurrentRow.Index].Cells["ConNo"].Value.ToString();
                    Decimal TienConNo = Decimal.Parse(ChuoiTienConNo);
                    Decimal TienTraKyNay = Decimal.Parse(txtTienTraKyNay.Text);
                    if (ThaoTac=="Them")
                    {
                        if (TienTraKyNay > TienConNo)
                        {
                            MessageBox.Show("Xin vui lòng nhập số tiền trả kỳ này là nhỏ hơn hoặc bằng " + ChuoiTienConNo + " VNĐ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            grdvDSDonHangDaMua.Rows[grdvDSDonHangDaMua.CurrentRow.Index].Cells["TraKyNay"].Value = txtTienTraKyNay.Text;
                            Decimal TongTien = 0;
                            for (int i = 0; i < grdvDSDonHangDaMua.RowCount; i++)
                            {
                                TongTien += Decimal.Parse(grdvDSDonHangDaMua.Rows[i].Cells["TraKyNay"].Value.ToString());
                            }
                            txtSoTien.Text = TongTien.ToString();
                            txtSoTienBangChu.Text = "(Viết bằng chữ): " + clsSupport.ConvertMoneyToText(TongTien.ToString());
                        }
                    }
                    else
                    {
                        string ChuoiTienDaTraKyNay = grdvDSDonHangDaMua.Rows[grdvDSDonHangDaMua.CurrentRow.Index].Cells["DaTraKyNay"].Value.ToString();
                        Decimal DaTraKyNay = Decimal.Parse(ChuoiTienDaTraKyNay);
                        Decimal TienNoTruocKhiTra = TienConNo + DaTraKyNay;
                        if (TienTraKyNay > TienNoTruocKhiTra)
                        {
                            MessageBox.Show("Xin vui lòng nhập số tiền trả kỳ này là nhỏ hơn hoặc bằng " + TienNoTruocKhiTra.ToString() + " VNĐ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            grdvDSDonHangDaMua.Rows[grdvDSDonHangDaMua.CurrentRow.Index].Cells["TraKyNay"].Value = txtTienTraKyNay.Text;
                            Decimal TongTienMua = Decimal.Parse(grdvDSDonHangDaMua.Rows[grdvDSDonHangDaMua.CurrentRow.Index].Cells["TongCong"].Value.ToString());
                            //Khoi phuc lai tien con no
                            TienConNo += DaTraKyNay;
                            //Tinh lai tien con no sau khi da tra tien ky nay duoc cap nhat
                            TienConNo -= TienTraKyNay;
                            //Tinh lai tien da tra
                            Decimal TinhTienDaTra = TongTienMua - TienConNo;
                            grdvDSDonHangDaMua.Rows[grdvDSDonHangDaMua.CurrentRow.Index].Cells["ConNo"].Value =clsSupport.CurrencyNumber(TienConNo.ToString());
                            grdvDSDonHangDaMua.Rows[grdvDSDonHangDaMua.CurrentRow.Index].Cells["DaTra"].Value = clsSupport.CurrencyNumber(TinhTienDaTra.ToString());
                            Decimal TongTien = 0;
                            for (int i = 0; i < grdvDSDonHangDaMua.RowCount; i++)
                            {
                                TongTien += Decimal.Parse(grdvDSDonHangDaMua.Rows[i].Cells["TraKyNay"].Value.ToString());
                            }
                            txtSoTien.Text = TongTien.ToString();
                            txtSoTienBangChu.Text = "(Viết bằng chữ): " + clsSupport.ConvertMoneyToText(TongTien.ToString());
                        }
                    }
                    
                }

            }
            catch
            {
                MessageBox.Show("Xin vui lòng chọn phiếu nhập kho muốn trả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string Loi = "";
            try
            {
                Loi = "Xin vui lòng kiểm tra lại dữ liệu nhập";
                clsPhieuChiHangDTO PhieuChi = KhoiTaoPhieuChi(ref Loi);
                if (PhieuChi != null)
                {
                    Loi = "Lỗi kết nối cơ sở dữ liệu";
                    if (ThaoTac == "Them")
                    {
                        if (PhieuChiHangBus.LayThongTin(PhieuChi.MaPhieuChi) == null)//Phieu chi hang nay chua ton tai
                        {
                            if (PhieuChiHangBus.Them(PhieuChi) != -1)
                            {
                                DialogResult result = MessageBox.Show("Lưu phiếu chi tiền " + txtMaPhieuChi.Text + " thành công! Bạn có muốn In phiếu chi tiền này không?", "Xac nhan", MessageBoxButtons.YesNo);
                                if (result == DialogResult.Yes)
                                {
                                    //In hoa don ban le
                                    In();
                                }
                                //MessageBox.Show("Lưu phiếu chi " + PhieuChi.MaPhieuChi + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LamTuoi();
                                txtMaPhieuChi.Text = PhieuChiHangBus.LayMaPhieuChiMoi();
                                Loi = "Thành Công";
                            }
                            else
                            {
                                MessageBox.Show("Lưu phiếu chi không thành công, nguyên nhân do phiếu chi này đã tồn tại rồi. Xin vui lòng nhập phiếu chi khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lưu phiếu chi " + PhieuChi.MaPhieuChi + " không thành công, nguyên nhân do phiếu chi này đã tồn tại rồi. Xin vui lòng nhập phiếu chi khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else//Thao tac cap nhat lai phieu Chi chua duoc tra tien
                    {
                        if (PhieuChiHangBus.LayThongTin(PhieuChi.MaPhieuChi) != null)
                        {
                            DialogResult result = MessageBox.Show("Bạn có thật sự muốn cập nhật phiếu Chi " + PhieuChi.MaPhieuChi + " không?", "Xác nhận thông tin", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                if (PhieuChiHangBus.Sua(PhieuChi) != -1)
                                {
                                    DialogResult result1 = MessageBox.Show("Lưu phiếu chi tiền " + txtMaPhieuChi.Text + " thành công! Bạn có muốn In phiếu chi tiền này không?", "Xac nhan", MessageBoxButtons.YesNo);
                                    if (result1 == DialogResult.Yes)
                                    {
                                        //In hoa don ban le
                                        In();
                                    }
                                    //MessageBox.Show("Lưu phiếu chi " + PhieuChi.MaPhieuChi + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LamTuoi();
                                    txtMaPhieuChi.Text = PhieuChiHangBus.LayMaPhieuChiMoi();
                                    AnCacVungNhapLieu(true);
                                    ThaoTac = "Them";
                                    Loi = "Thành Công";
                                }
                                else
                                {
                                    MessageBox.Show("Lưu phiếu chi không thành công, nguyên nhân do phiếu chi này đã tồn tại rồi. Xin vui lòng nhập phiếu chi khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lưu phiếu chi " + PhieuChi.MaPhieuChi + " không thành công, nguyên nhân do phiếu chi này không tồn tại rồi. Xin vui lòng nhập phiếu chi khác", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //Khởi tạo phiếu chi
        private clsPhieuChiHangDTO KhoiTaoPhieuChi(ref string Loi)
        {
            clsPhieuChiHangDTO PhieuChi = new clsPhieuChiHangDTO();
            PhieuChi.NguoiChi = clsUser.MaNhanVien;
            if (txtSoTien.Text.Trim()=="0")
            {
                Loi = "Xin vui lòng nhập tiền trả kỳ này!";
                return null;
            }
            if (txtMaPhieuChi.Text.Length >= 3)
            {
                if (txtMaPhieuChi.Text.Substring(0, 2) == "PC")
                {
                    int SoPhieuChi = -1;
                    if (int.TryParse(txtMaPhieuChi.Text.Substring(2, (txtMaPhieuChi.Text.Length - 2)), out SoPhieuChi) == true && SoPhieuChi > 0)
                    {
                        PhieuChi.MaPhieuChi = txtMaPhieuChi.Text;
                    }
                    else
                    {
                        Loi = "Xin vui lòng nhập Phiếu chi có dạng như sau: PC + Số thứ tự (Số nguyên dương) vd: PC1, PC2,... !";
                        return null;
                    }
                }
                else
                {
                    Loi = "Xin vui lòng nhập Phiếu chi có dạng như sau: PC + Số thứ tự (Số nguyên dương) vd: PC1, PC2,... !";
                    return null;
                }
            }
            if (txtTenNguoiNhan.Text.Trim() == "")
            {
                Loi = "Xin vui lòng nhập họ Tên người nhận.";
                txtTenNguoiNhan.Focus();
                return null;
            }
            else
            {
                PhieuChi.NguoiNhan = txtTenNguoiNhan.Text.Trim();
            }
            PhieuChi.NgayChi = dtpNgayChi.Value;
            Loi = "Xin vui lòng chọn nhà cung cấp!";
            if (cboNhaCungCap.SelectedItem == null || ((DataRowView)cboNhaCungCap.SelectedItem).Row["MaNhaCungCap"].ToString().Trim() == "")
            {
                return null;
            }
            PhieuChi.NhaCungCap = ((DataRowView)cboNhaCungCap.SelectedItem).Row["MaNhaCungCap"].ToString();
            PhieuChi.SoTien = double.Parse(txtSoTien.Text);
            Loi = "Xin vui lòng kiểm tra lại các phiếu nhập muốn chi trả tiền!";
            PhieuChi.LyDo = txtLyDo.Text;
            PhieuChi.DS_ChiTietPhieuChi = KhoiTaoChiTietPhieuChi(PhieuChi.MaPhieuChi);
            return PhieuChi;
        }

        //Khởi tạo chi tiết phiếu chi tiền hàng đã mua
        private List<clsChiTietPhieuChiDTO> KhoiTaoChiTietPhieuChi(string MaPhieuChi)
        {
            List<clsChiTietPhieuChiDTO> DS_CTPCH = new List<clsChiTietPhieuChiDTO>();
            for (int i = 0; i < grdvDSDonHangDaMua.Rows.Count; i++)
            {
                Double TienTraKyNay = double.Parse(grdvDSDonHangDaMua.Rows[i].Cells["TraKyNay"].Value.ToString());
                Double TienConNo=double.Parse(grdvDSDonHangDaMua.Rows[i].Cells["ConNo"].Value.ToString());
                //if (TienTraKyNay > 0 && TienTraKyNay <= TienConNo)
                if (TienTraKyNay > 0 )
                {
                    clsChiTietPhieuChiDTO CTTPCH = new clsChiTietPhieuChiDTO();
                    CTTPCH.MaPhieuChi = MaPhieuChi;
                    CTTPCH.PhieuNhap.MaPhieuNhap = grdvDSDonHangDaMua.Rows[i].Cells["MaPhieuNhap"].Value.ToString().Trim();
                    //Cap nhat lai so tien con no
                    CTTPCH.PhieuNhap.ConNo = TienConNo - TienTraKyNay;
                    CTTPCH.SoTien = TienTraKyNay;
                    DS_CTPCH.Add(CTTPCH);
                }
            }
            return DS_CTPCH;
        }

        private void LamTuoi()
        {
            try
            {
                txtTenNguoiNhan.Text = "";
                txtDiaChi.Text = "";
                txtMaSoThue.Text = "";
                txtLyDo.Text = "Trả tiền hàng";
                txtSoTien.Text = "0";
                txtSoTienBangChu.Text = "";
                txtTienTraKyNay.Text = "0";
                dtpNgayChi.Value = DateTime.Now;
                //cboNhaCungCap.SelectedIndex = -1;
                cboNhaCungCap.Enabled = true;
                grdvDSDonHangDaMua.Rows.Clear();
            }
            catch (Exception Loi)
            {

            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        //Xem thông tin phiếu chi
        /// <summary>
        /// Xem thông tin phiếu chi dựa vào mã phiếu chi hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaPhieuChi_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    XemPhieuChiTheoMaPhieuChi(txtMaPhieuChi.Text.Trim());
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Xin vui lòng thử nhập lại Phiếu chi khác!");
            }
        }

        private void XemPhieuChiTheoMaPhieuChi(string MaPhieuChi)
        {
           
            clsPhieuChiHangDTO PhieuChi = PhieuChiHangBus.LayThongTin(MaPhieuChi);
            if (PhieuChi != null)
            {
                AnCacVungNhapLieu(false);
                ThaoTac = "CapNhat";
                cboNhaCungCap.SelectedValue = PhieuChi.DS_ChiTietPhieuChi[0].PhieuNhap.NhaCungCap.MaNhaCungCap;
                LamTuoi();
                cboNhaCungCap.Enabled = false;
                txtMaPhieuChi.Text = MaPhieuChi;
                txtMaSoThue.Text = ((DataRowView)cboNhaCungCap.SelectedItem).Row["MaSoThue"].ToString();
                txtDiaChi.Text = ((DataRowView)cboNhaCungCap.SelectedItem).Row["DiaChi"].ToString();
                txtSoTien.Text = "0";
                txtTienTraKyNay.Text = "";
                dtpNgayChi.Value = PhieuChi.NgayChi;
                txtTenNguoiNhan.Text = PhieuChi.NguoiNhan;
                txtLyDo.Text = PhieuChi.LyDo;
                double TongTienDaTra = 0;
                for (int i = 0; i < PhieuChi.DS_ChiTietPhieuChi.Count; i++)
                {
                    object[] Dong = new object[9];
                    int STT = i + 1;
                    Dong[0] = STT.ToString();
                    Dong[1] = PhieuChi.DS_ChiTietPhieuChi[i].PhieuNhap.MaPhieuNhap;
                    Dong[2] = ChuyenDoiNgay(PhieuChi.DS_ChiTietPhieuChi[i].PhieuNhap.NgayNhap.ToShortDateString());
                    DateTime NgayNhap = PhieuChi.DS_ChiTietPhieuChi[i].PhieuNhap.NgayNhap;
                    int ThuoiNo = DateTime.Now.DayOfYear - NgayNhap.DayOfYear;
                    Dong[3] = ThuoiNo.ToString();
                    Dong[4] = clsSupport.CurrencyNumber(PhieuChi.DS_ChiTietPhieuChi[i].PhieuNhap.TongTien.ToString());
                    double DaTra = PhieuChi.DS_ChiTietPhieuChi[i].PhieuNhap.TongTien - PhieuChi.DS_ChiTietPhieuChi[i].PhieuNhap.ConNo;
                    Dong[5] = clsSupport.CurrencyNumber(DaTra.ToString());
                    Dong[6] = clsSupport.CurrencyNumber(PhieuChi.DS_ChiTietPhieuChi[i].PhieuNhap.ConNo.ToString());
                    Dong[7] = clsSupport.CurrencyNumber(PhieuChi.DS_ChiTietPhieuChi[i].SoTien.ToString());
                    Dong[8] = clsSupport.CurrencyNumber(PhieuChi.DS_ChiTietPhieuChi[i].SoTien.ToString());
                    TongTienDaTra += PhieuChi.DS_ChiTietPhieuChi[i].SoTien;
                    grdvDSDonHangDaMua.Rows.Add(Dong);
                }
                grdvDSDonHangDaMua.Columns["TraKyNay"].HeaderText = "Đã Trả Kỳ Này";
                txtSoTien.Text = TongTienDaTra.ToString();
                txtSoTienBangChu.Text = "(Viết bằng chữ): " + clsSupport.ConvertMoneyToText(TongTienDaTra.ToString());
            }
            else
            {
                LamTuoi();
                ThaoTac = "Them";
                grdvDSDonHangDaMua.Columns["TraKyNay"].HeaderText = "Trả Kỳ Này";
                AnCacVungNhapLieu(true);
            }
        }

        private void AnCacVungNhapLieu(Boolean isEnable)
        {
            //txtTienTraKyNay.Enabled = isEnable;
            //btnLuu.Enabled = isEnable;
            cboNhaCungCap.Enabled = isEnable;
            btnTimNCC.Enabled = isEnable;
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
                rptPhieuChi phieuChiHang = new rptPhieuChi();
                phieuChiHang.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                DataTable bang = PhieuChiHangBus.ReportPhieuChiHang(txtMaPhieuChi.Text.Trim());
                if (bang.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();

                    DataSet cacBang = new DataSet();
                    cacBang.Tables.Add(bang);
                    cacBang.Tables.Add(CongTy);

                    phieuChiHang.SetDataSource(cacBang);
                    phieuChiHang.SetParameterValue("@MaPhieuChi", txtMaPhieuChi.Text.Trim());
                    phieuChiHang.SetParameterValue("@TienBangChu", txtSoTienBangChu.Text.Trim());

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = phieuChiHang;
                    dlgHienThi.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có thông tin về phiếu chi này.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        
        }

        private void cboNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboNhaCungCap.SelectedItem != null && cboNhaCungCap.SelectedIndex != -1)
                {
                    HienThiCacPhieuNhapKho();
                }
            }
            catch (Exception loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}