using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsNhanVienBUS
    {
        /// <summary>
        /// Đối tượng nhân viên DAO
        /// </summary>
        private clsNhanVienDAO NhanVienDAO = new clsNhanVienDAO();

        public DataTable LayBang()
        {
            return NhanVienDAO.LayBang();
        }

        public DataTable LayBangChoQuanTri()
        {
            return NhanVienDAO.LayBangChoQuanTri();
        }

        /// <summary>
        /// Thêm thông tin nhân viên
        /// </summary>
        /// <param name="LoaiMatHang">
        /// MaLoaiMatHang   nvarchar(10)
        /// TenLoaiMatHang  nvarchar(255)
        /// DienGiai   ntext
        /// NgayTao  smalldatetime
        /// TrangThai int
        /// </param>
        public int Them(clsNhanVienDTO NhanVien)
        {
            return NhanVienDAO.Them(NhanVien);
        }

        /// <summary>
        /// Sửa thông tin  nhân viên
        /// </summary>
        /// <param name="LoaiMatHang">
        /// MaLoaiMatHang  nvarchar(10)
        /// TenLoaiMatHang  nvarchar(255)
        /// DienGiai  ntext
        /// </param>
        public int Sua(clsNhanVienDTO NhanVien)
        {
            return NhanVienDAO.Sua(NhanVien);
        }

        /// <summary>
        /// Xóa thông tin nhân viên
        /// </summary>
        /// <param name="MaLoaiMatHang">Mã loại mặt hàng</param>
        public int Xoa(string MaNhanVien)
        {
            return NhanVienDAO.Xoa(MaNhanVien);
        }

        public string LayMaNhanVienMoi()
        {
            return NhanVienDAO.LayMaNhanVienMoi();
        }

        public clsNhanVienDTO LayThongTin(string MaNhanVien)
        {
            return NhanVienDAO.LayThongTin(MaNhanVien);
        }

        public clsNhanVienDTO LayThongTinTheoTenNguoiDung(string TenNguoiDung)
        {
            return NhanVienDAO.LayThongTinTheoTenNguoiDung(TenNguoiDung);
        }

        /// <summary>
        /// Cập nhật Nhân viên
        /// </summary>
        public int DoiMatKhau(string TenNguoiDung, string MatKhau)
        {
            return NhanVienDAO.DoiMatKhau(TenNguoiDung, MatKhau);
        }

          /// <summary>
        /// Cập nhật quyền hạn sử dụng
        /// </summary>
        public int DoiQuyenHanSuDung(string MaNhanVien, int QuyenHan)
        {
            return NhanVienDAO.DoiQuyenHanSuDung(MaNhanVien, QuyenHan);
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        public Boolean KiemTraNguoiDung(string TenNguoiDung, string MatKhau)
        {
            return NhanVienDAO.KiemTraNguoiDung(TenNguoiDung, MatKhau);
        }

        public DataTable ReportDSNhanVien(string TenNhanVien, string TenNguoiDung)
        {
            return NhanVienDAO.ReportDSNhanVien(TenNhanVien, TenNguoiDung);
        }
    }
}
