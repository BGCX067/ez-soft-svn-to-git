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
    public partial class frmQuyTienMat : frmTemplete
    {
        #region Thuộc tính
        private clsCongTyBUS CongTyBus = new clsCongTyBUS();
        #endregion

        public frmQuyTienMat()
        {
            InitializeComponent();
        }

        private void frmQuyTienMat_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoComboLyDo();
                dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpDenNgay.Value = DateTime.Now;
                txtTonDauDy.Text ="0";
                txtTonCuoiKy.Text = "0";

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
        //Các lý do
        #region Các lý do
        //Lấy danh sách Các lý do
        private void KhoiTaoComboLyDo()
        {
            DataTable BangLyDo = CongTyBus.LayBangLyDo();
            DataRow DongTam1 = BangLyDo.NewRow();
            DongTam1["Ma"] = BangLyDo.Rows[0]["Ma"];
            DongTam1["LyDo"] = BangLyDo.Rows[0]["LyDo"];
            BangLyDo.Rows.Add(DongTam1);
            BangLyDo.Rows[0]["Ma"]=-2;
            BangLyDo.Rows[0]["LyDo"]="Tất cả";

            //DataRow DongTam2 = BangLyDo.NewRow();
            //DongTam2["Ma"] = BangLyDo.Rows[1]["Ma"];
            //DongTam2["LyDo"] = BangLyDo.Rows[1]["LyDo"];
            //BangLyDo.Rows.Add(DongTam2);
            //BangLyDo.Rows[1]["Ma"] = -1;
            //BangLyDo.Rows[1]["LyDo"] = "Tổng Thu";

            //DataRow DongTam3 = BangLyDo.NewRow();
            //DongTam3["Ma"] = BangLyDo.Rows[2]["Ma"];
            //DongTam3["LyDo"] = BangLyDo.Rows[2]["LyDo"];
            //BangLyDo.Rows.Add(DongTam3);
            //BangLyDo.Rows[2]["Ma"] = 0;
            //BangLyDo.Rows[2]["LyDo"] = "Tổng Chi";


            cboLyDo.DataSource = BangLyDo;
            cboLyDo.DisplayMember = "LyDo";
            cboLyDo.ValueMember = "LyDo";
            cboLyDo.SelectedIndex = 0;
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
                KhoiTao(((DataRowView)cboLyDo.SelectedItem).Row["LyDo"].ToString().Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KhoiTao(string LyDo)
        {
            grdvDSThuChi.Columns.Clear();
            if (grdvDSThuChi.Rows.Count > 0)
            {
                grdvDSThuChi.Rows.Clear();
            }
            DataTable Bang;
            if (LyDo == "Tất cả")
            {
                Bang = CongTyBus.LayBangChiTietThuChi(dtpTuNgay.Value, dtpDenNgay.Value,"TatCa",txtDoiTuong.Text.Trim());
            }
            else
            {
                Bang = CongTyBus.LayBangChiTietThuChi(dtpTuNgay.Value, dtpDenNgay.Value, LyDo, txtDoiTuong.Text.Trim());
            }
            Double TienTonDauKy = CongTyBus.LayTienTonDauKy();
            Double TienTonCuoiKy = TienTonDauKy;
            for (int i = 0; i < Bang.Rows.Count; i++)
            {
                Double TienThu = double.Parse(Bang.Rows[i]["Thu"].ToString());
                Double TienChi = double.Parse(Bang.Rows[i]["Chi"].ToString());
                TienTonCuoiKy = TienTonCuoiKy + TienThu - TienChi;
                Bang.Rows[i]["Ton"] = TienTonCuoiKy;
            }
            txtTonDauDy.Text = clsSupport.CurrencyNumber(TienTonDauKy.ToString());
            txtTonCuoiKy.Text = clsSupport.CurrencyNumber(TienTonCuoiKy.ToString());
            grdvDSThuChi.DataSource = Bang;
            AnCotTrenLuoi();
            DinhDangCot();
            btnInRa.Enabled = true;
        }

        private void AnCotTrenLuoi()
        {
           
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdvDSThuChi.Columns["Ngay"].HeaderText = "Ngày";

            grdvDSThuChi.Columns["SoPhieu"].HeaderText = "Số Phiếu";

            grdvDSThuChi.Columns["LoaiPhieu"].HeaderText = "Loại Phiếu";

            grdvDSThuChi.Columns["TenDoiTuong"].HeaderText = "Tên Đối Tượng";

            grdvDSThuChi.Columns["LyDo"].HeaderText = "Lý Do";

            grdvDSThuChi.Columns["Thu"].HeaderText = "Thu";
            grdvDSThuChi.Columns["Thu"].DefaultCellStyle = CellStyle;

            grdvDSThuChi.Columns["Chi"].HeaderText = "Chi";
            grdvDSThuChi.Columns["Chi"].DefaultCellStyle = CellStyle;

            grdvDSThuChi.Columns["Ton"].HeaderText = "Tồn";
            grdvDSThuChi.Columns["Ton"].DefaultCellStyle = CellStyle;

        }

        private void DinhDangCot()
        {
            grdvDSThuChi.Columns[0].Width = 40;
            DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();
            CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdvDSThuChi.Columns["STT"].DefaultCellStyle = CellStyle;
            for (int i = 1; i < grdvDSThuChi.Columns.Count; i++)
            {
                if (grdvDSThuChi.Columns[i].Visible == true)
                {
                    grdvDSThuChi.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grdvDSThuChi.Columns[i].ReadOnly = true;
                }
            }
            DataGridViewCellStyle CellStyleCurrency = new DataGridViewCellStyle();
            CellStyleCurrency.Alignment = DataGridViewContentAlignment.MiddleRight;
            CellStyleCurrency.Format = "#,##0.############";
            grdvDSThuChi.Columns["Thu"].DefaultCellStyle = CellStyleCurrency;
            grdvDSThuChi.Columns["Chi"].DefaultCellStyle = CellStyleCurrency;
            grdvDSThuChi.Columns["Ton"].DefaultCellStyle = CellStyleCurrency;

            DataGridViewCellStyle CellStyleDate = new DataGridViewCellStyle();
            CellStyleDate.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CellStyleDate.Format = "dd/MM/yyyy";
            grdvDSThuChi.Columns["Ngay"].DefaultCellStyle = CellStyleDate;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        private void btnSuaLai_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdvDSThuChi.CurrentRow!=null && grdvDSThuChi.CurrentRow.Index != -1)
                {
                    string LoaiPhieu = grdvDSThuChi.Rows[grdvDSThuChi.CurrentRow.Index].Cells["LoaiPhieu"].Value.ToString().Trim();
                    string SoPhieu = grdvDSThuChi.Rows[grdvDSThuChi.CurrentRow.Index].Cells["SoPhieu"].Value.ToString().Trim();
                    switch (LoaiPhieu)
                    {
                        case "Chi hàng":
                            frmTraTien F1 = new frmTraTien(SoPhieu);
                            F1.ShowDialog();
                            break;
                        case "Chi khác":
                            frmTraTienKhac F2 = new frmTraTienKhac(SoPhieu);
                            F2.ShowDialog();
                            break;
                        case "Thu bán hàng":
                            frmThuTien F3 = new frmThuTien(SoPhieu);
                            F3.ShowDialog();
                            break;
                        case "Thu khác":
                            frmThuTienKhac F4 = new frmThuTienKhac(SoPhieu);
                            F4.ShowDialog();
                            break;
                    }
                    HienThiTimKiem();
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn phiếu thu/chi muốn cập nhật!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdvDSThuChi.CurrentRow != null && grdvDSThuChi.CurrentRow.Index != -1)
                {
                    string LoaiPhieu = grdvDSThuChi.Rows[grdvDSThuChi.CurrentRow.Index].Cells["LoaiPhieu"].Value.ToString().Trim();
                    string SoPhieu = grdvDSThuChi.Rows[grdvDSThuChi.CurrentRow.Index].Cells["SoPhieu"].Value.ToString().Trim();
                    DialogResult result = MessageBox.Show("Bạn có thật sự muốn hủy phiếu thu/chi " + SoPhieu + " (Chú ý: Khi hủy phiếu thu/chi thì công nợ sẽ được phục hồi", "Xác nhận thông tin", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        switch (LoaiPhieu)
                        {
                            case "Chi hàng":
                                if (new clsPhieuChiHangBUS().Huy(SoPhieu) != -1)
                                {
                                    grdvDSThuChi.Rows.RemoveAt(grdvDSThuChi.CurrentRow.Index);
                                }
                                else
                                {
                                    MessageBox.Show("Hủy phiếu Chi " + SoPhieu + " không thành công. Xin vui lòng thử lại", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                break;
                            case "Chi khác":
                                if (new clsPhieuChiKhacBUS().Xoa(SoPhieu) != -1)
                                {
                                    grdvDSThuChi.Rows.RemoveAt(grdvDSThuChi.CurrentRow.Index);
                                }
                                else
                                {
                                    MessageBox.Show("Hủy phiếu Chi " + SoPhieu + " không thành công. Xin vui lòng thử lại", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                break;
                            case "Thu bán hàng":
                                if (new clsPhieuThuBanHangBUS().Huy(SoPhieu) != -1)
                                {
                                    grdvDSThuChi.Rows.RemoveAt(grdvDSThuChi.CurrentRow.Index);
                                }
                                else
                                {
                                    MessageBox.Show("Hủy phiếu Chi " + SoPhieu + " không thành công. Xin vui lòng thử lại", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                break;
                            case "Thu khác":
                                if (new clsPhieuThuKhacBUS().Xoa(SoPhieu) != -1)
                                {
                                    grdvDSThuChi.Rows.RemoveAt(grdvDSThuChi.CurrentRow.Index);
                                }
                                else
                                {
                                    MessageBox.Show("Hủy phiếu Chi " + SoPhieu + " không thành công. Xin vui lòng thử lại", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                break;
                        }
                        HienThiTimKiem();
                    }
                }
                else
                {
                    MessageBox.Show("Xin vui lòng chọn phiếu thu/chi muốn cập nhật!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (grdvDSThuChi.RowCount == 0)
                {
                    MessageBox.Show("In không thành công vì không có phiếu nào trong khoảng thời gian trên.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                frmTheHienReport dlgHienThi = new frmTheHienReport();
                rptQuyTienMat quyTienMat = new rptQuyTienMat();
                quyTienMat.SetDatabaseLogon(clsConnection.LoginName, clsConnection.LoginPassword, clsConnection.ServerName, clsConnection.Databasename);

                string LyDo = ((DataRowView)cboLyDo.SelectedItem).Row["LyDo"].ToString().Trim();
                DataTable Bang;

                if (LyDo == "Tất cả")
                {
                    Bang = CongTyBus.ReportQuyTienMat(dtpTuNgay.Value, dtpDenNgay.Value, "TatCa", txtDoiTuong.Text.Trim());
                }
                else
                {
                    Bang = CongTyBus.ReportQuyTienMat(dtpTuNgay.Value, dtpDenNgay.Value, LyDo, txtDoiTuong.Text.Trim());
                }
                Double TienTonDauKy = double.Parse(txtTonDauDy.Text);
                for (int i = 0; i < Bang.Rows.Count; i++)
                {
                    Double TienThu = double.Parse(Bang.Rows[i]["Thu"].ToString());
                    Double TienChi = double.Parse(Bang.Rows[i]["Chi"].ToString());
                    TienTonDauKy = TienTonDauKy + TienThu - TienChi;
                    Bang.Rows[i]["Ton"] = TienTonDauKy;
                }

                if (Bang.Rows.Count != 0)
                {
                    DataTable CongTy = CongTyBus.ReportCongTy();
                    DataSet cacBang = new DataSet();
                    cacBang.Tables.Add(Bang);
                    cacBang.Tables.Add(CongTy);

                    quyTienMat.SetDataSource(cacBang);
                    quyTienMat.SetParameterValue("@TuNgay", dtpTuNgay.Value);
                    quyTienMat.SetParameterValue("@DenNgay", dtpDenNgay.Value);
                    quyTienMat.SetParameterValue("@LyDo", LyDo);
                    quyTienMat.SetParameterValue("@TenDoiTuong", txtDoiTuong.Text.Trim());


                    quyTienMat.SetParameterValue("@TonDau", txtTonDauDy.Text.Trim());
                    quyTienMat.SetParameterValue("@TonCuoi", txtTonCuoiKy.Text.Trim());
                    quyTienMat.SetParameterValue("@NgayTu", dtpTuNgay.Value);
                    quyTienMat.SetParameterValue("@NgayDen", dtpDenNgay.Value);

                    dlgHienThi.CrystalReportViewer_TheHien.ReportSource = quyTienMat;
                    dlgHienThi.ShowDialog();
                }
                else
                    MessageBox.Show("Không có phiếu nào trong khoảng thời gian trên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }     
        }

    }
}