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
    public partial class frmTraTienKhac : Form
    {
        #region Thuộc tính
        private clsPhieuChiKhacBUS PhieuChiKhacBus = new clsPhieuChiKhacBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        public string ThaoTac = "Them";
        public string MaPhieuChiCanXem = null;
        #endregion

        public frmTraTienKhac()
        {
            InitializeComponent();
        }

        public frmTraTienKhac(string _MaPhieuChi)
        {
            MaPhieuChiCanXem=_MaPhieuChi;
            ThaoTac = "CapNhat";
            InitializeComponent();
        }

        private void frmTraTienKhac_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboLyDo();
                dtpNgayChi.Value = DateTime.Now;
                txtSoTienBangChu.Text = "(Viết bằng chữ): không đồng ";
                if (MaPhieuChiCanXem != null)
                {
                    txtMaPhieuChi.Text = MaPhieuChiCanXem;
                    XemPhieuChiTheoMaPhieuChi(MaPhieuChiCanXem);
                }
                else
                {
                    txtMaPhieuChi.Text = PhieuChiKhacBus.LayMaPhieuChiMoi();
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

        //Các lý do
        #region Các lý do
        //Lấy danh sách Các lý do
        private void KhoiTaoComboLyDo()
        {
            DataTable BangLyDo = PhieuChiKhacBus.LayBangLyDo();
            cboLyDo.DataSource = BangLyDo;
            cboLyDo.DisplayMember = "LyDo";
            cboLyDo.ValueMember = "LyDo";
        }
        #endregion

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string Loi = "";
            try
            {
                Loi = "Xin vui lòng kiểm tra lại dữ liệu nhập";
                clsPhieuChiKhacDTO PhieuChi = KhoiTaoPhieuChi(ref Loi);
                if (PhieuChi != null)
                {
                    Loi = "Lỗi kết nối cơ sở dữ liệu";
                    if (ThaoTac == "Them")
                    {
                        if (PhieuChiKhacBus.LayThongTin(PhieuChi.MaPhieuChi) == null)//Phieu chi hang nay chua ton tai
                        {
                            if (PhieuChiKhacBus.Them(PhieuChi) != -1)
                            {

                                //MessageBox.Show("Lưu phiếu chi " + PhieuChi.MaPhieuChi + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DialogResult result = MessageBox.Show("Lưu phiếu chi tiền " + txtMaPhieuChi.Text + " thành công! Bạn có muốn In phiếu chi tiền này không?", "Xac nhan", MessageBoxButtons.YesNo);
                                if (result == DialogResult.Yes)
                                {
                                    //In hoa don ban le
                                    In();
                                }
                                LamTuoi();
                                txtMaPhieuChi.Text = PhieuChiKhacBus.LayMaPhieuChiMoi();
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
                        if (PhieuChiKhacBus.LayThongTin(PhieuChi.MaPhieuChi) != null)
                        {
                            DialogResult result = MessageBox.Show("Bạn có thật sự muốn cập nhật phiếu Chi " + PhieuChi.MaPhieuChi + " không?", "Xác nhận thông tin", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                if (PhieuChiKhacBus.Sua(PhieuChi) != -1)
                                {
                                   // MessageBox.Show("Lưu phiếu chi " + PhieuChi.MaPhieuChi + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    DialogResult result1 = MessageBox.Show("Lưu phiếu chi tiền " + txtMaPhieuChi.Text + " thành công! Bạn có muốn In phiếu chi tiền này không?", "Xac nhan", MessageBoxButtons.YesNo);
                                    if (result1 == DialogResult.Yes)
                                    {
                                        //In hoa don ban le
                                        In();
                                    }
                                    LamTuoi();
                                    txtMaPhieuChi.Text = PhieuChiKhacBus.LayMaPhieuChiMoi();
                                    //AnCacVungNhapLieu(true);
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
        private clsPhieuChiKhacDTO KhoiTaoPhieuChi(ref string Loi)
        {
            clsPhieuChiKhacDTO PhieuChi = new clsPhieuChiKhacDTO();
            PhieuChi.NguoiChi = clsUser.MaNhanVien;
            if (txtSoTien.Text.Trim() == "0")
            {
                Loi = "Xin vui lòng nhập số tiền muốn trả!";
                txtSoTien.Focus();
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
                        txtMaPhieuChi.Focus();
                        return null;
                    }
                }
                else
                {
                    Loi = "Xin vui lòng nhập Phiếu chi có dạng như sau: PC + Số thứ tự (Số nguyên dương) vd: PC1, PC2,... !";
                    txtMaPhieuChi.Focus();
                    return null;
                }
            }
            if (txtTenNguoiTra.Text.Trim() == "")
            {
                Loi = "Xin vui lòng nhập họ Tên người cần trả.";
                txtTenNguoiTra.Focus();
                return null;
            }
            else
            {
                PhieuChi.NguoiNhan = txtTenNguoiTra.Text.Trim();
            }
            PhieuChi.NgayChi = dtpNgayChi.Value;
            PhieuChi.SoTien = double.Parse(txtSoTien.Text);

            PhieuChi.LyDo = cboLyDo.Text;
            PhieuChi.DiaChi = txtDiaChi.Text;
            return PhieuChi;
        }

        private void LamTuoi()
        {
            KhoiTaoComboLyDo();
            dtpNgayChi.Value = DateTime.Now;
            txtSoTien.Text = "0";
            txtSoTienBangChu.Text = "(Viết bằng chữ): không đồng ";
            txtTenNguoiTra.Text = "";
            txtDiaChi.Text = "";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        private void txtSoTien_TextChanged(object sender, EventArgs e)
        {
            if (txtSoTien.Text != "")
            {
                txtSoTienBangChu.Text = "(Viết bằng chữ): " + clsSupport.ConvertMoneyToText(double.Parse(txtSoTien.Text.ToString()).ToString());
            }
            else
            {
                txtSoTienBangChu.Text = "(Viết bằng chữ): " + clsSupport.ConvertMoneyToText("0");
            }
        }

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
                MessageBox.Show("Xin vui lòng thử nhập lại Phiếu chi khác!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XemPhieuChiTheoMaPhieuChi(string MaPhieuChi)
        {
            LamTuoi();
            clsPhieuChiKhacDTO PhieuChi = PhieuChiKhacBus.LayThongTin(MaPhieuChi);
            if (PhieuChi != null)
            {
                ThaoTac = "CapNhat";
                cboLyDo.SelectedValue = PhieuChi.LyDo;
                txtDiaChi.Text = PhieuChi.DiaChi;
                txtSoTien.Text = "0";
                dtpNgayChi.Value = PhieuChi.NgayChi;
                txtTenNguoiTra.Text = PhieuChi.NguoiNhan;
                txtSoTien.Text = PhieuChi.SoTien.ToString();
                txtSoTienBangChu.Text = "(Viết bằng chữ): " + clsSupport.ConvertMoneyToText(PhieuChi.SoTien.ToString());
            
            }
            else
            {
                ThaoTac = "Them";
                MessageBox.Show("Phiếu chi khác này không tồn tại!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptPhieuChiKhac phieuChiKhac = new rptPhieuChiKhac();
                phieuChiKhac.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                DataTable bang = PhieuChiKhacBus.ReportPhieuChiKhac(txtMaPhieuChi.Text.Trim());
                if (bang.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();

                    DataSet cacBang = new DataSet();
                    cacBang.Tables.Add(bang);
                    cacBang.Tables.Add(CongTy);

                    phieuChiKhac.SetDataSource(cacBang);
                    phieuChiKhac.SetParameterValue("@MaPhieuChi", txtMaPhieuChi.Text.Trim());
                    phieuChiKhac.SetParameterValue("@TienBangChu", txtSoTienBangChu.Text.Trim());

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = phieuChiKhac;
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
    }
}