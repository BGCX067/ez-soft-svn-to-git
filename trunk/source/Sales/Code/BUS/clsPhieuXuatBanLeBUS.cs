using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsPhieuXuatBanLeBUS : clsPhieuXuatBUS
    {
        #region Attribute
        private clsPhieuXuatBanLeDAO PhieuXuatBanLeDAO = new clsPhieuXuatBanLeDAO();
        #endregion

        
        /// <summary>
        /// Lấy thông tin phiếu xuất bán lẻ
        /// </summary>
        /// <param name="PhieuThu">
        /// MaPhieuXuat                  nvarchar(10)
        /// NgayXuat                       smalldatetime
        /// MaNhanVienBanHang     nvarchar(10)
        /// TongTien                        float
        /// DaTra                             float
        /// KhachHang                     nvarchar(255)
        /// LoaiPhieuXuat                 nvarchar(100)
        /// TrangThai                       int
        /// </param>
        public clsPhieuXuatBanLeDTO LayThongTin(string MaPhieuXuat)
        {
            return PhieuXuatBanLeDAO.LayThongTin(MaPhieuXuat);
        }

        /// <summary>
        /// Lấy thông tin phiếu xuất bán lẻ
        /// </summary>
        /// <param name="PhieuThu">
        /// MaPhieuXuat                  nvarchar(10)
        /// NgayXuat                       smalldatetime
        /// MaNhanVienBanHang     nvarchar(10)
        /// TongTien                        float
        /// DaTra                             float
        /// KhachHang                     nvarchar(255)
        /// LoaiPhieuXuat                 nvarchar(100)
        /// TrangThai                       int
        /// </param>
        public clsPhieuXuatBanLeDTO LayThongTinTheoPhieuNhap(string MaPhieuXuat)
        {
            return PhieuXuatBanLeDAO.LayThongTinTheoPhieuNhap(MaPhieuXuat);
        }

        /// <summary>
        /// Tìm kiếm thông tin phiếu xuất từ ngày đến ngày với tất cả các khách hàng
        /// </summary>
        /// <param name="TuNgay">
        /// MaPhieuXuat                  nvarchar(10)
        /// NgayXuat                       smalldatetime
        /// MaNhanVienBanHang     nvarchar(10)
        /// TongTien                        float
        /// DaTra                             float
        /// KhachHang                     nvarchar(255)
        /// LoaiPhieuXuat                 nvarchar(100)
        /// TrangThai                       int
        /// </param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuXuatBanLeDAO.TimKiem(TuNgay,DenNgay);
        }
        /// <summary>
        /// Tìm kiếm thông tin phiếu xuất từ ngày đến ngày với theo khách hàng
        /// </summary>
        /// <param name="TuNgay">
        /// MaPhieuXuat                  nvarchar(10)
        /// NgayXuat                       smalldatetime
        /// MaNhanVienBanHang     nvarchar(10)
        /// TongTien                        float
        /// DaTra                             float
        /// KhachHang                     nvarchar(255)
        /// LoaiPhieuXuat                 nvarchar(100)
        /// TrangThai                       int
        /// </param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay, clsKhachHangDTO KhachHang)
        {
            return PhieuXuatBanLeDAO.TimKiem(TuNgay,DenNgay,KhachHang);
        }

        public DataTable ReportHoaDonBanLe(string MaPhieuXuat)
        {
            return PhieuXuatBanLeDAO.ReportHoaDonBanLe(MaPhieuXuat);
        }

        public DataTable ReportCTHoaDonBanLe(string MaPhieuXuat)
        {
            return PhieuXuatBanLeDAO.ReportCTHoaDonBanLe(MaPhieuXuat);
        }
        public int Them(clsPhieuXuatBanLeDTO PhieuXuatBanLe)
        {
            return PhieuXuatBanLeDAO.Them(PhieuXuatBanLe);
        }
        /// <summary>
        /// Sửa thông tin phiếu xuất bán lẻ
        /// </summary>
        /// <param name="PhieuXuat">
        /// MaPhieuXuat                  nvarchar(10)
        /// NgayXuat                       smalldatetime
        /// MaNhanVienBanHang     nvarchar(10)
        /// TongTien                        float
        /// DaTra                             float
        /// KhachHang                     nvarchar(255)
        /// LoaiPhieuXuat                 nvarchar(100)
        /// TrangThai                       int
        /// </param>
        public int Sua(clsPhieuXuatBanLeDTO PhieuXuatBanLe)
        {
            return PhieuXuatBanLeDAO.Sua(PhieuXuatBanLe);
        }
        /// <summary>
        /// Xóa thông tin phiếu xuất bán lẻ
        /// </summary>
        /// <param name="MaPhieuXuat">MaPhieuXuat   string</param>
        public int Xoa(string MaPhieuXuat)
        {
            return PhieuXuatBanLeDAO.Xoa(MaPhieuXuat);
        }
    }  
}
