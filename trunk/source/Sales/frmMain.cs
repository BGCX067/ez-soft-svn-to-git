using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using control.toolbar;

namespace Sales
{
    public partial class frmMain : Form
    {
        #region Attribute

        ToolBarManager _toolBarManager;
        private StatusBarPanel sbPnlVungTrong = new StatusBarPanel();
        private StatusBarPanel sbPnlHoTen = new StatusBarPanel();
        private StatusBarPanel sbPnlTenDangNhap = new StatusBarPanel();
        private StatusBarPanel sbPnlNgay = new StatusBarPanel();
        private StatusBarPanel sbPnlGio = new StatusBarPanel();
        Timer BoDinhGio;
        StatusBar statusBar;
        private bool _Trial;

        #endregion

        private void initToolBarManager()
        {
            BoDinhGio = new Timer();
            BoDinhGio.Interval = 60000;
            BoDinhGio.Enabled = true;
            BoDinhGio.Tick += new EventHandler(BoDinhGio_Tick);

            _toolBarManager = new ToolBarManager(this, this);
            panelTop.AutoSize = true;
            panelTop.Size = new Size(this.Width, 44);
            _toolBarManager.AddControl(panelTop, DockStyle.Top);    
            statusBar = new StatusBar();
            statusBar.ShowPanels = true;
            statusBar.Panels.AddRange((StatusBarPanel[])new StatusBarPanel[] {sbPnlVungTrong, sbPnlHoTen, sbPnlTenDangNhap, sbPnlNgay, sbPnlGio });

            sbPnlVungTrong.BorderStyle = StatusBarPanelBorderStyle.Raised;
            sbPnlVungTrong.AutoSize = StatusBarPanelAutoSize.Spring;
            //sbPnlHoTen.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            
            sbPnlHoTen.BorderStyle = StatusBarPanelBorderStyle.Raised;
            //sbPnlHoTen.AutoSize = StatusBarPanelAutoSize.Spring;
            //sbPnlHoTen.Alignment = System.Windows.Forms.HorizontalAlignment.Right;

            sbPnlTenDangNhap.BorderStyle = StatusBarPanelBorderStyle.Raised;
            //sbPnlTenDangNhap.AutoSize = StatusBarPanelAutoSize.Spring;
           // sbPnlHoTen.Alignment = System.Windows.Forms.HorizontalAlignment.Right;

            sbPnlNgay.BorderStyle = StatusBarPanelBorderStyle.Raised;
           //sbPnlNgay.AutoSize = StatusBarPanelAutoSize.Spring;
           // sbPnlNgay.Alignment = System.Windows.Forms.HorizontalAlignment.Right;

            sbPnlGio.BorderStyle = StatusBarPanelBorderStyle.Raised;
           //sbPnlGio.AutoSize = StatusBarPanelAutoSize.Spring;
           // sbPnlGio.Alignment = System.Windows.Forms.HorizontalAlignment.Right;

            this.Controls.Add(statusBar);
        }

        private void BoDinhGio_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
            {
                sbPnlNgay.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            }
            DateTime Gio = DateTime.Now;
            sbPnlGio.Text = Gio.ToShortTimeString(); 
        }

        public frmMain()
        {
            InitializeComponent();
            initToolBarManager();
            
        }

        public frmMain(bool IsTrial)
        {          
            InitializeComponent();
            initToolBarManager();
            if (IsTrial == false)
            {
                //Đã đăng ký 
            }

            _Trial = IsTrial;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
           
           clsConnection.ReadKeys();
            if (clsConnection.ServerName == null || clsConnection.Databasename == null || clsConnection.LoginName == null || clsConnection.LoginPassword == null)
            {

                frmLoginServer F = new frmLoginServer();
                F.ShowDialog();
                if (frmLoginServer.isConnect == false)
                {
                    Application.Exit();
                }
                else
                {
                    DangNhapHeThong();
                }
            }
            else
            {
                if (clsConnection.IsCheckConnect(clsConnection.ServerName, clsConnection.Databasename, clsConnection.LoginName, clsConnection.LoginPassword) == false)
                {
                    frmLoginServer F = new frmLoginServer();
                    F.ShowDialog();
                    if (frmLoginServer.isConnect == false)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        DangNhapHeThong();
                    }
                }
                else
                {
                    DangNhapHeThong();
                }
            }
            }
            catch (Exception Loi)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DangNhapHeThong()
        {
            frmLogin F = new frmLogin();
            F.ShowDialog();
            if (frmLogin.isLogin == true)
            {
                treeViewChucNang.SelectedNode = treeViewChucNang.Nodes[0];
                treeViewChucNang.SelectedNode.Expand();
                // hạ cờ các chức năng trước của người dùng
                ClearAllTooltipTreeNode();
                this.FormClosing += new FormClosingEventHandler(frmMain_FormClosing);
                //Lấy thông tin người dùng và chức năng phân quyền của người đó
                clsNhanVienDTO NguoiDung = frmLogin.User.LayThongTinNguoiDung();
                //Duyệt và bật cờ các chức năng của người dùng  này được cấp
                for (int k = 0; k < NguoiDung.QuyenHan.DS_PhanQuyenChucNang.Count; k++)
                {
                    for (int i = 0; i < treeViewChucNang.Nodes[0].Nodes.Count; i++)
                    {
                        for (int j = 0; j < treeViewChucNang.Nodes[0].Nodes[i].Nodes.Count; j++)
                        {
                            if (treeViewChucNang.Nodes[0].Nodes[i].Nodes[j].Text.Trim().ToLower() == NguoiDung.QuyenHan.DS_PhanQuyenChucNang[k].ChucNang.TenChucNang.Trim().ToLower())
                            {
                                treeViewChucNang.Nodes[0].Nodes[i].Nodes[j].ToolTipText = treeViewChucNang.Nodes[0].Nodes[i].Nodes[j].Text.Trim();
                                break;
                            }
                        }
                    }
                }
                //Xóa các chức năng người dùng này không có quyền hạn
                PhanQuyenChucNang();
                //Xóa đi các nút cha trống
                RemoveNodeEmpty();
                sbPnlHoTen.Text = NguoiDung.TenNhanVien;
                sbPnlTenDangNhap.Text = NguoiDung.TenNguoiDung;
                DateTime Gio = DateTime.Now;
                sbPnlGio.Text = Gio.ToShortTimeString();
                sbPnlNgay.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            }
            else
            {
                Application.Exit();
            }
        }

        private void ClearAllTooltipTreeNode()
        {
            for (int i = 0; i < treeViewChucNang.Nodes[0].Nodes.Count; i++)
            {
                for (int j = 0; j < treeViewChucNang.Nodes[0].Nodes[i].Nodes.Count; j++)
                {
                    //if (treeViewChucNang.Nodes[j].Text.Trim() == "Bán Hàng" || treeViewChucNang.Nodes[j].Text.Trim() == "Mua Hàng" ||
                    //    treeViewChucNang.Nodes[j].Text.Trim() == "Quản Lý Kho" || treeViewChucNang.Nodes[j].Text.Trim() == "Quản Lý Tiền" ||
                    //    treeViewChucNang.Nodes[j].Text.Trim() == "Thiết Lập" || treeViewChucNang.Nodes[j].Text.Trim() == "Quản lý bán hàng")
                    //{
                    //    continue;
                    //}
                    //else
                    //{
                    treeViewChucNang.Nodes[0].Nodes[i].Nodes[j].ToolTipText = "";
                    //}
                }
            }
        }

        private void PhanQuyenChucNang()
        {
            for (int i = 0; i < treeViewChucNang.Nodes[0].Nodes.Count; i++)
            {
                for (int j = 0; j < treeViewChucNang.Nodes[0].Nodes[i].Nodes.Count; j++)
                {
                    if (treeViewChucNang.Nodes[0].Nodes[i].Nodes[j].ToolTipText.Trim() == "")
                    {
                        treeViewChucNang.Nodes[0].Nodes[i].Nodes[j].Remove();
                        i--;
                        break;
                    }
                }
            }
        }

        private void RemoveNodeEmpty()
        {
            for (int i = 0; i < treeViewChucNang.Nodes[0].Nodes.Count; i++)
            {
                if (treeViewChucNang.Nodes[0].Nodes[i].Nodes.Count == 0)
                {
                    treeViewChucNang.Nodes[0].Nodes[i].Remove();
                    i = 0;
                }
            }
        }

        public Form getForm(string formName)
        {
            bool Result = false;
            int i = 0;
            //Đóng tất cả các form còn lại
             int j = 0;
            while (j < this.MdiChildren.Length)
            {
                this.MdiChildren[j].Close() ;
                j++;
            }
            //Mở form hiện hành đang chọn
            while (i < this.MdiChildren.Length && Result == false)
            {
                if (this.MdiChildren[i].Name == formName)
                    Result = true;
                else i++;
            }
            if (Result) return this.MdiChildren[i];
            else return (Form)null;
        }

        private void treeViewChucNang_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //==START:  Minh edit 06/09/2009 -> Bo Phan Quyen Chuc Nang nay cho mat hang __________________________________________________________
            try
            {
                if (treeViewChucNang.SelectedNode.Tag != null)
                {
                    string tag = treeViewChucNang.SelectedNode.Tag.ToString();
                    switch (tag)
                    {
                        #region minhnb
                        //==START:  Minh edit 08/09/2009 -> Ban Hang __________________________________________________________
                        case "TagBanLe":
                            showFormBanLe();
                            break;
                        case "TagBanSi":
                            showFormBanSi();
                            break;
                        case "TagThuTien":
                            showFormThuTien();
                            break;
                        case "TagKhachHang":
                            showFormKhachHang();
                            break;
                        case "TagMatHangBan":
                            showFormMatHangBan();
                            break;
                        case "TagXemDonHangDaBan":
                            showFormXemDonHangDaBan();
                            break;
                        case "TagDanhSachPhieuThu":
                            showFormDSPhieuThu();
                            break;
                        case "TagDoanhThuTheoNVBH":
                            showFormDoanhThuTheoNVBH();
                            break;
                        case "TagCongNoKhachHang":
                            showFormCongNoKhachHang();
                            break;
                        case "TagDoanhThu_ChiPhi":
                            showFormDoanhThu_ChiPhi();
                            break;
                        case "TagSanPhamBanChay":
                            showFormSanPhamBanChay();
                            break;
                        case "TagNhapHangVaoKho":
                            showFormNhapHangVaoKho();
                            break;
                        case "TagTraTien":
                            showFormTraTien();
                            break;
                        case "TagNhaCungCap":
                            showFormNhaCungCap();
                            break;
                        case "TagMatHangMua":
                            showFormMatHangMua();
                            break;
                        case "TagDonHangDaMua":
                            showFormDonHangDaMua();
                            break;
                        case "TagDanhSachPhieuChi":
                            showFormDanhSachPhieuChi();
                            break;
                        case "TagDonDatHang":
                            showFormDonDatHang();
                            break;
                        case "TagCongNoNhaCungCap":
                            showFormCongNoNhaCungCap();
                            break;
                        case "TagThietLapDinhMucHang":
                            showFormThietLapDinhMuc();
                            break;
                        case "TagChiTietHangNhap":
                            showFormChiTietHangNhap();
                            break;
                        case "TagChiTietHangXuat":
                            showFormChiTietHangXuat();
                            break;
                        case "TagTonKhoHangHoa":
                            showFormTonKhoHangHoa();
                            break;
                        case "TagQuanLyDonHangDaBan":
                            showFormQuanLyDonHangDaBan();
                            break;
                        case "TagTienQuyDauKy":
                            showTienQuyDauKy();
                            break;
                        case "TagThuTienKhac":
                            showThuTienKhac();
                            break;
                        case "TagTraTienKhac":
                            showTraTienKhac();
                            break;
                        case "TagQuyTienMat":
                            showQuyTienMat();
                            break;
                        case "TagNhanVien":
                            showNhanVien();
                            break;
                        case "TagNhomHang":
                            showNhomHang();
                            break;
                        case "TagThongTinCongTy":
                            showThongTinCongTy();
                            break;
                        case "TagPhanQuyen":
                            showPhanQuyen();
                            break;
                        case "TagSaoLuuDuPhong":
                            showSaoLuuDuPhong();
                            break;
                        case "TagKhoiPhucDuLieu":
                            showPhucHoiDuLieu();
                            break;
                        case "TagKetNoiCSDL":
                            showKetNoiCSDL();
                            break;
                        //==END:  Minh edit 08/09/2009 -> Ban Hang __________________________________________________________
                        #endregion 

                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            //==END:  Minh edit 06/09/2009 -> Bo Phan Quyen Chuc Nang nay cho mat hang __________________________________________________________
        }
        
        #region minhnb
        //Ban hang
        public frmBanLe showFormBanLe()
        {
            frmBanLe F = (frmBanLe)getForm("frmBanLe");
            if (F == null)
            {
                F = new frmBanLe();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmBanSi showFormBanSi()
        {
            frmBanSi F = (frmBanSi)getForm("frmBanSi");
            if (F == null)
            {
                F = new frmBanSi();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmThuTien showFormThuTien()
        {
            frmThuTien F = (frmThuTien)getForm("frmThuTien");
            if (F == null)
            {
                F = new frmThuTien();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmKhachHang showFormKhachHang()
        {
            frmKhachHang F = (frmKhachHang)getForm("frmKhachHang");
            if (F == null)
            {
                F = new frmKhachHang();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmMatHangBan showFormMatHangBan()
        {
            frmMatHangBan F = (frmMatHangBan)getForm("frmMatHangBan");
            if (F == null)
            {
                F = new frmMatHangBan();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmMatHangMua showFormMatHangMua()
        {
            frmMatHangMua F = (frmMatHangMua)getForm("frmMatHangMua");
            if (F == null)
            {
                F = new frmMatHangMua();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmXemDonHangDaBan showFormXemDonHangDaBan()
        {
            frmXemDonHangDaBan F = (frmXemDonHangDaBan)getForm("frmXemDonHangDaBan");
            if (F == null)
            {
                F = new frmXemDonHangDaBan();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmDSPhieuThu showFormDSPhieuThu()
        {
            frmDSPhieuThu F = (frmDSPhieuThu)getForm("frmDSPhieuThu");
            if (F == null)
            {
                F = new frmDSPhieuThu();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmDoanhThuTheoNVBH showFormDoanhThuTheoNVBH()
        {
            frmDoanhThuTheoNVBH F = (frmDoanhThuTheoNVBH)getForm("frmDoanhThuTheoNVBH");
            if (F == null)
            {
                F = new frmDoanhThuTheoNVBH();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmCongNoKhachHang showFormCongNoKhachHang()
        {
            frmCongNoKhachHang F = (frmCongNoKhachHang)getForm("frmCongNoKhachHang");
            if (F == null)
            {
                F = new frmCongNoKhachHang();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmDoanhThu_ChiPhi showFormDoanhThu_ChiPhi()
        {
            frmDoanhThu_ChiPhi F = (frmDoanhThu_ChiPhi)getForm("frmDoanhThu_ChiPhi");
            if (F == null)
            {
                F = new frmDoanhThu_ChiPhi();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmSanPhamBanChay showFormSanPhamBanChay()
        {
            frmSanPhamBanChay F = (frmSanPhamBanChay)getForm("frmSanPhamBanChay");
            if (F == null)
            {
                F = new frmSanPhamBanChay();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        //Mua hang 
        public frmNhapHangVaoKho showFormNhapHangVaoKho()
        {
            frmNhapHangVaoKho F = (frmNhapHangVaoKho)getForm("frmNhapHangVaoKho");
            if (F == null)
            {
                F = new frmNhapHangVaoKho();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmTraTien showFormTraTien()
        {
            frmTraTien F = (frmTraTien)getForm("frmTraTien");
            if (F == null)
            {
                F = new frmTraTien();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmNhaCungCap showFormNhaCungCap()
        {
            frmNhaCungCap F = (frmNhaCungCap)getForm("frmNhaCungCap");
            if (F == null)
            {
                F = new frmNhaCungCap();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmDonHangDaMua showFormDonHangDaMua()
        {
            frmDonHangDaMua F = (frmDonHangDaMua)getForm("frmDonHangDaMua");
            if (F == null)
            {
                F = new frmDonHangDaMua();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmDSPhieuChi showFormDanhSachPhieuChi()
        {
            frmDSPhieuChi F = (frmDSPhieuChi)getForm("frmDSPhieuChi");
            if (F == null)
            {
                F = new frmDSPhieuChi();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmDonDatHang showFormDonDatHang()
        {
            frmDonDatHang F = (frmDonDatHang)getForm("frmDonDatHang");
            if (F == null)
            {
                F = new frmDonDatHang();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmCongNoNhaCungCap showFormCongNoNhaCungCap()
        {
            frmCongNoNhaCungCap F = (frmCongNoNhaCungCap)getForm("frmCongNoNhaCungCap");
            if (F == null)
            {
                F = new frmCongNoNhaCungCap();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        //Quan ly kho
        public frmThietLapDinhMuc showFormThietLapDinhMuc()
        {
            frmThietLapDinhMuc F = (frmThietLapDinhMuc)getForm("frmThietLapDinhMuc");
            if (F == null)
            {
                F = new frmThietLapDinhMuc();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmChiTietHangNhap showFormChiTietHangNhap()
        {
            frmChiTietHangNhap F = (frmChiTietHangNhap)getForm("frmChiTietHangNhap");
            if (F == null)
            {
                F = new frmChiTietHangNhap();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmChiTietHangXuat showFormChiTietHangXuat()
        {
            frmChiTietHangXuat F = (frmChiTietHangXuat)getForm("frmChiTietHangXuat");
            if (F == null)
            {
                F = new frmChiTietHangXuat();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmTonKhoHangHoa showFormTonKhoHangHoa()
        {
            frmTonKhoHangHoa F = (frmTonKhoHangHoa)getForm("frmTonKhoHangHoa");
            if (F == null)
            {
                F = new frmTonKhoHangHoa();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmQuanLyDonHangDaBan showFormQuanLyDonHangDaBan()
        {
            frmQuanLyDonHangDaBan F = (frmQuanLyDonHangDaBan)getForm("frmQuanLyDonHangDaBan");
            if (F == null)
            {
                F = new frmQuanLyDonHangDaBan();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        //Quan ly tien 
        public frmTienQuyDauKy showTienQuyDauKy()
        {
            frmTienQuyDauKy F =new frmTienQuyDauKy();
            F.ShowDialog();
            F.Activate();
            //if (F == null)
            //{
            //    F = new frmTienQuyDauKy();
            //    F.Show();
            //}
            //F.MdiParent = this;
            //F.WindowState = FormWindowState.Maximized;
            //F.Activate();
            return null;
        }
        public frmThuTienKhac showThuTienKhac()
        {
            frmThuTienKhac F = new frmThuTienKhac();
            F.ShowDialog();
            F.Activate();
            //if (F == null)
            //{
            //    F = new frmThuTienKhac();
            //    F.Show();
            //}
            //F.MdiParent = this;
            //F.WindowState = FormWindowState.Maximized;
            //F.Activate();
            return null;
        }
        public frmTraTienKhac showTraTienKhac()
        {
            frmTraTienKhac F =new frmTraTienKhac();
            F.ShowDialog();
            F.Activate();
            //if (F == null)
            //{
            //    F = new frmTraTienKhac();
            //    F.Show();
            //}
            //F.MdiParent = this;
            //F.WindowState = FormWindowState.Maximized;
            //F.Activate();
            return null;
        }
        public frmQuyTienMat showQuyTienMat()
        {
            frmQuyTienMat F = (frmQuyTienMat)getForm("frmQuyTienMat");
            if (F == null)
            {
                F = new frmQuyTienMat();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        //Thiet lap
        public frmNhanVien showNhanVien()
        {
            frmNhanVien F = (frmNhanVien)getForm("frmNhanVien");
            if (F == null)
            {
                F = new frmNhanVien();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmNhomHang showNhomHang()
        {
            frmNhomHang F = (frmNhomHang)getForm("frmNhomHang");
            if (F == null)
            {
                F = new frmNhomHang();
                F.Show();
            }
            F.MdiParent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Activate();
            return F;
        }
        public frmThongTinCongTy showThongTinCongTy()
        {
            frmThongTinCongTy F =new  frmThongTinCongTy();
            F.ShowDialog();
            F.Activate();
            return null;
        }
        public frmPhanQuyen showPhanQuyen()
        {
            frmPhanQuyen F = new frmPhanQuyen();
            F.ShowDialog();
            F.Activate();
            return null;
        }
        public frmSaoLuuDuPhong showSaoLuuDuPhong()
        {
            frmSaoLuuDuPhong F = new frmSaoLuuDuPhong();
            F.ShowDialog();
            F.Activate();
            return null;
        }

        public frmPhucHoi showPhucHoiDuLieu()
        {
            frmPhucHoi F = new frmPhucHoi();
            F.ShowDialog();
            F.Activate();
            return null;
        }

        public frmLoginServer showKetNoiCSDL()
        {
            frmLoginServer F = new frmLoginServer();
            F.ShowDialog();
            F.Activate();
            return null;
        }
        
        #endregion

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.MdiChildren.Length == 0) e.Cancel = false;
            else
                if (this.MdiChildren.Length == 1 && this.MdiChildren[0].Visible == true)
                    e.Cancel = false;

                else
                {
                    int i = 0;
                    while (i < this.MdiChildren.Length)
                    {
                        if (this.MdiChildren[i].Visible == true || this.MdiChildren[i].WindowState != FormWindowState.Minimized)
                        {
                            if (this.MdiChildren[i].Name != "frmFlash")
                            {
                                this.MdiChildren[i].Visible = false;
                                this.MdiChildren[i].WindowState = FormWindowState.Minimized;
                            }
                        }
                        i++;
                    }
                }
        }

    }
}