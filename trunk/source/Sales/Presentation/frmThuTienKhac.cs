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
    public partial class frmThuTienKhac : Form
    {
        #region Thuộc tính
        private clsPhieuThuKhacBUS PhieuThuKhacBus = new clsPhieuThuKhacBUS();
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        public string ThaoTac = "Them";
        public string MaPhieuThuCanXem = null;
        #endregion

        public frmThuTienKhac()
        {
            InitializeComponent();
        }

        public frmThuTienKhac(string _MaPhieuThu)
        {
            MaPhieuThuCanXem = _MaPhieuThu;
            InitializeComponent();
        }
        private void frmThuTienKhac_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboLyDo();
                dtpNgayThu.Value = DateTime.Now;
                txtSoTienBangChu.Text = "(Viết bằng chữ): không đồng ";
                if (MaPhieuThuCanXem != null)
                {
                    txtMaPhieuThu.Text = MaPhieuThuCanXem;
                    XemPhieuThuTheoMaPhieuThu(MaPhieuThuCanXem);
                }
                else
                {
                    txtMaPhieuThu.Text = PhieuThuKhacBus.LayMaPhieuThuMoi();
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
            DataTable BangLyDo = PhieuThuKhacBus.LayBangLyDo();
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
                clsPhieuThuKhacDTO PhieuThu = KhoiTaoPhieuThu(ref Loi);
                if (PhieuThu != null)
                {
                    Loi = "Lỗi kết nối cơ sở dữ liệu";
                    if (ThaoTac == "Them")
                    {
                        if (PhieuThuKhacBus.LayThongTin(PhieuThu.MaPhieuThu) == null)//Phieu thu hang nay chua ton tai
                        {
                            if (PhieuThuKhacBus.Them(PhieuThu) != -1)
                            {

                                //MessageBox.Show("Lưu phiếu thu " + PhieuThu.MaPhieuThu + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DialogResult result = MessageBox.Show("Lưu phiếu thu " + PhieuThu.MaPhieuThu + " thành công! Bạn có muốn In phiếu xuất này không?", "Xac nhan", MessageBoxButtons.YesNo);
                                if (result == DialogResult.Yes)
                                {
                                    // In hoa don ban si
                                    In();
                                }
                                LamTuoi();
                                txtMaPhieuThu.Text = PhieuThuKhacBus.LayMaPhieuThuMoi();
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
                        if (PhieuThuKhacBus.LayThongTin(PhieuThu.MaPhieuThu) != null)
                        {
                            DialogResult result = MessageBox.Show("Bạn có thật sự muốn cập nhật phiếu Thu " + PhieuThu.MaPhieuThu + " không?", "Xác nhận thông tin", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                if (PhieuThuKhacBus.Sua(PhieuThu) != -1)
                                {
                                    //MessageBox.Show("Lưu phiếu thu " + PhieuThu.MaPhieuThu + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    DialogResult result1 = MessageBox.Show("Lưu phiếu thu " + PhieuThu.MaPhieuThu + " thành công! Bạn có muốn In phiếu xuất này không?", "Xac nhan", MessageBoxButtons.YesNo);
                                    if (result1 == DialogResult.Yes)
                                    {
                                        // In hoa don ban si
                                        In();
                                    }
                                    LamTuoi();
                                    txtMaPhieuThu.Text = PhieuThuKhacBus.LayMaPhieuThuMoi();
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
        private clsPhieuThuKhacDTO KhoiTaoPhieuThu(ref string Loi)
        {
            clsPhieuThuKhacDTO PhieuThu = new clsPhieuThuKhacDTO();
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
                        txtMaPhieuThu.Focus();
                        return null;
                    }
                }
                else
                {
                    Loi = "Xin vui lòng nhập Phiếu thu có dạng như sau: PT + Số thứ tự (Số nguyên dương) vd: PT1, PT2,... !";
                    txtMaPhieuThu.Focus();
                    return null;
                }
            }
            if (txtTenNguoiNop.Text.Trim() == "")
            {
                Loi = "Xin vui lòng nhập họ tên người nộp tiền";
                txtTenNguoiNop.Focus();
                return null;
            }
            else
            {
                PhieuThu.NguoiNop = txtTenNguoiNop.Text.Trim();
            }
            PhieuThu.NgayThu = dtpNgayThu.Value;
            PhieuThu.SoTien = double.Parse(txtSoTien.Text);
            PhieuThu.LyDo = cboLyDo.Text;
            PhieuThu.DiaChi = txtDiaChi.Text;
            return PhieuThu;
        }

        private void LamTuoi()
        {
            try
            {
                txtTenNguoiNop.Text = "";
                txtDiaChi.Text = "";
                cboLyDo.SelectedIndex = 0;
                txtSoTien.Text = "0";
                txtSoTienBangChu.Text = "(Viết bằng chữ): không đồng ";
                dtpNgayThu.Value = DateTime.Now;
            }
            catch (Exception Loi)
            {

            }

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

        private void XemPhieuThuTheoMaPhieuThu(string MaPhieuThu)
        {
            LamTuoi();
            clsPhieuThuKhacDTO PhieuThu = PhieuThuKhacBus.LayThongTin(MaPhieuThu);
            if (PhieuThu != null)
            {
                ThaoTac = "CapNhat";
                cboLyDo.SelectedValue = PhieuThu.LyDo;
                txtDiaChi.Text = PhieuThu.DiaChi;
                dtpNgayThu.Value = PhieuThu.NgayThu;
                txtTenNguoiNop.Text = PhieuThu.NguoiNop;
                txtSoTien.Text = PhieuThu.SoTien.ToString();
                txtSoTienBangChu.Text = "(Viết bằng chữ): " + clsSupport.ConvertMoneyToText(PhieuThu.SoTien.ToString());
            }
            else
            {
                ThaoTac = "Them";
                MessageBox.Show("Phiếu thu khác này không tồn tại!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
                rptPhieuThuKhac phieuThuKhac = new rptPhieuThuKhac();
                phieuThuKhac.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                DataTable bang = PhieuThuKhacBus.ReportPhieuThuKhac(txtMaPhieuThu.Text.Trim());

                if (bang.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();
                    DataSet cacBang = new DataSet();
                    cacBang.Tables.Add(bang);
                    cacBang.Tables.Add(CongTy);

                    phieuThuKhac.SetDataSource(cacBang);
                    phieuThuKhac.SetParameterValue("@MaPhieuThu", txtMaPhieuThu.Text.Trim());
                    phieuThuKhac.SetParameterValue("@TienBangChu", txtSoTienBangChu.Text.Trim());

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = phieuThuKhac;
                    dlgHienThi.ShowDialog();
                }
                else
                    MessageBox.Show("Không có thông tin về phiếu thu " + txtMaPhieuThu.Text.Trim(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}