using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsPhieuXuatBanSiBUS : clsPhieuXuatBUS
    {
        #region Attribute
        private clsPhieuXuatBanSiDAO PhieuXuatBanSiDAO = new clsPhieuXuatBanSiDAO();
        #endregion


        /// <summary>
        /// Lấy thông tin phiếu xuất bán sỉ
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
        public clsPhieuXuatBanSiDTO LayThongTin(string MaPhieuXuat)
        {
            return PhieuXuatBanSiDAO.LayThongTin(MaPhieuXuat);
        }

        /// <summary>
        /// Lấy thông tin phiếu xuất bán sỉ
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
        public clsPhieuXuatBanSiDTO LayThongTinTheoPhieuNhap(string MaPhieuXuat)
        {
            return PhieuXuatBanSiDAO.LayThongTinTheoPhieuNhap(MaPhieuXuat);
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
            return PhieuXuatBanSiDAO.TimKiem(TuNgay, DenNgay);
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
            return PhieuXuatBanSiDAO.TimKiem(TuNgay, DenNgay, KhachHang);
        }

        public DataTable CongNoKhachHang(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuXuatBanSiDAO.CongNoKhachHang(TuNgay, DenNgay);
        }

        public DataTable CongNoKhachHangTheoKH(DateTime TuNgay, DateTime DenNgay, clsKhachHangDTO KhachHang)
        {
            return PhieuXuatBanSiDAO.CongNoKhachHangTheoKH(TuNgay, DenNgay, KhachHang);
        }

        public DataTable ReportHoaDonBanSi(string MaPhieuXuat)
        {
            return PhieuXuatBanSiDAO.ReportHoaDonBanSi(MaPhieuXuat);
        }

        public DataTable ReportCTHoaDonBanSi(string MaPhieuXuat)
        {
            return PhieuXuatBanSiDAO.ReportCTHoaDonBanSi(MaPhieuXuat);
        }

        public DataTable ReportCongNoKhachHang(DateTime DenNgay, string KhachHang)
        {
            return PhieuXuatBanSiDAO.ReportCongNoKhachHang(DenNgay, KhachHang);
        }

        public int Them(clsPhieuXuatBanSiDTO PhieuXuatBanLe)
        {
            return PhieuXuatBanSiDAO.Them(PhieuXuatBanLe);
        }
        /// <summary>
        /// Sửa thông tin phiếu xuất bán sỉ
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
        public int Sua(clsPhieuXuatBanSiDTO PhieuXuatBanLe)
        {
            return PhieuXuatBanSiDAO.Sua(PhieuXuatBanLe);
        }
        /// <summary>
        /// Xóa thông tin phiếu xuất bán lẻ
        /// </summary>
        /// <param name="MaPhieuXuat">MaPhieuXuat   string</param>
        public int Xoa(string MaPhieuXuat)
        {
            return PhieuXuatBanSiDAO.Xoa(MaPhieuXuat);
        }
    }
}
