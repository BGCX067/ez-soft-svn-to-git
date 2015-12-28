using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsPhieuXuatBUS
    {
        #region Attribute
        private clsPhieuXuatDAO PhieuXuatDAO = new clsPhieuXuatDAO();
        #endregion

        /// <summary>
        /// Lấy Mã phiếu xuất kế tiếp để phục vụ cho chức năng Insert
        /// </summary>
        public string LayMaPhieuXuatMoi()
        {
            return PhieuXuatDAO.LayMaPhieuXuatMoi();
        }

        /// <summary>
        /// Lấy danh sách thông tin chi tiết  Phiếu xuất hàng vào kho theo từ ngày đến ngày
        /// </summary>
        /// <param name="TuNgay">Từ ngày</param>
        /// <param name="DenNgay">Đến ngày</param>
        /// <returns></returns>
        public DataTable LayBang(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuXuatDAO.LayBang(TuNgay, DenNgay);
        }

        // Lấy thông tin phiếu xuất còn thu theo nhà cung cấp
        public DataTable LayBangConThu(clsKhachHangDTO KhachHang)
        {
            return PhieuXuatDAO.LayBangConThu(KhachHang);
        }

        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay, clsKhachHangDTO KhachHang)
        {
            return PhieuXuatDAO.TimKiem( TuNgay,  DenNgay,  KhachHang);
        }

        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuXuatDAO.TimKiem(TuNgay, DenNgay);
        }

        public DataTable TimKiemNV(DateTime TuNgay, DateTime DenNgay, clsNhanVienDTO NhanVien)
        {
            return PhieuXuatDAO.TimKiemNV(TuNgay, DenNgay, NhanVien);
        }

        public DataTable TimKiemNV(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuXuatDAO.TimKiemNV(TuNgay, DenNgay);
        }

        public DataTable TimKiemNV_CT(DateTime TuNgay, DateTime DenNgay, clsNhanVienDTO NhanVien)
        {
            return PhieuXuatDAO.TimKiemNV_CT(TuNgay, DenNgay, NhanVien);
        }

        public DataTable TimKiemNV_CT(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuXuatDAO.TimKiemNV_CT(TuNgay, DenNgay);
        }

        public int Huy(string MaPhieuXuat)
        {
            return PhieuXuatDAO.Huy(MaPhieuXuat);
        }

        public DataTable GetDoanhThu(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuXuatDAO.GetDoanhThu(TuNgay, DenNgay);
        }

        public DataTable GetGiaVon(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuXuatDAO.GetGiaVon(TuNgay, DenNgay);
        }

        public DataTable GetSPBanChay_DoanhSo(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuXuatDAO.GetSPBanChay_DoanhSo(TuNgay, DenNgay);
        }

        public DataTable GetSPBanChay_SoLuong(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuXuatDAO.GetSPBanChay_SoLuong(TuNgay, DenNgay);
        }

        public DataTable ReportPhieuXuatTheoTK(DateTime TuNgay, DateTime DenNgay, string MaKhachHang, string MaNhanVien)
        {
            return PhieuXuatDAO.ReportPhieuXuatTheoTK(TuNgay, DenNgay, MaKhachHang, MaNhanVien);
        }

        public DataTable ReportPhieuXuatTheoNV(DateTime TuNgay, DateTime DenNgay, string MaNhanVien)
        {
            return PhieuXuatDAO.ReportPhieuXuatTheoNV(TuNgay, DenNgay, MaNhanVien);
        }

        public DataTable ReportCT_PhieuXuatTheoNV(DateTime TuNgay, DateTime DenNgay, string MaNhanVien)
        {
            return PhieuXuatDAO.ReportCT_PhieuXuatTheoNV(TuNgay, DenNgay, MaNhanVien);
        }

        public DataTable ReportChiTietHangXuat(DateTime TuNgay, DateTime DenNgay, string MaMatHang, string MaKhachHang)
        {
            return PhieuXuatDAO.ReportChiTietHangXuat(TuNgay, DenNgay, MaMatHang, MaKhachHang);
        }
    }
}
